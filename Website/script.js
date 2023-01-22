
var Measurements = [];

var Config = {};

var meas_url = "PHPScripts/GenerateSamples.php";

var set_data_choice = true;
var set_chart_choice = true;
var set_unit_choice = true;
//var set_chart_fig = true;

var MyChart;
var t_chart = 0.0;

function makeMatrix(row, col)
{
	$.get("PHPScripts/GenerateMatrix.php", function(data, status){
		let LEDlist = data;
		for(let i=0; i < LEDlist.length; i++)
		{
			let RGBarray = LEDlist[i];
			let R = RGBarray[0];
			let G = RGBarray[1];
			let B = RGBarray[2];
			let colorcode = "#"+R.toString(16).padStart(2,'0')+G.toString(16).padStart(2,'0')+B.toString(16).padStart(2,'0');
			//console.log(colorcode);
			let element = $("<div>", {id: "led_"+i,
				"class": 'LED',
				title: "LED"+(i+1),
				style: "background-color:"+colorcode+";"
			});
			if(i % col == 0 && i > 0)
			{
				$("#Matrix").append("<div style=\"clear:both;\"></div>");
			}
			$("#Matrix").append(element);
		}
		$("#Matrix").append("<div style=\"clear:both;\"></div>");
		$('.LED').click(function(){
			let R = 0;
			let G = 0;
			let B = 0;
			for(let slide of $('.RGBSlider'))
			{
				if(slide.id == "R") R = parseInt(slide.value);
				if(slide.id == "G") G = parseInt(slide.value);
				if(slide.id == "B") B = parseInt(slide.value);
			}
			let colorcode = "#"+R.toString(16).padStart(2,'0')+G.toString(16).padStart(2,'0')+B.toString(16).padStart(2,'0');
			$(this).css('background-color', colorcode);
			this.classList.add('LEDChanged');
		});
	}, "json");
	
}

function setupChart()
{
	let ChartsData = {
				labels: [],
				datasets: [{
					label: '',
					data: [],
					fill: false,
					borderColor: 'rgb(75, 192, 192)',
					tension: 0.1
					}]
			};
		if($('#myChart').length > 0)
		{
			myChart  = document.getElementById('myChart').getContext('2d');
			MyChart = new Chart(myChart, {
				type: 'line', 
				data: ChartsData,
				options: {
					scales: {
						xAxes: [{
						  scaleLabel: {
							display: true,
							labelString: 'time[s]'
							}
						}],
						yAxes: [{
						  scaleLabel: {
							display: true,
							labelString: ''
							}
						}]
					}     
				}
			});
		}
		
		if(set_chart_choice)
		{
			$('#SamplingTime').val(Config.Tp);
		}
}

function updateChart(sample_val, y_name, Tp_val)
{
	MyChart.data.labels.push(t_chart.toFixed(2));
	MyChart.data.datasets[0].data.push(sample_val);
	MyChart.data.datasets[0].label = y_name;
	MyChart.options.scales.yAxes[0].scaleLabel.labelString = $('#SelectUnit').val();
	if(MyChart.data.labels.length >= 100)
	{
		MyChart.data.labels.splice(0,1);
		MyChart.data.datasets[0].data.splice(0,1);
	}
	MyChart.update();
	t_chart = t_chart+Tp_val;
}

