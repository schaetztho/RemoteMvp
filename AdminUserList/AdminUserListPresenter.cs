using RemoteMvpClient;
using RemoteMvpLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;



namespace AdminUserList
{
    public class AdminUserListPresenter
    {
        private readonly IActionAdapter _adapter;
        private UserListView _userListView;



        public AdminUserListPresenter(IActionAdapter adapter)
        {
            _adapter = adapter;
            _userListView = new UserListView();
;


            _userListView.UserDeleteRequested += OnUserDeleteRequested;
            _userListView.ShowUserRequested += OnShowUserRequested;
        }

        private async void OnShowUserRequested(object? sender, EventArgs e)
        {
            RemoteActionRequest AdminRequest = new RemoteActionRequest(ActionType.ShowUser, "admin", "admin",UserType.Admin);
            await ProcessRequest(AdminRequest);
        }

        private async void OnUserDeleteRequested(object? sender, string e)
        {
            RemoteActionRequest deleteRequest = new RemoteActionRequest(ActionType.Delete, e, null, 0);
            await ProcessRequest(deleteRequest);
        }

        public void OpenUI(bool isModal)
        {
            if (isModal)
            {
                _userListView.ShowDialog();
            }
            else
            {
                _userListView.Show();
            }

        }

        private async Task ProcessRequest(RemoteActionRequest request)
        {
            // Execute action in actionlistener and wait for result asynchronously
            RemoteActionResponse response = await _adapter.PerformActionAsync(request);

            // Process result

            switch (response.Type)
            {
                case ResponseType.Error:
                    _userListView.ShowErrorMessage(response.Message);
                    break;
                case ResponseType.Success:
                    switch (request.Type)
                    {
                        case ActionType.Delete:
                            _userListView.DeleteOk(response.Message);
                            break;
                        case ActionType.ShowUser:
                            _userListView.ShowUserOK(response.Message);
                            break;
                    }
                    break;
            }
        }    
                        
                  
}
}
