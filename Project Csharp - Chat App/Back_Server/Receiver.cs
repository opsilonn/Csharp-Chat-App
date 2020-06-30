using Back_Communication;
using System;
using System.Collections.Generic;
using System.Net.Sockets;


namespace Back_Server
{
    class Receiver
    {
        public TcpClient comm;

        // Creating a Delegate called CommunicationWithTheServer, and an event using it
        public delegate void CommunicationWithTheServer(TcpClient comm);
        public event CommunicationWithTheServer When_Server_Exit;
        public event CommunicationWithTheServer When_Server_LogIn;
        public event CommunicationWithTheServer When_Server_LogOff;

        // Creating a Delegate about creating some structures, and events using them
        public delegate void CreatingProfile(Profile newProfile);
        public event CreatingProfile When_Profile_Create;

        public delegate void CreatingTopic(Topic newTopic);
        public event CreatingTopic When_Topic_Create;

        public delegate void CreatingChat(Chat newChat);
        public event CreatingChat When_Chat_Create;

        public delegate void CreatingMessage(MessageCreation messageCreation);
        public event CreatingMessage When_Message_Create;

        public delegate void MemberAction(Structure structureHostingMessage, Profile profile);
        public event MemberAction When_Member_Join;
        public event MemberAction When_Member_Leave;

         


        /// <summary>
        /// Constructor of the Class
        /// </summary>
        /// <param name="s"></param>
        public Receiver(TcpClient s)
        {
            comm = s;
        }


        /// <summary>
        /// Server's HUB : it does stuff.
        /// </summary>
        public void DoOperation()
        {
            while (true)
            {
                // We receive a Communication from the client
                Communication communication = Net.COMMUNICATION.Receive(comm.GetStream());

                // We extract the data
                Instructions instruction = communication.instruction;
                Object content = communication.content;

                // We display the instruction
                CONSOLE.WriteLine(ConsoleColor.Cyan, "\nINSTRUCTION : " + instruction);

                // According to the instruction, we'll do a specific action
                switch (instruction)
                {
                    case Instructions.Exit_Software:
                        // We raise the event : a user left the software
                        When_Server_Exit?.Invoke(comm);
                        break;




                    // CREDENTIALS
                    case Instructions.LogIn:
                        if (LogIn((Credentials)content))
                        {
                            // We raise the event : a user has logged in
                            When_Server_LogIn?.Invoke(comm);
                        }
                        break;

                    case Instructions.SignIn:
                        // We get the profile Signed In
                        Profile newProfile = SignIn((Profile)content);

                        // If the Profile's ID is not Empty (means that the Sign In was successful)
                        if (newProfile.ID != Guid.Empty)
                        {
                            // We raise the event : a user has logged in
                            When_Server_LogIn?.Invoke(comm);

                            // We raise the event : a Profile has been created
                            When_Profile_Create(newProfile);
                        }
                        break;

                    case Instructions.LogOut:
                        // We raise the event : a user has logged off
                        When_Server_LogOff?.Invoke(comm);
                        break;




                    // PROFILE
                    case Instructions.Profile_GetAll:
                        SendAllProfiles();
                        break;

                    case Instructions.Profile_GetByID:
                        GetProfileByID((Guid)content);
                        break;

                    case Instructions.Profile_GetByName:
                        GetProfileByName((string)content);
                        break;




                    // TOPIC
                    case Instructions.Topic_GetAll:
                        SendAllTopics();
                        break;

                    case Instructions.Topic_GetByID:
                        SendTopicByID((Guid)content);
                        break;

                    case Instructions.Topic_New:
                        // We get the newly created Topic
                        Topic newTopic = NewTopic((Topic)content);

                        // If the Topic can be created (ID is not empty)
                        if(newTopic.ID != Guid.Empty)
                        {
                            // We raise the event : a Topic has been created
                            When_Topic_Create?.Invoke(newTopic);
                        }
                        break;




                    // CHAT
                    case Instructions.Chat_GetAll:
                        GetAllChats((Guid)content);
                        break;

                    case Instructions.Chat_GetByID:
                        GetChatByID((Guid)content);
                        break;

                    case Instructions.Chat_New:
                        // We get the newly created Chat
                        Chat newChat = NewChat((Chat)content);

                        // If the Chat can be created (ID is not empty)
                        if (newChat.ID != Guid.Empty)
                        {
                            // We raise the event : a Chat has been created
                            When_Chat_Create?.Invoke(newChat);
                        }
                        break;




                    // MEMBER
                    case Instructions.Member_Join:
                        // We receive the Member
                        Member newMember = (Member)content;

                        // We retreive the Structure's reference from the Database
                        newMember.structure = Database.STRUCTURE.GetByReference(newMember.structure);

                        // If the operation is successful
                        if (MemberJoin(newMember))
                        {
                            // We raise the event : a user has joined a Structure
                            When_Member_Join?.Invoke(newMember.structure, Database.PROFILE.GetByName(newMember.nameProfile));
                        }
                        break;

                    case Instructions.Member_Leave:
                        // We receive the Member
                        Member oldMember = (Member)content;

                        // We initialize some variables
                        Structure structureNoLongerHostingMember = Database.STRUCTURE.GetByReference(oldMember.structure);
                        string nameProfileLeaving = oldMember.nameProfile;

                        // If the operation is successful
                        if (MemberLeave(oldMember))
                        {
                            // We raise the event : a user has left a Structure
                            When_Member_Leave?.Invoke(structureNoLongerHostingMember, Database.PROFILE.GetByName(nameProfileLeaving));
                        }
                        break;




                    // MESSAGE
                    case Instructions.Message_New:
                        // FIRST - we receive the Structure in which the Message will be hosted
                        // We get the Structure from the Client, and get the correct reference from the Database
                        MessageCreation messageCreation = (MessageCreation)content;

                        // We retreive the Structure's reference from the Database
                        messageCreation.structure = Database.STRUCTURE.GetByReference(messageCreation.structure);

                        // We get the Message newly created
                        messageCreation.message = NewMessage(messageCreation);

                        // If we can create the new Message (ID is not empty)
                        if (messageCreation.message.ID != Guid.Empty)
                        {
                            // We raise the event : a Message has been created
                            When_Message_Create?.Invoke(messageCreation);
                        }
                        break;

                    case Instructions.Message_GetByID:
                        GetMessageByID((Guid)content);
                        break;




                    // otherwise : Error (should not occur, but we're not taking any chance)
                    default:
                        CONSOLE.WriteLine(ConsoleColor.Red, "Message instruction not understood : " + instruction);
                        break;
                }
            }
        }






