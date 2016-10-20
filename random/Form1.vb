Imports System
Imports System.IO

Public Class Form1

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'コントロールを初期化する
        ProgressBar1.Minimum = 0
        ProgressBar1.Maximum = TextBox4.Text
        ProgressBar1.Value = 0
        Dim DirBox1 As String = TextBox1.Text

        '入力チェックを行う
        If Not System.IO.Directory.Exists(TextBox1.Text) Then Exit Sub
        If Not IsNumeric(TextBox3.Text) Then Exit Sub
        If Not IsNumeric(TextBox4.Text) Then Exit Sub
        If ComboBox1.SelectedIndex = -1 Then Exit Sub
        '生成ファイルサイズを取得する
        Dim sz As Integer = Val(TextBox3.Text) * 1024 ^ ComboBox1.SelectedIndex
        '生成個数を取得する
        Dim cnt As Integer = Val(TextBox4.Text)

        '生成個数分くりかえす
        For i As Integer = 1 To cnt
            '一時ファイルを作成し、フルパス名を取得する
            Dim tmpName As String = System.IO.Path.GetTempFileName()
            '生成した一時ファイルのファイルサイズを変更する
            Dim f As New System.IO.FileStream(tmpName, IO.FileMode.Open)
            f.SetLength(sz)
            f.Flush()
            f.Close()
            '生成した一時ファイルを作業フォルダに移動する
            System.IO.File.Move(tmpName, System.IO.Path.Combine(TextBox1.Text, System.IO.Path.GetFileName(tmpName)))
        Next

        If CheckBox1.Checked = True Then
            For Each tempFile As String In System.IO.Directory.GetFiles(DirBox1)
                System.IO.File.Delete(tempFile)
                ProgressBar1.Value = cnt
            Next
        Else

        End If

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim dlg As New FolderBrowserDialog
        'FolderBrowserDialogを用いて作業フォルダを設定する
        dlg.SelectedPath = TextBox1.Text
        If dlg.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            TextBox1.Text = dlg.SelectedPath + "\"
        End If
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim stCurrentDir As String = System.IO.Directory.GetCurrentDirectory()
        TextBox1.Text = stCurrentDir + "\" + "Dust" + "\"
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged

    End Sub
End Class
