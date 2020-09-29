Imports System.IO
Imports LibWarDB2VB.LibWarDB2.Structures
Imports System.Runtime.CompilerServices

Namespace LibWarDB2.Extensions
    Module BinaryWriterExtensions
        <Extension()>
        Sub Write(ByVal writer As BinaryWriter, ByVal wdc As WDC3)
            writer.Write(wdc.Header)

            For i As Integer = 0 To wdc.SectionHeaders.Length - 1
                writer.Write(wdc.SectionHeaders(i))
            Next

            For i As Integer = 0 To wdc.Fields.Length - 1
                writer.Write(wdc.Fields(i))
            Next

            For i As Integer = 0 To wdc.FieldInfo.Length - 1
                writer.Write(wdc.FieldInfo(i))
            Next

            For i As Integer = 0 To wdc.PalletData.Length - 1
                writer.Write(wdc.PalletData(i))
            Next

            For i As Integer = 0 To wdc.CommonData.Length - 1
                writer.Write(wdc.CommonData(i))
            Next

            For i As Integer = 0 To wdc.DataSections.Length - 1
                writer.Write(wdc.DataSections(i), wdc.Header, wdc.SectionHeaders(i))
            Next
        End Sub

        <Extension()>
        Sub Write(ByVal writer As BinaryWriter, ByVal header As WDC3Header)
            writer.Write(header.Magic)
            writer.Write(header.RecordCount)
            writer.Write(header.FieldCount)
            writer.Write(header.RecordSize)
            writer.Write(header.StringTableSize)
            writer.Write(header.TableHash)
            writer.Write(header.LayoutHash)
            writer.Write(header.MinId)
            writer.Write(header.MaxId)
            writer.Write(header.Locale)
            writer.Write(CUShort(header.Flags))
            writer.Write(header.IdIndex)
            writer.Write(header.TotalFieldCount)
            writer.Write(header.BitpackedDataOffset)
            writer.Write(header.LookupColumnCount)
            writer.Write(header.FieldStorageInfoSize)
            writer.Write(header.CommonDataSize)
            writer.Write(header.PalletDataSize)
            writer.Write(header.SectionCount)
        End Sub

        <Extension()>
        Sub Write(ByVal writer As BinaryWriter, ByVal sectionHeader As WDC3SectionHeader)
            writer.Write(sectionHeader.TactKeyHash)
            writer.Write(sectionHeader.FileOffset)
            writer.Write(sectionHeader.RecordCount)
            writer.Write(sectionHeader.StringTableSize)
            writer.Write(sectionHeader.OffsetRecordsEnd)
            writer.Write(sectionHeader.IdListSize)
            writer.Write(sectionHeader.RelationshipDataSize)
            writer.Write(sectionHeader.OffsetMapIdCount)
            writer.Write(sectionHeader.CopyTableCount)
        End Sub

        <Extension()>
        Sub Write(ByVal writer As BinaryWriter, ByVal field As FieldStructure)
            writer.Write(field.Size)
            writer.Write(field.Position)
        End Sub

        <Extension()>
        Sub Write(ByVal writer As BinaryWriter, ByVal info As FieldStorageInfo)
            writer.Write(info.FieldOffsetBits)
            writer.Write(info.FieldSizeBits)
            writer.Write(info.AdditionalDataSize)
            writer.Write(CUInt(info.StorageType))
            writer.Write(info.Value1)
            writer.Write(info.Value2)
            writer.Write(info.Value3)
        End Sub

        <Extension()>
        Sub Write(ByVal writer As BinaryWriter, ByVal section As Section, ByVal header As WDC3Header, ByVal sectionHeader As WDC3SectionHeader)
            If header.Flags.HasFlag(WDB4Flags.HasOffsetMap) Then

                For i As Integer = 0 To section.VariableRecordData.Length - 1
                    writer.Write(section.VariableRecordData)
                Next
            Else

                For i As Integer = 0 To section.Records.Length - 1
                    writer.Write(section.Records(i))
                Next

                For i As Integer = 0 To sectionHeader.StringTableSize - 1
                    writer.Write(section.StringData(i))
                Next
            End If

            For i As Integer = 0 To section.IdList.Length - 1
                writer.Write(section.IdList(i))
            Next

            If section.CopyTable IsNot Nothing AndAlso section.CopyTable.Length > 0 Then

                For i As Integer = 0 To section.CopyTable.Length - 1
                    writer.Write(section.CopyTable(i))
                Next
            End If

            For i As Integer = 0 To section.OffsetMap.Length - 1
                writer.Write(section.OffsetMap(i))
            Next

            If section.RelationshipMap IsNot Nothing Then writer.Write(section.RelationshipMap)

            For i As Integer = 0 To section.OffsetMapIdList.Length - 1
                writer.Write(section.OffsetMapIdList(i))
            Next
        End Sub

        <Extension()>
        Sub Write(ByVal writer As BinaryWriter, ByVal record As RecordData)
            For i As Integer = 0 To record.Data.Length - 1
                writer.Write(record.Data(i))
            Next
        End Sub

        <Extension()>
        Sub Write(ByVal writer As BinaryWriter, ByVal entry As CopyTableEntry)
            writer.Write(entry.IdOfNewRow)
            writer.Write(entry.IdOfCopiedRow)
        End Sub

        <Extension()>
        Sub Write(ByVal writer As BinaryWriter, ByVal entry As OffsetMapEntry)
            writer.Write(entry.Offset)
            writer.Write(entry.Size)
        End Sub

        <Extension()>
        Sub Write(ByVal writer As BinaryWriter, ByVal mapping As RelationshipMapping)
            writer.Write(mapping.NumEntries)
            writer.Write(mapping.MinId)
            writer.Write(mapping.MaxId)

            For i As Integer = 0 To mapping.NumEntries - 1
                writer.Write(mapping.Entries(i))
            Next
        End Sub

        <Extension()>
        Sub Write(ByVal writer As BinaryWriter, ByVal entry As RelationshipEntry)
            writer.Write(entry.ForeignId)
            writer.Write(entry.RecordIndex)
        End Sub
    End Module
End Namespace
