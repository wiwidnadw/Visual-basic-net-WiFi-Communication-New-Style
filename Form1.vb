Imports System.IO
Imports System.Net.Sockets

Public Class Form1
    Dim client As TcpClient
    Dim Rx As StreamReader
    Dim Tx As StreamWriter
    Dim rawdata As String
    Dim dataseparate As Array



    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ToolStripStatusLabel1.Text = "Form Ready"
        CheckForIllegalCrossThreadCalls = False


    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        'connect'
        Try
            client = New TcpClient(IP_address.Text, "80")

            If client.GetStream.CanRead = True Or client.GetStream.CanWrite Then
                Rx = New StreamReader(client.GetStream)
                Tx = New StreamWriter(client.GetStream)
                Threading.ThreadPool.QueueUserWorkItem(AddressOf Connected)



            End If


        Catch ex As Exception
            MsgBox("Failed to connect")
        End Try

    End Sub

    Function Connected()
        If Rx.BaseStream.CanRead = True Then
            Try
                While Rx.BaseStream.CanRead = True
                    rawdata = Rx.ReadLine
                    dataseparate = rawdata.Split(","",")
                    Label2.Text = dataseparate(0)
                    Label5.Text = dataseparate(1)
                    Label7.Text = dataseparate(2)

                    'Threading.ThreadPool.QueueUserWorkItem(AddressOf MSG1, "Hello World.")

                    ToolStripStatusLabel1.Text = rawdata


                End While
            Catch ex As Exception
                client.Close()
            End Try
            Rx.DiscardBufferedData()
        End If

        Return True

    End Function
    Function MSG1(ByVal Data As String)
        REM Creates a messageBox for new threads to stop freezing
        ToolStripStatusLabel1.Text = Data
        Return True
    End Function
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        'disconnect'

        client.Close()
        Button4.Enabled = False

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'led 1'

        SendToServer("led1")


    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'led 2'

        SendToServer("led2")


    End Sub
    Function SendToServer(ByVal data As String)
        Try
            Tx.WriteLine(data)
            Tx.Flush()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        Return True
    End Function


End Class