        // PROFILE


        /// <summary>
        /// Sends all Profiles to the Client-Side
        /// </summary>
        public void SendAllProfiles()
        {
            // We send all the topics
            Net.PROFILE.SendAll(comm.GetStream(), Database.profiles);
        }


        /// <summary>
        /// Sends a Profile of which we know the ID
        /// </summary>
        /// <param name="ID">ID of the Profile we seek</param>
        public void GetProfileByID(Guid ID)
        {
            // Writing a Log in the Console
            CONSOLE.WriteLine(ConsoleColor.Yellow, "Profile's ID : " + ID);

            // We send the default Profile if no match was found / the correct one if a match was found
            Net.PROFILE.Send(comm.GetStream(), Database.PROFILE.GetByID(ID));
        }


        /// <summary>
        /// Sends a Profile of which we know the name
        /// </summary>
        /// <param name="ID">name of the Profile we seek</param>
        public void GetProfileByName(string name)
        {
            // Writing a Log in the Console
            CONSOLE.WriteLine(ConsoleColor.Yellow, "Profile's name : " + name);

            // We send the default Profile if no match was found / the correct one if a match was found
            Net.PROFILE.Send(comm.GetStream(), Database.PROFILE.GetByName(name));
        }








        // CREDENTIALS


        /// <summary>
        /// Server-side verification for the Login
        /// Returns an instance of the Profile logged in (if it didn't work, its ID = -1)
        /// </summary>
        /// <param name="credentials">Credentials of the user</param>
        /// <returns> Whether the Login was successful or not</returns>
        public bool LogIn(Credentials credentials)
        {
            // We extract the credential's data
            string username = credentials.username;
            string password = credentials.password;

            // We create a integer representing the ID of the user ;
            // By default, its ID is negative (= not an actual profile)
            Guid ID_toReturn = Guid.Empty;


            // We iterate through all the Database's Profiles
            foreach (Profile profile in Database.profiles)
            {
                // If we found a match : BINGO
                if (username == profile.name && password == profile.password)
                {
                    ID_toReturn = profile.ID;
                    break;
                }
            }

            // We send the default Profile if no match was found / the correct one if a match was found
            Net.STRING.Send(comm.GetStream(), ID_toReturn.ToString());


            // Returns whether we succeeded or not in Logging in
            return ID_toReturn != Guid.Empty;
        }


