<?php

$servername = "localhost";
$_userDB= "root";
$_passwordDB = "root";
$database = "pokergameDB";

//Form data was sent with the POST method and is retrieved in PHP using POST.
$username = $_POST["username"];
$userpass = $_POST["userpass"];

//Create Connection
$conn = new mysqli($servername, $_userDB, $_passwordDB,$database);

// Validate Connection
if ($conn->connect_error) {
    die("404". $conn->connect_error . "Connection to database failed"  );
}

//Validate data
function clean_input($data){
    $data = trim($data);
    $data = stripslashes($data);
    $data = htmlspecialchars($data);
    return $data;
}

$username = clean_input($username);
$userpass = clean_input($userpass);

//// Data validation

//Password hashing
$securePass = password_hash($userpass,PASSWORD_DEFAULT);


//Now that we have the data, interact with the database
//We want to utilise INSERT INTO, in order to add new user to our database
//we will however first check if user already exists within the database, using SELECT
$sql = "SELECT username FROM users WHERE username = '$username'";


//Use SQL to query if it already exists
if ($conn->query($sql)===TRUE) {
    die( "301". " user already exists" );
} else {  //Use SQL to add that user to the Database
        $sql = "INSERT INTO users(username,password) VALUES ('$username','$securePass')";

        if ($conn->query($sql) === TRUE) {
            echo "0 successfully added a new user to database";
        }else{
            die ("402 ". "failed at this");
        }
}


//Prepared statements variation is more useful against SQL injections
//can be used for inserting, updating, maybe selecting



// Close connection
$conn -> close();

?>