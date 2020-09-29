Imports System.IO
Imports System.Runtime.InteropServices
Imports LibWarDB2VB.LibWarDB2.Structures
Imports System.Runtime.CompilerServices

Namespace LibWarDB2.Extensions
    Module BinaryReaderExtensions
        <Extension()>
        Function ReadWDC3(ByVal reader As BinaryReader) As WDC3
            Dim wdc = New WDC3()
            wdc.Header = reader.ReadWDC3Header()
            If wdc.Header.Magic <> WDC3.Signature Then Throw New InvalidDataException("The file magic is not WDC3")
            wdc.SectionHeaders = New WDC3SectionHeader(wdc.Header.SectionCount - 1) {}

            For i As Integer = 0 To wdc.Header.SectionCount - 1
                wdc.SectionHeaders(i) = reader.ReadWDC3SectionHeader()
            Next

            wdc.Fields = New FieldStructure(wdc.Header.TotalFieldCount - 1) {}

            For i As Integer = 0 To wdc.Header.TotalFieldCount - 1
                wdc.Fields(i) = reader.ReadFieldStructure()
            Next

            wdc.FieldInfo = New FieldStorageInfo(wdc.Header.FieldStorageInfoSize / Marshal.SizeOf(GetType(FieldStorageInfo)) - 1) {}

            For i As Integer = 0 To wdc.Header.FieldStorageInfoSize / Marshal.SizeOf(GetType(FieldStorageInfo)) - 1
                wdc.FieldInfo(i) = reader.ReadFieldStorageInfo()
            Next

            wdc.PalletData = New Byte(wdc.Header.PalletDataSize - 1) {}

            For i As Integer = 0 To wdc.Header.PalletDataSize - 1
                wdc.PalletData(i) = reader.ReadByte()
            Next

            wdc.CommonData = New Byte(wdc.Header.CommonDataSize - 1) {}

            For i As Integer = 0 To wdc.Header.CommonDataSize - 1
                wdc.CommonData(i) = reader.ReadByte()
            Next

            wdc.DataSections = New Section(wdc.Header.SectionCount - 1) {}

            For i As Integer = 0 To wdc.Header.SectionCount - 1
                wdc.DataSections(i) = reader.ReadSection(wdc.Header, wdc.SectionHeaders(i))
            Next

            Return wdc
        End Function

        <Extension()>
        Function ReadWDC3Header(ByVal reader As BinaryReader) As WDC3Header
            Return New WDC3Header() With {
                .Magic = reader.ReadUInt32(),
                .RecordCount = reader.ReadUInt32(),
                .FieldCount = reader.ReadUInt32(),
                .RecordSize = reader.ReadUInt32(),
                .StringTableSize = reader.ReadUInt32(),
                .TableHash = reader.ReadUInt32(),
                .LayoutHash = reader.ReadUInt32(),
                .MinId = reader.ReadUInt32(),
                .MaxId = reader.ReadUInt32(),
                .Locale = reader.ReadUInt32(),
                .Flags = CType(reader.ReadUInt16(), WDB4Flags),
                .IdIndex = reader.ReadUInt16(),
                .TotalFieldCount = reader.ReadUInt32(),
                .BitpackedDataOffset = reader.ReadUInt32(),
                .LookupColumnCount = reader.ReadUInt32(),
                .FieldStorageInfoSize = reader.ReadUInt32(),
                .CommonDataSize = reader.ReadUInt32(),
                .PalletDataSize = reader.ReadUInt32(),
                .SectionCount = reader.ReadUInt32()
            }
        End Function

        <Extension()>
        Function ReadWDC3SectionHeader(ByVal reader As BinaryReader) As WDC3SectionHeader
            Return New WDC3SectionHeader() With {
                .TactKeyHash = reader.ReadUInt64(),
                .FileOffset = reader.ReadUInt32(),
                .RecordCount = reader.ReadUInt32(),
                .StringTableSize = reader.ReadUInt32(),
                .OffsetRecordsEnd = reader.ReadUInt32(),
                .IdListSize = reader.ReadUInt32(),
                .RelationshipDataSize = reader.ReadUInt32(),
                .OffsetMapIdCount = reader.ReadUInt32(),
                .CopyTableCount = reader.ReadUInt32()
            }
        End Function

        <Extension()>
        Function ReadFieldStructure(ByVal reader As BinaryReader) As FieldStructure
            Return New FieldStructure() With {
                .Size = reader.ReadInt16(),
                .Position = reader.ReadUInt16()
            }
        End Function

        <Extension()>
        Function ReadFieldStorageInfo(ByVal reader As BinaryReader) As FieldStorageInfo
            Return New FieldStorageInfo() With {
                .FieldOffsetBits = reader.ReadUInt16(),
                .FieldSizeBits = reader.ReadUInt16(),
                .AdditionalDataSize = reader.ReadUInt32(),
                .StorageType = CType(reader.ReadUInt32(), FieldCompression),
                .Value1 = reader.ReadUInt32(),
                .Value2 = reader.ReadUInt32(),
                .Value3 = reader.ReadUInt32()
            }
        End Function

        <Extension()>
        Function ReadSection(ByVal reader As BinaryReader, ByVal header As WDC3Header, ByVal sectionHeader As WDC3SectionHeader) As Section
            Dim section = New Section()

            If header.Flags.HasFlag(WDB4Flags.HasOffsetMap) Then
                section.VariableRecordData = New Byte(sectionHeader.OffsetRecordsEnd - sectionHeader.FileOffset - 1) {}

                For i As Integer = 0 To sectionHeader.OffsetRecordsEnd - sectionHeader.FileOffset - 1
                    section.VariableRecordData(i) = reader.ReadByte()
                Next
            Else
                section.Records = New RecordData(sectionHeader.RecordCount - 1) {}

                For i As Integer = 0 To sectionHeader.RecordCount - 1
                    section.Records(i) = reader.ReadRecordData(header)
                Next

                section.StringData = New Byte(sectionHeader.StringTableSize - 1) {}

                For i As Integer = 0 To sectionHeader.StringTableSize - 1
                    section.StringData(i) = reader.ReadByte()
                Next
            End If

            ''' Cannot convert ExpressionStatementSyntax, CONVERSION ERROR: Conversion for SizeOfExpression not implemented, please report this issue in 'sizeof(uint)' at character 5506
            '''    at ICSharpCode.CodeConverter.VB.NodesVisitor.DefaultVisit(SyntaxNode node)
            '''    at Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor`1.VisitSizeOfExpression(SizeOfExpressionSyntax node)
            '''    at Microsoft.CodeAnalysis.CSharp.Syntax.SizeOfExpressionSyntax.Accept[TResult](CSharpSyntaxVisitor`1 visitor)
            '''    at Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor`1.Visit(SyntaxNode node)
            '''    at ICSharpCode.CodeConverter.VB.CommentConvertingNodesVisitor.DefaultVisit(SyntaxNode node)
            '''    at Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor`1.VisitSizeOfExpression(SizeOfExpressionSyntax node)
            '''    at Microsoft.CodeAnalysis.CSharp.Syntax.SizeOfExpressionSyntax.Accept[TResult](CSharpSyntaxVisitor`1 visitor)
            '''    at ICSharpCode.CodeConverter.VB.NodesVisitor.VisitBinaryExpression(BinaryExpressionSyntax node)
            '''    at Microsoft.CodeAnalysis.CSharp.Syntax.BinaryExpressionSyntax.Accept[TResult](CSharpSyntaxVisitor`1 visitor)
            '''    at Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor`1.Visit(SyntaxNode node)
            '''    at ICSharpCode.CodeConverter.VB.CommentConvertingNodesVisitor.DefaultVisit(SyntaxNode node)
            '''    at Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor`1.VisitBinaryExpression(BinaryExpressionSyntax node)
            '''    at Microsoft.CodeAnalysis.CSharp.Syntax.BinaryExpressionSyntax.Accept[TResult](CSharpSyntaxVisitor`1 visitor)
            '''    at ICSharpCode.CodeConverter.VB.CommonConversions.ReduceArrayUpperBoundExpression(ExpressionSyntax expr)
            '''    at ICSharpCode.CodeConverter.VB.NodesVisitor.<VisitArrayCreationExpression>b__83_1(ExpressionSyntax s)
            '''    at System.Linq.Enumerable.WhereSelectEnumerableIterator`2.MoveNext()
            '''    at System.Linq.Enumerable.Any[TSource](IEnumerable`1 source)
            '''    at ICSharpCode.CodeConverter.VB.NodesVisitor.VisitArrayCreationExpression(ArrayCreationExpressionSyntax node)
            '''    at Microsoft.CodeAnalysis.CSharp.Syntax.ArrayCreationExpressionSyntax.Accept[TResult](CSharpSyntaxVisitor`1 visitor)
            '''    at Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor`1.Visit(SyntaxNode node)
            '''    at ICSharpCode.CodeConverter.VB.CommentConvertingNodesVisitor.DefaultVisit(SyntaxNode node)
            '''    at Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor`1.VisitArrayCreationExpression(ArrayCreationExpressionSyntax node)
            '''    at Microsoft.CodeAnalysis.CSharp.Syntax.ArrayCreationExpressionSyntax.Accept[TResult](CSharpSyntaxVisitor`1 visitor)
            '''    at ICSharpCode.CodeConverter.VB.NodesVisitor.MakeAssignmentStatement(AssignmentExpressionSyntax node)
            '''    at ICSharpCode.CodeConverter.VB.NodesVisitor.VisitAssignmentExpression(AssignmentExpressionSyntax node)
            '''    at Microsoft.CodeAnalysis.CSharp.Syntax.AssignmentExpressionSyntax.Accept[TResult](CSharpSyntaxVisitor`1 visitor)
            '''    at Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor`1.Visit(SyntaxNode node)
            '''    at ICSharpCode.CodeConverter.VB.CommentConvertingNodesVisitor.DefaultVisit(SyntaxNode node)
            '''    at Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor`1.VisitAssignmentExpression(AssignmentExpressionSyntax node)
            '''    at Microsoft.CodeAnalysis.CSharp.Syntax.AssignmentExpressionSyntax.Accept[TResult](CSharpSyntaxVisitor`1 visitor)
            '''    at ICSharpCode.CodeConverter.VB.MethodBodyVisitor.ConvertSingleExpression(ExpressionSyntax node)
            '''    at ICSharpCode.CodeConverter.VB.MethodBodyVisitor.VisitExpressionStatement(ExpressionStatementSyntax node)
            '''    at Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionStatementSyntax.Accept[TResult](CSharpSyntaxVisitor`1 visitor)
            '''    at Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor`1.Visit(SyntaxNode node)
            '''    at ICSharpCode.CodeConverter.VB.CommentConvertingMethodBodyVisitor.ConvertWithTrivia(SyntaxNode node)
            '''    at ICSharpCode.CodeConverter.VB.CommentConvertingMethodBodyVisitor.DefaultVisit(SyntaxNode node)
            ''' 
            ''' Input: 
            '''             section.IdList = new uint[sectionHeader.IdListSize / sizeof(uint)];

            section.IdList = New UInteger((sectionHeader.IdListSize / 4) - 1) {}
            ''' 
            ''' Cannot convert ForStatementSyntax, CONVERSION ERROR: Conversion for SizeOfExpression not implemented, please report this issue in 'sizeof(uint)' at character 5580
            '''    at ICSharpCode.CodeConverter.VB.NodesVisitor.DefaultVisit(SyntaxNode node)
            '''    at Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor`1.VisitSizeOfExpression(SizeOfExpressionSyntax node)
            '''    at Microsoft.CodeAnalysis.CSharp.Syntax.SizeOfExpressionSyntax.Accept[TResult](CSharpSyntaxVisitor`1 visitor)
            '''    at Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor`1.Visit(SyntaxNode node)
            '''    at ICSharpCode.CodeConverter.VB.CommentConvertingNodesVisitor.DefaultVisit(SyntaxNode node)
            '''    at Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor`1.VisitSizeOfExpression(SizeOfExpressionSyntax node)
            '''    at Microsoft.CodeAnalysis.CSharp.Syntax.SizeOfExpressionSyntax.Accept[TResult](CSharpSyntaxVisitor`1 visitor)
            '''    at ICSharpCode.CodeConverter.VB.NodesVisitor.VisitBinaryExpression(BinaryExpressionSyntax node)
            '''    at Microsoft.CodeAnalysis.CSharp.Syntax.BinaryExpressionSyntax.Accept[TResult](CSharpSyntaxVisitor`1 visitor)
            '''    at Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor`1.Visit(SyntaxNode node)
            '''    at ICSharpCode.CodeConverter.VB.CommentConvertingNodesVisitor.DefaultVisit(SyntaxNode node)
            '''    at Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor`1.VisitBinaryExpression(BinaryExpressionSyntax node)
            '''    at Microsoft.CodeAnalysis.CSharp.Syntax.BinaryExpressionSyntax.Accept[TResult](CSharpSyntaxVisitor`1 visitor)
            '''    at ICSharpCode.CodeConverter.VB.MethodBodyVisitor.ConvertForToSimpleForNext(ForStatementSyntax node, StatementSyntax& block)
            '''    at ICSharpCode.CodeConverter.VB.MethodBodyVisitor.VisitForStatement(ForStatementSyntax node)
            '''    at Microsoft.CodeAnalysis.CSharp.Syntax.ForStatementSyntax.Accept[TResult](CSharpSyntaxVisitor`1 visitor)
            '''    at Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor`1.Visit(SyntaxNode node)
            '''    at ICSharpCode.CodeConverter.VB.CommentConvertingMethodBodyVisitor.ConvertWithTrivia(SyntaxNode node)
            '''    at ICSharpCode.CodeConverter.VB.CommentConvertingMethodBodyVisitor.DefaultVisit(SyntaxNode node)
            ''' 
            ''' Input: 
            '''             for (int i = 0; i < sectionHeader.IdListSize / sizeof(uint); i++)
            '''             
            For i = 0 To (sectionHeader.IdListSize / 4) - 1
                section.IdList(i) = reader.ReadUInt32()
            Next

            ''' 
            If sectionHeader.CopyTableCount > 0 Then
                section.CopyTable = New CopyTableEntry(sectionHeader.CopyTableCount - 1) {}

                For i As Integer = 0 To sectionHeader.CopyTableCount - 1
                    section.CopyTable(i) = reader.ReadCopyTableEntry()
                Next
            End If

            section.OffsetMap = New OffsetMapEntry(sectionHeader.OffsetMapIdCount - 1) {}

            For i As Integer = 0 To sectionHeader.OffsetMapIdCount - 1
                section.OffsetMap(i) = reader.ReadOffsetMapEntry()
            Next

            If sectionHeader.RelationshipDataSize > 0 Then section.RelationshipMap = reader.ReadRelationshipMapping()
            section.OffsetMapIdList = New UInteger(sectionHeader.OffsetMapIdCount - 1) {}

            For i As Integer = 0 To sectionHeader.OffsetMapIdCount - 1
                section.OffsetMapIdList(i) = reader.ReadUInt32()
            Next

            Return section
        End Function

        <Extension()>
        Function ReadRecordData(ByVal reader As BinaryReader, ByVal header As WDC3Header) As RecordData
            Dim record = New RecordData()
            record.Data = New Byte(header.RecordSize - 1) {}

            For i As Integer = 0 To header.RecordSize - 1
                record.Data(i) = reader.ReadByte()
            Next

            Return record
        End Function

        <Extension()>
        Function ReadCopyTableEntry(ByVal reader As BinaryReader) As CopyTableEntry
            Return New CopyTableEntry() With {
                .IdOfNewRow = reader.ReadUInt32(),
                .IdOfCopiedRow = reader.ReadUInt32()
            }
        End Function

        <Extension()>
        Function ReadOffsetMapEntry(ByVal reader As BinaryReader) As OffsetMapEntry
            Return New OffsetMapEntry() With {
                .Offset = reader.ReadUInt32(),
                .Size = reader.ReadUInt16()
            }
        End Function

        <Extension()>
        Function ReadRelationshipMapping(ByVal reader As BinaryReader) As RelationshipMapping
            Dim mapping = New RelationshipMapping()
            mapping.NumEntries = reader.ReadUInt32()
            mapping.MinId = reader.ReadUInt32()
            mapping.MaxId = reader.ReadUInt32()
            mapping.Entries = New RelationshipEntry(mapping.NumEntries - 1) {}

            For i As Integer = 0 To mapping.NumEntries - 1
                mapping.Entries(i) = reader.ReadRelationshipEntry()
            Next

            Return mapping
        End Function

        <Extension()>
        Function ReadRelationshipEntry(ByVal reader As BinaryReader) As RelationshipEntry
            Return New RelationshipEntry() With {
                .ForeignId = reader.ReadUInt32(),
                .RecordIndex = reader.ReadUInt32()
            }
        End Function
    End Module
End Namespace
