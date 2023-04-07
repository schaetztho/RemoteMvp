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

        public Userlist()
        {
            _users = new List<User>();
        }

        public UserListActionResult LoginUser(string username, string password)
        {
            foreach (var user in _users)
            {
                if (user.UserName.Equals(username))
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
            }

            return UserListActionResult.UserNotExisting;
        }

        public UserListActionResult RegisterUser(string username, string password)
        {
            User newUser = new(username, password);
            foreach (var user in _users)
            {
                if (user.UserName.Equals(username))
                {
                    return UserListActionResult.UserAlreadyExists;
                }
            }

            _users.Add(newUser);
            return UserListActionResult.RegistrationOk;
        }

        public void RemoveUser(string UserName)
        {
            // TODO
        }

    }
}
