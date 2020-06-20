
Imports MySql.Data.MySqlClient
Public Class FormRegister

    Dim con As MySqlConnection


    Sub New()


        InitializeComponent()

        con = New MySqlConnection

        con.ConnectionString = "server=localhost; database=usuario_roles;Uid=root;Pwd=;"



    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click



        Label4.Text = ""
        Labeldifferentpass.Text = ""
        Label6.Text = ""
        If (txtUser.Text <> "" And txtPass.Text <> "" And txtRepeatPass.Text <> "") Then

            If (txtPass.Text.Length >= 6) Then
                If (txtPass.Text = txtRepeatPass.Text) Then

                    If (CheckUsuarioYaRegistrado(txtUser.Text, con)) Then
                        Label4.Text = "Ya existe un nombre de usuario con este nombre"
                    Else
                        RegistrarUsuario(txtUser.Text, txtPass.Text, con)




                    End If

                Else

                    Labeldifferentpass.Text = "No coincide con la contraseña"


                End If

            Else
                Label6.Text = "Minimo 6 digitos"
            End If












        Else
                Label4.Text = "No pueden haber campos vacios"


        End If









    End Sub


    Function CheckUsuarioYaRegistrado(ByVal nombre As String, ByVal con As MySqlConnection)
        Dim resultado As Boolean

        Dim Cmd As New MySqlCommand
        Dim reader As MySqlDataReader



        Try
            con.Open()
            Cmd.Connection = con

            Cmd.CommandText = "SELECT Nombre FROM usuarios WHERE Nombre=@nombre"

            Cmd.Prepare()

            Cmd.Parameters.AddWithValue("@nombre", nombre)


            reader = Cmd.ExecuteReader()



            If (reader.HasRows) Then
                MsgBox("Este nombre de usuario ya existe")


            Else
                resultado = False



            End If




            con.Close()


            Return resultado







        Catch ex As Exception
            MsgBox("Error al conectar a la base de datos")
        End Try


    End Function




    Private Sub RegistrarUsuario(ByVal nombre As String, ByVal pass As String, ByVal con As MySqlConnection)
        Dim Cmd As New MySqlCommand


        Try
            con.Open()

            Cmd.Connection = con

            Cmd.CommandText = "INSERT INTO usuarios(Nombre,Pass,idRol,Activo) VALUES(@nombre,@pass,@rol,@active)"

            Cmd.Prepare()

            Cmd.Parameters.AddWithValue("@nombre", nombre)
            Cmd.Parameters.AddWithValue("@pass", pass)
            Cmd.Parameters.AddWithValue("@rol", 3)
            Cmd.Parameters.AddWithValue("@active", 0)

            Cmd.ExecuteNonQuery()


            con.Close()

            MsgBox("Usuario registrado correctamente")
            txtUser.Text = ""
            txtPass.Text = ""
            txtRepeatPass.Text = ""



        Catch ex As Exception

            Label4.Text = "Se produzco un error al registrar el usuario"

        End Try







    End Sub

    Private Sub FormRegister_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtPass.PasswordChar = "*"
        txtRepeatPass.PasswordChar = "*"
    End Sub



    Private Sub CheckBox1_CheckedChanged_1(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked Then
            txtPass.PasswordChar = ""
            txtRepeatPass.PasswordChar = ""


        End If

        If CheckBox1.Checked = False Then
            txtPass.PasswordChar = "*"
            txtRepeatPass.PasswordChar = "*"

        End If

    End Sub
End Class