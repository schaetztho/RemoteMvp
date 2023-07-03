﻿using RemoteMvpLib;

namespace RemoteMvpApp
{
    public class ApplicationController
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
            _users.UserListChanged += OnUserListChanged;
        }

        private void OnUserListChanged(object? sender, EventArgs e)
        {
            _users.WriteUserListInCSV();
        }

        public void RunActionEndPoint() => _actionEndpoint.RunActionEndpoint();


        public Task RunActionEndPointAsync()
        {
            var task = new Task(_actionEndpoint.RunActionEndpoint);
            task.Start();
            return task;
        }

        private void EndpointOnActionPerformed(object? sender, RemoteActionRequest request)
        {
            if (sender is not RemoteActionEndpoint) return;

            var handler = (RemoteActionEndpoint)sender;
            switch (request.Type)
            {
                case ActionType.Login:
                    Process_Login(handler, request.UserName, request.Password);
                    break;

                case ActionType.Register:
                    Process_Register(handler, request.UserName, request.Password);
                    break;

                case ActionType.Delete:
                     Process_Delete(handler, request.UserName, request.Password);
                    break;

                case ActionType.ShowUser:
                     Process_ShowUser(handler, request.UserName, request.Password);
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
                    handler.PerformActionResponse(handler.Handler, new RemoteActionResponse(ResponseType.Success, $"Access granted for {username}."));
                    break;
                case UserListActionResult.UserOkPasswordWrong:
                    handler.PerformActionResponse(handler.Handler, new RemoteActionResponse(ResponseType.Error, "Wrong password.")); 
                    break;
                case UserListActionResult.UserNotExisting:
                    handler.PerformActionResponse(handler.Handler, new RemoteActionResponse(ResponseType.Error, $"User {username} not existing."));
                    break;
                default:
                    handler.PerformActionResponse(handler.Handler, new RemoteActionResponse(ResponseType.Error, "Unsupported action."));
                    break;
            }
        }

        private void Process_Register(RemoteActionEndpoint handler, string username, string password)
        {
            switch (_users.RegisterUser(username, password))
            {
                case UserListActionResult.UserAlreadyExists:
                    Console.WriteLine("Error registering: User already existing.");
                    handler.PerformActionResponse(handler.Handler, new RemoteActionResponse(ResponseType.Error, $"Error! User {username} is already existing."));
                    break;
                case UserListActionResult.RegistrationOk:
                    Console.WriteLine("User registration OK.");
                    handler.PerformActionResponse(handler.Handler, new RemoteActionResponse(ResponseType.Success, $"Registration successful for {username}. You can now login."));
                    break;
                default:
                    Console.WriteLine("Unknown action.");
                    handler.PerformActionResponse(handler.Handler, new RemoteActionResponse(ResponseType.Error, "Unsupported operation."));
                    break;
            }
        }

        private void Process_Delete(RemoteActionEndpoint handler, string username, string password)
        {
            switch (_users.RemoveUser(username))
            {
                case UserListActionResult.UserDeleteOK:
                    Console.WriteLine( "User Deleted" );
                    handler.PerformActionResponse(handler.Handler, new RemoteActionResponse(ResponseType.Success, $"User: {username} is deleted."));
                    break;
                case UserListActionResult.UserNotExisting:
                    Console.WriteLine( "User cant be deleted!" );
                    handler.PerformActionResponse(handler.Handler, new RemoteActionResponse(ResponseType.Error, "Cant delete User!"));
                    break;
            }
        }
        private void Process_ShowUser(RemoteActionEndpoint handler, string username, string password)
        {
            switch (_users.UserlistToString(out string responseString))
            {
                case UserListActionResult.ShowUserOK:
                    Console.WriteLine("Show User prepeared");
                    handler.PerformActionResponse(handler.Handler, new RemoteActionResponse(ResponseType.Success, responseString));
                    break;
                case UserListActionResult.NoUserExisting:
                    Console.WriteLine("No User in  Userlist, can not show Userlist");
                    handler.PerformActionResponse(handler.Handler, new RemoteActionResponse(ResponseType.Error, "No User in  Userlist, can not show Userlist"));
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
