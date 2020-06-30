using System;
using System.Windows.Forms;
using System.Net.Sockets;
using Back_Communication;
using System.Collections.Generic;

namespace Front_WindowsForm
{
    public partial class FormInviteMember : Form
    {
        TcpClient comm;
        Guid ID_user;
        Chat chat;


        public FormInviteMember(TcpClient comm, Guid ID_user, Chat chat)
        {
            this.comm = comm;
            this.ID_user = ID_user;
            this.chat = chat;

            InitializeComponent();


            // Ask for all the Profiles from the Database
            Net.COMMUNICATION.Send(comm.GetStream(), new Communication(Instructions.Profile_GetAll, null));


            // Get all the Profile from the Database
            List<Profile> profiles = Net.PROFILE.ReceiveAll(comm.GetStream());

            // We initialize the ComboBox
            comboBox.Items.Clear();
            
            // We fill the ComboBox
            foreach (Profile profile in profiles)
            {
                if(profile.ID != ID_user && !chat.containsMemberByID(profile.ID))
                {
                    comboBox.Items.Add(profile.name);
                }
            }

            if(comboBox.Items.Count == 0)
            {
                label.Text = "you cannot add a user : none are available to be add !";
                comboBox.Visible = false;
                button.Enabled = false;
            }
        }


        /// <summary>
        /// Event raised when clicking on the button to validate the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Click(object sender, EventArgs e)
        {
            string name = comboBox.Text;
            string errorMessage = "";

            // We have to make sure that the name entered isn't the one the user (HE CANNOT ADD HIMSELF !!)
            // Asking for a Profile
            Net.COMMUNICATION.Send(comm.GetStream(), new Communication(Instructions.Profile_GetByID, ID_user));

            // We check the names
            if(comboBox.Text == Net.PROFILE.Receive(comm.GetStream()).name)
            {
                errorMessage = PrefabMessage.NEWCHATMEMBER_ADD_SELF;
            }
            // if the field is too long : ERROR
            else if (name.Length > PrefabMessage.INPUT_MAXSIZE_PROFILE_NAME)
            {
                errorMessage = PrefabMessage.INCORRECT_INPUT_SIZE;
            }
            // If at least one field has one incorrect character : ERROR
            else if (!PrefabMessage.CorrectInput(name))
            {
                errorMessage = PrefabMessage.INCORRECT_INPUT_CHARACTER;
            }
            else
            {
                // We send the new Member
                Instructions instruction = Instructions.Member_Join;
                Object content = new Member(chat, name);
                Net.COMMUNICATION.Send(comm.GetStream(), new Communication(instruction, content));


                // We get whether it worked or not
                bool itWorked = Net.BOOL.Receive(comm.GetStream());

                // We display a return message
                if(itWorked)
                {
                    MessageBox.Show(PrefabMessage.NEWCHATMEMBER_SUCCESS,
                        "Success !",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation,
                        MessageBoxDefaultButton.Button1);

                    // We hide the current GUI
                    Hide();
                }
                if (!itWorked)
                {
                    errorMessage = PrefabMessage.NEWCHATMEMBER_FAILURE;
                }
            }


            // If we arrive to this bit of code : we display the result, if needed
            if (errorMessage != "")
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