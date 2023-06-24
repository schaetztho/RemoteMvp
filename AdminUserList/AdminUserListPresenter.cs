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
        private UserListView _userListView;


        public AdminUserListPresenter()
        {
            _userListView = new UserListView();


            _userListView.UserDeleteRequested += OnUserDeleteRequested;

        }


        private void OnUserDeleteRequested(object? sender, string e)
        {
          
        }
    }
}
