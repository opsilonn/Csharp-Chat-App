using System;
using System.Collections.Generic;
using System.Net.Sockets;
using Back_Communication;


namespace Front_Console
{
    class Client
    {
        Guid ID_user;
        bool continuing;

        TcpClient comm;
        private string hostname;
        private int port;


        public Client(string h, int p)
        {
            hostname = h;
            port = p;
        }


        public void Start()
        {
            // Initializing some variables
            comm = new TcpClient(hostname, port);
            ID_user = Guid.Empty;
            continuing = true;


            while (continuing)
            {
                // CONNECTION PANEL (LOGIN - SIGN-IN)
                PANEL_CONNECTION();


                // CONNECTED PANEL (TOPIC - CHAT : see / add / delete)
                if (ID_user != Guid.Empty && continuing)
                {
                    PANEL_CONNECTED();
                }


                // DISPLAY A GOODBYE MESSAGE
                if (!continuing)
                {
                    PANEL_EXITSOFTWARE();
                }
            }
        }




        // PANELS


        /// <summary>
        /// A panel displaying all data about connecting to the program (Login / Sign-in)
        /// </summary>
        private void PANEL_CONNECTION()
        {
            int choice = Choice_Selection.GetChoice(Choice_Prefabs.CHOICE_CONNECTION);
            switch (choice)
            {
                case 0:
                    LogIn();
                    break;

                case 1:
                    SignIn();
                    break;

                case 2:
                    continuing = false;
                    break;

                default:
                    Console.WriteLine("Error at the Login / Sign in");
                    break;
            }
        }


        /// <summary>
        /// A panel displaying all options a user can perform while logged to the program (Topic / Chat)
        /// </summary>
        private void PANEL_CONNECTED()
        {
            bool continuingConnection = true;

            while (continuingConnection)
            {
                int choice = Choice_Selection.GetChoice(Choice_Prefabs.CHOICE_CONNECTED);
                switch (choice)
                {
                    case 0:
                        DisplayProfile(ID_user);
                        break;

                    case 1:
                        PANEL_TOPICS();
                        break;

                    case 2:
                        NewTopic();
                        break;

                    case 3:
                        PANEL_CHATS();
                        break;

                    case 4:
                        NewChat();
                        break;

                    case 5:
                        LogOut();
                        continuingConnection = false;
                        break;

                    default:
                        Console.WriteLine("Error at the Topic / Chat");
                        break;
                }

                CONSOLE.WaitForInput();
            }
        }


        /// <summary>
        /// Panel displaying all the Topics
        /// </summary>
        private void PANEL_TOPICS()
        {
            bool continuingTopics = true;


            while (continuingTopics)
            {
                // GETTING THE VARIABLES

                // Asking for all the Topics
                Instructions instruction = Instructions.Topic_GetAll;
                Object content = null;
                Net.COMMUNICATION.Send(comm.GetStream(), new Communication(instruction, content));

                // Receiving all the Topics
                List<Topic> topics = (List<Topic>)Net.TOPIC.ReceiveAll(comm.GetStream());


                // CHOICE


                // We dynamically create a List containing all the topic's name
                List<string> choiceString = new List<string>();
                foreach (Topic topic in topics)
                {
                    choiceString.Add(topic.name);
                }

                // We add as a last choice the option to "Go Back"
                choiceString.Add("Go Back");

                // We create the Choice
                Choice choice = new Choice("please Select a topic (last one = leave) : ", choiceString);
                int index = Choice_Selection.GetChoice(choice);

                if (index != choiceString.Count - 1)
                {
                    DisplayTopic(topics[index].ID);
                }
                else
                {
                    continuingTopics = false;
                }
            }
        }


