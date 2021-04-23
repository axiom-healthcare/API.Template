Feature: Entities
	CRUD Operations Test

@mytag
Scenario: Get Entities returns OK
	When I send a GET Request to ./Entities
	Then An Ok HTTP Status Code should be returned

Scenario: Get Entities returns Entities
	Given An Entity was added to the Data Store
	When I send a GET Request to ./Entities
	Then The Entity should be returned
