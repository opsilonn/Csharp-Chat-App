using System;


namespace Back_Communication
{
    [Serializable]
    public class Credentials
    {
        private string _username;
        private string _password;

        public Credentials(string username, string password)
        {
            this._username = username;
            this.password = password;
        }


        public string username
        {
            get { return _username; }
            set { _username = value; }
        }
        public string password
        {
            get { return _password; }
            set { _password = value; }
        }


        public override string ToString()
        {
            return "Credentials : " + username + " - " + password;
        }
    }
}