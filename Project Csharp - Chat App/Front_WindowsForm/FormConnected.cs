using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Net.Sockets;
using Back_Communication;


namespace Front_WindowsForm
{
    public partial class FormConnected : Form
    {
        TcpClient comm;
        Guid ID_user;
        Guid ID_structure;
        bool isTopic;
        bool isChat;

        public FormConnected(TcpClient comm, Guid ID_user)
        {
            this.comm = comm;
            this.ID_user = ID_user;
            ID_structure = Guid.Empty;
            isTopic = false;
            isChat = false;

            InitializeComponent();

            // Since no Structure is selected by default, we hide the fields detailling a Structure's data
            labelName.Visible = false;
            tableLayoutPanel5.Visible = false;

            FillTopics();
            FillChats();
        }



        /// <summary>
        /// Fills the Topic TreeView
        /// </summary>
        public void FillTopics()
        {
            // Asking for all the Topics
            Instructions instruction = Instructions.Topic_GetAll;
            Object content = null;
            Net.COMMUNICATION.Send(comm.GetStream(), new Communication(instruction, content));

            // Receiving all the Topics
            List<Topic> topics = Net.TOPIC.ReceiveAll(comm.GetStream());

            // We empty the Tree
            treeViewTopics.Nodes.Clear();

            // We fill the Tree
            foreach (Topic topic in topics)
            {
                // We create the TreeNode hosting the data
                TreeNode treeNode = new TreeNode();
                treeNode.Name = topic.ID.ToString();
                treeNode.Text = topic.name;

                // If the user is a member of this Topic : we highlight it
                if (topic.ID_members.Contains(ID_user))
                {
                    treeNode.NodeFont = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                }
                // If the user created this Topic : we highlight it with a higher font's size
                if (topic.ID_members.Contains(ID_user))
                {
                    treeNode.NodeFont = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                }

                treeViewTopics.Nodes.Add(treeNode);
            }


            // If there is no Topic available : we display a message
            if(treeViewTopics.Nodes.Count == 0)
            {
                // We create the TreeNode hosting the data
                TreeNode treeNode = new TreeNode();
                treeNode.Name = Guid.Empty.ToString();
                treeNode.Text = "There is no Topic available !";

                treeNode.NodeFont = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                treeViewTopics.Nodes.Add(treeNode);
            }
        }


        /// <summary>
        /// Fills the Topic TreeView
        /// </summary>
        public void FillChats()
        {
            // Clear the Chats Queue
            treeViewChats.Nodes.Clear();

            // Asking for all the Chats (we have to send our ID : we can only access the chat that we are connected to)
            Instructions instruction = Instructions.Chat_GetAll;
            Object content = ID_user;
            Net.COMMUNICATION.Send(comm.GetStream(), new Communication(instruction, content));

            // Receiving all the Topics
            List<Chat> chats = Net.CHAT.ReceiveAll(comm.GetStream());

            // We fill the Tree
            foreach (Chat chat in chats)
            {
                // We create the TreeNode hosting the data
                TreeNode treeNode = new TreeNode();
                treeNode.Name = chat.ID.ToString();
                treeNode.Text = chat.name;

                treeViewChats.Nodes.Add(treeNode);
            }


            // If there is no Topic available : we display a message
            if (treeViewChats.Nodes.Count == 0)
            {
                // We create the TreeNode hosting the data
                TreeNode treeNode = new TreeNode();
                treeNode.Name = Guid.Empty.ToString();
                treeNode.Text = "There is no Chat available !";

                treeNode.NodeFont = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                treeViewChats.Nodes.Add(treeNode);
            }
        }

