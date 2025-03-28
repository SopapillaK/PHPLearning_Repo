<?php

require 'ConnectionSettings.php';

// Check connection
if ($conn->connect_error) {
  die("Connection failed: " . $conn->connect_error);
}

//vars sumbitted by user
$itemID = $_POST["itemID"];

$path = "http://localhost/UnityPHPLearning/ItemIcons/" . $itemID . ".png";

//get the image  and convert into string
$image = file_get_contents($path);

echo $image;

$conn->close();
?>