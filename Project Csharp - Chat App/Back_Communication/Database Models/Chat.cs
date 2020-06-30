using System;
using System.Collections.Generic;


namespace Back_Communication
{
    [Serializable]
    public class Chat : Structure
    {
        /// <summary>
        /// Default constructor for the Chat class
        /// </summary>
        public Chat()
            : base("DEFAULT CHAT'S NAME", "DEFAULT CHAT'S DESCRIPTION")
        {
        }



        /// <summary>
        /// Small constructor for the Chat class. Used to create a new Chat.
        /// </summary>
        /// <param name="name"> Name of the instance </param>
        /// <param name="description"> Password of the instance </param>
        /// <param name="ID_members"> List of the IDs of the members of the instance </param>
        public Chat(string name, string description, List<Guid> ID_members)
            : base(name, description, ID_members)
        {
            ID = Guid.NewGuid();
        }



        /// <summary>
        /// Complete constructor for the Chat class. Used to receive a complete Chat from the database.
        /// </summary>
        /// <param name="ID"> ID of the instance </param>
        /// <param name="name"> Name of the instance </param>
        /// <param name="description"> Password of the instance </param>
        /// <param name="ID_members"> List of all the ID of the members of the instance </param>
        /// <param name="ID_messages"> List of all the Messages of the instance </param>
        /// <param name="dateCreation"> Date when the instance has been created </param>
        public Chat(Guid ID, string name, string description, List<Guid> ID_members, List<Guid> ID_messages, DateTime dateCreation)
            : base(ID, name, description, ID_members, ID_messages, dateCreation)
        {
        }



        /// <summary>
        /// Returns a textual representation of the instance
        /// </summary>
        /// <returns> a textual representation of the instance </returns>
        public override string ToString()
        {
            return "Topic n° " + ID + " : " + name +
                "\n" + description +
                "\n   Number of members : " + numberOfMembers +
                "\n   Number of messages : " + numberOfMessages +
                "\ncreated the " + dateCreation + "\n";
        }
    }
}