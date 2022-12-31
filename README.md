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
        "x": 1,
        "y": 2,
        "R": 250,
        "G": 69,
        "B": 0
    }
]
```
***
Serwer should be able to receive all the LED data in the format shown above.

Generated endpoint can be seen on: [Website](https://documenter.getpostman.com/view/25109980/2s8Z6yVY2X).

***
Dtos can be as follows written in c#:

```csharp
public class DataObjectDto
{
    public string? Name { get; set; }
    public float? Value { get; set; }
    public string? Unit { get; set; }
}
```

```csharp
public class DataStructDto
{
    public string? Name { get; set; }

    public IEnumerable<string>? Units { get; set; }

    public float? Value { get; set; }

    public string? DefaultUnit { get; set; }
}
```

```csharp
public class LedDto
{
    public int? X { get; set; }

    public int? Y { get; set; }

    public int? R { get; set; }

    public int? G { get; set; }

    public int? B { get; set; }
}
```
```csharp
public class SelectedDefaultUnitDto
{
    public string? Name { get; set; }

    public string? Unit { get; set; }
}
```