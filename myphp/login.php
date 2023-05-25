<?php
$servername = "localhost";
$_userDB= "root";
$_passwordDB = "root";
$database = "pokergameDB";
$username = $_POST["username"];
$userpass = $_POST["userpass"];

//Create Connection
$conn = new mysqli($servername, $_userDB, $_passwordDB,$database);

// Validate Connection
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
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

//Get information from the Unity form
//Create variables that will store the $_POSTED FIELD from the unity form.
//Form data was sent with the POST method and is retrieved in PHP using POST.


//Now that we have the data, interact with the database
//We want to utilise INSERT INTO, in order to add new user to our database
//we will however first check if user already exists within the database, using SELECT
$sql = "SELECT * FROM users WHERE username = '$username'";

$results = $conn->query($sql);

//Use SQL to query if it already exists
if ($results->num_rows == 1 ){  /* the function num rows checks to see if only one row
     for our desired user is found*/

    $row = $results->fetch_assoc(); //fetch assoc puts all the results into an associative array
    if ( password_verify($userpass,$row["password"])===TRUE){
        echo "0. Successful login as password matches". "\t";
        echo $row["userID"]. "\t";
        echo $row["NoGames"] ."\t";
        echo $row["NoWins"] ;
    } else{
        die("403". "actual password is =  ". $row["password"] . " " . $row["userID"]);
    }
} else{
    die("302". "This user does not exist");
}


// Close connection
$conn -> close();

?>