using System;


namespace Back_Server
{
    class Program
    {
        static void Main(string[] args)
        {
            // Creating the server's console Interface
            Console.Write("Launching the server...");
            Server server = new Server(8976);
            server.Start();
        }
    }
}