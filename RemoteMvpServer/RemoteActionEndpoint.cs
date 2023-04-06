using System.Net.Sockets;
using System.Net;
using System.Text;

namespace RemoteMvpLib
{
    public class RemoteActionEndpoint : IActionEndpoint
    {
        public event EventHandler<string> OnActionPerformed;

        private readonly IPAddress _ipAddress;
        private readonly IPEndPoint _localEndPoint;
        private readonly IPHostEntry _host;

        public Socket Handler { get; private set; }

        public RemoteActionEndpoint(int port)
        {
            try
            {
                _host ??= Dns.GetHostEntry("localhost");
                _ipAddress = _host.AddressList[0];
                _localEndPoint = new IPEndPoint(_ipAddress, port);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public RemoteActionEndpoint(string host, int port) : this(port)
        {
            this._host = Dns.GetHostEntry(host);
        }

        public async void RunActionEndpoint()
        {
            // Get Host IP Address that is used to establish a connection
            // In this case, we get one IP address of localhost that is IP : 127.0.0.1
            // If a host has multiple addresses, you will get a list of addresses

            try
            {

                // Create a Socket that will use Tcp protocol
                Socket listener = new Socket(_ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                // A Socket must be associated with an endpoint using the Bind method
                listener.Bind(_localEndPoint);
                // Specify how many requests a Socket can listen before it gives Server busy response.
                // We will listen 10 requests at a time
                listener.Listen(10);
                while (true)
                {
                    Console.WriteLine("Waiting for a connection...");
                    Handler = listener.Accept();

                    // Incoming data from the client.
                    string data = null;

                    byte[] bytes = new byte[1024];

                    Console.WriteLine("Remote client connected! Waiting for data ...");
                    int bytesRec = await Handler.ReceiveAsync(bytes);

                    data = Encoding.ASCII.GetString(bytes, 0, bytesRec);
                    Console.WriteLine("Text received : {0}", data);
                    
                    OnActionPerformed?.Invoke(this, data);

                    if (data.Equals("APPEXIT")) break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine("\n Press any key to continue...");
            Console.ReadKey();
        }

        public void PerformActionResponse(Socket handler, string response)
        {
            byte[] msg = Encoding.ASCII.GetBytes(response);
            handler.Send(msg);
            handler.Shutdown(SocketShutdown.Both);
            handler.Close();
        }
    }
}
