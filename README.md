# Data Converter
This file includes documentation of this project.

| Table of Content |
| --- |
| Getting Started |
| Usage of this API |


# Getting Started

## Openning Solution
Be sure you have all requirements then open `.sln` file.
### Requirements
- To open this solution you need **Visual Studio**.
- .Net Core 2.1+

### Run Tests
This project contains Test project of source code under **tests** folder.
You can run all tests Easily via clicking **TESTS** > **RUN** > **ALL TESTS**. 
After a test was succeed, it produces output file to your computers desktop location. You can check there to get results.

# Usage of This API
To use this API, include library to your project. There is an object **MainCityData** which has implementation of abilities from **Abstraction** folder.


## To Read Data:

#### From CSV:
```csharp
	//Read your file
	string csv = File.ReadAllString("C:\MySampleData.csv");
	//Generate object from csv string
	var data = MainCityData.FromCsv(csv);
```

#### From XML:
```csharp
	//Read your file
	string xml = File.ReadAllString("C:\MySampleData.xml");
	
	//Generate object from csv string
	var data = MainCityData.FromCsv(xml);
```

## To Apply Filter

```csharp
	var data = MainCityData.FromCsv(csv); // Generate object again from csv or xml
	
	data.Filter(x => x.CityName == "Antalya"); //Apply your filter as you wish with 
```

## Work with object


```csharp
	var data = MainCityData.FromCsv(csv); // Generate object again from csv or xml
	
	//You can apply a LINQ to datas.
	var onlyDistricst = obj.ObjectResult.Select(s => s.DistrictName);
```

## Getting Result
You can get result as csv or xml like below:
```csharp
	var data = MainCityData.FromCsv(csv); // Generate object again from csv or xml
	
	data.Filter(x => x.CityName == "Ankara"); //Apply a filter as you wish

	var resultXml = data.AsXml(); // Generates plaing string result as Xml format

	var resultCsv = data.AsCsv(); // Generates plaing string result as Csv format
```

You can write those strings to files easily using via `Sytstem.IO`

```csharp

	//...

	var resultXml = data.AsXml(); // Generates plaing string result as Xml format
	File.WriteAllText("C:\SampleOutput.xml", resultXml);

	var resultCsv = data.AsCsv(); // Generates plaing string result as Csv format
	File.WriteAllText("C:\SampleOutput.csv", resultCsv);
```