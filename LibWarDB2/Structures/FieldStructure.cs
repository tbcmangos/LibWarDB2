namespace LibWarDB2.Structures
{
    public class FieldStructure
    {
        /// <summary>
        /// Size in bits as calculated by: byteSize = (32 - size) / 8; this value can be negative to indicate field sizes larger than 32-bits
        /// </summary>
        public short Size { get; set; }
        /// <summary>
        /// Position of the field within the record, relative to the start of the record
        /// </summary>
        public ushort Position { get; internal set; }
    }
}
