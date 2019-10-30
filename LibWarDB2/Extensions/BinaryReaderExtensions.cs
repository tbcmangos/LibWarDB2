using System.IO;
using System.Runtime.InteropServices;
using LibWarDB2.Structures;

namespace LibWarDB2.Extensions
{
    public static class BinaryReaderExtensions
    {
        public static WDC3 ReadWDC3(this BinaryReader reader)
        {
            var wdc = new WDC3();
            wdc.Header = reader.ReadWDC3Header();
            wdc.SectionHeaders = new WDC3SectionHeader[wdc.Header.SectionCount];
            for (int i = 0; i < wdc.Header.SectionCount; i++)
                wdc.SectionHeaders[i] = reader.ReadWDC3SectionHeader();
            wdc.Fields = new FieldStructure[wdc.Header.TotalFieldCount];
            for (int i = 0; i < wdc.Header.TotalFieldCount; i++)
                wdc.Fields[i] = reader.ReadFieldStructure();
            wdc.FieldInfo = new FieldStorageInfo[wdc.Header.FieldStorageInfoSize / Marshal.SizeOf(typeof(FieldStorageInfo))];
            for (int i = 0; i < wdc.Header.FieldStorageInfoSize / Marshal.SizeOf(typeof(FieldStorageInfo)); i++)
                wdc.FieldInfo[i] = reader.ReadFieldStorageInfo();
            wdc.PalletData = new byte[wdc.Header.PalletDataSize];
            for (int i = 0; i < wdc.Header.PalletDataSize; i++)
                wdc.PalletData[i] = reader.ReadByte();
            wdc.CommonData = new byte[wdc.Header.CommonDataSize];
            for (int i = 0; i < wdc.Header.CommonDataSize; i++)
                wdc.CommonData[i] = reader.ReadByte();
            wdc.DataSections = new Section[wdc.Header.SectionCount];
            for (int i = 0; i < wdc.Header.SectionCount; i++)
                wdc.DataSections[i] = reader.ReadSection(wdc.Header, wdc.SectionHeaders[i]);
            return wdc;
        }

        public static WDC3Header ReadWDC3Header(this BinaryReader reader)
        {
            return new WDC3Header()
            {
                Magic = reader.ReadUInt32(),
                RecordCount = reader.ReadUInt32(),
                FieldCount = reader.ReadUInt32(),
                RecordSize = reader.ReadUInt32(),
                StringTableSize = reader.ReadUInt32(),
                TableHash = reader.ReadUInt32(),
                LayoutHash = reader.ReadUInt32(),
                MinId = reader.ReadUInt32(),
                MaxId = reader.ReadUInt32(),
                Locale = reader.ReadUInt32(),
                Flags = (WDB4Flags)reader.ReadUInt16(),
                IdIndex = reader.ReadUInt16(), 
                TotalFieldCount = reader.ReadUInt32(),
                BitpackedDataOffset = reader.ReadUInt32(),
                LookupColumnCount = reader.ReadUInt32(),
                FieldStorageInfoSize = reader.ReadUInt32(),
                CommonDataSize = reader.ReadUInt32(),
                PalletDataSize = reader.ReadUInt32(),
                SectionCount = reader.ReadUInt32()
            };
        }

        public static WDC3SectionHeader ReadWDC3SectionHeader(this BinaryReader reader)
        {
            return new WDC3SectionHeader()
            {
                TactKeyHash = reader.ReadUInt64(),
                FileOffset = reader.ReadUInt32(),
                RecordCount = reader.ReadUInt32(),
                StringTableSize = reader.ReadUInt32(),
                OffsetRecordsEnd = reader.ReadUInt32(),
                IdListSize = reader.ReadUInt32(),
                RelationshipDataSize = reader.ReadUInt32(),
                OffsetMapIdCount = reader.ReadUInt32(),
                CopyTableCount = reader.ReadUInt32()
            };
        }

        public static FieldStructure ReadFieldStructure(this BinaryReader reader)
        {
            return new FieldStructure()
            {
                Size = reader.ReadInt16(),
                Position = reader.ReadUInt16()
            };
        }

