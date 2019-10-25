using nanoFramework.Hardware.Esp32;
using System;
using System.Threading;
using Windows.Devices.Gpio;
using Windows.Devices.WiFi;

namespace WheaterStation
{
    static class Programm
    {
        public static void Main()
        {
            Console.WriteLine("Starting!");

            var gpioController = new GpioController();
            var pin = gpioController.OpenPin(Gpio.IO13);

            pin.SetDriveMode(GpioPinDriveMode.Output);

            try
            {
                // Get the first WiFI Adapter
                WiFiAdapter wifi = WiFiAdapter.FindAllAdapters()[0];

                // Set up the AvailableNetworksChanged event to pick up when scan has completed
                wifi.AvailableNetworksChanged += Wifi_AvailableNetworksChanged;

                // Loop forever scanning every 30 seconds
                while (true)
                {
                    Console.WriteLine("starting WiFi scan");
                    wifi.ScanAsync();

                    Thread.Sleep(30000);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("message:" + ex.Message);
                Console.WriteLine("stack:" + ex.StackTrace);
            }


            while (true)
            {
                pin.Write(GpioPinValue.High);
                Thread.Sleep(1000);
                pin.Write(GpioPinValue.Low);
                Thread.Sleep(1000);
            }
        }

        private static void Wifi_AvailableNetworksChanged(WiFiAdapter sender, object e)
        {
            Console.WriteLine("Wifi_AvailableNetworksChanged - get report");

            // Get Report of all scanned WiFi networks
            WiFiNetworkReport report = sender.NetworkReport;

            // Enumerate though networks looking for our network
            foreach (WiFiAvailableNetwork net in report.AvailableNetworks)
            {
                // Show all networks found
                Console.WriteLine($"Net SSID :{net.Ssid},  BSSID : {net.Bsid},  rssi : {net.NetworkRssiInDecibelMilliwatts.ToString()},  signal : {net.SignalBars.ToString()}");
            }
        }
    }
}
