//using System;
//using System.Text;
//using Windows.Devices.WiFi;

//namespace WheaterStation.Connectivity
//{
//    internal class WiFi
//    {
//        private readonly WiFiAdapter wiFiAdapter;

//        public WiFi()
//        {
//            wiFiAdapter = WiFiAdapter.FindAllAdapters()[0];
//        }

//        public void StartSniffing()
//        {
//            wiFiAdapter.AvailableNetworksChanged += AvailableNetworksChanged;
//        }

//        public void StopSniffing()
//        {
//            wiFiAdapter.AvailableNetworksChanged -= AvailableNetworksChanged;
//        }

//        private void AvailableNetworksChanged(WiFiAdapter sender, object e)
//        {
//            Console.WriteLine("Wifi_AvailableNetworksChanged - get report");

//            // Get Report of all scanned WiFi networks
//            WiFiNetworkReport report = sender.NetworkReport;

//            // Enumerate though networks looking for our network
//            foreach (WiFiAvailableNetwork net in report.AvailableNetworks)
//            {
//                // Show all networks found
//                Console.WriteLine($"Net SSID :{net.Ssid},  BSSID : {net.Bsid},  rssi : {net.NetworkRssiInDecibelMilliwatts.ToString()},  signal : {net.SignalBars.ToString()}");
//            }
//        }
//    }
//}
