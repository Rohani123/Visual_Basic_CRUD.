Imports System.Data.Odbc
Public Class Form1
    Sub TampilGrid()
        bukakoneksi()

        DA = New OdbcDataAdapter("select * From tbl_mhs", CONN)
        DS = New DataSet
        DA.Fill(DS, "tbl_mhs")
        DataGridView1.DataSource = DS.Tables("tbl_mhs")

        tutupkoneksi()
    End Sub
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        MunculCombo()
        TampilGrid()
    End Sub
    Sub MunculCombo()
        ComboBox.Items.Add("Ilmu Komputer")
        ComboBox.Items.Add("Kimia")
        ComboBox.Items.Add("Fisika")
        ComboBox.Items.Add("Matematika")

    End Sub

    Sub KosongkanData()
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Or TextBox4.Text = "" Then
            MsgBox("Silahkan Isi Semua Form")
        Else
            bukakoneksi()
            Dim simpan As String = "insert into tbl_mhs values ('" & TextBox1.Text & "','" & TextBox2.Text & "','" & TextBox3.Text & "','" & TextBox4.Text & "', '" & ComboBox.Text & "')"

            CMD = New OdbcCommand(simpan, CONN)
            CMD.ExecuteNonQuery()
            MsgBox("Input data berhasil")
            TampilGrid()
            KosongkanData()

            tutupkoneksi()
        End If
    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        TextBox1.MaxLength = 6
        If e.KeyChar = Chr(13) Then
            bukakoneksi()
            CMD = New OdbcCommand("Select * From tbl_mhs where nim_mhs='" & TextBox1.Text & "'", CONN)
            RD = CMD.ExecuteReader
            RD.Read()
            If Not RD.HasRows Then
                MsgBox("NIM Tidak Ada, Silahkan coba lagi!")
                TextBox1.Focus()
            Else
                TextBox2.Text = RD.Item("nama_mhs")
                TextBox3.Text = RD.Item("alamat_mhs")
                TextBox4.Text = RD.Item("telepon_mhs")
                ComboBox.Text = RD.Item("jurusan_mhs")
                TextBox2.Focus()
            End If
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        bukakoneksi()
        Dim edit As String = "UPDATE tbl_mhs SET nama_mhs='" & TextBox2.Text & "', alamat_mhs='" & TextBox3.Text & "', telepon_mhs='" & TextBox4.Text & "', jurusan_mhs='" & ComboBox.Text & "' WHERE nim_mhs='" & TextBox1.Text & "'"

        CMD = New OdbcCommand(edit, CONN)
        CMD.ExecuteNonQuery()
        MsgBox("Data Berhasil diUpdate")
        TampilGrid()
        KosongkanData()
        tutupkoneksi()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If TextBox1.Text = "" Then
            MsgBox("Silahkan Pilih data yang akan di hapus dengan Masukkan NIM dan ENTER")
        Else
            If MessageBox.Show("Yakin akan dihapus..?", "", MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                bukakoneksi()
                Dim hapus As String = "delete From tbl_mhs where nim_mhs='" & TextBox1.Text & "'"
                CMD = New OdbcCommand(hapus, CONN)
                CMD.ExecuteNonQuery()
                TampilGrid()
                KosongkanData()
                tutupkoneksi()
            End If
        End If
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub
End Class
