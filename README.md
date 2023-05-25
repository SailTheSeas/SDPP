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
The server was set up utilising LAMPP. 
You will thus need to install :
* PHP from php.net to run php, as well as any text editor
* To install Apache2, and MySQL
* To install LAMPP

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

# turning on the server upon return
The server needs to be turned on everytime int











