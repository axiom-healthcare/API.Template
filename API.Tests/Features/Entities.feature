Feature: Entities
	CRUD Operations Test

@mytag
Scenario: Get Entities returns OK
	When I send a GET Request to ./Entities
	Then an Ok HTTP Status Code should be returned

Scenario: Get Entities returns Entities
	Given these Two Entities was added to the Data Store:
	| Name  |
	| Test  |
	| Test2 |
	When I send a GET Request to ./Entities
	Then the two Entity should be returned
