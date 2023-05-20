<?php

$servername = "localhost";
$_userDB= "root";
$_passwordDB = "root";
$database = "pokergameDB";
$username = "koikoi";
$userpass = "password";

//Create Connection
$conn = new mysqli($servername, $_userDB, $_passwordDB,$database);

// Validate Connection
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
}


//$sql = "SELECT username FROM users WHERE username = '$username'";
// MAYBE THE OUPUT IS LITERALLY JUST THE USERNAME TABLE
$sql = "SELECT * FROM users WHERE username = '".$username."';";

$results = $conn->query($sql);


//Use SQL to query if it already exists
if ($results->num_rows > 0 ){  /* the function num rows checks to see if only one row
     for our desired user is found*/

    $row = $results->fetch_assoc(); //fetch assoc puts all the results into an associative array
    echo "Username " . $row['username']  ;
    echo "any limits" . " Password " . $row['password'];

    if ($userpass == $row['password']){
        echo "0. Successful login as password matches";
    } else{
       die("this was the " . $row['userID']);
    }
} else{
    die("302". "This user does not exist");
}


/*Now,consider a method in which you break up the user password
Into two things, and hash it to make it more secure
*/


// Close connection
$conn -> close();

