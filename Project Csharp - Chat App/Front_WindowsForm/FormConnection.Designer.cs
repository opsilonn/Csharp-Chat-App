using System;


namespace Front_WindowsForm
{
    partial class FormConnection
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


        private void buttonLogin_Click(object sender, EventArgs e)
        {
            Login();
        }
        private void buttonSignIn_Click(object sender, EventArgs e)
        {
            SignIn();
        }


        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormConnection));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelLoginError = new System.Windows.Forms.Label();
            this.buttonLogin = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxLoginPassword = new System.Windows.Forms.TextBox();
            this.textBoxLoginUsername = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxSignInPasswordVerif = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxSignInEmail = new System.Windows.Forms.TextBox();
            this.labelSignInError = new System.Windows.Forms.Label();
            this.buttonSignIn = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxSignInPassword = new System.Windows.Forms.TextBox();
            this.textBoxSignInUsername = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 3, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(832, 553);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelLoginError);
            this.groupBox1.Controls.Add(this.buttonLogin);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBoxLoginPassword);
            this.groupBox1.Controls.Add(this.textBoxLoginUsername);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(127, 113);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(202, 325);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Login";
            // 
            // labelLoginError
            // 
            this.labelLoginError.AutoSize = true;
            this.labelLoginError.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelLoginError.ForeColor = System.Drawing.Color.DarkRed;
            this.labelLoginError.Location = new System.Drawing.Point(3, 18);
            this.labelLoginError.Name = "labelLoginError";
            this.labelLoginError.Size = new System.Drawing.Size(90, 17);
            this.labelLoginError.TabIndex = 5;
            this.labelLoginError.Text = "Error ! Error !";
            this.labelLoginError.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonLogin
            // 
            this.buttonLogin.Location = new System.Drawing.Point(25, 278);
            this.buttonLogin.Name = "buttonLogin";
            this.buttonLogin.Size = new System.Drawing.Size(144, 32);
            this.buttonLogin.TabIndex = 4;
            this.buttonLogin.Text = "Login";
            this.buttonLogin.UseVisualStyleBackColor = true;
            this.buttonLogin.Click += new System.EventHandler(this.buttonLogin_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 118);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Password";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Username";
            // 
            // textBoxLoginPassword
            // 
            this.textBoxLoginPassword.Location = new System.Drawing.Point(25, 138);
            this.textBoxLoginPassword.Name = "textBoxLoginPassword";
            this.textBoxLoginPassword.PasswordChar = '*';
            this.textBoxLoginPassword.Size = new System.Drawing.Size(144, 22);
            this.textBoxLoginPassword.TabIndex = 1;
            // 
            // textBoxLoginUsername
            // 
            this.textBoxLoginUsername.Location = new System.Drawing.Point(25, 78);
            this.textBoxLoginUsername.Name = "textBoxLoginUsername";
            this.textBoxLoginUsername.Size = new System.Drawing.Size(144, 22);
            this.textBoxLoginUsername.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.textBoxSignInPasswordVerif);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.textBoxSignInEmail);
            this.groupBox2.Controls.Add(this.labelSignInError);
            this.groupBox2.Controls.Add(this.buttonSignIn);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.textBoxSignInPassword);
            this.groupBox2.Controls.Add(this.textBoxSignInUsername);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(501, 113);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(202, 325);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "SignIn";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(33, 160);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(152, 17);
            this.label6.TabIndex = 15;
            this.label6.Text = "Password - Verification";
            // 
            // textBoxSignInPasswordVerif
            // 
            this.textBoxSignInPasswordVerif.Location = new System.Drawing.Point(36, 180);
            this.textBoxSignInPasswordVerif.Name = "textBoxSignInPasswordVerif";
            this.textBoxSignInPasswordVerif.PasswordChar = '*';
            this.textBoxSignInPasswordVerif.Size = new System.Drawing.Size(144, 22);
            this.textBoxSignInPasswordVerif.TabIndex = 14;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(33, 209);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 17);
            this.label3.TabIndex = 13;
            this.label3.Text = "Email";
            // 
            // textBoxSignInEmail
            // 
            this.textBoxSignInEmail.Location = new System.Drawing.Point(36, 229);
            this.textBoxSignInEmail.Name = "textBoxSignInEmail";
            this.textBoxSignInEmail.Size = new System.Drawing.Size(144, 22);
            this.textBoxSignInEmail.TabIndex = 12;
            // 
            // labelSignInError
            // 
            this.labelSignInError.AutoSize = true;
            this.labelSignInError.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelSignInError.ForeColor = System.Drawing.Color.DarkRed;
            this.labelSignInError.Location = new System.Drawing.Point(3, 18);
            this.labelSignInError.Name = "labelSignInError";
            this.labelSignInError.Size = new System.Drawing.Size(90, 17);
            this.labelSignInError.TabIndex = 11;
            this.labelSignInError.Text = "Error ! Error !";
            this.labelSignInError.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonSignIn
            // 
            this.buttonSignIn.Location = new System.Drawing.Point(36, 278);
            this.buttonSignIn.Name = "buttonSignIn";
            this.buttonSignIn.Size = new System.Drawing.Size(144, 32);
            this.buttonSignIn.TabIndex = 10;
            this.buttonSignIn.Text = "SignIn";
            this.buttonSignIn.UseVisualStyleBackColor = true;
            this.buttonSignIn.Click += new System.EventHandler(this.buttonSignIn_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(33, 109);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 17);
            this.label4.TabIndex = 9;
            this.label4.Text = "Password";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(33, 49);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 17);
            this.label5.TabIndex = 8;
            this.label5.Text = "Username";
            // 
            // textBoxSignInPassword
            // 
            this.textBoxSignInPassword.Location = new System.Drawing.Point(36, 129);
            this.textBoxSignInPassword.Name = "textBoxSignInPassword";
            this.textBoxSignInPassword.PasswordChar = '*';
            this.textBoxSignInPassword.Size = new System.Drawing.Size(144, 22);
            this.textBoxSignInPassword.TabIndex = 7;
            // 
            // textBoxSignInUsername
            // 
            this.textBoxSignInUsername.Location = new System.Drawing.Point(36, 69);
            this.textBoxSignInUsername.Name = "textBoxSignInUsername";
            this.textBoxSignInUsername.Size = new System.Drawing.Size(144, 22);
            this.textBoxSignInUsername.TabIndex = 6;
            // 
            // FormConnection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(832, 553);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(850, 600);
            this.Name = "FormConnection";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "C# Chat App - Connection";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxLoginPassword;
        private System.Windows.Forms.TextBox textBoxLoginUsername;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonLogin;
        private System.Windows.Forms.Label labelLoginError;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxSignInEmail;
        private System.Windows.Forms.Label labelSignInError;
        private System.Windows.Forms.Button buttonSignIn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxSignInPassword;
        private System.Windows.Forms.TextBox textBoxSignInUsername;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxSignInPasswordVerif;
    }
}