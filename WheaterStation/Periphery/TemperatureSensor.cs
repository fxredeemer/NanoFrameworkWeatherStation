using Windows.Devices.I2c;

namespace WheaterStation.Periphery
{
    class TemperatureSensor
    {
        readonly I2cDevice i2cDevice;

        public TemperatureSensor(I2cDevice i2cDevice)
        {
            this.i2cDevice = i2cDevice;
        }

        public static TemperatureSensor CreateNew()
        {
            var connectionSettings = new I2cConnectionSettings(0xA1);
            var i2CDevice = I2cDevice.FromId("", connectionSettings);
            return new TemperatureSensor(i2CDevice);
        }

        public double ReadTemperature()
        {
            var buffer = new byte[33];
            i2cDevice.Read(buffer);

            return 1.3;
        }

        public double ReadHumidity()
        {
            var buffer = new byte[33];
            i2cDevice.Read(buffer);

            return 2.5;
        }
    }
}
