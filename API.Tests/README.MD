# Integration Test
Specflow based API Integration Test Suite

### HTML Based Reports
follow steps to generate Integration Tests Reports

1. Change directory from solution root to API.Test Project output directory
`cd API.Tests\bin\Debug\.net5.0\`

2. Generate Report
`livingdoc test-assembly API.Tests.dll -t TestExecution.json`

3. View Report
`LivingDoc.html`