        public static FieldStorageInfo ReadFieldStorageInfo(this BinaryReader reader)
        {
            return new FieldStorageInfo()
            {
                FieldOffsetBits = reader.ReadUInt16(),
                FieldSizeBits = reader.ReadUInt16(),
                AdditionalDataSize = reader.ReadUInt32(),
                StorageType = (FieldCompression)reader.ReadUInt32(),
                Value1 = reader.ReadUInt32(),
                Value2 = reader.ReadUInt32(),
                Value3 = reader.ReadUInt32(),
            };
        }

        public static Section ReadSection(this BinaryReader reader, WDC3Header header, WDC3SectionHeader sectionHeader)
        {
            var section = new Section();
            if (header.Flags.HasFlag(WDB4Flags.HasOffsetMap))
            {
                section.VariableRecordData = new byte[sectionHeader.OffsetRecordsEnd - sectionHeader.FileOffset];
                for (int i = 0; i < sectionHeader.OffsetRecordsEnd - sectionHeader.FileOffset; i++)
                    section.VariableRecordData[i] = reader.ReadByte();
            }
            else
            {
                section.Records = new RecordData[sectionHeader.RecordCount];
                for (int i = 0; i < sectionHeader.RecordCount; i++)
                    section.Records[i] = reader.ReadRecordData(header);
                section.StringData = new byte[sectionHeader.StringTableSize];
                for (int i = 0; i < sectionHeader.StringTableSize; i++)
                    section.StringData[i] = reader.ReadByte();
            }
            section.IdList = new uint[sectionHeader.IdListSize / sizeof(uint)];
            for (int i = 0; i < sectionHeader.IdListSize / sizeof(uint); i++)
                section.IdList[i] = reader.ReadUInt32();
            if (sectionHeader.CopyTableCount > 0)
            {
                section.CopyTable = new CopyTableEntry[sectionHeader.CopyTableCount];
                for (int i = 0; i < sectionHeader.CopyTableCount; i++)
                    section.CopyTable[i] = reader.ReadCopyTableEntry();
            }
            section.OffsetMap = new OffsetMapEntry[sectionHeader.OffsetMapIdCount];
            for (int i = 0; i < sectionHeader.OffsetMapIdCount; i++)
                section.OffsetMap[i] = reader.ReadOffsetMapEntry();
            if (sectionHeader.RelationshipDataSize > 0)
                section.RelationshipMap = reader.ReadRelationshipMapping();
            section.OffsetMapIdList = new uint[sectionHeader.OffsetMapIdCount];
            for (int i = 0; i < sectionHeader.OffsetMapIdCount; i++)
                section.OffsetMapIdList[i] = reader.ReadUInt32();
            return section;
        }

        public static RecordData ReadRecordData(this BinaryReader reader, WDC3Header header)
        {
            var record = new RecordData();
            record.Data = new byte[header.RecordSize];
            for (int i = 0; i < header.RecordSize; i++)
                record.Data[i] = reader.ReadByte();
            return record;
        }

        public static CopyTableEntry ReadCopyTableEntry(this BinaryReader reader)
        {
            return new CopyTableEntry()
            {
                IdOfNewRow = reader.ReadUInt32(),
                IdOfCopiedRow = reader.ReadUInt32()
            };
        }

        public static OffsetMapEntry ReadOffsetMapEntry(this BinaryReader reader)
        {
            return new OffsetMapEntry()
            {
                Offset = reader.ReadUInt32(),
                Size = reader.ReadUInt16()
            };
        }

        public static RelationshipMapping ReadRelationshipMapping(this BinaryReader reader)
        {
            var mapping = new RelationshipMapping();
            mapping.NumEntries = reader.ReadUInt32();
            mapping.MinId = reader.ReadUInt32();
            mapping.MaxId = reader.ReadUInt32();
            mapping.Entries = new RelationshipEntry[mapping.NumEntries];
            for (int i = 0; i < mapping.NumEntries; i++)
                mapping.Entries[i] = reader.ReadRelationshipEntry();
            return mapping;
        }

        public static RelationshipEntry ReadRelationshipEntry(this BinaryReader reader)
        {
            return new RelationshipEntry()
            {
                ForeignId = reader.ReadUInt32(),
                RecordIndex = reader.ReadUInt32()
            };
        }
    }
}
