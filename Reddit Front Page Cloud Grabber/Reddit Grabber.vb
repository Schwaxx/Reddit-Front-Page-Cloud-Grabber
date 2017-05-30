Imports System.Net
Imports System.IO
Public Class Main
    Private Sub Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Date.Today > My.Settings.LastUpdate Then
            Dim wrRequest As WebRequest
            Dim wrResponse As String
            Do Until wrResponse <> ""
                Try
                    wrRequest = HttpWebRequest.Create("https://www.reddit.com/")
                    wrResponse = New StreamReader(wrRequest.GetResponse().GetResponseStream).ReadToEnd
                Catch ex As Exception
                    Threading.Thread.Sleep(3000)
                End Try
            Loop
            Dim delim As String() = New String(0) {"data-outbound-expiration="}
            Dim returnValue As String = vbNewLine
            For i = 0 To wrResponse.Split(delim, StringSplitOptions.None).Count - 1
                If Not i \ 2 = 0 Then
                    Dim tempText As String = wrResponse.Split(delim, StringSplitOptions.None)(i)
                    tempText = tempText.Substring(tempText.IndexOf(">") + 1, tempText.IndexOf("<") - tempText.IndexOf(">") - 1)
                    returnValue += tempText & vbNewLine
                End If
            Next
            My.Computer.FileSystem.WriteAllText("C:\Users\SWB\Documents\list.txt", returnValue, True)
            My.Settings.LastUpdate = Date.Today
            My.Settings.Save()
        End If
        Me.Close()
    End Sub
End Class
