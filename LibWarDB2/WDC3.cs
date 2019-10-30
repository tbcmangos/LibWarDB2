using LibWarDB2.Structures;

namespace LibWarDB2
{
    public class WDC3
    {
        public WDC3Header Header { get; set; }
        public WDC3SectionHeader[] SectionHeaders { get; set; }
        public FieldStructure[] Fields { get; set; }
        public FieldStorageInfo[] FieldInfo { get; set; }
        public byte[] PalletData { get; set; }
        public byte[] CommonDate { get; set; }
        public Section[] DataSections { get; set; }
    }
}
