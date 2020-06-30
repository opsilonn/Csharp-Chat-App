using System;


namespace Back_Communication
{
    [Serializable]
    public class Message
    {
        private Guid _ID;
        private string _content;
        private Guid _ID_author;
        private DateTime _dateCreation;


        /// <summary>
        /// Default constructor for the Message class
        /// </summary>
        public Message()
        {
            ID = Guid.Empty;
            content = "";
            ID_author = Guid.Empty;
            dateCreation = DateTime.Now;
        }



        /// <summary>
        /// Small constructor for the Message class. Used to create a new Message.
        /// </summary>
        /// <param name="content"> Content of the instance </param>
        /// <param name="ID_author"> ID of the Creator of the instance </param>
        public Message(string content, Guid ID_author)
        {
            this.ID = Guid.NewGuid();
            this.content = content;
            this.ID_author = ID_author;
            this.dateCreation = DateTime.Now;
        }



        /// <summary>
        /// Complete constructor for the Message class. Used to receive a complete Message from the database.
        /// </summary>
        /// <param name="ID"> ID of the instance </param>
        /// <param name="content"> Content of the instance </param>
        /// <param name="ID_author"> ID of the Creator of the instance </param>
        /// <param name="dateCreation"> Date when the instance has been created </param>
        public Message(Guid ID, string content, Guid ID_author, DateTime dateCreation)
        {
            this.ID = ID;
            this.content = content;
            this.ID_author = ID_author;
            this.dateCreation = dateCreation;
        }




        public Guid ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        public string content
        {
            get { return _content; }
            set { _content = value; }
        }
        public Guid ID_author
        {
            get { return _ID_author; }
            set { _ID_author = value; }
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
            return "Message n° " + ID + " : " +
                "\n" + content +
                "\ncreated by Profile of ID " + ID_author + " the " + dateCreation + "\n";
        }
    }
}
