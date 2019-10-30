using System.IO;
using LibWarDB2.Structures;

namespace LibWarDB2.Extensions
{
    public static class BinaryWriterExtensions
    {
        public static void Write(this BinaryWriter writer, WDC3 wdc)
        {
            writer.Write(wdc.Header);
            for (int i = 0; i < wdc.SectionHeaders.Length; i++)
                writer.Write(wdc.SectionHeaders[i]);
            for (int i = 0; i < wdc.Fields.Length; i++)
                writer.Write(wdc.Fields[i]);
            for (int i = 0; i < wdc.FieldInfo.Length; i++)
                writer.Write(wdc.FieldInfo[i]);
            for (int i = 0; i < wdc.PalletData.Length; i++)
                writer.Write(wdc.PalletData[i]);
            for (int i = 0; i < wdc.CommonData.Length; i++)
                writer.Write(wdc.CommonData[i]);
            for (int i = 0; i < wdc.DataSections.Length; i++)
                writer.Write(wdc.DataSections[i], wdc.Header, wdc.SectionHeaders[i]);
        }

        public static void Write(this BinaryWriter writer, WDC3Header header)
        {
            writer.Write(header.Magic);
            writer.Write(header.RecordCount);
            writer.Write(header.FieldCount);
            writer.Write(header.RecordSize);
            writer.Write(header.StringTableSize);
            writer.Write(header.TableHash);
            writer.Write(header.LayoutHash);
            writer.Write(header.MinId);
            writer.Write(header.MaxId);
            writer.Write(header.Locale);
            writer.Write((ushort)header.Flags);
            writer.Write(header.IdIndex);
            writer.Write(header.TotalFieldCount);
            writer.Write(header.BitpackedDataOffset);
            writer.Write(header.LookupColumnCount);
            writer.Write(header.FieldStorageInfoSize);
            writer.Write(header.CommonDataSize);
            writer.Write(header.PalletDataSize);
            writer.Write(header.SectionCount);
        }

        public static void Write(this BinaryWriter writer, WDC3SectionHeader sectionHeader)
        {
            writer.Write(sectionHeader.TactKeyHash);
            writer.Write(sectionHeader.FileOffset);
            writer.Write(sectionHeader.RecordCount);
            writer.Write(sectionHeader.StringTableSize);
            writer.Write(sectionHeader.OffsetRecordsEnd);
            writer.Write(sectionHeader.IdListSize);
            writer.Write(sectionHeader.RelationshipDataSize);
            writer.Write(sectionHeader.OffsetMapIdCount);
            writer.Write(sectionHeader.CopyTableCount);
        }

        public static void Write(this BinaryWriter writer, FieldStructure field)
        {
            writer.Write(field.Size);
            writer.Write(field.Position);
        }

        public static void Write(this BinaryWriter writer, FieldStorageInfo info)
        {
            writer.Write(info.FieldOffsetBits);
            writer.Write(info.FieldSizeBits);
            writer.Write(info.AdditionalDataSize);
            writer.Write((uint)info.StorageType);
            writer.Write(info.Value1);
            writer.Write(info.Value2);
            writer.Write(info.Value3);
        }

        public static void Write(this BinaryWriter writer, Section section, WDC3Header header, WDC3SectionHeader sectionHeader)
        {
            if (header.Flags.HasFlag(WDB4Flags.HasOffsetMap))
            {
                for (int i = 0; i < section.VariableRecordData.Length; i++)
                    writer.Write(section.VariableRecordData);
            }
            else
            {
                for (int i = 0; i < section.Records.Length; i++)
                    writer.Write(section.Records[i]);
                for (int i = 0; i < sectionHeader.StringTableSize; i++)
                    writer.Write(section.StringData[i]);
            }
            for (int i = 0; i < section.IdList.Length; i++)
                writer.Write(section.IdList[i]);
            if (section.CopyTable != null && section.CopyTable.Length > 0)
            {
                for (int i = 0; i < section.CopyTable.Length; i++)
                    writer.Write(section.CopyTable[i]);
            }
            for(int i = 0; i < section.OffsetMap.Length; i++)
                writer.Write(section.OffsetMap[i]);
            if (section.RelationshipMap != null)
                writer.Write(section.RelationshipMap);
            for (int i = 0; i < section.OffsetMapIdList.Length; i++)
                writer.Write(section.OffsetMapIdList[i]);
        }

        public static void Write(this BinaryWriter writer, RecordData record)
        {
            for (int i = 0; i < record.Data.Length; i++)
                writer.Write(record.Data[i]);
        }

        public static void Write(this BinaryWriter writer, CopyTableEntry entry)
        {
            writer.Write(entry.IdOfNewRow);
            writer.Write(entry.IdOfCopiedRow);
        }

        public static void Write(this BinaryWriter writer, OffsetMapEntry entry)
        {
            writer.Write(entry.Offset);
            writer.Write(entry.Size);
        }

        public static void Write(this BinaryWriter writer, RelationshipMapping mapping)
        {
            writer.Write(mapping.NumEntries);
            writer.Write(mapping.MinId);
            writer.Write(mapping.MaxId);
            for (int i = 0; i < mapping.NumEntries; i++)
                writer.Write(mapping.Entries[i]);
        }

        public static void Write(this BinaryWriter writer, RelationshipEntry entry)
        {
            writer.Write(entry.ForeignId);
            writer.Write(entry.RecordIndex);
        }
    }
}
