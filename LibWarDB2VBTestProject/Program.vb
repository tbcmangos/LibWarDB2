Imports System
Imports System.Text
Imports LibWarDB2VB.LibWarDB2
Imports LibWarDB2VB.LibWarDB2.Structures

Module Program
    Sub Main(args As String())

        Dim wdc = New WDC3()

        wdc.Load("GarrPlotUICategory.db2")
        ''might have multiple sections?
        'Dim sec As Section() = wdc.DataSections
        'For Each id In sec(0).IdList

        'Next

        'test change id

        wdc.DataSections(0).IdList(0) = 12

        Dim i As Integer = wdc.SectionHeaders(0).StringTableSize
        Dim sChar As Byte() = wdc.DataSections(0).StringData
        Dim s = Encoding.UTF8.GetString(sChar) 'vbNullChar & vbNullChar & "小型" & vbNullChar & "中型" & vbNullChar & "大型" & vbNullChar

        wdc.Save("GarrPlotUICategory.db2")
    End Sub
End Module