        /// <summary>
        /// Server-side verification for the Sign-in
        /// Returns an instance of the newly created Profile (if it didn't work, its ID = -1)
        /// </summary>
        /// <param name="profileReceived">Profile of the user trying to Sign in</param>
        /// <returns> Whether the Signin was successful or not</returns>
        public Profile SignIn(Profile profileReceived)
            {
                // We extract the profile's data
                string username = profileReceived.name;
                string password = profileReceived.password;
                string email = profileReceived.email;

                Profile profileToReturn = new Profile();

                // We create a bool repertoring whether or not a Profile already has the same data as the new user
                // By default, this is false
                bool isTaken = false;


                // We iterate through all the Database's Profiles
                foreach (Profile profile in Database.profiles)
                {
                    // If we found a match : ERROR !
                    if (username == profile.name || email == profile.email)
                    {
                        isTaken = true;
                        break;
                    }
                }

                // If the Credentials don't exist : we can create a new Profile
                if (!isTaken)
                {
                    // We create the new Profile
                     profileToReturn = new Profile(username, password, email);
                }


                // We send the ID of the Profile (Empty = SignIn failed)
                Net.STRING.Send(comm.GetStream(), profileToReturn.ID.ToString());

                // We return the profile created
                return profileToReturn;
            }









        // TOPIC


        /// <summary>
        /// Sends all Topics to the Client-Side
        /// </summary>
        public void SendAllTopics()
        {
            // We send all the topics
            Net.TOPIC.SendAll(comm.GetStream(), Database.topics);
        }


        /// <summary>
        /// Sends a Topic by the requested ID
        /// </summary>
        /// <param name="ID">ID of the Topic</param>
        public void SendTopicByID(Guid ID)
        {
            // Writing a Log in the Console
            CONSOLE.WriteLine(ConsoleColor.Yellow, "Topic's ID : " + ID);

            // We search for the topic to send
            Topic topicToReturn = new Topic();
            foreach (Topic topic in Database.topics)
            {
                if (ID == topic.ID)
                {
                    topicToReturn = topic;
                    break;
                }
            }

            // We send the topic
            Net.TOPIC.Send(comm.GetStream(), topicToReturn);
        }


        /// <summary>
        /// Creates a Topic
        /// </summary>
        /// <param name="topicReceived">Topic's data send by the client</param>
        public Topic NewTopic(Topic topicReceived)
        {
            // We return whether or not the Topic was added to the database

            // ...Let's say we do some verification here...

            // Since there are no verification needed, we return True
            Net.BOOL.Send(comm.GetStream(), true);

            // We return the new Topic
            return new Topic(topicReceived.name, topicReceived.description, topicReceived.ID_creator);
        }









        // CHAT


        /// <summary>
        /// Sends all Chats to the Client-Side
        /// </summary>
        /// <param name="ID_user">ID of the user (so that we only send the Chats he is connected to)</param>
        public void GetAllChats(Guid ID_user)
        {
            // We create the list of the Chats to send
            List<Chat> chatsToSend = new List<Chat>();

            // Foreach Chat in the database
            foreach (Chat chat in Database.chats)
            {
                // Foreach member that has acces to the current Chat
                foreach (Guid ID in chat.ID_members)
                {
                    // If the IDs correspond, we add the current chat to the list
                    if (ID_user == ID)
                    {
                        chatsToSend.Add(chat);
                        break;
                    }
                }
            }


            // We send all the topics
            Net.CHAT.SendAll(comm.GetStream(), chatsToSend);
        }


