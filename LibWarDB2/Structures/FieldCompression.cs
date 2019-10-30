namespace LibWarDB2.Structures
{
    public enum FieldCompression
    {
        /// <summary>
        /// The field is a 8-, 16-, 32-, or 64-bit integer in the record data
        /// </summary>
        None,
        /// <summary>
        /// The field is a bitpacked integer in the record data.
        /// It is field_size_bits long and starts at field_offset_bits.
        /// A bitpacked value occupies (field_size_bits + (field_offset_bits & 7) + 7) / 8
        /// bytes starting at byte field_offset_bits / 8 in the record data.
        /// These bytes should be read as a little-endian value, then the value is shifted to the right by (field_offset_bits & 7) and
        /// masked with ((1ull &lt;&lt; field_size_bits) - 1).
        /// </summary>
        Bitpacked,
        /// <summary>
        /// The field is assumed to be a default value, and exceptions
        /// from that default value are stored in the corresponding section in
        /// common_data as pairs of { uint32_t record_id; uint32_t value; }.
        /// </summary>
        CommonData,
        /// <summary>
        /// The field has a bitpacked index in the record data.
        /// This index is used as an index into the corresponding section in pallet_data.
        /// The pallet_data section is an array of uint32_t, so the index should be multiplied by 4 to obtain a byte offset.
        /// </summary>
        BitpackedIndexed,
        /// <summary>
        /// The field has a bitpacked index in the record data.
        /// This index is used as an index into the corresponding section in pallet_data.
        /// The pallet_data section is an array of uint32_t[array_count],
        /// </summary>
        BitpackedIndexedArray,
        /// <summary>
        /// Same as <see cref="Bitpacked"/>.
        /// </summary>
        BitpackedSigned
    }
}
