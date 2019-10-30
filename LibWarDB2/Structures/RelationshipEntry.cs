namespace LibWarDB2.Structures
{
    public class RelationshipEntry
    {
        /// <summary>
        /// This is the id of the foreign key for the record, e.g. SpellID in SpellX* tables.
        /// </summary>
        public uint ForeignId { get; set; }
        /// <summary>
        /// This is the index of the record in record_data. Note that this is *not* the record's own ID.
        /// </summary>
        public uint RecordIndex { get; set; }
    }
}
