using System;
using System.Collections.Generic;


namespace Back_Communication
{
    [Serializable]
    public abstract class Structure
    {
        private Guid _ID;
        private string _name;
        private string _description;
        private List<Guid> _ID_members;
        private List<Guid> _ID_messages;
        private DateTime _dateCreation;



        /// <summary>
        /// Default constructor for the Structure class
        /// </summary>
        /// <param name="name"> Name of the instance </param>
        /// <param name="description"> Description of the instance </param>
        public Structure(string name, string description)
        {
            this.ID = Guid.Empty;
            this.name = name;
            this.description = description;
            this.ID_members = new List<Guid>();
            this.ID_messages = new List<Guid>();
            this.dateCreation = DateTime.Now;
        }


        /// <summary>
        /// Small constructor for the Structure class. Used to create a new Structure.
        /// </summary>
        /// <param name="name"> Name of the instance </param>
        /// <param name="description"> Password of the instance </param>
        /// <param name="ID_members"> List of all the IDs of the members of the instance </param>
        public Structure(string name, string description, List<Guid> ID_members)
        {
            this.name = name;
            this.description = description;
            this.ID_members = ID_members;
            this.ID_messages = new List<Guid>();
            this.dateCreation = DateTime.Now;
        }


        /// <summary>
        /// Complete constructor for the Structure class. Used to receive a complete Structure from the database.
        /// </summary>
        /// <param name="ID_chat"> ID of the instance </param>
        /// <param name="name"> Name of the instance </param>
        /// <param name="description"> Password of the instance </param>
        /// <param name="ID_members"> List of all the IDs of the members of the instance </param>
        /// <param name="ID_messages"> List of all the Messages of the instance </param>
        /// <param name="dateCreation"> Date when the instance has been created </param>
        public Structure(Guid ID_chat, string name, string description, List<Guid> ID_members, List<Guid> ID_messages, DateTime dateCreation)
        {
            this.ID = ID_chat;
            this.name = name;
            this.description = description;
            this.ID_members = ID_members;
            this.ID_messages = ID_messages;
            this.dateCreation = dateCreation;
        }


        public Guid ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        public string name
        {
            get { return _name; }
            set { _name = value; }
        }
        public string description
        {
            get { return _description; }
            set { _description = value; }
        }
        public List<Guid> ID_members
        {
            get { return _ID_members; }
            set { _ID_members = value; }
        }
        public List<Guid> ID_messages
        {
            get { return _ID_messages; }
            set { _ID_messages = value; }
        }
        public DateTime dateCreation
        {
            get { return _dateCreation; }
            set { _dateCreation = value; }
        }


        /// <summary>
        /// Returns the number of members of the instance
        /// </summary>
        public int numberOfMembers
        {
            get { return ID_members.Count; }
        }


        /// <summary>
        /// Returns the number of messages of the instance
        /// </summary>
        public int numberOfMessages
        {
            get { return ID_messages.Count; }
        }


        /// <summary>
        /// Returns whether or not a given Profile is present amongst the members of the instance
        /// </summary>
        /// <param name="ID_given"> ID of the Profile of which we want to know if he is present in the members list</param>
        /// <returns> whether or not a given Profile is present amongst the members of the instance </returns>
        public bool containsMemberByID(Guid ID_given)
        {
            // We iterate through all the members
            foreach (Guid ID in ID_members)
            {
                // If the current member is the same as the Profile sent
                if (ID == ID_given)
                {
                    return true;
                }
            }

            // Otherwise, the profile is not a member
            return false;
        }


        public override abstract string ToString();
    }
}