        /// <summary>
        /// Sets up all the information about the Structure currently selected
        /// </summary>
        /// <param name="structure">Structure of which we want to display the data</param>
        public void FillStructure()
        {
            // IF THE ID IS NULL : Display nothing
            if (ID_structure == Guid.Empty)
            {
                // Sets the group as NOT visible
                labelName.Visible = false;
                tableLayoutPanel5.Visible = false;
            }
            // IF THE ID IS NOT NULL
            if(ID_structure != Guid.Empty)
            {
                // We initialize our variables
                Structure structure = GetStructure();


                // If we retreived a Structure (not null)
                if (structure != null)
                {
                    // Sets the group as visible
                    labelName.Visible = true;
                    tableLayoutPanel5.Visible = true;

                    // Sets the labels
                    labelName.Text = structure.name;
                    labelDescription.Text = structure.description;
                    labelDate.Text = "Created the : " + structure.dateCreation.ToString();
                    labelMembers.Text = "Members : " + structure.ID_members.Count.ToString();
                    labelMessages.Text = "Messages : " + structure.ID_messages.Count.ToString();

                    if (structure is Topic)
                    {
                        labelCreator.Text = "Created by : " + GetProfileByID(((Topic)structure).ID_creator).name;
                    }
                    else
                    {
                        labelCreator.Text = "";
                    }

                    // Display the messages
                    FillMessages(structure.ID_messages);


                    // The Buttons
                    if (structure is Topic)
                    {
                        if (structure.containsMemberByID(ID_user))
                        {
                            buttonMember.Text = "Leave Topic";
                        }
                        else
                        {
                            buttonMember.Text = "Join Topic";
                        }
                        buttonLeaveChat.Visible = false;
                    }
                    if (structure is Chat)
                    {
                        buttonMember.Text = "Invite someone";
                        buttonLeaveChat.Visible = true;
                    }
                }
            }
        }

