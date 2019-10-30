namespace LibWarDB2.Structures
{
    public class RelationshipMapping
    {
        public uint NumEntries { get; set; }
        public uint MinId { get; set; }
        public uint MaxId { get; set; }
        RelationshipEntry[] Entries { get; set; }
    }
}
