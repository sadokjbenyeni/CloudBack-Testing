Feature: BillingPayment creation feature
		The system creates a new billing for Chang
        and creates a payment workflow in Smart2Pay

Background: 
	Given Morgan is authentificated with roles 'Administrator'

@v1 @paymentAccount @creation 
Scenario: Morgan creates a new payment account after that customer has been registered on the web site
	Given the webapi is online
	When Morgan sends the payment account creation request for 'Chang'
	Then Creation of payment account is successful


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