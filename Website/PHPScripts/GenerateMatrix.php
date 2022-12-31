<?php

$matrix = array();

for($i = 0; $i < 64; $i++)
{
	array_push($matrix, array(192, 192, 192));
}

$result = json_encode($matrix);

echo $result;

?>