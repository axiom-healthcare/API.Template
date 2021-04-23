Feature: Entities
	CRUD Operations Test

@mytag
Scenario: Get All Entities returns OK
	When I send a HTTP GET Request to ./Entities
	Then An HttpStatusCode.Ok should be returned

Scenario: Get All Entities correct number of Entities
	Given An Entity was added to the Data Store
	When I send a HTTP GET Request to ./Entities
	Then one Entity should be returned
