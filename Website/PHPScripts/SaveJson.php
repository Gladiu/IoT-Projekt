<?php

$file = "configuration.json";
$text = $_GET['text'];
file_put_contents($file, $text);

?>