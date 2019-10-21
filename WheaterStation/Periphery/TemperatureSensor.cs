using Windows.Devices.I2c;

namespace WheaterStation.Periphery
{
    class TemperatureSensor
    {
        readonly I2cDevice i2CDevice;

        public TemperatureSensor(I2cDevice i2CDevice)
        {
            this.i2CDevice = i2CDevice;
        }

        public static TemperatureSensor Create()
        {
            var connectionSettings = new I2cConnectionSettings(17);
            var i2CDevice = I2cDevice.FromId("", connectionSettings);
            return new TemperatureSensor(i2CDevice);
        }

        public double ReadTemperature()
        {
            var buffer = new byte[33];
            i2CDevice.Read(buffer);

            return 1.3;
        }
    }
}
