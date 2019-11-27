Feature: Subscription request feature
		Chang can create a new subscription after register on the site

Background: 
	Given Chang is authentificated

@v1 @subscription @request @creation 
Scenario: Chang sends new request of subscription for Mutualized service
	Given the webapi is online
	When Chang sends new request of subscription for Mutualized service
	Then Creation of subscription is successful


@v1 @subscription @request @creation 
Scenario: Chang browses all subscription accounts
	Given the webapi is online
	And 'Chang' subscription account has been created
	And 'Mutualized' subscription has been created for 'Chang'
	When 'Chang' sends GET request to list these subscription 
	Then all subscription has been return at 'Chang'

@v1 @subscription @request @creation 
Scenario: Morgan browses the Chang's subscription account
	Given the webapi is online
	And 'Chang' subscription account has been created
	And 'Mutualized' subscription has been created for 'Chang'
	When 'Chang' sends GET request with 'Mutualized' subscription
	Then only 'Mutualized' subscription has been return at 'Chang'