Feature: PaymentAccount creation feature
		Morgan can create a new payment account after register on the site

Background: 
	Given Morgan is authentificated

@v1 @paymentAccount @creation 
Scenario: Morgan creates a new payment account after that customer has been registered on the web site
	Given the webapi is online
	When morgan sends the payment account creation request for 'Chang'
	Then Creation of payment account is successful


@v1 @paymentAccount @creation @reaquestById
Scenario: Chang browses his payment account
	Given the webapi is online
	And 'Chang' is authentificated
	And 'Chang' payment account has been created
	When 'Chang' gets his payment account
	Then get request return 'Chang' payment account description