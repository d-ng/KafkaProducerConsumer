using System;

namespace Kafka
{
    class MeterItem
    {
        public string MeterName { get; set; }

        public string Manufacturer { get; set; }

        public string ManufacturerModel { get; set; }

        public string Substation { get; set; }

        public string Transformer { get; set; }

        public DateTime Timestamp { get; set; }

        public Double Average_voltage { get; set; }

        public Double Delivered_kVARh { get; set; }

        public Double Delivered_kWh { get; set; }

        public Double Received_kWh { get; set; }

        public static MeterItem GenerateRandomMeterItem(int i, DateTime time, int meterNum, Random random)
        {
            MeterItem meterItem = new MeterItem();
            meterItem.MeterName = "meter_" + i;
            meterItem.Manufacturer = Enums.GetRandomEnum<Manufacturers>().ToString();
            meterItem.ManufacturerModel = Enums.GetRandomEnum<Models>().ToString();
            meterItem.Substation = meterNum / 10 == 0 ? "1950" : ((i / (meterNum / 10)) + 1950).ToString(); // 1950 to 1959
            meterItem.Transformer = meterNum / 100 == 0 ? "815970" : ((i / (meterNum / 100)) + 815970).ToString(); // 815900 to 815999
            meterItem.Timestamp = time;

            meterItem.Average_voltage = random.Next(100);
            meterItem.Delivered_kVARh = random.Next(100);
            meterItem.Delivered_kWh = random.Next(100);
            meterItem.Received_kWh = random.Next(100);

            return meterItem;
        }

        public override string ToString()
        {
            return string.Format(
                @"{{""Timestamp"": ""{0}"", ""MeterName"": ""{1}"", ""Average_voltage"": {2}, ""Delivered_kVARh"": {3}, ""Delivered_kWh"": {4}, ""Manufacturer"": ""{5}"", ""ManufacturerModel"": ""{6}"", ""Received_kWh"": {7}, ""Substation"": ""{8}"", ""Transformer"": ""{9}""}}",
                Timestamp.ToString("yyyy-MM-ddTHH:mm:ssZ"), MeterName, Average_voltage, Delivered_kVARh, Delivered_kWh, Manufacturer, ManufacturerModel, Received_kWh, Substation, Transformer);
        }
    }
}
