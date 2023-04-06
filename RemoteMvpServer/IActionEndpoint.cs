using System.Net.Sockets;

namespace RemoteMvpLib
{
    public interface IActionEndpoint
    {
        event EventHandler<string> OnActionPerformed;
        void RunActionEndpoint();
        void PerformActionResponse(Socket handler, string response);
    }
}