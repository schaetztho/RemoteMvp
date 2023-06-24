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
            string selectedUser = "";

            // Check which item is selected
            foreach (ListViewItem item in listViewUserList.Items)
            {
                if (item.Selected == true)
                {
                    selectedUser += item.Text.ToString();
                }
            }

            // Fire event
            UserDeleteRequested?.Invoke(this, selectedUser);

        }

    }
}