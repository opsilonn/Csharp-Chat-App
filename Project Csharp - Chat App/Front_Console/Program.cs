﻿using System;


namespace Front_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            // Creating the client's console Interface
            Client client = new Client("127.0.0.1", 8976);
            client.Start();
        }
    }
}