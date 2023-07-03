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
        public void ShowUserOK(string text)
        {
            listViewUserList.Show();
            UpdateView(text);
            MessageBox.Show(text, "ShowUser", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
        public void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public void UpdateView(string inputString)
        {
            // Update listview with new content
            listViewUserList.Items.Clear();

            string[] parts = inputString.Split(';');

            for (int i = 0; i < parts.Length-1; i++)
            {
                // Create a string Array
                string[] userArray = new string[2];

                // Fill array with the items of the article
                userArray[i] = parts[i];
                userArray[i+1] = parts[i+1];

                // Give listView the array to show it
                listViewUserList.Items.Add(new ListViewItem(userArray));
            }
 
        }
    }
}