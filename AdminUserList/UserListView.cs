using System.Windows.Forms;

namespace AdminUserList
{
    public partial class UserListView : Form
    {
        private List<Tuple<string, string>> _userForListBox = new List<Tuple<string, string>>();


        public event EventHandler<string> UserDeleteRequested;
        public event EventHandler ShowUserRequested;


        public UserListView()
        {
            InitializeComponent();
            listViewUserList.Columns.Add("UserName");;
        }

        private void btnShowUser_Click(object sender, EventArgs e)
        {
            ShowUserRequested?.Invoke(this, EventArgs.Empty);
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
        public void ShowUserOK(string message)
        {
            MessageBox.Show("Successfully Connected as Admin", "Info", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

            _userForListBox = ConvertData(message);

            UpdateView(message);
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

            for (int i = 0; i < parts.Length - 1; i++)
            {
                // Create a string Array
                string[] userArray = new string[2];

                // Fill array with the items of the article
                userArray[i] = parts[i];
                userArray[i + 1] = parts[i + 1];

                // Give listView the array to show it
                listViewUserList.Items.Add(new ListViewItem(userArray));
            }

        }

       

        private List<Tuple<string, string>> ConvertData(string userListString)
        {
            List<Tuple<string, string>> userConverted = new List<Tuple<string, string>>();

            string[] lines = userListString.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string line in lines)
            {

                string[] parts = line.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length == 2)
                {
                    //string name = parts[0].Substring(parts[0].IndexOf(":") + 2);
                    //string password = parts[1].Substring(parts[1].IndexOf(":") + 2);
                    string name = parts[0] + "\t";
                    string password = parts[1];

                    userConverted.Add(Tuple.Create(name, password));
                }
                else if (parts.Length == 1)
                {
                    string name = parts[0].Substring(parts[0].IndexOf(":") + 2);
                    userConverted.Add(Tuple.Create(name, "Missing!"));


                }
                else
                {
                    userConverted.Add(Tuple.Create("Error", "Missing!"));

                }
            }

            return userConverted;
        }
    }
}