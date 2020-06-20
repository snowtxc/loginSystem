Imports MySql.Data.MySqlClient
Public Class FormLogin

    Dim con As MySqlConnection


    Sub New()

        InitializeComponent()


        con = New MySqlConnection

        con.ConnectionString = "server=localhost;database=usuario_roles;Uid=root;Pwd=;"



    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click


        Dim formregister As New FormRegister


        formregister.Show()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Label4.Text = ""

        If (txtUser.Text <> "" And txtPass.Text <> "") Then
            CheckUserExist(txtUser.Text, txtPass.Text, con)






        Else

            Label4.Text = "No puede haber campos vacios"

        End If







    End Sub

    Private Sub FormLogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtPass.PasswordChar = "*"
    End Sub


    Function CheckUserExist(ByVal user As String, ByVal pass As String, ByVal con As MySqlConnection)
        Dim cmd As New MySqlCommand
        Dim reader As MySqlDataReader


        Try
            con.Open()
            cmd.Connection = con
            cmd.CommandText = "SELECT Pass,Activo FROM usuarios WHERE Nombre=@nombre"
            cmd.Prepare()

            cmd.Parameters.AddWithValue("@nombre", user)



            reader = cmd.ExecuteReader()





            If (reader.HasRows) Then
                While reader.Read()
                    If (reader(0).ToString = pass) Then
                        If (reader(1).ToString = "True") Then
                            Dim FormMain As New frmMain
                            FormMain.Show()














                        Else
                            Label4.Text = "No puede ingresar por que tu estado es inactivo"



                        End If




                    Else
                            Label4.Text = "Contraseña incorrecta"

                    End If






                End While











            Else
                Label4.Text = "Nombre de usuario incorrecto "




            End If








            con.Close()


        Catch ex As Exception
            Label4.Text = "Error con la base de datos"
            con.Close()

        End Try






    End Function

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged


        If CheckBox1.Checked Then
            txtPass.PasswordChar = ""


        End If

        If CheckBox1.Checked = False Then
            txtPass.PasswordChar = "*"

        End If


    End Sub
End Class
