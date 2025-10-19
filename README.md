FMMI ( Forsvarsministeriets Materiel- og Indkøbsstyrelse ) - Krudt og Kugler



## T.O.C

* [Intro](#Intro)
* [Requeriments](#Requeriments)

* [Instructions](#Instructions)

   * [Libraries](#Libraries)
   * [Structure](#Structure)

* [Changelog](#Changelog)
* [To-Do](#To-do)
* [Contributing](#Contributing)


## Requirements

-  [See Here]()


## Intro

This website is a monitoring interface designed to track the status of sensors in a factory. It also provides various functionalities such as device management, alarm handling, and data filtering.
By placing IoT devices at strategic locations throughout the factory, data can be collected from sources such as:
-	Telemetri
-	Sikkerhedskameraer
-	Sensorer

<img width="500" height="599" alt="image" src="https://github.com/user-attachments/assets/82c6aacd-81a7-4e34-aa4d-0cc9601138f7" />

The architecture diagram provides a clearer overview of the system. One or more sensors use the MQTT “publish/subscribe” protocol to send data through a broker - in this case, HiveMQ.
The data is stored in two different databases: MongoDB and PostgreSQL. MongoDB is used as a data lake, where all raw data is stored without processing. Meanwhile, the processed data, which is tailored for monitoring purposes, is stored in PostgreSQL.

## Instructions

### Sensor Intructions
1. For the construction and configuration of the sensor: [Se her](https://github.com/BTT-Development/FmmiIot)

### Web Intructions
1. Install the required libraries by following their respective installation instructions.
2. Open Visual Studio Code.
3. The following libreries are required for the system.

### Libraries

#### UI

| Name                   | Version   |
|------------------------|-----------|
| Blazored.Modal             | v7.3.1  |
| Blazored.Toast   |  v4.2.1  |
| Syncfusion.Blazor.Calendars  | v31.1.23 |
| Syncfusion.Blazor.Charts  | v31.1.23  |
| Syncfusion.Blazor.Layouts   | v31.1.23  |
| Syncfusion.Blazor.Navigations  | v31.1.23  |
| Syncfusion.Blazor.Themes   | v31.1.23  |
| Microsoft.Identity.Web  | v3.14.1  |
| Microsoft.Identity.Web.UI   | v3.14.1  |

#### Domain layer

| Name                   | Version   |
|------------------------|-----------|
| Microsoft.EntityFrameworkCore | v9.0.9  |
| Microsoft.EntityFrameworkCore.Design |  v9.0.9  |
| Microsoft.EntityFrameworkCore.SqlServer | v9.0.9 |
| Microsoft.EntityFrameworkCore.Tools  | v9.0.9  |
| Npgsql.EntityFrameworkCore.PostgreSQL | v9.0.4  |


#### Service layer

| Name                   | Version   |
|------------------------|-----------|
| Microsoft.AspNetCore.Authentication.OpenIdConnect | v9.0.9  |
| Microsoft.Extensions.Hosting.Abstactions |  v9.0.9  |
| MongoDB.Driver | v3.5.0 |
| MQTTnet  | v4.3.7.1207  |
| MQTTnet.Extensions.TopicTemplate | v4.3.7.1207  |

### Structure

  #### 
    FMMI-Domain - Database logic
    FMMI-Service - Function logic
    FMMI-WEB - UI
    FMMI-Worker - BackgroundService MQTT
  

## Changelog
[Github](https://github.com/BTT-Development/FMMI-Web/branches) - The full project is ready for you to try it.

## To-do



## Contributing

* Im working with Thomas, he was a big help with the project.
