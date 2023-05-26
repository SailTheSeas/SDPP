# SDPP
Software Project Backend Briefing


# Where to Navigate Scripts
The two files are the containers of all the scripts made:
* myphp
* MyUnityScripts 

# What is Within Those Scripts
Within the my php folder, the 3 files update.php, signup.php, and login.php are the only files needed to work with the database.
Within MyUnityScripts, the class files Sign Up and Log In allow for users to perform those actions. Records Link publishes changes to the database.
The static class DB Controller stores user records and a user log in status.
The class Input Validation checks and validates user input. 


# Setting Up PHP Server
The server was set up utilising XAMPP. 
You will thus need to install :
* PHP from php.net to run php, as well as any text editor
* To install Apache2, and MySQL
* To install XAMPP

# if on debian system
Step 1, download Xampp from the apache friends website
give the installer file read write priveledges. Use the command : sudo chmod 755 (installer-file-name.run)

Then you can install the xampp server with sudo ./(installer-file-name.run)

This opens the XAMPP setup wizard when complete. Utilise the directory opt/lampp as the place XAMPP will be installed to.
XAMPP is then finally installed onto the computer.

When finsihed and you launch XAMPP for the first time, click on manage servers.
Then start the Apache Web Server, and start the MySQL Database server.

If the servers are not able to start, it might mean that there is a pre-exisisting server installed on your pc. You will therefore only need to configure the port numbers to an unused port.
While the servers are running, you can now access them on the web browser by typing localhost. If the port is not the default, you will access localhost with the port you provided like localhost:81.

To access the MySQL database server, navigate to the PHPmyAdmin page.

The phpMyAdmin page is where you have control over the databases.

# executing php files on the server
In order to execute php files on the server, you need to navigate to the htdocs folder within the opt/lampp directory or whichever directory XAMPP was installed in. 
Within the htdocs folder is where php scripts can be saved. This will allow them to be run on the server by simply navigating to localhost/(the directory that leads to the php file name)

For example : localhost/phpFolder/helloworld.php

Access first needs to be given to the htdocs folder in older to create/read and execute files.
Using the command : sudo chown -R $USER:$USER (name-of-folder-containing php scripts)

# setting up the database on myphp Admin
all the php scripts have 
~~~
// Variables storing DB information

$servername = "localhost";
$_userDB= "root";
$_passwordDB = "root";
$database = "pokergameDB";

//Create Connection

$conn = new mysqli($servername, $_userDB, $_passwordDB,$database);
~~~
In order to properly run the php scripts on the server, some setup needs to made on the myphpAdmin page. 
To do this navigate to the user accounts page and make sure that the user with username "root", hostname "localhost", has passwords set to Yes and then make the password equal to root. Making this change will cause an error with the configuration file and a fix is mentioned in a section below. However the idea is to make it so that the php files connect to the database on the local host in accordance with the code. Meaning that as part of a local setup, a database will need to be either imported or created with name "pokergameDB."

# fixing access to phpmyAdmin Error after password change
The cause of this error is due to the change of the localhost root user without adjusting it inside the configuration files. To fix this, you will need to navigate to where XAMPP was installed, then to the folder phpmyadmin. Within this folder, make sure you have -write priveledges on the file "config.inc.php". 
* Look for a block of code like below : 
~~~
/* Authentication type */
$cfg['Servers'][$i]['auth_type'] = 'config';
$cfg['Servers'][$i]['user'] = 'root';
$cfg['Servers'][$i]['password'] = 'root';
/* Server parameters */
//$cfg['Servers'][$i]['host'] = 'localhost';
$cfg['Servers'][$i]['compress'] = false;
$cfg['Servers'][$i]['AllowNoPassword'] = true;
~~~
What needs to be done is to simply change the password to what it was changed to which in this case was root. Then the error should disappear as the configuration files match. 

# turning on the server upon return
The server needs to be turned on everytime in order to utilise it. 
To do this, you need to navigate to the directory XAMPP is installed. This should be the opt/lampp directory.
After which run the the manager-linux-x64.run file with : sudo./(filename.run)

This will return the wizard with which you can use to turn back on the servers, and access them.












