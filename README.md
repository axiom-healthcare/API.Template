# API Template

Boilerplate setup to create a API and includes the following projects:  
1. Data  
Data Layer of the API build using Entity Framework: object-relational mapping framework.
This project also leverage Entity Framework Migrations to enable Agile Development of the Underlyging SQL Database.
2. Data.Tests
Unit Testing Framework used to test the Data Layer.
A choice between an SQLite or InMemory data stores are provided to enable the testin of the data layer without affecting the existing database.



## Data Layer

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