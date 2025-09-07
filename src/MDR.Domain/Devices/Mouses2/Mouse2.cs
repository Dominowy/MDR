namespace MDR.Domain.Devices.Mouses2
{
    public class Mouse2 : Device
    {
        public double AlarmThreshold { get; set; }
        public virtual List<Mouse2Data> DeviceDatas { get; set; } = [];
    }
}
