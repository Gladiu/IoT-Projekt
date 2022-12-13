# IOT Project
Simple applications for graphical interfaces such as Desktop application, Web application and Android application. All of that goes along with serwer publishing data made on Raspberry Pi.

## Graphical application data management
Response from server must be json formated string with according value:
```json
{
    "name": "temperature",
    "defaultUnit": "C",
    "units": ["C", "F"],
    "value": 33.3
}
```
Server can answer with particular data type when called for:
```csharp
$"{server_url}/temperature.json"
```
Example above shows calling for json file called temperature and the answer should be as above.

***
Server can albo respond with list of items currently being managed by it. For example:
```json
[
    {
        "name": "temperature",
        "defaultUnit": "C",
        "units": ["C", "F"],
        "value": 33.3
    },
    {
        "name": "humidity",
        "defaultUnit": "%",
        "units": ["%", "mmHg"],
        "value": 33.3
    }
]
```
This way graphical application can get list of all the data along with default returning value and then manage data on it's side.
***
Server should also be able to receive post values such as LED light request in RGB format. LED POST would be sent as a list of LED objects with structure as follows:
```json
[
    {
        "id": 1,
        "R": 250,
        "G": 69,
        "B": 0
    }
]
```
***
Serwer should be able to receive all the LED data in the format shown above.

