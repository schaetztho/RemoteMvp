namespace AdminUserList
{
    public partial class UserListView : Form
    {
        public event EventHandler<string> UserDeleteRequested;


        public UserListView()
        {
            InitializeComponent();
            listViewUserList.Columns.Add("UserName");
            listViewUserList.Columns.Add("Password");
        }


        private void btnDelete_Click(object sender, EventArgs e)
        {
            string selectedUsername = "thomas.Admin";

            // Check which item is selected
            foreach (ListViewItem item in listViewUserList.Items)
            {
                if (item.Selected == true)
                {
                    selectedUsername += item.Text.ToString();
                }
            }

            // Fire event
            UserDeleteRequested?.Invoke(this, selectedUsername);

        }
        public void DeleteOk(string text)
        {
            MessageBox.Show(text, "Delete", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
        public void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}