function updateData()
{
	let main = '<tr><td align="left" class="mainTab">name</td><td align="left" class="mainTab">value</td><td align="left" class="mainTab">unit</td></tr>';
	let choice = '';
	let adds = '';
	
	if(set_unit_choice)
	{
		$('#SelectUnit').find('option').remove();
		setupChart();
	}
	
	for(let measurement of Measurements)
	{
		//console.log(measurement);
		let name = measurement.name; 
		let value = measurement.value; 
		let defaultUnit = measurement.defaultUnit;
		let units = measurement.units;
		
		choice = choice + '<tr><td align="right">' + name + '</td>' + '<td align="left"><select class="Chosen" name="unit_' + name + '">';
		if(set_chart_choice)
		{
			$('#SelectChart').append($('<option>', {
				value: name,
				text: name
			}));
		}
		for(let un of units)
		{
			choice = choice + '<option value="' + un + '">' + un + '</option>';
			if(set_unit_choice && name == $('#SelectChart').val())
			{
				$('#SelectUnit').append($('<option>', {
					value: un,
					text: un
				}));
			}
		}
		choice = choice + '</select></td></tr>';
		
		let add_unit = defaultUnit;
		if($('select[name=unit_' + name + ']').val())
		{
			add_unit = $('select[name=unit_' + name + ']').val();
		}
		
		if($('#SelectChart').val() == name)
		{
			//let tp = parseFloat($('#SamplingTime').val());
			updateChart(value, name, Config.Tp);
		}
		
		adds = '<tr><td align="left" class="MeasureCell">' + name + '</td>' + '<td align="left" class="MeasureCell">' + value + '</td>' + '<td align="left" class="MeasureCell">' + add_unit + '</td></tr>';
		
		main = main + adds;
	}
	//console.log(main);
	
	set_chart_choice = false;
	set_unit_choice = false;
	
	$('#DataTable').html(main);
	
	if(set_data_choice && $('#UnitsChoice').length > 0)
	{
		//console.log(choice);
		$('#UnitsChoice').html(choice);
		//console.log("false");
		set_data_choice = false;
	}
}

function setupChartHTML()
{
	let choice = '';
	
	if(set_unit_choice)
	{
		$('#SelectUnit').find('option').remove();
		setupChart();
	}
	
	for(let measurement of Measurements)
	{
		//console.log(measurement);
		let name = measurement.name; 
		let value = measurement.value; 
		let defaultUnit = measurement.defaultUnit;
		let units = measurement.units;
		
		choice = choice + '<tr><td align="right">' + name + '</td>' + '<td align="left"><select class="Chosen" name="unit_' + name + '">';
		if(set_chart_choice)
		{
			$('#SelectChart').append($('<option>', {
				value: name,
				text: name
			}));
		}
		for(let un of units)
		{
			choice = choice + '<option value="' + un + '">' + un + '</option>';
			if(set_unit_choice && name == $('#SelectChart').val())
			{
				$('#SelectUnit').append($('<option>', {
					value: un,
					text: un
				}));
			}
		}
		choice = choice + '</select></td></tr>';
		
		let add_unit = defaultUnit;
		if($('select[name=unit_' + name + ']').val())
		{
			add_unit = $('select[name=unit_' + name + ']').val();
		}
		
		if($('#SelectChart').val() == name)
		{
			updateChart(value, name, Config.Tp);
		}
	}
	
	set_chart_choice = false;
	set_unit_choice = false;
	
	if(set_data_choice && $('#UnitsChoice').length > 0)
	{
		$('#UnitsChoice').html(choice);
		set_data_choice = false;
	}
}

function updateDataChart()
{
	for(let measurement of Measurements)
	{
		let name = measurement.name; 
		let value = measurement.value;
		
		if($('#SelectChart').val() == name)
		{
			updateChart(value, name, Config.Tp);
		}
	}
}

function updateDataData()
{
	for(let measurement of Measurements)
	{
		let name = measurement.name; 
		let value = measurement.value;
		let unit = measurement.unit;
		
		$('[name=value_'+name+']').html(value);
		$('[name=u_'+name+']').html(unit);
	}
}

function setupDataHTML()
{
	let main = '<tr><td align="left" class="mainTab">name</td><td align="left" class="mainTab">value</td><td align="left" class="mainTab">unit</td></tr>';
	let choice = '';
	let adds = '';
	
	for(let measurement of Measurements)
	{
		//console.log(measurement);
		let name = measurement.name; 
		let value = measurement.value; 
		let defaultUnit = measurement.defaultUnit;
		let units = measurement.units;
		
		choice = choice + '<tr><td align="right">' + name + '</td>' + '<td align="left"><select class="Chosen" name="unit_' + name + '">';
		
		for(let un of units)
		{
			choice = choice + '<option value="' + un + '">' + un + '</option>';
		}
		choice = choice + '</select></td></tr>';
		
		let add_unit = defaultUnit;
		if($('select[name=unit_' + name + ']').val())
		{
			add_unit = $('select[name=unit_' + name + ']').val();
		}
		
		adds = '<tr><td align="left" class="MeasureCell">' + name + '</td>' + '<td align="left" class="MeasureCell" name="value_' + name + '">' + value + '</td>' + '<td align="left" class="MeasureCell" name="u_' + name + '">' + add_unit + '</td></tr>';
		
		main = main + adds;
	}
	//console.log(main);
	
	set_unit_choice = false;
	
	$('#DataTable').html(main);
	
	if(set_data_choice && $('#UnitsChoice').length > 0)
	{
		$('#UnitsChoice').html(choice);
		set_data_choice = false;
	}
}

