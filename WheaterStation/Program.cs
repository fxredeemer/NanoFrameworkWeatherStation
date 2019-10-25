using nanoFramework.Hardware.Esp32;
using System;
using System.Threading;
using WheaterStation.Connectivity;
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
                var wifi = new WiFi();
                wifi.StartSniffing();

                Thread.Sleep(Timeout.Infinite);
            }
            catch (Exception ex)
            {
                Console.WriteLine("message:" + ex.Message);
                Console.WriteLine("stack:" + ex.StackTrace);
            }

        }
    }
}
