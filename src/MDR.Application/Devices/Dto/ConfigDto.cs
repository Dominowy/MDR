namespace MDR.Application.Devices.Dto
{
    public class Mouse2ConfigDto
    {
        public double AlarmThreshold { get; set; }
    }

    public class Mouse2BConfigDto
    {
        public double AlarmThreshold { get; set; }
        public double WireLength { get; set; }
    }

    public class MouseComboConfigDto
    {
        public double AlarmThreshold { get; set; }
    }

    public class Mas2ConfigDto
    {
        public double TempAlarmThreshold { get; set; }
        public double HumidityAlarmThreshold { get; set; }
    }
}