function updateMeasChart()
{
	if($('#SelectUnit').val())
	{
		let chart_url = "PHPScripts/NewSample.php?ID=";
		chart_url = chart_url + $('#SelectChart').val();
		console.log(chart_url);
		
		$.ajax(meas_url, {
			type: 'GET', dataType: 'json',
			success: function(measure, status, xhr){
				Measurements = measure;
				updateDataChart();
			}
			});
	}
	setTimeout("updateMeasChart()", 1000*Config.Tp);
}

function updateMeasData()
{
	let data_url = "PHPScripts/NewSample.php";
	console.log(data_url);
	
	$.ajax(data_url, {
		type: 'GET', dataType: 'json',
		success: function(measure, status, xhr){
			Measurements = measure;
			updateDataData();
		}
		});
		
	setTimeout("updateMeasData()", 1000*Config.Tp);
}

function updateMeasurements()
{
	$.ajax(meas_url, {
		type: 'GET', dataType: 'json',
		success: function(measure, status, xhr){
			Measurements = measure;
			updateData();
		}
		});
		
		//console.log(Measurements);
		
		setTimeout("updateMeasurements()", 1000*Config.Tp);
}

function setChart()
{
	$.ajax(meas_url, {
		type: 'GET', dataType: 'json',
		success: function(measure, status, xhr){
			Measurements = measure;
			setupChartHTML();
		}
		});
}

function setData()
{
	$.ajax(meas_url, {
		type: 'GET', dataType: 'json',
		success: function(measure, status, xhr){
			Measurements = measure;
			setupDataHTML();
		}
		});
}


async function updateConfiguration()
{
	const res = await fetch('PHPScripts/configuration.json');
	
	Config = await res.json();
	
	$('input[name="IP"]').val(Config.IP);
	$('input[name="Port"]').val(Config.Port);
	$('input[name="API"]').val(Config.API);
	$('input[name="Tp"]').val(Config.Tp);
	$('input[name="Count"]').val(Config.Count);
	
	
	//updateMeasurements();
	
	if($('#SelectChart').length > 0)
	{
		setChart();
		updateMeasChart();
	}
	
	if($('#DataTable').length > 0)
	{
		setData();
		updateMeasData();
	}
	
}

