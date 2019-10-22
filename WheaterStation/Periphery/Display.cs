using System.Text;
using Windows.Devices.I2c;

namespace WheaterStation.Periphery
{
    internal class Display
    {
        private readonly I2cDevice i2CDevice;

        public Display(I2cDevice i2CDevice)
        {
            this.i2CDevice = i2CDevice;
        }

        public static Display CreateNew()
        {
            var connectionSettings = new I2cConnectionSettings(0x17);
            var i2CDevice = I2cDevice.FromId("", connectionSettings);
            return new Display(i2CDevice);
        }

        public void WriteLine(int line, string value)
        {
            var a = Encoding.UTF8.GetBytes(value);

            i2CDevice.Write(a);

        }
    }
}
