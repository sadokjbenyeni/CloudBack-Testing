Feature: Subscription validation request feature
		Chang can create a new subscription after register on the site

Background:
	Given Morgan is authentificated with roles 'Administrator'
	And Chang is authentificated with roles 'Client'
	And the webapi is online
	And 'Chang' subscription account has been created
	And 'Mutualized' subscription has been created for 'Chang'

@v1 @subscription @request @creation
Scenario: Chang browses all subscription request
	When 'Chang' sends GET request these subscriptions
	Then subscriptions are returned for 'Chang'

@v1 @subscription @request @creation
Scenario: Chang browses description for specific subscription
	When 'Chang' sends GET request subscription
	Then The subscription required that:
		| field             | value      |
		| Status            | Pending    |
		| Subscriber        | Chang      |
		| Type              | Mutualized |
		| IsSystemValidated | true       |
		| OrderId           | 1          |

@v1 @subscription @request @creation @validationRequest @browse @data @readModel @ignore
Scenario: Morgan browses the Chang's mutualized subscription account
	When 'Morgan' sends GET admin request with 'Mutualized' subscription
	Then only 'Mutualized' subscription has been return at 'Chang'

@v1 @subscription @request @creation @validationRequest @browse @data @readModel
Scenario: Morgan browses subscription requests open
	Given populates repositry with subscription account and requests
	When 'Morgan' sends GET request on subscription request which are being created
	Then all subscription request has been return