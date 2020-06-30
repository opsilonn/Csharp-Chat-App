using System.Windows.Forms;
using System.Net.Sockets;
using Back_Communication;
using System;


namespace Front_WindowsForm
{
    public partial class FormConnection : Form
    {
        TcpClient comm;

        public FormConnection(TcpClient comm)
        {
            this.comm = comm;
            InitializeComponent();

            labelLoginError.ResetText();
            labelSignInError.ResetText();
        }



        /// <summary>
        /// Connects the Credentials (if they are valid) with the server, and if corrects, connects the User
        /// </summary>
        public void Login()
        {
            string username = textBoxLoginUsername.Text;
            string password = textBoxLoginPassword.Text;

            // If at least one field is empty : ERROR
            if (username.Length == 0 || password.Length == 0)
            {
                labelLoginError.Text = PrefabMessage.INCOMPLETE_FIELDS;
            }
            // If at least one field is too long : ERROR
            else if (username.Length > PrefabMessage.INPUT_MAXSIZE_PROFILE_NAME || password.Length > PrefabMessage.INPUT_MAXSIZE_PROFILE_PASSWORD)
            {
                labelLoginError.Text = PrefabMessage.INCORRECT_INPUT_SIZE;
            }
            // If at least one field has one incorrect character : ERROR
            else if (!PrefabMessage.CorrectInput(username) || !PrefabMessage.CorrectInput(password))
            {
                labelLoginError.Text = PrefabMessage.INCORRECT_INPUT_CHARACTER;
            }
            // Otherwise : verify with the server
            else
            {
                // Sending the credentials
                Instructions instruction = Instructions.LogIn;
                Object content = new Credentials(username, password);
                Net.COMMUNICATION.Send(comm.GetStream(), new Communication(instruction, content));

                // Receiving the response ID
                Guid ID_user = Guid.Parse(Net.STRING.Receive(comm.GetStream()));

                // If the ID is Empty : there was no match found, we reset the login
                if (ID_user == Guid.Empty)
                {
                    labelLoginError.Text = PrefabMessage.LOGIN_FAILURE;
                }
                // Otherwise : the Login is SUCCESSFUL !
                // We launch the next GUI
                else
                {
                    LaunchConnection(ID_user);
                }
            }
        }



        /// <summary>
        /// Connects the Credentials (if they are valid) with the server, and if corrects, creates a new User
        /// </summary>
        public void SignIn()
        {
            string username = textBoxSignInUsername.Text;
            string password = textBoxSignInPassword.Text;
            string passwordVerif = textBoxSignInPasswordVerif.Text;
            string email = textBoxSignInEmail.Text;

            // If at least one field is empty : ERROR
            if (username.Length == 0 || password.Length == 0 || email.Length == 0 || passwordVerif.Length == 0)
            {
                labelSignInError.Text = PrefabMessage.INCOMPLETE_FIELDS;
            }
            // If at least one field is too long : ERROR
            else if (username.Length > PrefabMessage.INPUT_MAXSIZE_PROFILE_NAME || password.Length > PrefabMessage.INPUT_MAXSIZE_PROFILE_PASSWORD || passwordVerif.Length > PrefabMessage.INPUT_MAXSIZE_PROFILE_PASSWORD || email.Length > PrefabMessage.INPUT_MAXSIZE_PROFILE_EMAIL)
            {
                labelSignInError.Text = PrefabMessage.INCORRECT_INPUT_SIZE;
            }
            // If at least one field has one incorrect character : ERROR
            else if (!PrefabMessage.CorrectInput(username) || !PrefabMessage.CorrectInput(password) || !PrefabMessage.CorrectInput(passwordVerif) || !PrefabMessage.CorrectInput(email))
            {
                labelSignInError.Text = PrefabMessage.INCORRECT_INPUT_CHARACTER;
            }
            // If the passwords fields do not match : ERROR
            else if(password != passwordVerif)
            {
                labelSignInError.Text = PrefabMessage.SIGNIN_PASSWORD_DONT_MATCH;
            }
            // Otherwise : verify with the server
            else
            {
                // Sending the credentials
                Instructions instruction = Instructions.SignIn;
                Object content = new Profile(username, password, email);
                Net.COMMUNICATION.Send(comm.GetStream(), new Communication(instruction, content));

                // Receiving the response ID
                Guid ID_user = Guid.Parse(Net.STRING.Receive(comm.GetStream()));

                // If the ID is Empty : there was no match found, we reset the login
                if (ID_user == Guid.Empty)
                {
                    labelSignInError.Text = PrefabMessage.SIGNIN_FAILURE;
                }
                // Otherwise : the Login is SUCCESSFUL !
                // We launch the next GUI
                else
                {
                    LaunchConnection(ID_user);
                }
            }
        }




        /// <summary>
        /// Creates the GUI for the connected user, and dismiss the current Login/SignIn GUI
        /// </summary>
        /// <param name="ID_user">ID of the user logged in</param>
        private void LaunchConnection(Guid ID_user)
        {
            this.Hide();
            new FormConnected(comm, ID_user).ShowDialog();
        }
    }
}