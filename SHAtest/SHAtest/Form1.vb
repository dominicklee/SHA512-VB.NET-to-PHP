'	Basic SHA512 Hash checking system for PHP and Visual Basic .NET
'	Written by Dominick Lee (http://dominicklee.com)

'	Disclaimer:
'	This code is distributed "as is" and is licensed under the GNU AFFERO GENERAL PUBLIC LICENSE.
'	We are not liable for anything you do with this code.

'	Purpose: 
'	-This is the VB.NET portion of the system, which serves to verify the hash between the client and server. 
'	-The local time is acquired by querying the server's PHP file for the string.
'	-The time is formatted as a string which is changes to a unique number every minute (can be changed to seconds).
'	-The time is used as a basic salt added to the hash (or private key) on both PHP and VB.NET codes.

'	Usage:
'   -Before using this program, make sure you have already uploaded the provided PHP file on your server.
'   -Try running the program and filling out the fields.
'   -Click "Convert to SHA512"
'   -Specify the server path where you uploaded the PHP file
'   -Click "Acquire PHP Hash and math"
'	-You should receive a success message after clicking "Acquire PHP hash and match"
'	-Changing the private key or salt on either ends would result in an error

'	-If you want the time salt be be changed every second, change line 46 to:
'	return date("YmdHis"); 	//Show the date in the format of year, month, day, hour, minute

'	Security:
'	-Although this system uses SHA512 algorithm, we strongly encourage you to use SSL encryption,
'	 input filtering, injection protection, and a better salt method to protect yourself when officially deploying. 

'	We hope you enjoy!


Imports System.Text

Public Class Form1

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If (TextBox3.Text.StartsWith("http") = False Or TextBox3.Text.EndsWith(".php") = False) Then
            MsgBox("Please enter the URL to the provided PHP script.")
        Else
            Try
                'Accquire time string from PHP script
                Dim timeString As String = New System.Net.WebClient().DownloadString(TextBox3.Text & "?query=time")
                'The time string URL would be something like:  http://your-domain.com/hash.php?query=time

                TextBox2.Text = HexHash(TextBox1.Text)
                TextBox4.Text = HexHash(TextBox1.Text & timeString)
            Catch
                MsgBox("Failed to communicate to server")
            End Try
        End If
    End Sub

    Public Shared Function HexHash(ByVal clearText As String) As String
        Dim hashedBytes As Byte() = computeHash(clearText)

        ' Build the hex string by converting each byte.
        Dim hexString As New System.Text.StringBuilder()
        For i As Int32 = 0 To hashedBytes.Length - 1
            hexString.Append(hashedBytes(i).ToString("x2")) ' Use "x2" for lower case
        Next

        Return hexString.ToString()
    End Function

    Private Shared Function computeHash(ByVal clearText As String) As Byte()

        Dim encoder As New System.Text.UTF8Encoding
        Dim sha512hasher As New System.Security.Cryptography.SHA512Managed()
        Return sha512hasher.ComputeHash(encoder.GetBytes(clearText))

    End Function

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If (TextBox3.Text.StartsWith("http") = False Or TextBox3.Text.EndsWith(".php") = False) Then
            MsgBox("Please enter the URL to the provided PHP script.")
        Else
            Try
                'Accquire hash string from PHP script
                Dim hashString As String = New System.Net.WebClient().DownloadString(TextBox3.Text & "?query=hash")
                'The time string URL would be something like:  http://your-domain.com/hash.php?query=hash
                TextBox5.Text = hashString
            Catch
                MsgBox("Failed to communicate to server")
            End Try
        End If

        'Lets check the local hashString with the web hashString
        If TextBox4.Text = TextBox5.Text Then
            MsgBox("Your SHA512 hashes match successfully!")
        Else
            MsgBox("Your SHA512 hashes do not match.")
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        TextBox2.Clear()
        TextBox4.Clear()
        TextBox5.Clear()
    End Sub
End Class
