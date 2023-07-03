namespace AdminUserList
{
    partial class AdminLoginView
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
            btnShowUser = new Button();
            label2 = new Label();
            label1 = new Label();
            tbPassword = new TextBox();
            tbUsername = new TextBox();
            btnRegister = new Button();
            btnLogin = new Button();
            SuspendLayout();
            // 
            // btnShowUser
            // 
            btnShowUser.Location = new Point(113, 238);
            btnShowUser.Name = "btnShowUser";
            btnShowUser.Size = new Size(94, 29);
            btnShowUser.TabIndex = 12;
            btnShowUser.Text = "Show User";
            btnShowUser.UseVisualStyleBackColor = true;
            btnShowUser.Visible = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(29, 89);
            label2.Name = "label2";
            label2.Size = new Size(70, 20);
            label2.TabIndex = 10;
            label2.Text = "Password";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(29, 18);
            label1.Name = "label1";
            label1.Size = new Size(82, 20);
            label1.TabIndex = 11;
            label1.Text = "User Name";
            // 
            // tbPassword
            // 
            tbPassword.Location = new Point(29, 117);
            tbPassword.Margin = new Padding(3, 4, 3, 4);
            tbPassword.Name = "tbPassword";
            tbPassword.Size = new Size(294, 27);
            tbPassword.TabIndex = 9;
            // 
            // tbUsername
            // 
            tbUsername.Location = new Point(29, 46);
            tbUsername.Margin = new Padding(3, 4, 3, 4);
            tbUsername.Name = "tbUsername";
            tbUsername.Size = new Size(294, 27);
            tbUsername.TabIndex = 8;
            // 
            // btnRegister
            // 
            btnRegister.Enabled = false;
            btnRegister.Location = new Point(29, 187);
            btnRegister.Margin = new Padding(3, 4, 3, 4);
            btnRegister.Name = "btnRegister";
            btnRegister.Size = new Size(91, 36);
            btnRegister.TabIndex = 7;
            btnRegister.Text = "Register";
            btnRegister.UseVisualStyleBackColor = true;
            // 
            // btnLogin
            // 
            btnLogin.Enabled = false;
            btnLogin.Location = new Point(191, 186);
            btnLogin.Margin = new Padding(3, 4, 3, 4);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(133, 37);
            btnLogin.TabIndex = 6;
            btnLogin.Text = "Login";
            btnLogin.UseVisualStyleBackColor = true;
            // 
            // AdminLoginView
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(378, 311);
            Controls.Add(btnShowUser);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(tbPassword);
            Controls.Add(tbUsername);
            Controls.Add(btnRegister);
            Controls.Add(btnLogin);
            Name = "AdminLoginView";
            Text = "AdminLoginView";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnShowUser;
        private Label label2;
        private Label label1;
        private TextBox tbPassword;
        private TextBox tbUsername;
        private Button btnRegister;
        private Button btnLogin;
    }
}