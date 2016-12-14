<?php
/*	Basic SHA512 Hash checking system for PHP and Visual Basic .NET
	Written by Dominick Lee (http://dominicklee.com)
	
	Disclaimer:
	This code is distributed "as is" and is licensed under the GNU AFFERO GENERAL PUBLIC LICENSE.
	We are not liable for anything you do with this code.
	
	Purpose: 
	-This is the PHP portion of the system, which serves to synchronize the time between the client and server. 
	-The local time is acquired by detecting the user's IP address and detecting the timezone from that.
	-The time is formatted as a string which is changes to a unique number every minute (can be changed to seconds).
	-The time is used as a basic salt added to the hash (or private key) on both PHP and VB.NET codes.
	
	Usage:
	-Modify the $rawString with your desired content (or private key)
	-Upload this file to your PHP enabled server. Remember to give it proper permissions.
	-Open the provided VB.NET program, and proceed with filling out the information.
	-You should receive a success message after clicking "Acquire PHP hash and match"
	-Changing the private key or salt on either ends would result in an error
	
	-If you want the time salt be be changed every second, change line 46 to:
	return date("YmdHis"); 	//Show the date in the format of year, month, day, hour, minute
	
	Security:
	-Although this system uses SHA512 algorithm, we strongly encourage you to use SSL encryption,
	 input filtering, injection protection, and a better salt method to protect yourself when officially deploying. 
	 
	We hope you enjoy!
*/


$rawString = "HelloWorld";	//Modify this with your desired content (or private key)

function getTime()
{
	$ip = $_SERVER['REMOTE_ADDR'];	//Get client's IP address here
	$query = @unserialize(file_get_contents('http://ip-api.com/php/'.$ip));	//Fetch information about our client IP here
	$timezn = "na";		//Prepare variable for storing timezone
	if($query && $query['status'] == 'success')		//If we successfully got the timezone
	{
		$timezn = $query['timezone'];		//Store the timezone
	}
	if ($timezn != "na")		//Check to see if timezone was acquired
		date_default_timezone_set($timezn);	//Set timezone

	return date("YmdHi"); 	//Show the date in the format of year, month, day, hour, minute
}


//Check what the user is calling for
if (isset($_GET['query']) && !empty($_GET['query']))	//Do something only if query has been specified
{
	if ($_GET['query'] == "time")	//If user is asking for the current time
	{
		echo getTime();
		
	}
	else if ($_GET['query'] == "hash")
	{
		$curTime = getTime();
		
		echo hash("sha512", $rawString.$curTime);
	}
	else
	{
		echo "Error: Invalid parameters";
		exit;
	}
}
else
{
	echo "Error: No parameters";
	exit;
}

?>