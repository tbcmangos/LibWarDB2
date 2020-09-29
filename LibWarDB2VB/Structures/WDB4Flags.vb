Imports System

Namespace LibWarDB2.Structures
    <Flags>
    Public Enum WDB4Flags ':ushort???
        None = &H0
        HasOffsetMap = &H1
        HasRelationshipData = &H2
        HasNonInlineIds = &H4
        IsBitpacked = &H10
    End Enum
End Namespace