        /// <summary>
        /// Displays all the Messages of a Structure
        /// </summary>
        /// <param name="ID_messages">List of the Messages ID to display</param>
        public void FillMessages(List<Guid> ID_messages)
        {
            // Clear the message Queue
            treeViewMessages.Nodes.Clear();


            // If there are no message
            if (ID_messages.Count == 0)
            {
                // We create the TreeNode hosting the message
                TreeNode noMessage = new TreeNode();
                noMessage.Text = PrefabMessage.MESSAGE_EMPTY;
                noMessage.NodeFont = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                // We add all the TreeNodes (+ one, to add a blank space
                treeViewMessages.Nodes.Add(noMessage);
            }
            // otherwise : Display all the Messages
            else
            {
                // We iterate through the messages
                foreach (Guid ID_message in ID_messages)
                {
                    // Asking to receive a Message
                    Net.COMMUNICATION.Send(comm.GetStream(), new Communication(Instructions.Message_GetByID, ID_message));

                    // We receive the Message from the server
                    Back_Communication.Message message = Net.MESSAGE.Receive(comm.GetStream());



                    // Asking to receive the author
                    Net.COMMUNICATION.Send(comm.GetStream(), new Communication(Instructions.Profile_GetByID, message.ID_author));

                    // We receive the Profile of the author from the server
                    Profile profile = Net.PROFILE.Receive(comm.GetStream());



                    // We create the TreeNode hosting the author's data
                    TreeNode treeNodeAuthor = new TreeNode();
                    treeNodeAuthor.Name = profile.ID.ToString();
                    treeNodeAuthor.Text = profile.name + " - " + message.dateCreation.ToString();
                    treeNodeAuthor.NodeFont = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));


                    // We create the TreeNode hosting the content's data
                    TreeNode treeNodeContent = new TreeNode();
                    treeNodeContent.Name = message.ID.ToString();
                    treeNodeContent.Text = message.content;

                    // We add all the TreeNodes (+ one, to add a blank space
                    treeViewMessages.Nodes.Add(treeNodeAuthor);
                    treeViewMessages.Nodes.Add(treeNodeContent);
                    treeViewMessages.Nodes.Add(new TreeNode());
                }

            }
        }







        // EVENTS

        /// <summary>
        /// Event raised when double-clicking on a Node from the Topic's TreeView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeViewTopics_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // Setting some variables
            isTopic = true;
            isChat = false;

            // Changing the ID
            ID_structure = Guid.Parse(treeViewTopics.SelectedNode.Name);

            // Display the Topic
            FillStructure();
        }



        /// <summary>
        /// Event raised when double-clicking on a Node from the Chat's TreeView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeViewChats_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // Setting some variables
            isTopic = false;
            isChat = true;

            // Changing the ID
            ID_structure = Guid.Parse(treeViewChats.SelectedNode.Name);

            // Display the Chat
            FillStructure();
        }


        /// <summary>
        /// Event raised when clicking on the button to Join / Leave the Structure
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonMember_Click(object sender, EventArgs e)
        {
            // If we are in a Topic
            if (isTopic)
            {
                // Asking for the Topic
                Net.COMMUNICATION.Send(comm.GetStream(), new Communication(Instructions.Topic_GetByID, ID_structure));

                // Receiving the Topic
                Topic topic = Net.TOPIC.Receive(comm.GetStream());

                // If the user is a member of the Topic : LEAVE IT
                if (topic.containsMemberByID(ID_user))
                {
                    // Asking to remove a Member
                    Instructions instruction = Instructions.Member_Leave;
                    Object content = new Member(topic, GetProfileByID(ID_user).name);
                    Net.COMMUNICATION.Send(comm.GetStream(), new Communication(instruction, content));


                    // Now, the button proposes us the opposite : to Join
                    buttonMember.Text = "Join Topic";
                }
                // If the user is NOT a member of the Topic : JOIN IT
                else
                {
                    // Asking to send a new Member
                    Instructions instruction = Instructions.Member_Join;
                    Object content = new Member(topic, GetProfileByID(ID_user).name);
                    Net.COMMUNICATION.Send(comm.GetStream(), new Communication(instruction, content));

                    // Now, the button proposes us the opposite : to Leave
                    buttonMember.Text = "Leave Topic";
                }


                // At the end, we refresh the whole part listing the Topics
                FillTopics();
            }
            // If we are in a Chat
            if(isChat)
            {
                // Asking for the Chat
                Net.COMMUNICATION.Send(comm.GetStream(), new Communication(Instructions.Chat_GetByID, ID_structure));

                // Receiving the Topic
                Chat chat = Net.CHAT.Receive(comm.GetStream());

                // We launch the GUI to invite someone
                new FormInviteMember(comm, ID_user, chat).ShowDialog();
            }


            // At the end, we refresh the whole part detailling the data about the current Structure
            FillStructure();
        }


        /// <summary>
        /// Event raised when clicking on the button to leave a Chat
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonLeaveChat_Click(object sender, EventArgs e)
        {
            // Asking for the Chat
            Net.COMMUNICATION.Send(comm.GetStream(), new Communication(Instructions.Chat_GetByID, ID_structure));

            // Receiving the Chat
            Chat chat = Net.CHAT.Receive(comm.GetStream());


            // Asking to remove ourself from the Chat
            Instructions instruction = Instructions.Member_Leave;
            Object content = new Member(chat, GetProfileByID(ID_user).name);
            Net.COMMUNICATION.Send(comm.GetStream(), new Communication(instruction, content));


            // Refreshing the Chat's list (amongst other things)
            ID_structure = Guid.Empty;
            isChat = false;
            FillChats();
            FillStructure();
        }


        /// <summary>
        /// Launches the GUI to create a new Topic
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonNewTopic_Click(object sender, EventArgs e)
        {
            // We create the GUI to create a new Topic
            new FormNewStructure(comm, ID_user, new Topic()).ShowDialog();

            // Refresh the list of Topics
            FillTopics();
        }


        /// <summary>
        /// Launches the GUI to create a new Chat
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonNewChat_Click(object sender, EventArgs e)
        {
            // We create the GUI to create a new Topic
            new FormNewStructure(comm, ID_user, new Chat()).ShowDialog();

            // Refresh the list of Chats
            FillChats();
        }


        /// <summary>
        /// Launches the GUI to create a new Message
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonNewMessage_Click(object sender, EventArgs e)
        {
            // We create the GUI to create a new Message
            new FormNewMessage(comm, ID_user, GetStructure()).ShowDialog();

            // Refresh the current Structure
            FillStructure();
        }





        // Useful methods

        /// <summary>
        /// Gets a Profile by its ID
        /// </summary>
        /// <param name="ID"> ID of the Profile seeked </param>
        /// <returns> The Profile with the ID from the Database </returns>
        private Profile GetProfileByID(Guid ID)
        {
            // Asking for a Profile
            Net.COMMUNICATION.Send(comm.GetStream(), new Communication(Instructions.Profile_GetByID, ID));

            // We return the Profile received
            return Net.PROFILE.Receive(comm.GetStream());
        }


        /// <summary>
        /// Gets the current Structure from the server
        /// </summary>
        /// <returns>The current Structure from the server</returns>
        private Structure GetStructure()
        {
            // We retreive the Structure from the database
            if (isTopic)
            {
                // Asking for the Topic
                Net.COMMUNICATION.Send(comm.GetStream(), new Communication(Instructions.Topic_GetByID, ID_structure));

                // Receiving the Topic
                return Net.TOPIC.Receive(comm.GetStream());
            }
            if (isChat)
            {
                // Asking for the Chat
                Net.COMMUNICATION.Send(comm.GetStream(), new Communication(Instructions.Chat_GetByID, ID_structure));

                // Receiving the Chat
                return Net.CHAT.Receive(comm.GetStream());
            }

            return null;
        }
    }
}