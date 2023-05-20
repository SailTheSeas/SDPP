
<?php

$servername = "localhost";
$_userDB= "root";
$_passwordDB = "root";
$database = "pokergameDB";
$username = $_POST["username"];
$userpass = $_POST["userpass"];

// Run on browser
//echo "Hello World, this will be the stage for the Poker game. ";

//Create Connection

$conn = new mysqli($servername, $_userDB, $_passwordDB,$database);

// Validate Connection

if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
}
echo "Connected successfully to Database " . $conn->server_info;

//Get information from the Unity form
//Create variables that will store the $_POSTED FIELD from the unity form.
//Form data was sent with the POST method and is retrieved in PHP using POST.



//Now that we have the data, interact with the database
//We want to utilise INSERT INTO, in order to add new user to our database
//we will however first check if user already exists within the database, using SELECT
$sql = "SELECT username FROM users WHERE username = '$username'";


//Use SQL to query if it already exists
if ($conn->query($sql)===TRUE) {
    echo "user already exists";
} else {
//Use SQL to add that user to the Database

    $sql = "INSERT INTO users(username,password) VALUES ('$username','$userpass')";
    if ($conn->query($sql) === TRUE) {
        echo "successfully added a new user to database";
    }else{
        echo "failed at this";
    }
}



/*Now,consider a method in which you break up the user password
Into two things, and hash it to make it more secure
*/


// Close connection
$conn -> close()

?>