
Imports MySql.Data.MySqlClient
Public Class frmMain

    Dim conexion As MySqlConnection



    Sub New()


        InitializeComponent()

        conexion = New MySqlConnection



        conexion.ConnectionString = "server=localhost; database=usuario_roles;Uid=root;Pwd=;"


        '  If (rol = 1) Then
        '  Panel1.Visible = True
        'Panel2.Visible = False
        'ElseIf (rol = 2) Then
        '    Label4.Text = "Bienvenido "
        '
        '    Panel2.Visible = True
        '    Panel1.Visible = False
        '
        'ElseIf (rol = 3) Then
        '
        '    Label4.Text = "Bienvenido "
        '    Panel2.Visible = True
        '    Panel1.Visible = False
        '
        'Else
        '    MsgBox("error")
        '
        '
        'End If
        '


    End Sub



    Sub ActualizarSelect()
        Dim ds As DataSet = New DataSet
        Dim cmd As New MySqlCommand


        Dim adaptador As MySqlDataAdapter = New MySqlDataAdapter




        Try
            conexion.Open()

            cmd.Connection = conexion

            cmd.CommandText = "SELECT * FROM usuarios ORDER BY Nombre ASC"
            adaptador.SelectCommand = cmd
            adaptador.Fill(ds, "Tabla")
            DataGridView1.DataSource = ds
            DataGridView1.DataMember = "Tabla"

            conexion.Close()




        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

















    End Sub


    Private Sub DataGridView1_SelectionChanged(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick

        If (DataGridView1.SelectedRows.Count > 0) Then
            TextBox1.Text = DataGridView1.Item("Nombre", DataGridView1.SelectedRows(0).Index).Value
            TextBox2.Text = DataGridView1.Item("Pass", DataGridView1.SelectedRows(0).Index).Value
            TextBox4.Text = DataGridView1.Item("idUsuario", DataGridView1.SelectedRows(0).Index).Value
        End If
    End Sub

    Friend Sub Show()
        Throw New NotImplementedException()
    End Sub

    Friend Sub Show(v1 As Short, v2 As String)
        Throw New NotImplementedException()
    End Sub

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ActualizarSelect()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim nombre As String
        nombre = TextBox1.Text

        ActivarUsuario(nombre, conexion)

        ActualizarSelect()



    End Sub



    Private Sub ActivarUsuario(ByVal nombre As String, ByVal con As MySqlConnection)
        Dim Cmd As New MySqlCommand
        Dim reader As MySqlDataReader
        Try



            conexion.Open()

            Cmd.Connection = con

            Cmd.CommandText = "SELECT Activo FROM usuarios WHERE Nombre=@nombre AND Activo=1"

            Cmd.Prepare()


            Cmd.Parameters.AddWithValue("@nombre", nombre)


            reader = Cmd.ExecuteReader()

            If (reader.HasRows) Then
                MsgBox("El usuario seleccionado ya fue activado")
                reader.Close()



            Else
                reader.Close()

                Dim result As DialogResult = MessageBox.Show("Seguro que quiere activar a el usuario " & nombre & "?", "Activar", MessageBoxButtons.YesNo)


                If result = DialogResult.Yes Then


                    Cmd.CommandText = "UPDATE usuarios SET Activo=1 WHERE Nombre=@nombre"
                    Cmd.Prepare()


                    Cmd.Parameters.Clear()


                    Cmd.Parameters.AddWithValue("@nombre", nombre)






                    Cmd.ExecuteNonQuery()



                    MsgBox("Usuario Activado Corrrectamente")


                End If


            End If





        Catch ex As Exception
            MsgBox(ex.Message)

        End Try



        conexion.Close()


    End Sub


    Private Sub CambiarContraseña(ByVal NewPass As String, ByVal id As Integer, ByVal con As MySqlConnection, ByVal usu As String)



        Dim result As DialogResult = MessageBox.Show("Seguro que quieres cambiar la contraseña a el usuario " & usu & "?", "Cambiar contraseña", MessageBoxButtons.YesNo)

        If result = DialogResult.Yes Then

            Dim Cmd As New MySqlCommand


            Try
                conexion.Open()

                Cmd.Connection = con

                Cmd.CommandText = "UPDATE usuarios SET Pass=@pass WHERE idUsuario=@ID"

                Cmd.Prepare()



                Cmd.Parameters.AddWithValue("@pass", NewPass)

                Cmd.Parameters.AddWithValue("@ID", id)


                Cmd.ExecuteNonQuery()

                MsgBox("Contraseña cambiada correctamente")

            Catch ex As Exception

            End Try


        End If

        conexion.Close()







    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim Id As Integer
        Dim newPass As String

        Id = Integer.Parse(TextBox4.Text)
        newPass = TextBox3.Text

        CambiarContraseña(newPass, Id, conexion, TextBox1.Text)

        ActualizarSelect()







    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged

    End Sub

    Private Sub Panel2_Paint(sender As Object, e As PaintEventArgs) Handles Panel2.Paint

    End Sub
End Class