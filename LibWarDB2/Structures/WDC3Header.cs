namespace LibWarDB2.Structures
{
    public class WDC3Header
    {
        /// <summary>
        /// WDC3
        /// </summary>
        public uint Magic { get; set; }
        /// <summary>
        /// This is for all sections combined now
        /// </summary>
        public uint RecordCount { get; set; }
        public uint FieldCount { get; set; }
        public uint RecordSize { get; set; }
        /// <summary>
        /// This is for all sections combined now
        /// </summary>
        public uint StringTableSize { get; set; }
        /// <summary>
        /// Hash of the table name
        /// </summary>
        public uint TableHash { get; set; }
        /// <summary>
        /// Hash field that changes only when the structure of the data changes
        /// </summary>
        public uint LayoutHash { get; set; }
        public uint MinId { get; set; }
        public uint MaxId { get; set; }
        /// <summary>
        /// As seen in TextWowEnum
        /// </summary>
        public uint Locale { get; set; }
        /// <summary>
        /// Possible values are listed in Known Flag Meanings
        /// </summary>
        public WDB4Flags Flags { get; set; }
        /// <summary>
        /// Index of the field containing ID values; this is ignored if flags & 0x04 != 0
        /// </summary>
        public ushort IdIndex { get; set; }
        /// <summary>
        /// From WDC1 onwards, this value seems to always be the same as the 'field_count' value
        /// </summary>
        public uint TotalFieldCount { get; set; }
        /// <summary>
        /// Relative position in record where bitpacked data begins; not important for parsing the file
        /// </summary>
        public uint BitpackedDataOffset { get; internal set; }
        public uint LookupColumnCount { get; set; }
        public uint FieldStorageInfoSize { get; set; }
        public uint CommonDataSize { get; set; }
        public uint PalletDataSize { get; set; }
        /// <summary>
        /// New to WDC2, this is number of sections of data
        /// </summary>
        public uint SectionCount { get; set; }
    }
}
