# API Template

Boilerplate setup to create a API which includes the following projects:  
1. API
`Service Layer` of the API building using ASP.NET API's.
2. Data  
`Data Layer` of the API build using Entity Framework: object-relational mapping framework, with a flexible, configurable, `Data Store`.
3. Mirations
Enable Agile development and management the structure of the `Data Store`, using Entity Framework Migrations
4. Data.Client
An C# Sample application which uese the `Data Layer`
5. API.Tests
Interation Testing of the `Service Layer` using LocalSQLDB as `Data Store`
6. Data.Tests
Unit Testing of the `Data Layer` with support for InMemory, SQLite or LocalSQLDB as `Data Store`s.


## Data Layer
### Unit Tests
> The InMemory `Data Store` is not designed to mimic a relational database. If you want to test against a `Data Store` which behaves like a relational database, then use SQLite.

## Data Store
Depending on the Environment in use, a different Data Store will be used:
1. Local `Development` : a Local SQL Database `Data Store`, created with the SQLLocalDb.exe Utility, is used.
2. Cloud `Development` : an Azure SQL Development Database `Data Store`
3. Cloud `Stagging` : an Azure SQL Stagging Database `Data Store`
4. Cloud `Production` : an Azure SQL Production Database `Data Store`

#### Local Machine Setup
1. Using command line, Create local SQL DB and ensure the DB exist 
```
sqllocaldb create Data
sqllocaldb info Data
```
2. Using command line, Create local testing SQL DB and ensure the BD exist
```
sqllocaldb create Data
sqllocaldb info Data
```
3. Update `appsettings.json` to points to the correct local directory path.

Note: Use SQL Management Studio to connect to the Test or Data db's. Enter either (localdb)\Data or (localdb)\Test as Servername.

### Migrations
The functionality provied by the Entity Framework Migrations is used at design time only.


#### Nuget Packages
* Microsoft.EntityFrameworkCore  
* Microsoft.EntityFrameworkCore.SqlServer  
* Microsoft.EntityFrameworkCore.Design  
* Microsoft.EntityFrameworkCore.Tools  

> Note: .Design and .Tools Packages is required to enable Entity Framework Migrations at Design Time

### Applied Agile  
#### Define   
Business Requirements are defined by the Product Owner, in collaboration with Stakeholders.

The requirements is defined using User Stories, in the form of:
> As a **User**,  
> When **Context**,  
> I want to **Business Requirement**,  
> So that **Business Value**  

#### Design
Using the assigned User Storie, the Back-End Developer identifies the needed Business Entities, associated Properties and possible Relationships between Entities.
Carefull consideration should be given to ensure alingment with the Integration Designs and exsiting Business Domain. The design stage is complete once the Back-End Developer and Quality Engineer has reached agreement on the proposed design.

#### Develop

1. Using command line, Create local SQL DB (Onetime action only) and ensure the DB exist 
```
sqllocaldb create Data
sqllocaldb info Data
```
2. Start Visual Studio, open the solution and ensure the Data project is set as the start-up project.
3. Using the Package Manager Console, Create a new migration which represents all Data Model and Context changes needed to implement the relevant User Story
```Powershell
Add-Migration <UserStory>
```
4. Apply the new migration to the Data Store
```Powershell
Update-Database
```

TODO: Add walkthrough using Postman to execute queries and see results in database using SQL Management Studio

Adjust API URL :  API | Properties | launchSetting.json | launch URL 