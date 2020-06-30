using System;


namespace Back_Communication
{
    public class ChatCredentials
    {
        Guid _ID_Chat;
        Guid _ID_user;

        public ChatCredentials(Guid ID_Chat, Guid ID_user)
        {
            this.ID_Chat = ID_Chat;
            this.ID_user = ID_user;
        }

        public Guid ID_Chat
        {
            get { return _ID_Chat; }
            set { _ID_Chat = value; }
        }
        public Guid ID_user
        {
            get { return _ID_user; }
            set { _ID_user = value; }
        }


        public override string ToString()
        {
            return "Credentials : chat : " + ID_Chat + " - user : " + ID_user;
        }
    }
}
