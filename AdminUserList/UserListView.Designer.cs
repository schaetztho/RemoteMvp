namespace AdminUserList
{
    partial class UserListView
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            listViewUserList = new ListView();
            btnDelete = new Button();
            btnShowUser = new Button();
            SuspendLayout();
            // 
            // listViewUserList
            // 
            listViewUserList.Location = new Point(11, 11);
            listViewUserList.Margin = new Padding(2);
            listViewUserList.Name = "listViewUserList";
            listViewUserList.Size = new Size(224, 338);
            listViewUserList.TabIndex = 0;
            listViewUserList.UseCompatibleStateImageBehavior = false;
            listViewUserList.View = View.Details;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(239, 154);
            btnDelete.Margin = new Padding(2);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(90, 27);
            btnDelete.TabIndex = 1;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnShowUser
            // 
            btnShowUser.Location = new Point(240, 11);
            btnShowUser.Name = "btnShowUser";
            btnShowUser.Size = new Size(94, 52);
            btnShowUser.TabIndex = 2;
            btnShowUser.Text = "Show User";
            btnShowUser.UseVisualStyleBackColor = true;
            btnShowUser.Click += btnShowUser_Click;
            // 
            // UserListView
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(350, 360);
            Controls.Add(btnShowUser);
            Controls.Add(btnDelete);
            Controls.Add(listViewUserList);
            Margin = new Padding(2);
            Name = "UserListView";
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion

        private ListView listViewUserList;
        private Button btnDelete;
        private Button btnShowUser;
    }
}