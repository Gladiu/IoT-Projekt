<?php

$file = "matrix.json";
$text = $_POST['FinalMatrix'];
$text = json_encode($text);
file_put_contents($file, $text);

?>