using RemoteMvpClient;
using RemoteMvpLib;

namespace AdminUserList
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new UserListView());


            var client = new RemoteActionAdapter("localhost", 11000);
            var clientController = new AdminUserListPresenter(client);
            clientController.OpenUI(true);
        }
    }
}