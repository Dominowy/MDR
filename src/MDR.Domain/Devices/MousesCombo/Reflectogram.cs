namespace MDR.Domain.Devices.MousesCombo
{
    public class Reflectogram : BaseEntity
    {
        public int SeriesNumber { get; set; }
        public byte[] Data { get; set; }

        public Guid MouseComboDataId { get; set; }
        public virtual MouseComboData MouseComboData { get; set; }
    }
}
