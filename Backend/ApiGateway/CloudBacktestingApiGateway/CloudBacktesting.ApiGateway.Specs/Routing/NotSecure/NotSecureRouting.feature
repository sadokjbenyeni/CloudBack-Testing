Feature: NotSecureRouting
	In order to avoid routing mistakes
	We want to be routed when I call a specific service

@registration @routing @notsecure @apiv1
Scenario: Call the sign up service
	Given Changs is a new user on the web site
	And Chang filled all information on the sign up form
	And API Version is 1
	When Chang click on submit	
	Then the api gateway routes the 'POST' request from 'source/api/signup' to 'registrationService/api/signup'
	
@registration @routing @notsecure @apiv1
Scenario: Keep the sign up information when api gateway route the call
	Given Changs is a new user on the web site
	And Chang filled all information on the sign up form
	And API Version is 1
	When Chang click on submit	
	And Api gateway routes the source 'POST' request from 'source/api/signup' to destination 'destination/api/signup'
	Then all information include in the source request are in the destination request

@registration @updateUser @routing @notsecure @apiv1
Scenario: Routes the countries query
	Given Changs a new customers on the web site
	And Chang is filling information on the form
	When Changs click to change set his countries
	And Web site sends query at the server to list all countries supported
	Then api gateway routes the query from 'source/api/countries' to 'destination/api/countries'
	And the HTTP Header has been preserved
	