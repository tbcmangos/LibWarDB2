Namespace LibWarDB2.Structures
    Public Class WDC3Header
        Public Property Magic As UInteger
        Public Property RecordCount As UInteger
        Public Property FieldCount As UInteger
        Public Property RecordSize As UInteger
        Public Property StringTableSize As UInteger
        Public Property TableHash As UInteger
        Public Property LayoutHash As UInteger
        Public Property MinId As UInteger
        Public Property MaxId As UInteger
        Public Property Locale As UInteger
        Public Property Flags As WDB4Flags
        Public Property IdIndex As UShort
        Public Property TotalFieldCount As UInteger
        Public Property BitpackedDataOffset As UInteger
        Public Property LookupColumnCount As UInteger
        Public Property FieldStorageInfoSize As UInteger
        Public Property CommonDataSize As UInteger
        Public Property PalletDataSize As UInteger
        Public Property SectionCount As UInteger
    End Class
End Namespace
