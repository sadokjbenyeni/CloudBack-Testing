Feature: PaymentMethod creation feature
		All customers can create / update / delete the payment methods 

Background: 
	Given Morgan is authentificated with roles 'Admin'
	Given Chang is authentificated with roles 'Client'
	Given 'Chang' payment account has been created

@v1 @paymentMethod @creation @success @creditCard
Scenario: Chang creates a new Credit card payment method 
	Given the webapi is online
	When 'Chang' creates a new payment method with:
	| Holder          | Numbers             | Network | ExpirationDate | Cryptogram |
	| Chang's Company | 4510 6459 8301 6543 | Visa    | 03/2021        | 359        |
	Then Creation of payment method creation is successful

@v1 @paymentMethod @creation @success @creditCard
Scenario: Chang creates a new Credit card payment method with already created others cards
	Given the webapi is online
	And 5 credit cards has been already created by 'Chang'
	When 'Chang' creates a new payment method with:
	| Holder          | Numbers             | Network | ExpirationDate | Cryptogram |
	| Chang's Company | 4510 6459 8301 6543 | Visa    | 03/2021        | 359        |
	Then Creation of payment method creation is successful

@v1 @paymentMethod @creation @failed @creditCard
Scenario: The Chang's creation payment method request is failed with wrong credit card format
	Given the webapi is online
	When 'Chang' creates a new payment method with:
	| Holder          | Numbers             | Network | ExpirationDate | Cryptogram |
	| Chang's Company | 1111 2222 3333 4444 | Visa    | 12/2030        | 111        |
	Then Creation of payment method creation is not successful

@v1 @paymentMethod @browseAll @success @creditCard
Scenario: Chang browses credit cards list empty
	Given the webapi is online
	When 'Chang' browses all payment method
	Then result of request is empty

@v1 @paymentMethod @browseAll @success @creditCard
Scenario: Chang browses credit cards list contains the specific credit card
	Given the webapi is online
	And 'Chang' created payment method with:
	| Holder          | Numbers             | Network | ExpirationDate | Cryptogram |
	| Chang's Company | 4510 6459 8301 6543 | Visa    | 03/2021        | 359        |
	When 'Chang' browses all payment method
	Then the credit card has been return by the request

@v1 @paymentMethod @browseAll @success @creditCard
Scenario: Chang browses credit cards list all credit cards
	Given the webapi is online
	And 3 credit cards has been already created by 'Chang'
	When 'Chang' browses all payment method
	Then the result of the request contains 3 credit cards created

@v1 @paymentMethod @browse @success @creditCard
Scenario: Chang browses a specific credit cards
	Given the webapi is online
	And 'Chang' created payment method with:
	| Holder          | Numbers             | Network | ExpirationDate | Cryptogram |
	| Chang's Company | 4510 6459 8301 6543 | Visa    | 03/2021        | 359        |
	When 'Chang' browses 'Chang's Company' payment method
	Then only this credit cards has been returned

@v1 @paymentMethod @browse @failed @creditCard
Scenario: Chang browses with wrong payment method identifier and result is not found
	Given the webapi is online
	And 'Chang' created payment method with:
	| Holder          | Numbers             | Network | ExpirationDate | Cryptogram |
	| Chang's Company | 4510 6459 8301 6543 | Visa    | 03/2021        | 359        |
	When 'Chang' browses wrong id payment method
	Then the api return an not found result
