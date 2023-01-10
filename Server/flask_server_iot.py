#!/usr/bin/python3
import flask as fsk
from urllib.request import urlopen
import json, random
import subprocess

from sense_emu import SenseHat
sense = SenseHat()

app = fsk.Flask(__name__)

global_DataStructure = []
with open('global_DataStructure.json', 'r') as f:
    global_DataStructure = json.load(f)


def change_def_units(units_to_change):

    for elem in units_to_change:
        for obj in global_DataStructure:
            if elem["name"] == obj["name"]:
                if obj["defaultUnit"] != elem["unit"]:
                    obj["defaultUnit"] = elem["unit"]

    return global_DataStructure


@app.route('/get/DataStructs', methods=['GET'])
def get_data_structs():

    data = global_DataStructure
    data = json.dumps(data)
    data = subprocess.check_output(['python', 'process_temperature.py', data])
    data = subprocess.check_output(['python', 'process_pressure.py', data])
    data = subprocess.check_output(['python', 'process_humidity.py', data])
    data = subprocess.check_output(['python', 'process_orientation.py', data])
    
    return data


@app.route('/get/DataObjects', methods=['GET'])
def get_data_objects():
    try:
        id = fsk.request.args.get("ID")
        orient_id = "orientation"
        if id in ["pitch", "roll", "yaw"]:
            orient_id = id
            id = "orientation"
        process = 'process_'+id+'.py'
        data = global_DataStructure
        data = json.dumps(data)
        data = subprocess.check_output(['python', process, data])
        output_data = []
        
        for element in json.loads(data):
            if element["name"] in ["pitch", "roll", "yaw"] and orient_id == "orientation" and id == "orientation":
                output_element = {
                    "name" : element["name"],
                    "value" : element["value"],
                    "unit" : element["defaultUnit"]
                }
                output_data.append(output_element)
            elif element["name"] in ["pitch", "roll", "yaw"] and orient_id == element["name"] and id == "orientation":
                output_element = {
                    "name" : element["name"],
                    "value" : element["value"],
                    "unit" : element["defaultUnit"]
                }
                output_data.append(output_element)
            elif element["name"] == id:
                output_element = {
                    "name" : element["name"],
                    "value" : element["value"],
                    "unit" : element["defaultUnit"]
                }
                output_data.append(output_element)

        return json.dumps(output_data)

    except:
        data = global_DataStructure
        data = json.dumps(data)
        data = subprocess.check_output(['python', 'process_temperature.py', data])
        data = subprocess.check_output(['python', 'process_pressure.py', data])
        data = subprocess.check_output(['python', 'process_humidity.py', data])
        data = subprocess.check_output(['python', 'process_orientation.py', data])

        output_data = []
        for element in json.loads(data):
            output_element = {
                "name" : element["name"],
                "value" : element["value"],
                "unit" : element["defaultUnit"]
            }
            output_data.append(output_element)

        return json.dumps(output_data)


@app.route('/get/Leds', methods=['GET'])
def get_leds():
    
    leds_state = []
    for x in range(8):
        for y in range(8):
            r, g, b = sense.get_pixel(x, y)
            data = {
            "x": x,
            "y": y,
            "R": r,
            "G": g,
            "B": b
            }
            leds_state.append(data)

    return json.dumps(leds_state)


@app.route('/post/Leds', methods=['POST'])
def post_led_colors():
    try:
        data = fsk.request.get_json()
        # data = []
        # with open('led.json', 'r') as f:
        #     data = json.load(f)
        for led_data in data:
            x = led_data['x']
            y = led_data['y']
            r = led_data['R']
            g = led_data['G']
            b = led_data['B']
            sense.set_pixel(x, y, r, g, b)
        return json.dumps([{"Result": 1}])
    except Exception as e:
        print(e)
        return json.dumps([{"Result": -1}])


@app.route('/post/DefaultUnits', methods=['POST'])
def post_default_units():
    try:
        def_units = fsk.request.get_json()
        # def_units = []
        # with open('testdefunits.json', 'r') as f:
        #     def_units = json.load(f)
        
        change_def_units(def_units)
    
        return json.dumps([{"Result": 1}])
    except Exception as e:
        print(e)
        return json.dumps([{"Result": -1}])


@app.route('/test', methods=['GET'])
def hello():
    return 'Witaj świecie!'

if __name__ == '__main__':
    app.run(host='192.168.31.145', debug=False)
