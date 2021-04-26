Feature: Entities
	CRUD Operations Test

@Create
Scenario: Create Entity
	When I send a POST request with header content type and body to ./Entities
	Then an CREATED HTTP Code should be returend
	And the response body contains the newly created Entity

@Retrieve
Scenario: Retrieve All Entities
	When I send a GET Request to ./Entities
	Then an OK HTTP Code should be returned
	And the response body contains all Entities in Data Store

@Retrieve
Scenario: Retrieve Specific Entity
	Given the Data Store contains an Entity
	When I send a GET Request to ./Entities/ appended with a valid Entity Id
	Then an OK HTTP Code should be returned
	And the response body contains the Entity with correct Id

@Update
Scenario: Update Entity
	Given the Data Store contains an Entity
	When I send a PUT Request with header content type and body to ./Entitities/ appended with the Entity's Id
	Then an NO CONTENT HTTP Code should be returned
	
@Delete
Scenario: Delete Entity
	Given the Data Store contains an Entity
	When I send a DELETE Reqeust to ./Entitites/ appended with the Entity's Id
	Then an NO CONTENT HTTP Code should be returned
	And the Entity should not exist in Data Store


