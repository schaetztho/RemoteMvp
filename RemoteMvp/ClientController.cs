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
    public class ClientController
    {
        private readonly ClientView _clientView;
        private readonly IActionAdapter _adapter;

        public ClientController(IActionAdapter adapter)
        {
            _adapter = adapter;
            _clientView = new ClientView();
            _clientView.onUiActionPerformed += ClientViewOnUiActionPerformed;
            
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

        /// <summary>
        /// Collect and process all UI events
        /// </summary>
        /// <param name="sender">Source of event</param>
        /// <param name="request">Property-based request</param>
        private async void ClientViewOnUiActionPerformed(object? sender, string request)
        {
            // Execute action in actionlistener and wait for result asynchronously
            string response = await _adapter.PerformActionAsync(request);

            // Process result
            var parameters = ProcessCmd(response);

            switch (parameters["action"])
            {
                case "error": _clientView.ShowErrorMessage(parameters["msg"]);
                    break;
                case "login": _clientView.LoginOk(parameters["msg"]);
                    break;
                case "register": _clientView.RegisterOk(parameters["msg"]);
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
