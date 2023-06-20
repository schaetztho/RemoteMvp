using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteMvpApp
{
    public enum UserListActionResult
    {
        UserNotExisting,
        UserAlreadyExists,
        UserOkPasswordWrong,
        AccessGranted,
        RegistrationOk
    }

    internal class Userlist
    {
        private record User(string UserName, string Password);
        private readonly List<User> _users;

        //Event
        public event EventHandler UserListChanged;

        public Userlist()
        {
            _users = new List<User>();
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
            UserListChanged?.Invoke(this,EventArgs.Empty);

            return UserListActionResult.RegistrationOk;
    
        }

        public void RemoveUser(string username)
        {
            _users.RemoveAll(user => user.UserName.Equals(username));

            // Fire event
            UserListChanged?.Invoke(this, EventArgs.Empty);

        }

        public void RemoveAllUsers()
        {
            _users.Clear();

            // Fire event
            UserListChanged?.Invoke(this, EventArgs.Empty);
        }
        public void WriteUserListInCSV()
        {
                  //mit private member filepath und dialogview evtl
        }
    }
}
