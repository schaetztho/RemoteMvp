using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RemoteMvpApp
{
    public enum UserListActionResult
    {
        NoUserExisting,
        ShowUserOK,
        UserNotExisting,
        UserAlreadyExists,
        UserOkPasswordWrong,
        AccessGranted,
        RegistrationOk,
        UserDeleteOK
    }

    internal class Userlist
    {
        private record User(string UserName, string Password);
        private readonly List<User> _users;

        private string _filepath;

        public string FilePath => _filepath;
       

        //Event
        public event EventHandler UserListChanged;

        public Userlist()
        {
            _users = new List<User>();
            _filepath = "testCSVFilepath.csv";
        }

        public UserListActionResult LoginUser(string username, string password)
        {
            foreach (var user in _users.Where(user => user.UserName.Equals(username)))
            {
                if (user.Password.Equals(password))
                {
                    return UserListActionResult.AccessGranted;
                }
                else
                {
                    return UserListActionResult.UserOkPasswordWrong;
                }
            }

            return UserListActionResult.UserNotExisting;
        }

        public UserListActionResult RegisterUser(string username, string password)
        {
            if (_users.Any(user => user.UserName.Equals(username)))
            {
                return UserListActionResult.UserAlreadyExists;
            }

            User newUser = new(username, password);
            _users.Add(newUser);
            // Fire event
            UserListChanged?.Invoke(this, EventArgs.Empty);

            return UserListActionResult.RegistrationOk;

        }

        public UserListActionResult RemoveUser(string username)
        { 

            if (_users.Any(user => user.UserName.Equals(username)))
            {
                _users.RemoveAll(user => user.UserName.Equals(username));

                // Fire event
                UserListChanged?.Invoke(this, EventArgs.Empty);

                return UserListActionResult.UserDeleteOK;
            }
            return UserListActionResult.UserNotExisting;
        }

        public void RemoveAllUsers()
        {
            _users.Clear();

            // Fire event
            UserListChanged?.Invoke(this, EventArgs.Empty);
        }
        public void WriteUserListInCSV()
        {
           //if(_filepath == null) { GetFilepath(); }

            try
            {
                using (var writer = new StreamWriter(_filepath))
                {
                    foreach (var item in _users)
                    {
                        string line = item.UserName + ";" + item.Password;
                        writer.WriteLine(line);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void GetFilepath()
        {
            string filepath = null;
            // open dialog 
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.ShowDialog();

            // check if filename is not empty
            if (saveFileDialog1.FileName == string.Empty)
            {
                MessageBox.Show("ERROR");
            }
            else
            {
                filepath = saveFileDialog1.FileName;
            }

            _filepath = filepath;
        }
        public UserListActionResult UserlistToString(out string responseString)
        {
            responseString = "";

            foreach (var item in _users)
            {
                responseString += item.UserName + ";" + item.Password + ";";
            }

            if(responseString=="")
            {
                return UserListActionResult.NoUserExisting;
            }
            return UserListActionResult.ShowUserOK;
        }
    }
}
