<?php


$servername = "localhost";
$_userDB = "root";
$_passwordDB = "root";
$database = "pokergameDB";

//Only need username to pull from.
$username = $_POST["username"];
$updatedPlayed = $_POST["played"];
$updatedWins = $_POST["won"];

//Create connection
$conn = new mysqli($servername, $_userDB, $_passwordDB, $database);

//Access player records in database
$sql = "=UPDATE users SET NoGames = '$updatedPlayed', NoWins = '$updatedWins' WHERE username = '$username'";
//$sql = "SELECT username FROM users WHERE username = '".$username."';"; //Proper way to write

$results = $conn->query($sql);

if ($results === TRUE) {
    echo "data successfully updated";
} else {
    die("405" . "This data could not be updated");
}
