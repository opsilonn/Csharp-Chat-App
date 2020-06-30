using System;


namespace Back_Communication
{
    [Serializable]
    public class MessageCreation
    {
        private Structure _structure;
        private Message _message;

        public MessageCreation(Structure structure, Message message)
        {
            this.structure = structure;
            this.message = message;
        }


        public Structure structure
        {
            get { return _structure; }
            set { _structure = value; }
        }
        public Message message
        {
            get { return _message; }
            set { _message = value; }
        }


        public override string ToString()
        {
            return "Message Creation : Structure : " + structure + " - message : " + message;
        }
    }
}