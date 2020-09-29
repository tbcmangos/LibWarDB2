Imports System
Imports LibWarDB2VB.LibWarDB2
Imports LibWarDB2VB.LibWarDB2.Structures

Module Program
    Sub Main(args As String())

        Dim wdc = New WDC3()

        wdc.Load("GarrPlotUICategory.db2")
        wdc.Save("GarrPlotUICategory.db2")
    End Sub
End Module
