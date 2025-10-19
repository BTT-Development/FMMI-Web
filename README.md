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

Dette hjemmeside er et monitorering interface som skal holde status på sensor i et fabrik. Samt skal give mulighed for diverse funktioner som: administration af enheder, alarmer og filtrering af data. 
Ved at placere IoT-enheder forskellige strategiske steder I fabrikken vil dataindsamling som:
-	Telemetri
-	Sikkerhedskameraer
-	Sensorer

!(<img width="969" height="617" alt="image" src="https://github.com/user-attachments/assets/82c6aacd-81a7-4e34-aa4d-0cc9601138f7" />)

Arkitektur diagram leverer et bedre overblik over systemet. Det er en eller flere sensorer som ved brug af MQTT protokollen "publish/subscribe" sender data igennem en broker som er HiveMQ i dette tilfæld.
Data bliver opbevar i to forskellige databaser MongoDB og PostgreSQL, da MongoDB bliver brugt som en lake, og derfor alt data bliver opbevare data uden at blive behandlet, men derudover det behandlede data som er tilpasse til monitorering skal opbevares i PostgreSQL.

## Instructions

1. For bygning og konfiguration af sensor: [Se her](https://github.com/BTT-Development/FmmiIot)
2. Install the required libraries by following their respective installation instructions.
3. Open Visual Studio Code.

### Libraries


### Structure

  #### 
    
  #### 

## Changelog
[Github](https://github.com/BTT-Development/FMMI-Web/branches) - The full project is ready for you to try it.

## To-do



## Contributing

* Im working with Thomas, he was a big help with the project.