        /// <summary>
        /// Panel displaying all the Chats the user has acces to
        /// </summary>
        private void PANEL_CHATS()
        {
            bool continuingChats = true;

            while (continuingChats)
            {
                // GETTING THE VARIABLES

                // Asking for all the CHATS
                // ...And precise the user's ID : we'll only receive the chat he is included in
                Instructions instruction = Instructions.Chat_GetAll;
                Object content = ID_user;
                Net.COMMUNICATION.Send(comm.GetStream(), new Communication(instruction, content));

                // Receiving all the Chats the user is included in
                List<Chat> chats = (List<Chat>)Net.CHAT.ReceiveAll(comm.GetStream());


                // CHOICE


                // We dynamically create a List containing all the Chat's name
                List<string> choiceString = new List<string>();
                foreach (Chat chat in chats)
                {
                    choiceString.Add(chat.name);
                }

                // We add as a last choice the option to "Go Back"
                choiceString.Add("Go Back");

                // We create the Choice
                Choice choice = new Choice("please Select a chat (last one = leave) : ", choiceString);

                int index = Choice_Selection.GetChoice(choice);

                if (index != choiceString.Count - 1)
                {
                    DisplayChat(chats[index].ID);
                }
                else
                {
                    continuingChats = false;
                }
            }
        }


        /// <summary>
        /// A panel saying goodbye to the user
        /// </summary>
        private void PANEL_EXITSOFTWARE()
        {
            // Displaying a farewell message
            Console.Clear();
            CONSOLE.WriteLine(ConsoleColor.Blue, "\n\n\n      Goodbye !! We hope to see you soon :)");

            // Warning the server we leave the software
            Instructions instruction = Instructions.Exit_Software;
            Object content = null;
            Net.COMMUNICATION.Send(comm.GetStream(), new Communication(instruction, content));


            // Environment.Exit(0);
        }




        // METHODS - CONNECTION

        /// <summary>
        /// Gets the Credentials of the user, and connects him / makes him retry accordingly
        /// </summary>
        private void LogIn()
        {
            bool continueLogin = true;
            string errorMessage = "";

            do
            {
                // Displaying some messages
                Console.Clear();
                CONSOLE.WriteLine(ConsoleColor.Blue, "\n   Enter empty fields to leave the LOGIN");


                // Displays a message if the credentials are incorrect
                CONSOLE.WriteLine(ConsoleColor.Red, errorMessage);


                // USERNAME
                Console.Write("\n Please enter your username : ");
                string name = Console.ReadLine();


                // PASSWORD
                Console.Write("\n Please enter your password : ");
                string password = Console.ReadLine();


                // All the fields are empty : go back to the menu
                if (name.Length == 0 && password.Length == 0)
                {
                    continueLogin = false;
                }
                // If at least one field is empty : ERROR
                else if (name.Length == 0 || password.Length == 0)
                {
                    errorMessage = PrefabMessage.INCOMPLETE_FIELDS;
                }
                // If at least one field is too long : ERROR
                else if (name.Length > PrefabMessage.INPUT_MAXSIZE_PROFILE_NAME || password.Length > PrefabMessage.INPUT_MAXSIZE_PROFILE_PASSWORD)
                {
                    errorMessage = PrefabMessage.INCORRECT_INPUT_SIZE;
                }
                // If at least one field has one incorrect character : ERROR
                else if (!PrefabMessage.CorrectInput(name) || !PrefabMessage.CorrectInput(password))
                {
                    errorMessage = PrefabMessage.INCORRECT_INPUT_CHARACTER;
                }
                // Otherwise : verify with the server
                else
                {
                    // Sending the credentials
                    Instructions instruction = Instructions.LogIn;
                    Object content = new Credentials(name, password);
                    Net.COMMUNICATION.Send(comm.GetStream(), new Communication(instruction, content));

                    // Receiving the response ID
                    ID_user = Guid.Parse( Net.STRING.Receive(comm.GetStream()) );

                    // If the ID is Empty : there was no match found, we reset the login
                    if (ID_user == Guid.Empty)
                    {
                        errorMessage = PrefabMessage.LOGIN_FAILURE;
                    }
                    // Match found, we proceed forward
                    else
                    {
                        continueLogin = false;
                    }
                }
            }
            while (continueLogin);
        }