        /// <summary>
        /// Sends a Chat by the requested ID
        /// </summary>
        /// <param name="ID">ID of the Chat</param>
        public void GetChatByID(Guid ID)
        {
            // Writing a Log in the Console
            CONSOLE.WriteLine(ConsoleColor.Yellow, "Chat's ID : " + ID);

            // We search for the topic to send
            Chat chatToReturn = new Chat();
            foreach (Chat chat in Database.chats)
            {
                if (ID == chat.ID)
                {
                    chatToReturn = chat;
                    break;
                }
            }

            // We send the topic
            Net.CHAT.Send(comm.GetStream(), chatToReturn);
        }


        /// <summary>
        /// Creates a New Chat
        /// </summary>
        /// <param name="chatReceived">Chat's data send by the client</param>
        public Chat NewChat(Chat chatReceived)
            {
                // We create a new Chat
                Chat newChat = new Chat();

                // ...Let's suppose we do some verifications...
                // Anyway, since there is no verification for the moment, we set the boolean to TRUE
                bool canBeAdded = true;

                if (canBeAdded)
                {
                    newChat = new Chat(chatReceived.name, chatReceived.description, chatReceived.ID_members);
                }

                // We return whether or not the Chat was added to the database
                Net.BOOL.Send(comm.GetStream(), canBeAdded);

                // We return the new Chat (its ID is Empty if it cannot be created)
                return newChat;
            }









        // MEMBER


        /// <summary>
        /// A Member joins a given Structure
        /// </summary>
        /// <param name="newMember">New Member, wanting to join a specific Structure</param>
        /// <returns>Whether the operation was successful or not</returns>
        public bool MemberJoin(Member newMember)
        {
            Structure structureProfileIsJoining = newMember.structure;
            string nameProfileJoining = newMember.nameProfile;

            bool valid = true;

            // If the Structure is a Topic : it means we add OURSELF : fine !
            // If the Structure is a Chat : it means we add ANOTHER member, so we have to do some verifications
            if (structureProfileIsJoining is Chat)
            {
                // FIRST - does the name received exist ?
                valid = Database.PROFILE.ContainsByName(nameProfileJoining);


                // THEN - does the name is already present in the Structure ? (we hope not : otherwise, it would be redundant)
                if (valid)
                {
                    valid = !Database.STRUCTURE.ContainsProfileName(structureProfileIsJoining, nameProfileJoining);            
                }

                // FINALLY - send if the method worked
                Net.BOOL.Send(comm.GetStream(), valid);
            }

            return valid;
        }


        /// <summary>
        /// A Member leaves a given Structure
        /// </summary>
        /// <param name="oldMember">Member wanting to leave a given Structure</param>
        /// <returns>Whether the operation was successful or not</returns>
        public bool MemberLeave(Member oldMember)
        {
            // Returns whether the Structure received exists
            // If yes, the Client will be able to leave the Structure
            // Otherwise... how could a user eave a Structure that doesn't exist ?
            return (Database.STRUCTURE.GetByReference(oldMember.structure) != null);
        }









        // MESSAGE


        /// <summary>
        /// Creates a new Message in the Database
        /// </summary>
        /// <param name="messageCreation">Data about the new Message and the Structure it is contained in</param>
        /// <returns> The Message created </returns>
        public Message NewMessage(MessageCreation messageCreation)
        {
            // We initialize some variables
            Message newMessage = new Message();

            // If we found the correct structure
            if (messageCreation.structure != null)
            {
                // We create a new Message from the one received
                newMessage = new Message(messageCreation.message.content, messageCreation.message.ID_author);
            }


            // We return the Message created
            return newMessage;
        }


        /// <summary>
        /// Sends a Mesage by the requested ID
        /// </summary>
        /// <param name="ID">ID of the message to return</param>
        public void GetMessageByID(Guid ID)
        {
            // Writing a Log in the Console
            CONSOLE.WriteLine(ConsoleColor.Yellow, "Message's ID : " + ID);


            // We search for the Message to send
            Message messageToReturn = new Message();
            foreach (Message message in Database.messages)
            {
                if (ID == message.ID)
                {
                    messageToReturn = message;
                    break;
                }
            }

            if(messageToReturn.ID == Guid.Empty)
            {
                CONSOLE.WriteLine(ConsoleColor.Red, "MESSAGE OF ID " + ID + "NOT FOUND");
            }

            // We send the Message
            Net.MESSAGE.Send(comm.GetStream(), messageToReturn);
        }
    }
}