using System;
using System.Threading;
using nanoFramework.Hardware.Esp32;

namespace WheaterStation
{
    public class Program
    {
        public static ManualResetEvent _touchEvent;

        public static void Main()
        {
            var i2cData = Configuration.GetFunctionPin(DeviceFunction.I2C1_DATA);
            var i2cClock = Configuration.GetFunctionPin(DeviceFunction.I2C1_CLOCK);

            Configuration.SetPinFunction(i2cData, DeviceFunction.I2C1_DATA);
            Configuration.SetPinFunction(i2cClock, DeviceFunction.I2C1_CLOCK);

            Sleep.EnableWakeupByTimer(TimeSpan.FromSeconds(5));

            using (var timer = new Timer(TimerTick, null, 5000, 5000))
            {
                Thread.Sleep(Timeout.Infinite);
            }
        }

        private static void TimerTick(object state)
        {

        }
    }
}