        /// <summary>
        /// Gets the Credentials of the user, and connects him / makes him retry accordingly
        /// </summary>
        private void SignIn()
        {
            bool continueSignin = true;
            string errorMessage = "";

            while (continueSignin)
            {
                // Displaying some messages
                Console.Clear();
                CONSOLE.WriteLine(ConsoleColor.Blue, "\n   Enter empty fields to leave the SIGN-IN");


                // Displays a message if the credentials are incorrect
                CONSOLE.WriteLine(ConsoleColor.Red, errorMessage);


                // USERNAME
                Console.Write("\n Please enter your username : ");
                string name = Console.ReadLine();


                // PASSWORD
                Console.Write("\n Please enter your password : ");
                string password = Console.ReadLine();


                // PASSWORD - VERIFICATION
                Console.Write("\n Please verify your password : ");
                string passwordVerif = Console.ReadLine();


                // EMAIL
                Console.Write("\n Please enter your email : ");
                string email = Console.ReadLine();


                // All the fields are empty : go back to the menu
                if (name.Length == 0 && password.Length == 0 && passwordVerif.Length == 0 && email.Length == 0)
                {
                    continueSignin = false;
                }
                // If at least one field is empty : ERROR
                else if (name.Length == 0 || password.Length == 0 || passwordVerif.Length == 0 || email.Length == 0)
                {
                    errorMessage = PrefabMessage.INCOMPLETE_FIELDS;
                }
                // The password and its verification do not match : ERROR
                else if (password != passwordVerif)
                {
                    errorMessage = PrefabMessage.SIGNIN_PASSWORD_DONT_MATCH;
                }
                // NOW, we don't have to verify the PasswordVerif field anymore !
                // If at least one field is too long : ERROR
                else if (name.Length > PrefabMessage.INPUT_MAXSIZE_PROFILE_NAME
                    || password.Length > PrefabMessage.INPUT_MAXSIZE_PROFILE_PASSWORD
                    || email.Length > PrefabMessage.INPUT_MAXSIZE_PROFILE_EMAIL)
                {
                    errorMessage = PrefabMessage.INCORRECT_INPUT_SIZE;
                }
                // If at least one field has one incorrect character : ERROR
                else if (!PrefabMessage.CorrectInput(name) || !PrefabMessage.CorrectInput(password) || !PrefabMessage.CorrectInput(email))
                {
                    errorMessage = PrefabMessage.INCORRECT_INPUT_CHARACTER;
                }
                // Otherwise : verify with the server
                else
                {
                    // Sending the credentials
                    Instructions instruction = Instructions.SignIn;
                    Object content = new Profile(name, password, email);
                    Net.COMMUNICATION.Send(comm.GetStream(), new Communication(instruction, content));

                    // We get an ID back
                    // CORRECT :  positive ID
                    // INCORRECT : negative ID
                    ID_user = Guid.Parse( Net.STRING.Receive(comm.GetStream()) );


                    // If the ID is Empty : the profile's name and/or email is already taken, we reset the sign-in
                    if (ID_user == Guid.Empty)
                    {
                        errorMessage = PrefabMessage.SIGNIN_FAILURE;
                    }
                    // SignIn successful, we proceed forward
                    else
                    {
                        continueSignin = false;
                    }
                }
            }
        }


        /// <summary>
        /// Ensures the user Logs Out of the SOftare (but remains to the Log In / Sign In menu)
        /// </summary>
        private void LogOut()
        {
            // Display a Farewell message
            CONSOLE.WriteLine(ConsoleColor.Blue, "\n\n     Goodbye ! We hope to see you soon :)\n\n");

            // Setting the ID to -1 : no user is logged in anymore
            ID_user = Guid.Empty;

            // Warning the server we log out
            Instructions instruction = Instructions.LogOut;
            Object content = null;
            Net.COMMUNICATION.Send(comm.GetStream(), new Communication(instruction, content));
        }




        // METHODS - CONNECTED

        /// <summary>
        /// Display all data about the Profile logged-in
        /// </summary>
        private void DisplayProfile(Guid ID)
        {
            // Display all a Profile's data
            Console.Clear();
            Console.WriteLine(Net.PROFILE.GetByID(comm, ID).ToString());
        }



