using System;

namespace LibWarDB2.Structures
{
    [Flags]
    public enum WDB4Flags : ushort
    {
        None = 0x00,
        HasOffsetMap = 0x01,
        /// <summary>
        /// This may be 'secondary keys' and is unrelated to WDC1+ relationships
        /// </summary>
        HasRelationshipData = 0x02,
        HasNonInlineIds = 0x04,
        /// <summary>
        /// // WDC1+
        /// </summary>
        IsBitpacked = 0x10
    }
}
