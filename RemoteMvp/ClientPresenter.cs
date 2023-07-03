using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using RemoteMvpLib;


namespace RemoteMvpClient
{
    public class ClientPresenter
    {
        private readonly ClientView _clientView;
        private readonly IActionAdapter _adapter;


        public ClientPresenter(IActionAdapter adapter)
        {
            _adapter = adapter;
            _clientView = new ClientView();

            _clientView.LoginRequested += OnLoginRequested;
            _clientView.RegisterRequested += OnRegisterRequested;
            _clientView.ShowUserRequested += OnShowUserRequested;
        }

    
        public void OpenUI(bool isModal)
        {
            if (isModal)
            {
                _clientView.ShowDialog();
            }
            else
            {
                _clientView.Show();
            }
            
        }

        private async void OnLoginRequested(object? sender, Tuple<string, string> e)
        {
            //Check if admin or user
            //Admin have to Register with username.Admin
            UserType userType = CheckForAdmin(e);

            RemoteActionRequest loginRequest = new RemoteActionRequest(ActionType.Login, e.Item1, e.Item2,userType);
            await ProcessRequest(loginRequest);

            if(loginRequest.UserType== UserType.Admin)
            {
                _clientView.ShowUserButtonForAdmin();
            }
        }

        private async void OnRegisterRequested(object? sender, Tuple<string, string> e)
        {
            //Check if admin or user
            //Admin have to Register with username.Admin
            UserType userType = CheckForAdmin(e);

            RemoteActionRequest registerRequest = new RemoteActionRequest(ActionType.Register, e.Item1, e.Item2, userType);
            await ProcessRequest(registerRequest);
        }

        private async void OnShowUserRequested(object? sender, Tuple<string, string> e)
        {
            RemoteActionRequest ShowUserRequest = new RemoteActionRequest(ActionType.ShowUser, e.Item1, e.Item2, UserType.Admin);
            await ProcessRequest(ShowUserRequest);
        }

        private UserType CheckForAdmin(Tuple<string, string> e)
        {
            UserType userType;

            if (e.Item1.Contains(".Admin"))
            {
                return userType = UserType.Admin;
            }
            else
            {
                return userType = UserType.User;
            }
        }


        /// <summary>
        /// Collect and process all UI events
        /// </summary>
        /// <param name="sender">Source of event</param>
        /// <param name="request">Property-based request</param>
        private async Task ProcessRequest(RemoteActionRequest request)
        {
            // Execute action in actionlistener and wait for result asynchronously
            RemoteActionResponse response = await _adapter.PerformActionAsync(request);

            // Process result

            switch (response.Type)
            {
                case ResponseType.Error:
                    _clientView.ShowErrorMessage(response.Message);
                    break;
                case ResponseType.Success:
                    switch (request.Type)
                    {
                        case ActionType.Register:
                            _clientView.RegisterOk(response.Message);
                            break;
                        case ActionType.Login:
                            _clientView.LoginOk(response.Message);
                            break;
                        case ActionType.Delete:
                             _clientView.DeleteOk(response.Message);
                            break;
                        case ActionType.ShowUser:
                           _clientView.ShowUserOK(response.Message);

                                break;
                    }
                    break;
            }
        }
    }
}