        /// <summary>
        /// Display all data about a given Topic
        /// </summary>
        /// <param name="ID_topic"> ID of the Topic we are displaying </param>
        private void DisplayTopic(Guid ID_topic)
        {
            bool continuingTopic = true;

            while (continuingTopic)
            {
                // We need to get the database's Topic everytime we refresh
                // Asking for the Topic
                Instructions instruction = Instructions.Topic_GetByID;
                Net.COMMUNICATION.Send(comm.GetStream(), new Communication(instruction, ID_topic));

                // Receiving the Topic
                Topic topic = (Topic)Net.TOPIC.Receive(comm.GetStream());



                // BUILDING THE CHOICE

                // Concerning the topic, we can either JOIN or LEAVE it, depending if we are following it or not
                Choice choice = new Choice();
                choice.message = Choice_Prefabs.CHOICE_TOPIC.message;
                foreach (string c in Choice_Prefabs.CHOICE_TOPIC.choices)
                {
                    choice.choices.Add(c);
                }


                // The user follows the topic : he can LEAVE it
                if (topic.containsMemberByID(ID_user))
                {
                    choice.choices.Add(Choice_Prefabs.CHOICE_TOPIC_LEAVE);
                }
                // The user doesn't follow the topic : he can JOIN it
                else
                {
                    choice.choices.Add(Choice_Prefabs.CHOICE_TOPIC_JOIN);
                }

                // We add the Go Back option
                choice.choices.Add(Choice_Prefabs.CHOICE_TOPIC_GOBACK);



                // CHOICE
                switch (Choice_Selection.GetChoice(choice))
                {
                    case 0:
                        DisplayStructure(topic);
                        break;

                    case 1:
                        NewMessage(topic);
                        break;


                    case 2:
                        // If the user is within the Topic : REMOVE IT
                        if (topic.containsMemberByID(ID_user))
                        {
                            MemberRemove(topic);
                        }
                        // If the user is not within the Topic : JOIN IT
                        else
                        {
                            MemberAdd(topic);
                        }
                        break;


                    case 3:
                        continuingTopic = false;
                        break;
                }

                // Waiting for user's input at the end
                CONSOLE.WaitForInput();
            }
        }



        /// <summary>
        /// Display all data about a given Chat
        /// </summary>
        /// <param name="ID_chat"> ID of the Chat we are displaying </param>
        private void DisplayChat(Guid ID_chat)
        {
            bool continuingChat = true;

            while (continuingChat)
            {
                // We need to get the database's Chat everytime we refresh
                // Asking for the Chat (we sent as content the ID of the Chat)
                Instructions instruction = Instructions.Chat_GetByID;
                Net.COMMUNICATION.Send(comm.GetStream(), new Communication(instruction, ID_chat));

                // Receiving the Chat
                Chat chat = (Chat)Net.CHAT.Receive(comm.GetStream());


                // CHOICE
                switch (Choice_Selection.GetChoice(Choice_Prefabs.CHOICE_CHAT))
                {
                    case 0:
                        DisplayStructure(chat);
                        break;

                    case 1:
                        NewMessage(chat);
                        break;

                    case 2:
                        MemberAdd(chat);
                        break;

                    case 3:
                        MemberRemove(chat);
                        continuingChat = false;
                        break;

                    case 4:
                        continuingChat = false;
                        break;
                }

                // Waiting for user's input at the end
                CONSOLE.WaitForInput();
            }
        }


