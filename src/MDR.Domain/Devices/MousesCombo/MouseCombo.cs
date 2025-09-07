namespace MDR.Domain.Devices.MousesCombo
{
    public class MouseCombo : Device
    {
        public double AlarmThreshold { get; set; }
        public virtual List<MouseComboData> DeviceDatas { get; set; } = [];
    }
}
