using System;



namespace Back_Server
{
    static class Constants
    {
        public static string server = "localhost";
        public static string database = "c_sharp";
        public static string port = "3306";
        public static string user = "root";
        public static string password = "";
        public static string connectionString()
        {
            return String.Format("server={0}; port={1}; user id={2}; password={3}; database={4};", server, port, user, password, database);
        }


        public static string query_Profile_GetAll = "SELECT * FROM profile";
    }
}