        /// <summary>
        /// Displays all data about a Structure
        /// </summary>
        /// <param name="structure"> Structure to display </param>
        private void DisplayStructure(Structure structure)
        {
            // DISPLAYING THE STRUCTURE'S DATA
            Console.Clear();

            // We display the head of the Structure
            CONSOLE.WriteLine(ConsoleColor.Blue, structure.name + "\n");
            Console.WriteLine(structure.description + "\n\n");
            Console.WriteLine("Number of members : " + structure.numberOfMembers + "\n");
            Console.WriteLine("Number of messages : " + structure.numberOfMessages + "\n\n");


            // If the Structure has no message, we display a custom initial message
            if (structure.numberOfMessages == 0)
            {
                CONSOLE.WriteLine(ConsoleColor.DarkGray, PrefabMessage.MESSAGE_EMPTY);
            }
            // We iterate through all the messages
            else
            {
                foreach (Guid ID in structure.ID_messages)
                {
                    // Asking to receive a Message
                    Net.COMMUNICATION.Send(comm.GetStream(), new Communication(Instructions.Message_GetByID, ID));

                    // We receive the Message from the server
                    Message message = Net.MESSAGE.Receive( comm.GetStream() );

                    // If the message is not null (would cause an error otherwise)
                    if (message.ID != Guid.Empty)
                    {
                        // If needed, we truncate the name of the author of the message
                        Profile author = Net.PROFILE.GetByID(comm, message.ID_author);
                        string authorName = "";
                        int MAX = 20;

                        // If the name is too short, we add blank space FIRST, then the actual name
                        if (author.name.Length <= MAX)
                        {
                            authorName = author.name;
                            for (int index = 0; index < MAX - authorName.Length; index++)
                            {
                                Console.Write(" ");
                            }
                        }
                        // Otherwise, we truncate the name
                        else
                        {
                            authorName = author.name.Substring(0, MAX - 3) + "...";
                        }


                        // DISPLAYING THE MESSAGE

                        // We display the author in gray
                        CONSOLE.Write(ConsoleColor.DarkGray, authorName);
                        // We display the content of the message in white
                        Console.WriteLine(" : " + message.content);
                    }
                }
            }
        }


        /// <summary>
        /// Adding a the current user to a Structure
        /// </summary>
        /// <param name="structure"> Structure where we add the current member (if Topic), or himself + a friend (if Chat)   </param>
        private void MemberAdd(Structure structure)
        {
            // If Topic : we add Ourselve (easy !)
            if(structure is Topic)
            {
                // Asking to send a new Member
                Instructions instruction = Instructions.Member_Join;
                Object content = new Member(structure, Net.PROFILE.GetByID(comm, ID_user).name);
                Net.COMMUNICATION.Send(comm.GetStream(), new Communication(instruction, content));
            }
            // If Chat : we add someone else (tough : we need to do more work)
            if(structure is Chat)
            {
                bool continuing = true;
                string errorMessage = "";

                do
                {
                    // FIRST - we display an error message if there is one
                    CONSOLE.WriteLine(ConsoleColor.Red, errorMessage);

                    // SECOND - we get the name of the Profile to send
                    CONSOLE.Write(ConsoleColor.Blue, "\n\n Please enter the name of the new member : ");
                    string nameNewMember = Console.ReadLine();

                    // We do some verifications :
                    // If the message is empty : abandon the procedure
                    if(nameNewMember.Length == 0)
                    {
                        continuing = false;
                    }
                    // If the name is too long : ERROR
                    if (nameNewMember.Length > PrefabMessage.INPUT_MAXSIZE_PROFILE_NAME)
                    {
                        errorMessage = PrefabMessage.INCORRECT_INPUT_SIZE;
                    }
                    // If the name doesn't obey the inputs limitations : ERROR
                    if (!PrefabMessage.CorrectInput(nameNewMember))
                    {
                        errorMessage = PrefabMessage.INCORRECT_INPUT_CHARACTER;
                    }
                    // If the name is the one of the user : ERROR (you cannot add yourself !)
                    if (Net.PROFILE.GetByID(comm, ID_user).name == nameNewMember)
                    {
                        errorMessage = PrefabMessage.NEWCHATMEMBER_ADD_SELF;
                    }
                    else
                    {
                        // We send the new Member
                        Instructions instruction = Instructions.Member_Join;
                        Object content = new Member(structure, nameNewMember);
                        Net.COMMUNICATION.Send(comm.GetStream(), new Communication(instruction, content));


                        // FINALLY - we display a return message
                        bool itWorked = Net.BOOL.Receive(comm.GetStream());
                        if (itWorked)
                        {
                            CONSOLE.WriteLine(ConsoleColor.Green, PrefabMessage.NEWCHATMEMBER_SUCCESS);
                            continuing = false;
                        }
                        else
                        {
                            errorMessage = PrefabMessage.NEWCHATMEMBER_FAILURE;
                        }
                    }
                }
                while (continuing);
            }
        }


