Imports System.Data.SqlClient


Public Class Form1



    Dim connectionString As String = "Data Source=.\SQLEXPRESS01;Initial Catalog=GrillMate;Integrated Security=True"
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
        Dim query As String = "SELECT COUNT(*) FROM fUser WHERE [User] = @Username AND [Password] = @Password"

        Using conn As New SqlConnection(connectionString)
            Using cmd As New SqlCommand(query, conn)

                cmd.Parameters.AddWithValue("@Username", username)
                cmd.Parameters.AddWithValue("@Password", password)

                Try
                    conn.Open()
                    Dim result As Integer = Convert.ToInt32(cmd.ExecuteScalar())

                    If result > 0 Then
                        MessageBox.Show("Login successful!", "Successful!", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Me.Hide()
                        Form2.Show()
                    Else
                        MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
                    End If

                Catch ex As Exception
                    MessageBox.Show("Database error: ", "ERROR", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
                End Try
            End Using
        End Using
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Application.Exit()
    End Sub
End Class
