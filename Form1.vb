Imports System.Data.SqlClient


Public Class Login




    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        TextBox2.PasswordChar = "*"c
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked Then
            TextBox2.PasswordChar = ""
        Else
            TextBox2.PasswordChar = "*"c
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim username As String = TextBox1.Text.Trim()
        Dim password As String = TextBox2.Text.Trim()

        Dim connectionString As String = "Server=.\SQLEXPRESS01;Database=GrillMate;Trusted_Connection=True;"
        Dim query As String = "SELECT Role FROM gUser WHERE [Username] = @Username AND [Password] = @Password"

        Using conn As New SqlConnection(connectionString)
            Using cmd As New SqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@Username", username)
                cmd.Parameters.AddWithValue("@Password", password)

                Try
                    conn.Open()
                    Dim roleObj As Object = cmd.ExecuteScalar()

                    If roleObj IsNot Nothing Then
                        Dim role As String = roleObj.ToString().ToLower()

                        MessageBox.Show("Login successful!", "Successful!", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Me.Hide()

                        If role.Trim().ToLower() = "admin" Then
                            adminForm.Show()
                        ElseIf role.Trim().ToLower() = "staff" Then
                            staffForm.Show()
                        Else
                            MessageBox.Show("Unknown role: " & role, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End If
                    Else
                        Dim msgResult As DialogResult = MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)

                        If msgResult = DialogResult.Retry Then

                            TextBox1.Clear()
                            TextBox2.Clear()
                            TextBox1.Focus()
                        ElseIf msgResult = DialogResult.Cancel Then
                            Application.Exit()
                        End If
                    End If
                Catch ex As Exception
                    MessageBox.Show("Database error: " & ex.Message, "ERROR", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
                End Try
            End Using
        End Using

    End Sub



    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Application.Exit()
    End Sub

  




End Class