window.onload = function()
{
	
	updateConfiguration();
	
	makeMatrix(8,8);
	
	$('.MainButtons').click(function(){
		let url = this.value + ".html";
		document.getElementById('Show').src = url;
		if(this.name == 'DataBut')
		{
			//console.log(this.name)
			set_data_choice = true;
		}
		
		if(this.name == 'Chart')
		{
			//console.log("ELO");
		}
		
		for(let butt of $('.MainButtons'))
		{
			butt.classList.remove('Pressed');
		}
		this.classList.add('Pressed');
	});
	
	$("#SaveButton").click(function () {
		//console.log("ELO");
		if(parseInt($('input[name="Port"]').val()) < 1 || parseInt($('input[name="Port"]').val()) > 65536)
		{
			let error_msg = "You put wrong port number! Please choose integer beetwen 1 and 65536";
			$('#Errors').html(error_msg);
			return;
		}
		
		if(parseFloat($('input[name="Tp"]').val()) < 0.01)
		{
			let error_msg = "You put wrong sampling time! Sampling time must be greater than 0.01 s";
			$('#Errors').html(error_msg);
			return;
		}
		
		if(parseInt($('input[name="Count"]').val()) < 1)
		{
			let error_msg = "You put wrong number of samples! Count of samples must be at least 1";
			$('#Errors').html(error_msg);
			return;
		}
		
		let dict = {
			IP: $('input[name="IP"]').val(),
			Port: parseInt($('input[name="Port"]').val()),
			API: $('input[name="API"]').val(),
			Tp: parseFloat($('input[name="Tp"]').val()),
			Count: parseInt($('input[name="Count"]').val())
		};
		Config = dict;
		console.log(Config);
		let save = JSON.stringify(dict);
		//console.log(save);
		let url_save = "PHPScripts/SaveJson.php?text=" + save;
		//console.log(url_save);
		$.ajax(url_save, {
		type: 'PUT', dataType: 'json'
		});
	});
	
	$("#LoadButton").click(function () {
		let url_load = "PHPScripts/LoadJson.php";
		$.ajax(url_load, {
		type: 'GET', dataType: 'text',
		success: function(responseLoad, status, xhr){
			let jsonLoad = JSON.parse(responseLoad);
			Config = jsonLoad;
			$('input[name="IP"]').val(jsonLoad.IP);
			$('input[name="Port"]').val(jsonLoad.Port);
			$('input[name="API"]').val(jsonLoad.API);
			$('input[name="Tp"]').val(jsonLoad.Tp);
			$('input[name="Count"]').val(jsonLoad.Count);
		}
		});
	});
	
	$('#SelectChart').change(function(){
		MyChart.data.datasets[0].data = [];
		MyChart.data.labels = [];
		t_chart = 0.0;
		set_unit_choice = true;
		setChart();
	});
	
	$('#SelectUnit').change(function(){
		MyChart.data.datasets[0].data = [];
		MyChart.data.labels = [];
		t_chart = 0.0;
		let temp_meas = [];
		for(meas of Measurements)
		{
			if($('#SelectChart').val() == meas.name) temp_meas.push({name: meas.name, unit: $('#SelectUnit').val()});
			else temp_meas.push({name: meas.name, unit: meas.defaultUnit});
		}
		
		let url_unit = 'PHPScripts/PostUnit.php';
		
		$.post(url_unit, {text: temp_meas});
	});
	

	$('#SamplingTime').change(function(){
		if(this.value > 0)
		{
			Config.Tp = parseFloat(this.value);
		}
	});
	
	$('.RGBSlider').change(function(){
		let R = 0;
		let G = 0;
		let B = 0;
		for(let slide of $('.RGBSlider'))
		{
			if(slide.id == "R") R = parseInt(slide.value);
			if(slide.id == "G") G = parseInt(slide.value);
			if(slide.id == "B") B = parseInt(slide.value);
		}
		let colorcode = "#"+R.toString(16).padStart(2,'0')+G.toString(16).padStart(2,'0')+B.toString(16).padStart(2,'0');
		$('#ExampLED').css('background-color', colorcode);
	});
	
	$('#SendMatrix').click(function(){
		let R = 0;
		let G = 0;
		let B = 0;
		let IDofLED = '';
		let MatrixResult = [];
		for(let led of $('.LEDChanged'))
		{
			let c = led.style.backgroundColor;
			let rgb = c.replace(/^rgba?\(|\s+|\)$/g,'').split(',');
			R = rgb[0];
			G = rgb[1];
			B = rgb[2];
			IDofLED = led.id.split('_')[1];
			MatrixResult.push({x: parseInt(IDofLED%8), y: parseInt(IDofLED/8), r: R, g: G, b: B});
		}
		
		let url_matrix = 'PHPScripts/GetMatrix.php';
		
		$.post(url_matrix, {FinalMatrix: MatrixResult});
		
	});
	
	
	$('#ClearMatrix').click(function(){
		let MatrixResult = [];
		
		for(let led of $('.LED'))
		{
			$(led).css('background-color','#000000');
			MatrixResult.push({id: led.id.split('_')[1], R: 0, G: 0, B: 0});
		}
		
		let url_matrix = 'PHPScripts/GetMatrix.php';
		
		$.post(url_matrix, {FinalMatrix: MatrixResult});
		
	});
	
	$('#SendUnits').click(function(){
		let temp_meas = [];
		for(let ch of $('.Chosen'))
		{
			let meas_name = ch.name.split('_')[1];
			let meas_unit = ch.value;
			temp_meas.push({name: meas_name, unit: meas_unit});
		}
		let url_unit = 'PHPScripts/PostUnit.php';
		
		$.post(url_unit, {text: temp_meas});
	});
	
}