Feature: PaymentAccount creation feature
		Morgan can create a new payment account after register on the site

Background: 
	Given the webapi is online
	Given Morgan is authentificated with roles 'Admin'
	Given Chang is authentificated with roles 'Client'
	Given 'Chang' payment account has been created
	Given 'Chang' payment method has been created
	Given 'Chang' billing has been created

@v1 @billing @payment @smart2pay @creation 
Scenario: Payment service creates a new creation payment request at smart2Pay	
	When Subscription active event was received
	Then billing domain start the payment creation for smart2Pay

@v1 @billing @payment @smart2pay @creation 
Scenario: Payment service creates a new creation payment request at smart2Pay with data	
	When Subscription active event was received
	And the payment request was emitted by the billing
	Then billing information for the payment has been emitted with:
	| Amount | Currency | Card.HoldName | Card.Number | Card.ExpirationMonth | Card.ExpirationYear | Card.SecurityCode |
	| 100    | EUR      | Chang Company | 11111111111 | 10                   | 2022                | 123               |

@v1 @paymentAccount @creation @reaquestById
Scenario: Chang browses his payment account
	Given the webapi is online
	And Chang is authentificated with roles 'Client'
	And 'Chang' payment account has been created
	When 'Chang' gets his payment account
	Then get request return 'Chang' payment account description

@v1 @paymentAccount @creation @reaquestById
Scenario: Chang creates a new payment account but the system block the request
	Given the webapi is online
	And Chang is authentificated with roles 'Client'
	And 'Chang' payment account has been created
	When 'Chang' sends the payment account creation request for 'Morgan'
	Then Creation of payment account is not successful