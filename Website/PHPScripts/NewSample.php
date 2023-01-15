<?php

class Measurement
{
	public $name;
	public $unit;
	public $value;
}

$meas1 = new Measurement();
$meas2 = new Measurement();

$meas1->name = 'temperature';
$meas2->name = 'preassure';

$meas1->unit = 'C';
$meas2->unit = 'hPa';

$meas1->value = rand(20, 30);
$meas2->value = rand(1000, 1030);

$result = array($meas1, $meas2);

if(!isset($_GET['ID'])) 
{
	echo json_encode($result);
}
else
{
	if($_GET['ID'] == 'temperature')
	{
		echo json_encode($result[0]);
	}
	if($_GET['ID'] == 'preassure')
	{
		echo json_encode($result[1]);
	}
}

?>