Feature: Subscription request feature
		Chang can create a new subscription after register on the site

Background:
	Given Chang is authentificated

@v1 @subscription @request @creation
Scenario: Chang sends new request of subscription for Mutualized service
	Given the webapi is online
	And 'Chang' subscription account has been created
	When Chang sends new request of subscription for Mutualized service
	Then Creation of subscription is successful

@v1 @subscription @request @creation
Scenario: Chang browses all subscription request
	Given the webapi is online
	And 'Chang' subscription account has been created
	And 'Mutualized' subscription has been created for 'Chang'
	When 'Chang' sends GET request these subscriptions
	Then subscriptions are returned for 'Chang'

@v1 @subscription @request @creation
Scenario: Chang browses description for specific subscription
	Given the webapi is online
	And 'Chang' subscription account has been created
	And 'Mutualized' subscription has been created for 'Chang'
	When 'Chang' sends GET request subscription
	Then The subscription required that:
		| field             | value      |
		| Status            | Pending    |
		| Subscriber        | Chang      |
		| Type              | Mutualized |
		| IsSystemValidated | true       |
		| OrderId		    | 1          |


@v1 @subscription @request @creation
Scenario: Chang browses the Chang's subscription account
	Given the webapi is online
	And 'Chang' subscription account has been created
	And 'Mutualized' subscription has been created for 'Chang'
	When 'Chang' sends GET request with 'Mutualized' subscription
	Then only 'Mutualized' subscription has been return at 'Chang'

	@v1 @subscription @request @creation
Scenario: The order id has been incremented when chang requested multiple resquests for subscriptions
	Given the webapi is online
	And 'Chang' subscription account has been created
	And 'Mutualized' subscription has been created for 'Chang'
	And 'Dedicated' subscription has been created for 'Chang'
	And 'Premium' subscription has been created for 'Chang'
	When 'Chang' sends GET request subscription
	Then The subscription required that:
		| field             | value      |
		| Status            | Pending    |
		| Type              | Premium    |
		| Subscriber        | Chang      |
		| IsSystemValidated | true       |
		| OrderId		    | 3          |

