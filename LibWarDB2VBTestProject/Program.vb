Imports System
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
        wdc.Save("GarrPlotUICategory.db2")
    End Sub
End Module
