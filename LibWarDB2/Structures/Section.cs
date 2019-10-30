namespace LibWarDB2.Structures
{
    public class Section
    {
        /// <summary>
        /// Normal records,
        /// Only when (<see cref="WDC3Header.Flags"/> & 1) == 0
        /// </summary>
        public RecordData[] Records { get; set; }
        /// <summary>
        /// Only when (<see cref="WDC3Header.Flags"/> & 1) == 0
        /// </summary>
        public byte[] StringData { get; set; }
        /// <summary>
        /// Offset map records -- these records have null-terminated strings inlined, and
        /// since they are variable-length, they are pointed to by an array of 6-byte offset+size pairs.
        /// Only when (<see cref="WDC3Header.Flags"/> & 1) != 0
        /// </summary>
        public byte[] VariableRecordData { get; set; }
        public uint[] IdList { get; set; }
        /// <summary>
        /// Only when <see cref="WDC3SectionHeader.CopyTableCount"/> > 0
        /// </summary>
        public CopyTableEntry[] CopyTable { get; set; }
        public OffsetMapEntry[] OffsetMap { get; set; }
        /// <summary>
        /// Only when <see cref="WDC3SectionHeader.RelationshipDataSize"/> > 0
        /// </summary>
        public RelationshipMapping RelationshipMap { get; set; }
        public uint OffsetMapIdList { get; set; }
    }
}
