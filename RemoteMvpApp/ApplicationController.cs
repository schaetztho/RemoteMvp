using RemoteMvpLib;

namespace RemoteMvpApp
{
    internal class ApplicationController
    {
        // Model 
        private readonly Userlist _users;

        // ActionEndpoint (to be called by the view)
        private readonly IActionEndpoint _actionEndpoint;

        public ApplicationController(IActionEndpoint actionEndpoint)
        {
            // Create new Model
            _users = new Userlist();

            // Link ActionEndpoint to local method
            _actionEndpoint = actionEndpoint;
            _actionEndpoint.OnActionPerformed += EndpointOnActionPerformed;

            // Run ActionEndPoint as async
            RunActionEndPointAsync();
        }

        private void RunActionEndPointAsync()
        {
            var task = new Task(_actionEndpoint.RunActionEndpoint);
            task.Start();
        }

        private void EndpointOnActionPerformed(object? sender, string cmd)
        {
            if (sender is not RemoteActionEndpoint) return;
            
            var parameters = ProcessCmd(cmd);

            var handler = (RemoteActionEndpoint)sender;
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
        
        private void Process_Login(RemoteActionEndpoint handler, string username, string password)
        {
            switch (_users.LoginUser(username, password))
            {
                case UserListActionResult.AccessGranted:
                    handler.PerformActionResponse(handler.Handler, "action=login;msg=Access granted.");
                    break;
                case UserListActionResult.UserOkPasswordWrong:
                    handler.PerformActionResponse(handler.Handler, "action=error;msg=Wrong password."); 
                    break;
                case UserListActionResult.UserNotExisting:
                    handler.PerformActionResponse(handler.Handler, "action=error;msg=User not existing.");
                    break;
                default:
                    handler.PerformActionResponse(handler.Handler, "action=error;msg=Unsupported action.");
                    break;
            }
        }

        private void Process_Register(RemoteActionEndpoint handler, string username, string password)
        {
            switch (_users.RegisterUser(username, password))
            {
                case UserListActionResult.UserAlreadyExists:
                    Console.WriteLine("Error registering: User already existing.");
                    handler.PerformActionResponse(handler.Handler, "action=error;msg=Error! User is already existing.");
                    break;
                case UserListActionResult.RegistrationOk:
                    Console.WriteLine("User registration OK.");
                    handler.PerformActionResponse(handler.Handler, "action=register;msg=Successful. You can now login.");
                    break;
                default:
                    Console.WriteLine("Unknown action.");
                    handler.PerformActionResponse(handler.Handler, "action=error;msg=Unsupported operation.");
                    break;
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
