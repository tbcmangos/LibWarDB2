namespace LibWarDB2.Structures
{
    /// <summary>
    /// A section = Records + String block + Id list + Copy table + Offset map + Offset map id list + Relationship map
    /// </summary>
    public class WDC3SectionHeader
    {
        /// <summary>
        /// TactKeyLookup hash
        /// </summary>
        public ulong TactKeyHash { get; set; }
        /// <summary>
        /// Absolute position to the beginning of the section
        /// </summary>
        public uint FileOffset { get; set; }
        /// <summary>
        /// 'record_count' for the section
        /// </summary>
        public uint RecordCount { get; set; }
        /// <summary>
        /// 'string_table_size' for the section
        /// </summary>
        public uint StringTableSize { get; set; }
        /// <summary>
        /// Offset to the spot where the records end in a file with an offset map structure;
        /// </summary>
        public uint OffsetRecordsEnd { get; set; }
        /// <summary>
        /// // Size of the list of ids present in the section
        /// </summary>
        public uint IdListSize { get; set; }
        /// <summary>
        /// // Size of the relationship data in the section
        /// </summary>
        public uint RelationshipDataSize { get; set; }
        /// <summary>
        /// // Count of ids present in the offset map in the section
        /// </summary>
        public uint OffsetMapIdCount { get; set; }
        /// <summary>
        /// // Count of the number of deduplication entries (you can multiply by 8 to mimic the old 'copy_table_size' field)
        /// </summary>
        public uint CopyTableCount { get; set; }
}
}
