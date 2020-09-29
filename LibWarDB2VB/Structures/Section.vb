Namespace LibWarDB2.Structures
    Public Class Section
        Public Property Records As RecordData()
        Public Property StringData As Byte()
        Public Property VariableRecordData As Byte()
        Public Property IdList As UInteger()
        Public Property CopyTable As CopyTableEntry()
        Public Property OffsetMap As OffsetMapEntry()
        Public Property RelationshipMap As RelationshipMapping
        Public Property OffsetMapIdList As UInteger()
    End Class
End Namespace
