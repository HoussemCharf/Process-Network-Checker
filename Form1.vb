Imports System.IO

Public Class Form1
    Dim l As New List(Of Integer)()
    Dim pl As New List(Of String)()
    Private Sub doo()
        Dim proc As ProcessStartInfo = New ProcessStartInfo("cmd.exe")
        Dim pr As Process
        Dim ch As String



        proc.CreateNoWindow = True
        proc.Verb = "runas"
        proc.UseShellExecute = False
        proc.RedirectStandardInput = True
        proc.RedirectStandardOutput = True
        pr = Process.Start(proc)
        pr.StandardInput.WriteLine("cls")
        pr.StandardInput.WriteLine("netstat  -n -o & pause")
        pr.StandardInput.Close()

        Dim r As StreamReader = pr.StandardOutput
        Dim line As String
        line = r.ReadLine()
        TextBox1.Text = ""


        Do

            If line.Contains("ESTABLISHED") Then
                ch = line.Substring(line.IndexOf("ESTABLISHED"), line.Length - line.IndexOf("ESTABLISHED"))
                ch = ch.Trim()
                ch = ch.Remove(0, 16)

                If Not (l.Contains(CInt(ch))) Then

                    If CInt(ch) > 1000 Then
                        l.Add(CInt(ch))
                    End If
                End If

            End If
            line = r.ReadLine()
        Loop Until line.Contains("Press") Or line.Contains("touche")

        pr.StandardOutput.Close()
        Dim pr1 As Process = Process.Start(proc)
        pr1.StandardInput.WriteLine("cls")
        pr1.StandardInput.WriteLine("tasklist & pause")
        pr1.StandardInput.Close()
        Dim b As StreamReader = pr1.StandardOutput
        line = b.ReadLine()

        Do

            If Test(l, line) Then
                pl.Add(line.Remove(line.IndexOf(".")))
                TextBox1.Text = line.Remove(line.IndexOf(".")) & "  PID=" & CStr(l.ElementAt(Pid(l, line))) & vbCrLf & TextBox1.Text

            End If

            line = b.ReadLine()
        Loop Until line.Contains("Press") Or line.Contains("touche")
        checkme(pl, CheckedListBox1)
        pr1.StandardOutput.Close()
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click


        doo()


    End Sub

    Private Function Test(ByVal L As List(Of Integer), ByVal ch As String) As Boolean
        Dim R As Boolean = False
        For Each c As Integer In L
            If ch.Contains(CStr(c)) Then
                R = True
            End If
        Next
        Return R
    End Function
    Private Function Pid(ByVal L As List(Of Integer), ByVal ch As String) As Integer

        Dim pos As Integer = 0

        For Each c In L
            If ch.Contains(CStr(c)) Then
                pos = L.IndexOf(c)

            End If
        Next
        Return pos

    End Function
    Private Sub checkme(ByVal l As List(Of String), ByRef c As CheckedListBox)
        c.Items.Clear()


        Dim ch As String
        For Each ch In l
            c.Items.Add(ch)

        Next
        l.Clear()
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        End
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim proc As ProcessStartInfo = New ProcessStartInfo("cmd.exe")
        Dim pr As Process
        Dim t As New List(Of Integer)()
        Dim mee As String

        If TextBox1.Text = "" Then
            MessageBox.Show("find them before you kill them :P")



        Else

            proc.CreateNoWindow = True
            proc.Verb = "runas"
            proc.UseShellExecute = False
            proc.RedirectStandardInput = True
            proc.RedirectStandardOutput = True
            pr = Process.Start(proc)

            For Each item In CheckedListBox1.CheckedItems
                For Each mee In TextBox1.Text.Split(vbNewLine)
                    If mee.Contains(item.ToString) Then
                        t.Add(CInt(mee.Substring(mee.IndexOf("=") + 1)))
                    End If
                Next
            Next


            For Each c As Integer In t
                pr.StandardInput.WriteLine("Taskkill /PID " & CStr(c))
            Next

            pr.StandardInput.Close()
            doo()
        End If
    End Sub






    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        MessageBox.Show("                          All Right Reserved © 2016 - Houssem Charfeddine" & vbCrLf & "this app was created by Houssem Charfeddine in order to help people check their use of internet and to keep track on active process using it in the background all you need to do is press a button and voila")
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        Dim i As Integer
        If Not (TextBox1.Text = "") Then
            If CheckBox1.Checked Then
                For i = 0 To CheckedListBox1.Items.Count() - 1

                    CheckedListBox1.SetItemChecked(i, True)
                Next
            Else

                For i = 0 To CheckedListBox1.Items.Count() - 1

                    CheckedListBox1.SetItemChecked(i, False)
                Next

            End If
        End If
    End Sub
End Class
