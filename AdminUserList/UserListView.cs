namespace AdminUserList
{
    public partial class UserListView : Form
    {
        public UserListView()
        {
            InitializeComponent();
            listViewUserList.Columns.Add("UserName");
            listViewUserList.Columns.Add("Password");
        }

        private void UserListView_Load(object sender, EventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

        private void listViewUserList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}