<?php

require 'ConnectionSettings.php';

//vars sumbitted by user
$itemID = $_POST["itemID"];
$userID = $_POST["userID"];
$ID = $_POST["ID"];

// Check connection
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
}

//first sql
$sql = "SELECT price FROM items WHERE ID = '". $itemID . "'";

$result = $conn->query($sql);

if ($result->num_rows > 0) {
    //store item price
    $itemPrice = $result->fetch_assoc()["price"];

    //second sql (delete item)
    $sql2 = "DELETE FROM usersitems WHERE ID = '". $ID ."'";

    $result2 = $conn->query($sql2);
    if($result2){
        //if deleted successfully
        $sql3 = "UPDATE `users` SET `coins` = coins + '". $itemPrice . "' WHERE `id` = '". $userID . "'";
        $conn->query($sql3);
    }
    else{
        echo "error: could not delete item";
    }
    
  } else {
      echo "0";
  }
  $conn->close();

?>