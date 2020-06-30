using System;
using System.Collections.Generic;


namespace Back_Communication
{
    [Serializable]
    public class Topic : Structure
    {
        private Guid _ID_creator;


        /// <summary>
        /// Default constructor for the Topic class
        /// </summary>
        public Topic()
            : base("", "")
        {
            ID = Guid.Empty;
            ID_creator = Guid.Empty;
        }



        /// <summary>
        /// Small constructor for the Topic class. Used to create a new Topic.
        /// </summary>
        /// <param name="name"> Name of the instance </param>
        /// <param name="description"> Password of the instance </param>
        /// <param name="ID_creator"> Profile of the creator of the instance </param>
        public Topic(string name, string description, Guid ID_creator)
            : base(name, description, new List<Guid>() { ID_creator })
        {
            this.ID = Guid.NewGuid();
            this.ID_creator = ID_creator;
        }



        /// <summary>
        /// Complete constructor for the Topic class. Used to receive a complete Topic from the database.
        /// </summary>
        /// <param name="ID"> ID of the instance </param>
        /// <param name="name"> Name of the instance </param>
        /// <param name="description"> Password of the instance </param>
        /// <param name="ID_members"> List of all the IDs of the active members of the topic </param>
        /// <param name="ID_messages"> List of all the Messages of the topic </param>
        /// <param name="ID_creator"> Creator of the instance </param>
        /// <param name="dateCreation"> Date when the instance has been created </param>
        public Topic(Guid ID, string name, string description, List<Guid> ID_members, List<Guid> ID_messages, Guid ID_creator, DateTime dateCreation)
            : base(ID, name, description, ID_members, ID_messages, dateCreation)
        {
            this.ID_creator = ID_creator;
        }




        public Guid ID_creator
        {
            get { return _ID_creator; }
            set { _ID_creator = value; }
        }



        /// <summary>
        /// Returns a textual representation of the instance
        /// </summary>
        /// <returns> a textual representation of the instance </returns>
        public override string ToString()
        {
            return "Topic n° " + ID + " : " + name +
                "\n   " + description +
                "\n   Number of members : " + numberOfMembers +
                "\n   Number of messages : " + numberOfMessages +
                "\n   created by user of ID " + ID_creator + " the " + dateCreation + "\n";
        }
    }
}