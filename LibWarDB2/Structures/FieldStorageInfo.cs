using System.Runtime.InteropServices;

namespace LibWarDB2.Structures
{
    [StructLayout(LayoutKind.Sequential)]
    public class FieldStorageInfo
    {
        public ushort FieldOffsetBits { get; set; }
        /// <summary>
        /// very important for reading bitpacked fields; size is the sum of all array pieces in bits - for example, uint32[3] will appear here as '96'
        /// additional_data_size is the size in bytes of the corresponding section in
        /// common_data or pallet_data.  These sections are in the same order as the
        /// field_info, so to find the offset, add up the additional_data_size of any
        /// previous fields which are stored in the same block (common_data or pallet_data).
        /// </summary>
        public ushort FieldSizeBits { get; set; }
        public uint AdditionalDataSize { get; set; }
        public FieldCompression StorageType { get; set; }
        /// <summary>
        /// Bitpacked || BitpackedSigned: uint32_t bitpacking_offset_bits; not useful for most purposes; formula they use to calculate is bitpacking_offset_bits = field_offset_bits - (header.bitpacked_data_offset * 8)
        /// CommonData: uint32_t default_value;
        /// BitpackedIndexed: uint32_t bitpacking_offset_bits; // not useful for most purposes; formula they use to calculate is bitpacking_offset_bits = field_offset_bits - (header.bitpacked_data_offset * 8)
        /// BitpackedIndexedArray: uint32_t bitpacking_offset_bits; // not useful for most purposes; formula they use to calculate is bitpacking_offset_bits = field_offset_bits - (header.bitpacked_data_offset * 8)
        /// Default: uint32_t unk_or_unused1;
        /// </summary>
        public uint Value1 { get; set; }
        /// <summary>
        /// Bitpacked || BitpackedSigned: bitpacking_size_bits; // not useful for most purposes
        /// CommonData: uint32_t unk_or_unused2;
        /// BitpackedIndexed: uint32_t bitpacking_size_bits; // not useful for most purposes
        /// BitpackedIndexedArray: uint32_t bitpacking_size_bits; // not useful for most purposes
        /// Default: uint32_t unk_or_unused2;
        /// </summary>
        public uint Value2 { get; set; }
        /// <summary>
        /// Bitpacked || BitpackedSigned: uint32_t flags; // known values - 0x01: sign-extend (signed)
        /// CommonData: uint32_t unk_or_unused3;
        /// BitpackedIndexed: uint32_t unk_or_unused3;
        /// BitpackedIndexedArray: uint32_t array_count;
        /// Default: uint32_t unk_or_unused3;
        /// </summary>
        public uint Value3 { get; set; }
    }
}
