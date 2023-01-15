<?php

$file = "unit.json";
$text = $_POST['text'];
$text = json_encode($text);
file_put_contents($file, $text);

?>