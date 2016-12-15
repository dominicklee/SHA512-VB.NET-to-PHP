# SHA512 VB.NET to PHP
An effective solution to match a SHA512 hash from VB.NET to PHP and vice-versa.

## Story
Several months ago, I learned about SHA-512 hashing algorithms, but I was not able to find any open-source implementation on getting a private key to match from Visual Basic .NET to a PHP application on my server. I wanted to securely send data to my server in as simple way as possible while using industry-standard cryptography algorithms. This is my first approach with SHA512.

## Purpose
This is the PHP portion of the system, which serves to synchronize the time between the client and server. The local time is acquired by detecting the user's IP address and detecting the timezone from that. The time is formatted as a string which is changes to a unique number every minute (can be changed to seconds).The time is used as a basic salt added to the hash (or private key) on both PHP and VB.NET codes.

## Usage
Usage instructions are also on both the PHP file and the VB.NET form code. Please read them carefully.

-Modify the $rawString (in hash.php) with your desired content (or private key)
-Upload this file to your PHP enabled server. Remember to give it proper permissions.
-Open the provided VB.NET program, and proceed with filling out the information.
-Click "Convert to SHA512" button. You should see the hashed string.
-You should receive a success message after clicking "Acquire PHP hash and match"
-Changing the private key or salt on either ends would result in an error

-If you want the time salt be be changed every second, change line 46 to:
```php
return date("YmdHis"); 	//Show the date in the format of year, month, day, hour, minute
```
## Requirements
To get the best out of this code, it is recommended you have PHP v5.3 or newer and Visual Basic 2008 or newer.
The VB.NET program is set up in a way that is ready to demonstrate the functionality. 

## Disclaimer
This code is distributed "as is" and is licensed under the GNU AFFERO GENERAL PUBLIC LICENSE.
We are not liable for anything you do with this code.

I hope this helps some developers to secure their apps. Enjoy!
