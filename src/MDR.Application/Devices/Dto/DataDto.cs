namespace MDR.Application.Devices.Dto
{
    public class Mouse2DataDto
    {
        public double Voltage { get; set; }
        public double Resistance { get; set; }
    }

    public class Mouse2BDataDto
    {
        public double Voltage { get; set; }
        public double Resistance { get; set; }
        public double LeakLocationPercent { get; set; }
    }

    public class ReflectogramDto
    {
        public int SeriesNumber { get; set; }
        public byte[] Bytes { get; set; } = Array.Empty<byte>();
    }

    public class MouseComboDataDto
    {
        public double Voltage { get; set; }
        public double Resistance { get; set; }
        public List<ReflectogramDto> Reflectograms { get; set; } = new();
    }

    public class Mas2DataDto
    {
        public double Temperature { get; set; }
        public double Humidity { get; set; }
    }
}