        /// <summary>
        /// Removing the current user to a Structure
        /// </summary>
        /// <param name="structure"> Structure where we remove the current member </param>
        private void MemberRemove(Structure structure)
        {
            // Asking to remove a Member
            Instructions instruction = Instructions.Member_Leave;
            Object content = new Member(structure, Net.PROFILE.GetByID(comm, ID_user).name);
            Net.COMMUNICATION.Send(comm.GetStream(), new Communication(instruction, content));
        }
        

        /// <summary>
        /// Creates a new Message
        /// </summary>
        /// <param name="currentStructure"> Structure where the Message will be created </param>
        private void NewMessage(Structure currentStructure)
        {
            bool continueNewMessage = true;
            string errorMessage = "";

            while(continueNewMessage)
            {
                // Displaying some messages
                Console.Clear();
                CONSOLE.WriteLine(ConsoleColor.Blue, "\n   Enter empty fields to leave the Creation of the new MESSAGE");

                // We display an error message, if there is any
                CONSOLE.WriteLine(ConsoleColor.Red, errorMessage);


                // CONTENT
                Console.Write("\n Please enter the content of your Message : ");
                string message = Console.ReadLine();


                // If the content's size is  0, we end the loop
                if(message.Length == 0)
                {
                    continueNewMessage = false;
                }
                // If the content's size is too long : ERROR
                else if (message.Length > PrefabMessage.INPUT_MAXSIZE_PROFILE_CONTENT)
                {
                    errorMessage = PrefabMessage.INCORRECT_INPUT_SIZE;
                }
                // If at least one field has one incorrect character : ERROR
                else if ( !PrefabMessage.CorrectInput(message) )
                {
                    errorMessage = PrefabMessage.INCORRECT_INPUT_CHARACTER;
                }
                // Otherwise : we transfer it to the server
                else
                {
                    // Sending the new Message (the Structure it is in + the actual Message)
                    Instructions instruction = Instructions.Message_New;
                    Object content = new MessageCreation(currentStructure, new Message(message, ID_user));
                    Net.COMMUNICATION.Send(comm.GetStream(), new Communication(instruction, content));

                    // We end the loop
                    continueNewMessage = false;
                }
            }
        }


        /// <summary>
        /// Creates a new Topic
        /// </summary>
        private void NewTopic()
        {
            bool continueNewTopic = true;
            string errorMessage = "";

            while (continueNewTopic)
            {
                // Displaying some messages
                Console.Clear();
                CONSOLE.WriteLine(ConsoleColor.Blue, "\n   Enter empty fields to leave the Creation of the new TOPIC");


                // Displays a message if the credentials are incorrect
                CONSOLE.WriteLine(ConsoleColor.Red, errorMessage);


                // NAME
                Console.Write("\n Please enter the name of the Topic : ");
                string name = Console.ReadLine();


                // DESCRIPTION
                Console.Write("\n Please enter the description of the Topic  : ");
                string description = Console.ReadLine();


                // All the fields are empty : go back to the menu
                if (name.Length == 0 && description.Length == 0)
                {
                    continueNewTopic = false;
                }
                // One of the field is empty : error
                else if (name.Length == 0 || description.Length == 0)
                {
                    errorMessage = PrefabMessage.INCOMPLETE_FIELDS;
                }
                else if (name.Length > PrefabMessage.INPUT_MAXSIZE_STRUCTURE_NAME
                    || description.Length > PrefabMessage.INPUT_MAXSIZE_STRUCTURE_DESCRIPTION)
                {
                    errorMessage = PrefabMessage.INCORRECT_INPUT_SIZE;
                }
                // If at least one field has one incorrect character : ERROR
                else if (!PrefabMessage.CorrectInput(name) || !PrefabMessage.CorrectInput(description))
                {
                    errorMessage = PrefabMessage.INCORRECT_INPUT_CHARACTER;
                }
                // Otherwise : send the new Topic
                else
                {
                    // Sending the new Topic
                    Instructions instruction = Instructions.Topic_New;
                    Object content = new Topic(name, description, ID_user);
                    Net.COMMUNICATION.Send(comm.GetStream(), new Communication(instruction, content));


                    // We receive whether the creation was successful or not
                    if (Net.BOOL.Receive(comm.GetStream()))
                    {
                        CONSOLE.WriteLine(ConsoleColor.Green, PrefabMessage.TOPIC_CREATION_SUCCESS);

                        // Ending the loop
                        continueNewTopic = false;
                    }
                    else
                    {
                        errorMessage = PrefabMessage.CHAT_CREATION_FAILURE;
                    }
                }
            }
        }


