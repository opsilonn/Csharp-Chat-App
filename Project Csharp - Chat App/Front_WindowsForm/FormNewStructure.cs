using System;
using System.Windows.Forms;
using System.Net.Sockets;
using Back_Communication;
using System.Collections.Generic;

namespace Front_WindowsForm
{
    public partial class FormNewStructure : Form
    {
        TcpClient comm;
        Guid ID_user;
        Structure structureType;


        public FormNewStructure(TcpClient comm, Guid ID_user, Structure structureType)
        {
            this.comm = comm;
            this.ID_user = ID_user;
            this.structureType = structureType;

            InitializeComponent();


            // If we create a Topic : we don't have to add a correspondent
            if (structureType is Topic)
            {
                labelCorrespondent.Visible = false;
                comboBox.Visible = false;
            }
            // If we create a Chat : we have to add a correspondent
            // To do so, we receive all the Profiles from the Database, and display them in the comboBox
            if(structureType is Chat)
            {
                // Ask for all the Profiles from the Database
                Net.COMMUNICATION.Send(comm.GetStream(), new Communication(Instructions.Profile_GetAll, null));


                // Get all the Profile from the Database
                List<Profile> profiles = Net.PROFILE.ReceiveAll(comm.GetStream());

                // We initialize the ComboBox
                comboBox.Items.Clear();

                // We fill the ComboBox
                foreach (Profile profile in profiles)
                {
                    // If the Profile is not the one of the User
                    if (profile.ID != ID_user)
                    {
                        comboBox.Items.Add(profile.name);
                    }
                }

                if (comboBox.Items.Count == 0)
                {
                    comboBox.Items.Add("you cannot add a user : none are available to be add !");
                    comboBox.Enabled = false;
                    button.Enabled = false;
                }
            }
        }



        /// <summary>
        /// Turns the fields into a new Topic OR a new Chat
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Click(object sender, EventArgs e)
        {
            string name = textBoxName.Text;
            string description = textBoxDescription.Text;
            string errorMessage = "";

            // One of the field is empty : error
            if (name.Length == 0 || description.Length == 0)
            {
                errorMessage = PrefabMessage.INCOMPLETE_FIELDS;
            }
            // if the field is too long : ERROR
            else if (name.Length > PrefabMessage.INPUT_MAXSIZE_STRUCTURE_NAME
                || description.Length > PrefabMessage.INPUT_MAXSIZE_STRUCTURE_DESCRIPTION)
            {
                errorMessage = PrefabMessage.INCORRECT_INPUT_SIZE;
            }
            // If at least one field has one incorrect character : ERROR
            else if (!PrefabMessage.CorrectInput(name) || !PrefabMessage.CorrectInput(description))
            {
                errorMessage = PrefabMessage.INCORRECT_INPUT_CHARACTER;
            }
            // Otherwise : send the new Structure
            else
            {
                // Is a Topic : no more verification needed
                if (structureType is Topic)
                {
                    // Sending the new Topic
                    Instructions instruction = Instructions.Topic_New;
                    Object content = new Topic(name, description, ID_user);
                    Net.COMMUNICATION.Send(comm.GetStream(), new Communication(instruction, content));

                    // We receive whether the creation was successful or not
                    if (Net.BOOL.Receive(comm.GetStream()))
                    {
                        // Ending the loop
                        errorMessage = "";
                    }
                    else
                    {
                        errorMessage = PrefabMessage.TOPIC_CREATION_FAILURE;
                    }
                }

                // Is a Chat : more verification is needed
                if (structureType is Chat)
                {
                    string nameCorrespondent = comboBox.Text;


                    // One of the field is empty : error
                    if (nameCorrespondent.Length == 0)
                    {
                        errorMessage = PrefabMessage.INCOMPLETE_FIELDS;
                    }
                    // if the field is too long : ERROR
                    else if (nameCorrespondent.Length > PrefabMessage.INPUT_MAXSIZE_STRUCTURE_NAME)
                    {
                        errorMessage = PrefabMessage.INCORRECT_INPUT_SIZE;
                    }
                    // If at least one field has one incorrect character : ERROR
                    else if (!PrefabMessage.CorrectInput(nameCorrespondent))
                    {
                        errorMessage = PrefabMessage.INCORRECT_INPUT_CHARACTER;
                    }
                    // Otherwise : we do more specific verifications
                    else
                    {
                        Profile user = Net.PROFILE.GetByID(comm, ID_user);
                        Profile correspondent = Net.PROFILE.GetByName(comm, nameCorrespondent);


                        // If the name entered corresponds to no user : ERROR
                        if(correspondent.ID == Guid.Empty)
                        {
                            errorMessage = "Name not found !";

                        }
                        // If the user typed his own name : ERROR (he cannot add himself as his correspondent)
                        else if (user.name == nameCorrespondent)
                        {
                            errorMessage = PrefabMessage.CHAT_CREATION_2ND_MEMBER_IS_SELF;
                        }
                        else
                        {
                            // Sending the new Chat
                            Instructions instruction = Instructions.Chat_New;
                            Object content = new Chat(name, description, new List<Guid>() { ID_user, correspondent.ID });
                            Net.COMMUNICATION.Send(comm.GetStream(), new Communication(instruction, content));

                            // We receive whether the creation was successful or not
                            if (Net.BOOL.Receive(comm.GetStream()))
                            {
                                // Ending the loop
                                errorMessage = "";
                            }
                            else
                            {
                                errorMessage = PrefabMessage.CHAT_CREATION_FAILURE;
                            }
                        }
                    }
                }
            }


            if(errorMessage == "")
            {
                string successMessage = "";

                if (structureType is Topic)
                {
                    successMessage = PrefabMessage.TOPIC_CREATION_SUCCESS;
                }
                if (structureType is Chat)
                {
                    successMessage = PrefabMessage.CHAT_CREATION_SUCCESS;
                }

                MessageBox.Show(successMessage,
                    "Success !",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button1);

                // We hide the current GUI
                Hide();
            }
            else
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