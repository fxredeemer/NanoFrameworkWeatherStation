using System;
using System.Threading;
using Windows.Devices.WiFi;

namespace WheaterStation.Connectivity
{
    internal class WiFi
    {
        private readonly WiFiAdapter wiFiAdapter;
        Timer timer;

        public WiFi()
        {
            wiFiAdapter = WiFiAdapter.FindAllAdapters()[0];
            wiFiAdapter.AvailableNetworksChanged += AvailableNetworksChanged;

        }

        public void StartSniffing()
        {
            timer = new Timer(ScanWifi, null, 0, 15000);
        }

        public void StopSniffing()
        {
            timer.Dispose();
        }

        private void ScanWifi(object state)
        {
            Console.WriteLine("scanning wifi");
            wiFiAdapter.ScanAsync();
        }

        private void AvailableNetworksChanged(WiFiAdapter sender, object e)
        {
            Console.WriteLine("Wifi_AvailableNetworksChanged - get report");

            // Get Report of all scanned WiFi networks
            var report = sender.NetworkReport;

            // Enumerate though networks looking for our network
            foreach (var net in report.AvailableNetworks)
            {
                // Show all networks found
                Console.WriteLine($"Net SSID :{net.Ssid},  BSSID : {net.Bsid},  rssi : {net.NetworkRssiInDecibelMilliwatts.ToString()},  signal : {net.SignalBars.ToString()}");
            }
        }
    }
}
