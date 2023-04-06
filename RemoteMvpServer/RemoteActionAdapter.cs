using System.Net;
using System.Net.Sockets;
using System.Text;

namespace RemoteMvpLib
{
    public class RemoteActionAdapter : IActionAdapter
    {

        private string _host = "localhost";
        private int _port;

        public RemoteActionAdapter(string host, int port)
        {
            _host = host;
            _port = port;
        }

        public async Task<string> PerformActionAsync(string request)
        {
            var bytes = new byte[1024];

            try
            {
                Console.WriteLine("Performing remote action: " + request);
                // Connect to a Remote server
                // Get Host IP Address that is used to establish a connection
                // In this case, we get one IP address of localhost that is IP : 127.0.0.1
                // If a host has multiple addresses, you will get a list of addresses
                var host = Dns.GetHostEntry(_host);
                var ipAddress = host.AddressList[0];
                var remoteEP = new IPEndPoint(ipAddress, _port);

                // Create a TCP/IP  socket.
                var sender = new Socket(ipAddress.AddressFamily,
                    SocketType.Stream, ProtocolType.Tcp);

                // Connect the socket to the remote endpoint. Catch any errors.
                try
                {
                    // Connect to Remote EndPoint
                    sender.Connect(remoteEP);

                    Console.WriteLine("Client connected to {0}",
                        sender.RemoteEndPoint.ToString());

                    // Encode the data string into a byte array.
                    var msg = Encoding.ASCII.GetBytes(request);

                    // Send the data through the socket asynchronously.
                    var bytesSent = await sender.SendAsync(msg);
                    Console.WriteLine($"{bytesSent} bytes sent to server. Waiting for response ...");

                    // Receive the response from the remote device asynchronously
                    var bytesRec = await sender.ReceiveAsync(bytes);
                    var response = Encoding.ASCII.GetString(bytes, 0, bytesRec);
                    Console.WriteLine($"Received {bytesRec} bytes: {response}");

                    // Release the socket.
                    sender.Shutdown(SocketShutdown.Both);
                    sender.Close();

                    return response;

                }
                catch (ArgumentNullException ane)
                {
                    Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                }
                catch (SocketException se)
                {
                    Console.WriteLine("SocketException : {0}", se.ToString());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Unexpected exception : {0}", e.ToString());
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return string.Empty;
        }

    }
}
