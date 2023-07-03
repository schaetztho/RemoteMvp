namespace RemoteMvpClient
{
    public partial class ClientView : Form
    {

        public event EventHandler<Tuple<string, string>> LoginRequested;
        public event EventHandler<Tuple<string, string>> RegisterRequested;
        public event EventHandler<Tuple<string, string>> ShowUserRequested;

        public ClientView()
        {
            InitializeComponent();
        }

        private void login_Click(object sender, EventArgs e)
        {
            LoginRequested?.Invoke(sender, new Tuple<string, string>(tbUsername.Text, tbPassword.Text));
        }

        private void register_Click(object sender, EventArgs e)
        {
            RegisterRequested?.Invoke(sender, new Tuple<string, string>(tbUsername.Text, tbPassword.Text));
        }

        private void btnShowUser_Click(object sender, EventArgs e)
        {
            ShowUserRequested?.Invoke(sender, new Tuple<string, string>(tbUsername.Text, tbPassword.Text));
        }


        public void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void LoginOk(string text)
        {
            MessageBox.Show(text, "Login", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
        public void DeleteOk(string text)
        {
            MessageBox.Show(text, "Delte", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        public void ShowUserOK(string text)
        {
            MessageBox.Show(text, "ShowUser", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
        public void RegisterOk(string text)
        {
            MessageBox.Show(text, "Register", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void TbUsernameTextChanged(object sender, EventArgs e)
        {
            if (tbUsername.Text.Length > 0 && tbPassword.Text.Length > 0)
            {
                button1.Enabled = true;
                button2.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
                button2.Enabled = false;
            }
        }

        private void TbPasswordTextChanged(object sender, EventArgs e)
        {
            if (tbUsername.Text.Length > 0 && tbPassword.Text.Length > 0)
            {
                button1.Enabled = true;
                button2.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
                button2.Enabled = false;
            }
        }
        public void ShowUserButtonForAdmin()
        {
            btnShowUser.Visible = true;
        }


    }
}