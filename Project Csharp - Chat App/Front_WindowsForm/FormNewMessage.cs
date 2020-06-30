using System;
using System.Windows.Forms;
using System.Net.Sockets;
using Back_Communication;


namespace Front_WindowsForm
{
    public partial class FormNewMessage : Form
    {
        TcpClient comm;
        Guid ID_user;
        Structure structureHosting;


        public FormNewMessage(TcpClient comm, Guid ID_user, Structure structureHosting)
        {
            this.comm = comm;
            this.ID_user = ID_user;
            this.structureHosting = structureHosting;

            InitializeComponent();
        }


        /// <summary>
        /// When asking to send the Message
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Click(object sender, EventArgs e)
        {
            string message = textBox.Text;
            string errorMessage = "";



            // If the content's size is  0, we end the loop
            if (message.Length == 0)
            {
                errorMessage = PrefabMessage.INCOMPLETE_FIELDS;
            }
            // If the content's size is too long : ERROR
            else if (message.Length > PrefabMessage.INPUT_MAXSIZE_PROFILE_CONTENT)
            {
                errorMessage = PrefabMessage.INCORRECT_INPUT_SIZE;
            }
            // If at least one field has one incorrect character : ERROR
            else if (!PrefabMessage.CorrectInput(message))
            {
                errorMessage = PrefabMessage.INCORRECT_INPUT_CHARACTER;
            }
            // Otherwise : we transfer it to the server
            else
            {
                // Sending the new Message (the Structure it is in + the actual Message)
                Instructions instruction = Instructions.Message_New;
                Object content = new MessageCreation(structureHosting, new Back_Communication.Message(message, ID_user));
                Net.COMMUNICATION.Send(comm.GetStream(), new Communication(instruction, content));

                // We hide the current GUI
                Hide();
            }


            // If we arrive to this bit of code : we display the result, if needed
            if(errorMessage != "")
            {
                MessageBox.Show(errorMessage,
                    "Failure...",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1);
            }
        }
    }
}
