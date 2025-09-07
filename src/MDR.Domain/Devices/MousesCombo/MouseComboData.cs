namespace MDR.Domain.Devices.MousesCombo
{
    public class MouseComboData : DeviceData
    {
        public double Voltage { get; set; }
        public double Resistance { get; set; }

        public virtual List<Reflectogram> Reflectograms { get; set; } = [];

        public Guid DeviceId { get; set; }
        public virtual MouseCombo Device { get; set; }
    }
}
