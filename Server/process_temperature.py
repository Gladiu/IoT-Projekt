#!/usr/bin/python3
import json
import sys
from sense_hat import SenseHat

sense = SenseHat()
temperature = sense.get_temperature()
# Odczytanie danych wejściowych jako tekstu
input_text = sys.argv[1]

# Konwersja danych na format obiektu (dict)
input_data = json.loads(input_text)

for obj in input_data:
    if obj["name"] == "temperature":
        if obj["defaultUnit"] == "C":
            obj["value"] = temperature
        if obj["defaultUnit"] == "F":
            obj["value"] = temperature * 1.8 + 32

# Konwersja danych na format tekstowy (string)
output_text = json.dumps(input_data)
print(output_text)
