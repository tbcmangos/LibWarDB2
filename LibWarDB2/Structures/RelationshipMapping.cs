namespace LibWarDB2.Structures
{
    public class RelationshipMapping
    {
        public uint NumEntries { get; set; }
        public uint MinId { get; set; }
        public uint MaxId { get; set; }
        public RelationshipEntry[] Entries { get; set; }
    }
}
