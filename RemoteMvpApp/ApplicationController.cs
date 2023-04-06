using RemoteMvpLib;

namespace RemoteMvpApp
{
    internal class ApplicationController
    {
        readonly IActionEndpoint _actionEndpoint;

        private record User(string UserName, string Password);

        private readonly List<User> _users;

        public ApplicationController(IActionEndpoint actionEndpoint) 
        {
            _users = new List<User>();

            _actionEndpoint = actionEndpoint;
            _actionEndpoint.OnActionPerformed += ActionEndpointOnActionPerformed;

            StartServerAsync();
        }

        private void StartServerAsync()
        {
            var task = new Task(_actionEndpoint.RunActionEndpoint);
            task.Start();
        }

        private void ActionEndpointOnActionPerformed(object? sender, string cmd) 
        {
            if (sender is RemoteActionEndpoint)
            {
                var parameters = ProcessCmd(cmd);

                RemoteActionEndpoint handler = (RemoteActionEndpoint)sender;
                switch (parameters["action"])
                {
                    case "login":
                        Process_Login(handler, parameters["user"],parameters["password"]);
                        break;
                    case "register":
                        Process_Register(handler, parameters["user"], parameters["password"]);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("Request not supported");
                }
            }
        }
        
        private void Process_Login(RemoteActionEndpoint handler, string username, string password)
        {

            foreach (var user in _users)
            {
                if (user.UserName.Equals(username))
                {
                    if (user.Password.Equals(password))
                    {
                        handler.PerformActionResponse(handler.Handler, "action=login;msg=Access granted.");
                        return;
                    }
                    else
                    {
                        handler.PerformActionResponse(handler.Handler, "action=error;msg=Wrong password.");
                        return;
                    }
                }
            }

            handler.PerformActionResponse(handler.Handler, "action=error;msg=User not existing.");
        }

        private void Process_Register(RemoteActionEndpoint handler, string username, string password)
        {
            User newUser = new(username, password);

            if (_users.Contains(newUser))
            {
                Console.WriteLine("Error registering: User already existing.");

                handler.PerformActionResponse(handler.Handler, "action=error;msg=Error! User is already existing.");
            }
            else
            {
                _users.Add(newUser);
                Console.WriteLine($"Added new user {newUser.UserName} with password {newUser.Password}");
                
                handler.PerformActionResponse(handler.Handler, "action=register;msg=Successful. You can now login.");
            }
        }

        /// <summary>
        /// Helper method to parse semicolon-separated key=value pairs
        /// </summary>
        /// <param name="cmd">A string semicolon-separated key=value pairs</param>
        /// <returns>A dictionary with key value pairs</returns>
        private Dictionary<string, string> ProcessCmd(string cmd)
        {
            cmd = cmd.TrimEnd(';');

            string[] parts = cmd.Split(new char[] { ';' });

            Dictionary<string, string> keyValuePairs = cmd.Split(';')
                .Select(value => value.Split('='))
                .ToDictionary(pair => pair[0], pair => pair[1]);

            return keyValuePairs;
        }
    }
}
