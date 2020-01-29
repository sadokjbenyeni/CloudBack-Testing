Feature: SubscriptionAccount creation feature
		MOrgan can create a new subscription account after register on the site

Background: 
	Given Morgan is authentificated with roles 'Administrator'

@v1 @subscriptionAccount @creation 
Scenario: Morgan creates a new subscription account after that customer has been registered on the web site
	Given the webapi is online
	When morgan sends the subscription account creation request for 'Chang'
	Then Creation of subscription account is successful


@v1 @subscriptionAccount @creation @reaquestById
Scenario: Chang browses his subscription account
	Given the webapi is online
	And Chang is authentificated with roles 'Client'
	And 'Chang' subscription account has been created
	When 'Chang' gets his subscription account
	Then get request return 'Chang' subscription account description