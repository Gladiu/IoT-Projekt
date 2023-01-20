#!/usr/bin/python3
import json
import sys
from sense_hat import SenseHat

sense = SenseHat()
orientation = sense.get_orientation()
# Odczytanie danych wejściowych jako tekstu
input_text = sys.argv[1]

# Konwersja danych na format obiektu (dict)
input_data = json.loads(input_text)

for obj in input_data:
    if obj["name"] == "pitch":
        if obj["defaultUnit"] == "deg":
            obj["value"] = orientation["pitch"]
        if obj["defaultUnit"] == "rad":
            obj["value"] = orientation["pitch"] * (3.1415/180)

    if obj["name"] == "roll":
        if obj["defaultUnit"] == "deg":
            obj["value"] = orientation["roll"]
        if obj["defaultUnit"] == "rad":
            obj["value"] = orientation["roll"] * (3.1415/180)

    if obj["name"] == "yaw":
        if obj["defaultUnit"] == "deg":
            obj["value"] = orientation["yaw"]
        if obj["defaultUnit"] == "rad":
            obj["value"] = orientation["yaw"] * (3.1415/180)



# Konwersja danych na format tekstowy (string)
output_text = json.dumps(input_data)
print(output_text)
