using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;


namespace Back_Communication
{
    public class Net
    {
        public static class COMMUNICATION
        {
            /// <summary>
            /// Sends a Communication
            /// </summary>
            /// <param name="s"> Stream </param>
            /// <param name="msg"> Communication to send </param>
            public static void Send(Stream s, Communication msg)
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(s, msg);
            }


            /// <summary>
            /// 
            /// Receives a Communication
            /// </summary>
            /// <param name="s"> Stream </param>
            /// <returns> The serialized Communication </returns>
            public static Communication Receive(Stream s)
            {
                BinaryFormatter bf = new BinaryFormatter();
                    return (Communication)bf.Deserialize(s);
            }
        }

        public static class STRING
        {
            /// <summary>
            /// Sends a String
            /// </summary>
            /// <param name="s"> Stream </param>
            /// <param name="msg"> String to send </param>
            public static void Send(Stream s, string msg)
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(s, msg);
            }


            /// <summary>
            /// Receives a String
            /// </summary>
            /// <param name="s"> Stream </param>
            /// <returns> The serialized String </returns>
            public static string Receive(Stream s)
            {
                BinaryFormatter bf = new BinaryFormatter();
                return (string)bf.Deserialize(s);
            }
        }

        public static class INT
        {
            /// <summary>
            /// Sends an integer
            /// </summary>
            /// <param name="s"> Stream </param>
            /// <param name="msg"> Integer to send </param>
            public static void Send(Stream s, int msg)
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(s, msg);
            }


            /// <summary>
            /// Receives an integer
            /// </summary>
            /// <param name="s"> Stream </param>
            /// <returns> The serialized Int </returns>
            public static int Receive(Stream s)
            {
                BinaryFormatter bf = new BinaryFormatter();
                return (int)bf.Deserialize(s);
            }
        }

        public static class BOOL
        {
            /// <summary>
            /// Sends an boolean
            /// </summary>
            /// <param name="s"> Stream </param>
            /// <param name="msg"> Boolean to send </param>
            public static void Send(Stream s, bool msg)
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(s, msg);
            }


            /// <summary>
            /// Receives a boolean
            /// </summary>
            /// <param name="s"> Stream </param>
            /// <returns> The serialized boolean </returns>
            public static bool Receive(Stream s)
            {
                BinaryFormatter bf = new BinaryFormatter();
                return (bool)bf.Deserialize(s);
            }
        }

        public static class CREDENTIALS
        {
            /// <summary>
            /// Sends a Credentials
            /// </summary>
            /// <param name="s"> Stream </param>
            /// <param name="credentials"> Credentials to send </param>
            public static void Send(Stream s, Credentials credentials)
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(s, credentials);
            }


            /// <summary>
            /// Receives a Credentials
            /// </summary>
            /// <param name="s"> Stream </param>
            /// <returns> The serialized Credentials </returns>
            public static Credentials Receive(Stream s)
            {
                BinaryFormatter bf = new BinaryFormatter();
                return (Credentials)bf.Deserialize(s);
            }
        }

        public static class PROFILE
        {
            /// <summary>
            /// Sends a Profile
            /// </summary>
            /// <param name="s"> Stream </param>
            /// <param name="profile"> Profile to send </param>
            public static void Send(Stream s, Profile profile)
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(s, profile);
            }


            /// <summary>
            /// Receives a Profile
            /// </summary>
            /// <param name="s"> Stream </param>
            /// <returns> The serialized Profile </returns>
            public static Profile Receive(Stream s)
            {
                BinaryFormatter bf = new BinaryFormatter();
                return (Profile)bf.Deserialize(s);
            }


            /// <summary>
            /// Sends a List of Profiles
            /// </summary>
            /// <param name="s"> Stream </param>
            /// <param name="topics"> List of Profiles to send </param>
            public static void SendAll(Stream s, List<Profile> profiles)
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(s, profiles);
            }


            /// <summary>
            /// Receives a List of Profiles
            /// </summary>
            /// <param name="s"> Stream </param>
            /// <returns></returns>
            public static List<Profile> ReceiveAll(Stream s)
            {
                BinaryFormatter bf = new BinaryFormatter();
                return (List<Profile>)bf.Deserialize(s);
            }


            /// <summary>
            /// Gets a Profile by its ID
            /// </summary>
            /// <param name="ID"> ID of the Profile seeked </param>
            /// <returns> The Profile with the ID from the Database </returns>
            public static Profile GetByID(TcpClient comm, Guid ID)
            {
                // Asking for a Profile
                Net.COMMUNICATION.Send(comm.GetStream(), new Communication(Instructions.Profile_GetByID, ID));

                // We return the Profile received
                return Net.PROFILE.Receive(comm.GetStream());
            }



            /// <summary>
            /// Gets a Profile by its name
            /// </summary>
            /// <param name="name"> Name of the Profile seeked </param>
            /// <returns> The Profile with the name from the Database </returns>
            public static Profile GetByName(TcpClient comm, string name)
            {
                // Asking for a Profile
                Net.COMMUNICATION.Send(comm.GetStream(), new Communication(Instructions.Profile_GetByName, name));

                // We return the Profile received
                return Net.PROFILE.Receive(comm.GetStream());
            }
        }

        public static class MEMBER
        {
            /// <summary>
            /// Sends a Member (whether to join or leave a Structure)
            /// </summary>
            /// <param name="s"> Stream </param>
            /// <param name="member"> Member to send </param>
            public static void Send(Stream s, Member member)
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(s, member);
            }


            /// <summary>
            /// Receives a Member (whether to join or leave a Structure)
            /// </summary>
            /// <param name="s"> Stream </param>
            /// <returns> The serialized Member </returns>
            public static Member Receive(Stream s)
            {
                BinaryFormatter bf = new BinaryFormatter();
                return (Member)bf.Deserialize(s);
            }
        }

        public static class STRUCTURE
        {
            /// <summary>
            /// Sends a Structure
            /// </summary>
            /// <param name="s"> Stream </param>
            /// <param name="topic"> Structure to send </param>
            public static void Send(Stream s, Structure topic)
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(s, topic);
            }


            /// <summary>
            /// Receives a Structure
            /// </summary>
            /// <param name="s"> Stream </param>
            /// <returns> The serialized Structure </returns>
            public static Structure Receive(Stream s)
            {
                BinaryFormatter bf = new BinaryFormatter();
                return (Structure)bf.Deserialize(s);
            }
        }

        public static class TOPIC
        {
            /// <summary>
            /// Sends a Topic
            /// </summary>
            /// <param name="s"> Stream </param>
            /// <param name="topic"> Topic to send </param>
            public static void Send(Stream s, Topic topic)
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(s, topic);
            }


            /// <summary>
            /// Receives a Topic
            /// </summary>
            /// <param name="s"> Stream </param>
            /// <returns> The serialized Topic </returns>
            public static Topic Receive(Stream s)
            {
                BinaryFormatter bf = new BinaryFormatter();
                return (Topic)bf.Deserialize(s);
            }


            /// <summary>
            /// Sends a List of Topics
            /// </summary>
            /// <param name="s"> Stream </param>
            /// <param name="topics"> List of Topics to send </param>
            public static void SendAll(Stream s, List<Topic> topics)
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(s, topics);
            }


            /// <summary>
            /// Receives a List of Topics
            /// </summary>
            /// <param name="s"> Stream </param>
            /// <returns></returns>
            public static List<Topic> ReceiveAll(Stream s)
            {
                BinaryFormatter bf = new BinaryFormatter();
                return (List<Topic>)bf.Deserialize(s);
            }
        }

        public static class CHAT
        {
            /// <summary>
            /// Sends a Chat
            /// </summary>
            /// <param name="s"> Stream </param>
            /// <param name="topic"> Chat to send </param>
            public static void Send(Stream s, Chat chat)
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(s, chat);
            }


            /// <summary>
            /// Receives a Chat
            /// </summary>
            /// <param name="s"> Stream </param>
            /// <returns> The serialized Chat </returns>
            public static Chat Receive(Stream s)
            {
                BinaryFormatter bf = new BinaryFormatter();
                return (Chat)bf.Deserialize(s);
            }


            /// <summary>
            /// Sends a List of Chat
            /// </summary>
            /// <param name="s"> Stream </param>
            /// <param name="topics"> List of Chat to send </param>
            public static void SendAll(Stream s, List<Chat> chats)
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(s, chats);
            }


            /// <summary>
            /// Receives a List of Chat
            /// </summary>
            /// <param name="s"> Stream </param>
            /// <returns></returns>
            public static List<Chat> ReceiveAll(Stream s)
            {
                BinaryFormatter bf = new BinaryFormatter();
                return (List<Chat>)bf.Deserialize(s);
            }
        }

        public static class MESSAGE
        {
            /// <summary>
            /// Sends a Message
            /// </summary>
            /// <param name="s"> Stream </param>
            /// <param name="msg"> Message to send </param>
            public static void Send(Stream s, Message msg)
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(s, msg);
            }


            /// <summary>
            /// Receives a Message
            /// </summary>
            /// <param name="s"> Stream </param>
            /// <returns> The serialized Message </returns>
            public static Message Receive(Stream s)
            {
                BinaryFormatter bf = new BinaryFormatter();
                return (Message)bf.Deserialize(s);
            }
        }




        //// UNIFORM MODEL
        /*
        /// <summary>
        /// Sends a Message
        /// </summary>
        /// <param name="s"> Stream </param>
        /// <param name="msg"> Message to send </param>
        public static void Send(Stream s, Object msg)
        {
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(s, msg);
        }


        /// <summary>
        /// Receives a Message
        /// </summary>
        /// <param name="s"> Stream </param>
        /// <returns> The serialized Message </returns>
        public static Object Receive(Stream s)
        {
            BinaryFormatter bf = new BinaryFormatter();
            return (Object)bf.Deserialize(s);
        }
        */
    }
}