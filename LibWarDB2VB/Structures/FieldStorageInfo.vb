Imports System.Runtime.InteropServices

Namespace LibWarDB2.Structures
    <StructLayout(LayoutKind.Sequential)>
    Public Class FieldStorageInfo
        Public Property FieldOffsetBits As UShort
        Public Property FieldSizeBits As UShort
        Public Property AdditionalDataSize As UInteger
        Public Property StorageType As FieldCompression
        Public Property Value1 As UInteger
        Public Property Value2 As UInteger
        Public Property Value3 As UInteger
    End Class
End Namespace
