using System.Net.Mime;
using System.Windows.Forms;
using RemoteMvpApp;
using RemoteMvpClient;
using RemoteMvpLib;


// Server-side classes
var server = new RemoteActionEndpoint("localhost",11000);
var app = new ApplicationController(server);
            
// Client-side classes
var client = new RemoteActionAdapter("localhost", 11000);
var clientController = new ClientController(client);
clientController.OpenUI(true);


