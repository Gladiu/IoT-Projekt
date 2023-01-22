#!/usr/bin/python3
import json
import sys
from sense_hat import SenseHat

sense = SenseHat()
pressure = sense.get_pressure()
# Odczytanie danych wejściowych jako tekstu
input_text = sys.argv[1]

# Konwersja danych na format obiektu (dict)
input_data = json.loads(input_text)

for obj in input_data:
    if obj["name"] == "pressure":
        if obj["defaultUnit"] == "hPa":
            obj["value"] = pressure
        if obj["defaultUnit"] == "mmHg":
            obj["value"] = pressure * 0.750061683

# Konwersja danych na format tekstowy (string)
output_text = json.dumps(input_data)
print(output_text)
