Public Class Form1
    ' The master keys from the original reverse-engineered algorithm.
    Private ReadOnly professionals_code As ULong() = {&H10051981, &H4011995, &H2061997, &H12091999, &H16062004, &H21042002, &H13062004, &H9112005, &H24112005}

    ' Helper function to shift 8 and B characters in the generated code.
    Private Function Shift8b(ByVal input As String) As String
        Dim result As New System.Text.StringBuilder()
        For Each c As Char In input
            Select Case Char.ToUpper(c)
                Case "8"c
                    result.Append("B"c)
                Case "B"c
                    result.Append("8"c)
                Case Else
                    result.Append(c)
            End Select
        Next
        Return result.ToString()
    End Function

    ' The main function to get the registration codes.
    Private Function GetCode(ByVal name As String) As String()
        If String.IsNullOrWhiteSpace(name) Then
            Return New String() {}
        End If

        Dim codes As New System.Collections.Generic.List(Of String)()
        Dim length As Integer = name.Length
        Dim v As Integer = length
        Dim code As ULong = &H47694C

        name = name.ToUpper()

        For i As Integer = 0 To length - 1
            If (i Mod 14) = 0 Then
                v = 39
            End If
            code += CULng(v * Asc(name(i)))
            v *= IIf((i + 3) Mod 14, 3, 7)
        Next

        For Each profCode As ULong In professionals_code
            codes.Add(Shift8b(Hex(code + profCode).ToUpper()))
        Next

        Return codes.ToArray()
    End Function

    ' Event handler for the Generate button click.
    Private Sub GenerateButton_Click(sender As Object, e As EventArgs) Handles GenerateButton.Click
        ' Clear the output textbox before generating new keys.
        OutputTextBox.Text = ""

        Dim keys As String() = GetCode(usertxtbox.Text)
        If keys.Length > 0 Then
            For Each key As String In keys
                OutputTextBox.AppendText(key & Environment.NewLine)
            Next
        End If
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Try
            ' Navigate to the specified URL when the link is clicked.
            System.Diagnostics.Process.Start("https://github.com/KhalidYT11")
        Catch ex As Exception
            ' Show an error message if the process can't be started.
            MessageBox.Show("Could not open the link. Please visit https://github.com/KhalidYT11 manually.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub LinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        Try
            ' Navigate to the specified URL when the link is clicked.
            System.Diagnostics.Process.Start("https://www.youtube.com/@MrKaminK11")
        Catch ex As Exception
            ' Show an error message if the process can't be started.
            MessageBox.Show("Could not open the link. Please visit https://www.youtube.com/@MrKaminK11 manually.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub


End Class