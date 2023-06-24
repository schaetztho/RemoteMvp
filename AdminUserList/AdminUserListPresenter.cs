using RemoteMvpClient;
using RemoteMvpLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using RemoteMvpApp;


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


            _userListView.UserDeleteRequested += OnUserDeleteRequested;

        }


        private async void OnUserDeleteRequested(object? sender, string e)
        {
            RemoteActionRequest loginRequest = new RemoteActionRequest(ActionType.Delete, e, null, 0);
            await ProcessRequest(loginRequest);
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
                    _userListView.DeleteOk(response.Message);
                    break;
            }
        }
    }
}