        /// <summary>
        /// Creates a new Chat
        /// </summary>
        private void NewChat()
        {
            bool continueNewChat = true;
            string errorMessage = "";

            while (continueNewChat)
            {
                // Displaying some messages
                Console.Clear();
                CONSOLE.WriteLine(ConsoleColor.Blue, "\n   Enter empty fields to leave the Creation of the new CHAT");


                // Displays a message if the credentials are incorrect
                CONSOLE.WriteLine(ConsoleColor.Red, errorMessage);



                // NAME
                Console.Write("\n Please enter the name of the Chat : ");
                string name = Console.ReadLine();


                // DESCRIPTION
                Console.Write("\n Please enter the description of the Chat  : ");
                string description = Console.ReadLine();


                // 2ND MEMBER
                Console.Write("\n Please enter the name of your correspondent : ");
                string correspondentName = Console.ReadLine();


                // All the fields are empty : go back to the menu
                if (name.Length == 0 && description.Length == 0 && correspondentName.Length == 0)
                {
                    continueNewChat = false;
                }
                // One of the field is empty : error
                else if (name.Length == 0 || description.Length == 0 || correspondentName.Length == 0)
                {
                    errorMessage = PrefabMessage.INCOMPLETE_FIELDS;
                }
                else if (name.Length > PrefabMessage.INPUT_MAXSIZE_STRUCTURE_NAME || description.Length > PrefabMessage.INPUT_MAXSIZE_STRUCTURE_DESCRIPTION || correspondentName.Length > PrefabMessage.INPUT_MAXSIZE_PROFILE_NAME)
                {
                    errorMessage = PrefabMessage.INCORRECT_INPUT_SIZE;
                }
                // If at least one field has one incorrect character : ERROR
                if (!PrefabMessage.CorrectInput(name) || !PrefabMessage.CorrectInput(description) || !PrefabMessage.CorrectInput(correspondentName))
                {
                    errorMessage = PrefabMessage.INCORRECT_INPUT_CHARACTER;
                }
                // The user tries to add himself as the 2nd member of the Chat : ERROR
                if (correspondentName == Net.PROFILE.GetByID(comm, ID_user).name)
                {
                    errorMessage = PrefabMessage.CHAT_CREATION_2ND_MEMBER_IS_SELF;
                }
                else
                {
                    Profile correspondent = Net.PROFILE.GetByName(comm, correspondentName);

                    // The correspondent's name doesn't exist : ERROR
                    if (correspondent.ID == Guid.Empty)
                    {
                        errorMessage = PrefabMessage.CHAT_CREATION_2ND_MEMBER_NOT_FOUND;
                    }
                    else
                    {
                        // Sending the new Chat
                        Instructions instruction = Instructions.Chat_New;
                        Object content = new Chat(name, description, new List<Guid>() { ID_user, correspondent.ID });
                        Net.COMMUNICATION.Send(comm.GetStream(), new Communication(instruction, content));


                        // We receive whether the creation was successful or not
                        if (Net.BOOL.Receive(comm.GetStream()))
                        {
                            CONSOLE.WriteLine(ConsoleColor.Green, PrefabMessage.CHAT_CREATION_SUCCESS);

                            // Ending the loop
                            continueNewChat = false;
                        }
                        else
                        {
                            errorMessage = PrefabMessage.CHAT_CREATION_FAILURE;
                        }
                    }
                }
            }
        }
    }
}