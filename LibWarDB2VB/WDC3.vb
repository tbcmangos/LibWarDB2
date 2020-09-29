Imports System.IO
Imports LibWarDB2VB.LibWarDB2.Extensions
Imports LibWarDB2VB.LibWarDB2.Structures

Namespace LibWarDB2
    Public Class WDC3
        Public Const Signature As UInteger = 860046423
        Public Property Header As WDC3Header
        Public Property SectionHeaders As WDC3SectionHeader()
        Public Property Fields As FieldStructure()
        Public Property FieldInfo As FieldStorageInfo()
        Public Property PalletData As Byte()
        Public Property CommonData As Byte()
        Public Property DataSections As Section()

        Public Sub Load(ByVal path As String)
            Using reader = New BinaryReader(File.Open(path, FileMode.Open, FileAccess.Read))
                Dim wdc = reader.ReadWDC3()
                Header = wdc.Header
                SectionHeaders = wdc.SectionHeaders
                Fields = wdc.Fields
                FieldInfo = wdc.FieldInfo
                PalletData = wdc.PalletData
                CommonData = wdc.CommonData
                DataSections = wdc.DataSections
            End Using
        End Sub

        Public Sub Save(ByVal path As String)
            Using writer = New BinaryWriter(File.Open(path, FileMode.Create, FileAccess.Write))
                writer.Write(Me)
            End Using
        End Sub
    End Class
End Namespace
