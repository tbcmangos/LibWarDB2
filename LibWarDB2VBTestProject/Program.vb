Imports System
Imports System.Text
Imports LibWarDB2VB.LibWarDB2
Imports LibWarDB2VB.LibWarDB2.Structures

Module Program
    Sub Main(args As String())

        Dim wdc = New WDC3()

        wdc.Load("ChrClassTitle.db2")
        ''might have multiple sections?
        'Dim sec As Section() = wdc.DataSections
        'For Each id In sec(0).IdList

        'Next

        'test change id


        'wdc.DataSections(0).IdList(0) = 12

        Dim i As Integer = wdc.SectionHeaders(0).StringTableSize
        Dim sChar As Byte() = wdc.DataSections(0).StringData
        Dim s = Encoding.UTF8.GetString(sChar)
        'vbNullChar & vbNullChar & "小型" & vbNullChar & "中型" & vbNullChar & "大型" & vbNullChar
        'vbNullChar & vbNullChar & "战争领主" & vbNullChar & "大领主" & vbNullChar & "狩猎大师" & vbNullChar & "暗影之刃" & vbNullChar & "大祭司" & vbNullChar & "死亡领主" & vbNullChar & "先知" & vbNullChar & "大法师" & vbNullChar & "虚空领主" & vbNullChar & "大宗师" & vbNullChar & "大德鲁伊" & vbNullChar & "屠魔者" & vbNullChar

        Dim fInfo As FieldStorageInfo() = wdc.FieldInfo


        Dim sHeader As String = ""
        For i = 0 To fInfo.Count - 1
            Dim sizeByte As Integer = 0
            If fInfo(i).StorageType <> FieldCompression.None Then
                sizeByte = fInfo(i).FieldSizeBits
            Else
                sizeByte = fInfo(i).FieldSizeBits / 8
            End If

            sHeader += String.Format("Field{0}({1}/{2})", i, sizeByte, fInfo(i).StorageType.ToString) & vbTab
        Next
        Console.WriteLine(sHeader)

        Dim rd As RecordData() = wdc.DataSections(0).Records
        For Each r As RecordData In rd
            'need to split the length for each field


        Next

        wdc.Save("ChrClassTitle.db2")
    End Sub
End Module
