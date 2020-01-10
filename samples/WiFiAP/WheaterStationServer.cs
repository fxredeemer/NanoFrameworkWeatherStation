using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Threading;

namespace WiFiAP
{
    public class WheaterStationServer
    {
        private HttpListener _listener;
        private Thread _serverThread;

        public void Start()
        {
            if (_listener == null)
            {
                _listener = new HttpListener("http");
                _serverThread = new Thread(RunServer);
                _serverThread.Start();
            }
        }

        public void Stop()
        {
            if (_listener != null)
            {
                _listener.Stop();
            }
        }

        private void RunServer()
        {
            _listener.Start();

            while (_listener.IsListening)
            {
                var context = _listener.GetContext();
                if (context != null)
                {
                    ProcessRequest(context);
                }
            }
            _listener.Close();

            _listener = null;
        }

        private void ProcessRequest(HttpListenerContext context)
        {
            var request = context.Request;
            var response = context.Response;
            string responseString;

            switch (request.HttpMethod)
            {
                case "GET":
                    string[] url = request.RawUrl.Split('?');
                    if (url[0] == "/favicon.ico")
                    {
                        response.ContentType = "image/png";
                        byte[] responseBytes = Resources.GetBytes(Resources.BinaryResources.favicon);
                        OutPutByteResponse(response, responseBytes);
                    }
                    else
                    {
                        response.ContentType = "text/html";
                        responseString = "<!DOCTYPE html><html><head><title>Hello World HTML</title></head><body><h1>Hello World</h1></body></html>";
                        OutPutResponse(response, responseString);
                    }
                    break;

                case "POST":
                    // Pick up POST parameters from Input Stream
                    var hashPars = ParseParamsFromStream(request.InputStream);
                    string ssid = (string)hashPars["ssid"];
                    string password = (string)hashPars["password"];

                    Console.WriteLine($"Wireless parameters SSID:{ssid} PASSWORD:{password}");

                    // Enable the Wireless station interface
                    Wireless80211.Configure(ssid, password);

                    // Disable the Soft AP
                    WirelessAP.Disable();

                    string message = "<p>New settings saved.</p><p>Reboot device to put into normal mode</p>";

                    responseString = CreateMainPage(message);

                    OutPutResponse(response, responseString);
                    break;
            }

            response.Close();
        }

        private static string ReplaceMessage(string page, string message)
        {
            int index = page.IndexOf("{message}");
            if (index >= 0)
            {
                return page.Substring(0, index) + message + page.Substring(index + 9);
            }

            return page;
        }

        private static void OutPutResponse(HttpListenerResponse response, string responseString)
        {
            byte[] responseBytes = System.Text.Encoding.UTF8.GetBytes(responseString);
            OutPutByteResponse(response, System.Text.Encoding.UTF8.GetBytes(responseString));
        }

        private static void OutPutByteResponse(HttpListenerResponse response, byte[] responseBytes)
        {
            response.ContentLength64 = responseBytes.Length;
            response.OutputStream.Write(responseBytes, 0, responseBytes.Length);
        }

        private static Hashtable ParseParamsFromStream(Stream inputStream)
        {
            byte[] buffer = new byte[inputStream.Length];
            inputStream.Read(buffer, 0, (int)inputStream.Length);

            return ParseParams(System.Text.Encoding.UTF8.GetString(buffer, 0, buffer.Length));
        }

        private static Hashtable ParseParams(string rawParams)
        {
            var hash = new Hashtable();

            string[] parPairs = rawParams.Split('&');
            foreach (string pair in parPairs)
            {
                string[] nameValue = pair.Split('=');
                hash.Add(nameValue[0], nameValue[1]);
            }

            return hash;
        }

        private static string CreateMainPage(string message)
        {

            return "<!DOCTYPE html><html><body>" +
                    "<h1>NanoFramework</h1>" +
                    "<form method='POST'>" +
                    "<fieldset><legend>Wireless configuration</legend>" +
                    "Ssid:</br><input type='input' name='ssid' value='' ></br>" +
                    "Password:</br><input type='password' name='password' value='' >" +
                    "<br><br>" +
                    "<input type='submit' value='Save'>" +
                    "</fieldset>" +
                    "<b>" + message + "</b>" +
                    "</form></body></html>";
        }
    }
}
