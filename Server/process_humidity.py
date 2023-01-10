#!/usr/bin/python3
import json
import sys
from sense_emu import SenseHat

sense = SenseHat()
humidity = sense.get_humidity()
# Odczytanie danych wejściowych jako tekstu
input_text = sys.argv[1]

# Konwersja danych na format obiektu (dict)
input_data = json.loads(input_text)

for obj in input_data:
    if obj["name"] == "humidity":
        if obj["defaultUnit"] == "%":
            obj["value"] = humidity
        if obj["defaultUnit"] == "number":
            obj["value"] = humidity / 100.0

# Konwersja danych na format tekstowy (string)
output_text = json.dumps(input_data)
print(output_text)
