using System;
using System.Windows.Forms;
using System.Net.Sockets;


namespace Front_WindowsForm
{
    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Setting some GUI-stuff up
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            // Create the TcpClient
            TcpClient comm = new TcpClient("127.0.0.1", 8976);

            // Launch the Login / SignIn GUI
            Application.Run(new FormConnection(comm));
        }
    }
}