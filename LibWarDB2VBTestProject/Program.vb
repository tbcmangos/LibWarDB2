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
        'vbNullChar & vbNullChar & "С��" & vbNullChar & "����" & vbNullChar & "����" & vbNullChar
        'vbNullChar & vbNullChar & "ս������" & vbNullChar & "������" & vbNullChar & "���Դ�ʦ" & vbNullChar & "��Ӱ֮��" & vbNullChar & "���˾" & vbNullChar & "��������" & vbNullChar & "��֪" & vbNullChar & "��ʦ" & vbNullChar & "�������" & vbNullChar & "����ʦ" & vbNullChar & "���³��" & vbNullChar & "��ħ��" & vbNullChar

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
