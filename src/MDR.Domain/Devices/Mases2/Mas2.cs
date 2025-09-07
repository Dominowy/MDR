namespace MDR.Domain.Devices.Mases2
{
    public class Mas2 : Device
    {
        public double TempAlarmThreshold { get; set; }
        public double HumidityAlarmThreshold { get; set; }

        public virtual List<Mas2Data> DeviceDatas { get; set; } = [];
    }
}
