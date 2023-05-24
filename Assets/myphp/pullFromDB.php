<?php

$servername = "localhost";
$_userDB= "root";
$_passwordDB = "root";
$database = "pokergameDB";

//Only need username to pull from.
$username = $_POST["username"];

//Create connection
$conn = new mysqli($servername, $_userDB, $_passwordDB,$database);

//Access player records in database
$sql = "SELECT * FROM users WHERE username = '$username'";
//$sql = "SELECT username FROM users WHERE username = '".$username."';"; //Proper way to write

$results = $conn->query($sql);

if ($results->num_rows == 1 ){  /* the function num rows checks to see if only one row
     for our desired user is found*/

    $row = $results->fetch_assoc(); //fetch assoc puts all the results into an associative array
    echo $row["userID"];
    echo $row["NoGames"] ;
    echo $row["NoWins"] ;

    //will need to retrieve this data from our PHP page
} else{
    die("302". "This user does not exist");
}
