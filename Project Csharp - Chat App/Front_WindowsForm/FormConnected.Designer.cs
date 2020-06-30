namespace Front_WindowsForm
{
    partial class FormConnected
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("TopicA");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("TopicB");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("TopicC");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("ChatA");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("ChatB");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("ChatC");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormConnected));
            this.treeViewTopics = new System.Windows.Forms.TreeView();
            this.treeViewChats = new System.Windows.Forms.TreeView();
            this.labelChats = new System.Windows.Forms.Label();
            this.labelTopics = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.treeViewMessages = new System.Windows.Forms.TreeView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonMember = new System.Windows.Forms.Button();
            this.buttonLeaveChat = new System.Windows.Forms.Button();
            this.buttonNewMessage = new System.Windows.Forms.Button();
            this.labelDate = new System.Windows.Forms.Label();
            this.labelMembers = new System.Windows.Forms.Label();
            this.labelMessages = new System.Windows.Forms.Label();
            this.labelCreator = new System.Windows.Forms.Label();
            this.labelDescription = new System.Windows.Forms.Label();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonNewChat = new System.Windows.Forms.Button();
            this.buttonNewTopic = new System.Windows.Forms.Button();
            this.labelName = new System.Windows.Forms.Label();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeViewTopics
            // 
            this.treeViewTopics.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewTopics.Location = new System.Drawing.Point(3, 3);
            this.treeViewTopics.Name = "treeViewTopics";
            treeNode1.Name = "Bla";
            treeNode1.NodeFont = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            treeNode1.Text = "TopicA";
            treeNode2.Name = "Nœud1";
            treeNode2.Text = "TopicB";
            treeNode3.Name = "Nœud2";
            treeNode3.Text = "TopicC";
            this.treeViewTopics.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3});
            this.treeViewTopics.Size = new System.Drawing.Size(296, 143);
            this.treeViewTopics.TabIndex = 1;
            this.treeViewTopics.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.treeViewTopics_MouseDoubleClick);
            // 
            // treeViewChats
            // 
            this.treeViewChats.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewChats.Location = new System.Drawing.Point(3, 349);
            this.treeViewChats.Name = "treeViewChats";
            treeNode4.Name = "Bla";
            treeNode4.Text = "ChatA";
            treeNode5.Name = "Nœud1";
            treeNode5.Text = "ChatB";
            treeNode6.Name = "Nœud2";
            treeNode6.Text = "ChatC";
            this.treeViewChats.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode4,
            treeNode5,
            treeNode6});
            this.treeViewChats.Size = new System.Drawing.Size(296, 145);
            this.treeViewChats.TabIndex = 2;
            this.treeViewChats.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.treeViewChats_MouseDoubleClick);
            // 
            // labelChats
            // 
            this.labelChats.AutoSize = true;
            this.labelChats.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelChats.Location = new System.Drawing.Point(3, 309);
            this.labelChats.Name = "labelChats";
            this.labelChats.Size = new System.Drawing.Size(296, 37);
            this.labelChats.TabIndex = 4;
            this.labelChats.Text = "CHATS";
            this.labelChats.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelTopics
            // 
            this.labelTopics.AutoSize = true;
            this.labelTopics.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelTopics.Location = new System.Drawing.Point(3, 149);
            this.labelTopics.Name = "labelTopics";
            this.labelTopics.Size = new System.Drawing.Size(296, 37);
            this.labelTopics.TabIndex = 5;
            this.labelTopics.Text = "TOPICS";
            this.labelTopics.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.Controls.Add(this.treeViewMessages, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel1, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 123);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(474, 371);
            this.tableLayoutPanel2.TabIndex = 15;
            // 
            // treeViewMessages
            // 
            this.treeViewMessages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewMessages.Location = new System.Drawing.Point(3, 3);
            this.treeViewMessages.Name = "treeViewMessages";
            this.treeViewMessages.Size = new System.Drawing.Size(349, 365);
            this.treeViewMessages.TabIndex = 7;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.buttonMember, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.buttonLeaveChat, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.buttonNewMessage, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(358, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(113, 365);
            this.tableLayoutPanel1.TabIndex = 14;
            // 
            // buttonMember
            // 
            this.buttonMember.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonMember.Location = new System.Drawing.Point(3, 3);
            this.buttonMember.Name = "buttonMember";
            this.buttonMember.Size = new System.Drawing.Size(112, 115);
            this.buttonMember.TabIndex = 11;
            this.buttonMember.Text = "Join / Leave";
            this.buttonMember.UseVisualStyleBackColor = true;
            this.buttonMember.Click += new System.EventHandler(this.buttonMember_Click);
            // 
            // buttonLeaveChat
            // 
            this.buttonLeaveChat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonLeaveChat.Location = new System.Drawing.Point(3, 245);
            this.buttonLeaveChat.Name = "buttonLeaveChat";
            this.buttonLeaveChat.Size = new System.Drawing.Size(112, 117);
            this.buttonLeaveChat.TabIndex = 13;
            this.buttonLeaveChat.Text = "Leave Chat";
            this.buttonLeaveChat.UseVisualStyleBackColor = true;
            this.buttonLeaveChat.Click += new System.EventHandler(this.buttonLeaveChat_Click);
            // 
            // buttonNewMessage
            // 
            this.buttonNewMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonNewMessage.Location = new System.Drawing.Point(3, 124);
            this.buttonNewMessage.Name = "buttonNewMessage";
            this.buttonNewMessage.Size = new System.Drawing.Size(112, 115);
            this.buttonNewMessage.TabIndex = 12;
            this.buttonNewMessage.Text = "New Message";
            this.buttonNewMessage.UseVisualStyleBackColor = true;
            this.buttonNewMessage.Click += new System.EventHandler(this.buttonNewMessage_Click);
            // 
            // labelDate
            // 
            this.labelDate.AutoSize = true;
            this.labelDate.Location = new System.Drawing.Point(3, 24);
            this.labelDate.Name = "labelDate";
            this.labelDate.Size = new System.Drawing.Size(56, 17);
            this.labelDate.TabIndex = 10;
            this.labelDate.Text = "- Date -";
            // 
            // labelMembers
            // 
            this.labelMembers.AutoSize = true;
            this.labelMembers.Location = new System.Drawing.Point(3, 96);
            this.labelMembers.Name = "labelMembers";
            this.labelMembers.Size = new System.Drawing.Size(115, 17);
            this.labelMembers.TabIndex = 9;
            this.labelMembers.Text = "- CPT members -";
            // 
            // labelMessages
            // 
            this.labelMessages.AutoSize = true;
            this.labelMessages.Location = new System.Drawing.Point(3, 72);
            this.labelMessages.Name = "labelMessages";
            this.labelMessages.Size = new System.Drawing.Size(121, 17);
            this.labelMessages.TabIndex = 8;
            this.labelMessages.Text = "- CPT messages -";
            // 
            // labelCreator
            // 
            this.labelCreator.AutoSize = true;
            this.labelCreator.Location = new System.Drawing.Point(3, 48);
            this.labelCreator.Name = "labelCreator";
            this.labelCreator.Size = new System.Drawing.Size(73, 17);
            this.labelCreator.TabIndex = 5;
            this.labelCreator.Text = "- Creator -";
            // 
            // labelDescription
            // 
            this.labelDescription.AutoSize = true;
            this.labelDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDescription.Location = new System.Drawing.Point(3, 0);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(97, 17);
            this.labelDescription.TabIndex = 4;
            this.labelDescription.Text = "- Description -";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.buttonNewChat, 0, 4);
            this.tableLayoutPanel3.Controls.Add(this.treeViewTopics, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.buttonNewTopic, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.labelTopics, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.labelChats, 0, 5);
            this.tableLayoutPanel3.Controls.Add(this.treeViewChats, 0, 6);
            this.tableLayoutPanel3.Controls.Add(this.labelName, 0, 3);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 7;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.5F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.5F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.5F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.5F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(302, 497);
            this.tableLayoutPanel3.TabIndex = 7;
            // 
            // buttonNewChat
            // 
            this.buttonNewChat.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonNewChat.Location = new System.Drawing.Point(3, 275);
            this.buttonNewChat.Name = "buttonNewChat";
            this.buttonNewChat.Size = new System.Drawing.Size(296, 31);
            this.buttonNewChat.TabIndex = 9;
            this.buttonNewChat.Text = "New Chat";
            this.buttonNewChat.UseVisualStyleBackColor = true;
            this.buttonNewChat.Click += new System.EventHandler(this.buttonNewChat_Click);
            // 
            // buttonNewTopic
            // 
            this.buttonNewTopic.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonNewTopic.Location = new System.Drawing.Point(3, 189);
            this.buttonNewTopic.Name = "buttonNewTopic";
            this.buttonNewTopic.Size = new System.Drawing.Size(296, 31);
            this.buttonNewTopic.TabIndex = 8;
            this.buttonNewTopic.Text = "New Topic";
            this.buttonNewTopic.UseVisualStyleBackColor = true;
            this.buttonNewTopic.Click += new System.EventHandler(this.buttonNewTopic_Click);
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelName.Location = new System.Drawing.Point(3, 223);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(296, 49);
            this.labelName.TabIndex = 10;
            this.labelName.Text = "- NAME -";
            this.labelName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 3;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel5, 2, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(882, 503);
            this.tableLayoutPanel4.TabIndex = 8;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 1;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Controls.Add(this.labelMembers, 0, 4);
            this.tableLayoutPanel5.Controls.Add(this.labelMessages, 0, 3);
            this.tableLayoutPanel5.Controls.Add(this.labelCreator, 0, 2);
            this.tableLayoutPanel5.Controls.Add(this.labelDescription, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.labelDate, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.tableLayoutPanel2, 0, 5);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(399, 3);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 6;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(480, 497);
            this.tableLayoutPanel5.TabIndex = 8;
            // 
            // FormConnected
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(882, 503);
            this.Controls.Add(this.tableLayoutPanel4);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(900, 550);
            this.Name = "FormConnected";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "C# Chat App - My Home";
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TreeView treeViewTopics;
        private System.Windows.Forms.TreeView treeViewChats;
        private System.Windows.Forms.Label labelChats;
        private System.Windows.Forms.Label labelTopics;
        private System.Windows.Forms.Label labelDescription;
        private System.Windows.Forms.Label labelCreator;
        private System.Windows.Forms.TreeView treeViewMessages;
        private System.Windows.Forms.Label labelMembers;
        private System.Windows.Forms.Label labelMessages;
        private System.Windows.Forms.Label labelDate;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button buttonMember;
        private System.Windows.Forms.Button buttonLeaveChat;
        private System.Windows.Forms.Button buttonNewMessage;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button buttonNewTopic;
        private System.Windows.Forms.Button buttonNewChat;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Label labelName;
    }
}