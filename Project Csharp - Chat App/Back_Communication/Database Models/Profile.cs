using System;
using System.Collections.Generic;

namespace Back_Communication
{
    [Serializable]
    public class Profile
    {
        private Guid _ID;
        private string _name;
        private string _password;
        private string _email;
        private DateTime _dateCreation;


        /// <summary>
        /// Default constructor for the Profile class
        /// </summary>
        public Profile()
        {
            ID = Guid.Empty;
            name = "";
            password = "";
            email = "";
            dateCreation = DateTime.Now;
        }



        /// <summary>
        /// Small constructor for the Profile class. Used to create a new Profile.
        /// </summary>
        /// <param name="name"> Name of the instance </param>
        /// <param name="password"> Password of the instance </param>
        /// <param name="email">Email of the instance </param>
        public Profile(string name, string password, string email)
        {
            this.ID = Guid.NewGuid();
            this.name = name;
            this.password = password;
            this.email = email;
            this.dateCreation = DateTime.Now;
        }



        /// <summary>
        /// Complete constructor for the Profile class. Used to receive a complete Profile from the database.
        /// </summary>
        /// <param name="ID"> ID of the instance </param>
        /// <param name="name"> Name of the instance </param>
        /// <param name="password"> Password of the instance </param>
        /// <param name="email">Email of the instance </param>
        /// <param name="dateCreation"> Date when the instance has been created </param>
        public Profile(Guid ID, string name, string password, string email, DateTime dateCreation)
        {
            this.ID = ID;
            this.name = name;
            this.password = password;
            this.email = email;
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
        public string password
        {
            get { return _password; }
            set { _password = value; }
        }
        public string email
        {
            get { return _email; }
            set { _email = value; }
        }
        public DateTime dateCreation
        {
            get { return _dateCreation; }
            set { _dateCreation = value; }
        }


        /// <summary>
        /// Returns a textual representation of the instance
        /// </summary>
        /// <returns> a textual representation of the instance </returns>
        public override string ToString()
        {
            return "Profile n° " + ID +
                "\n   Name : " + name +
                "\n   Password : " +  password +
                "\n   Email : " + email +
                "\n   Date of creation : " + dateCreation;
        }
    }
}