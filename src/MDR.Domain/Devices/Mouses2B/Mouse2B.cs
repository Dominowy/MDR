namespace MDR.Domain.Devices.Mouses2B
{
    public class Mouse2B : Device
    {
        public double AlarmThreshold { get; set; }
        public double CableLength { get; set; }

        public virtual List<Mouse2BData> DeviceDatas { get; set; } = [];

    }
}
