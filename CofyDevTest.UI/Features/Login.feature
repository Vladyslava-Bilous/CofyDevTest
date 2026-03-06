Feature: Login

	As a carrier provider, 
	I want a username/password login on the tracking website, 
	so that only genuine customers can access their tracking data.

Scenario: Open website as not logged in user
	Given I’m not logged in with a genuine user
	When I navigate to 'our-team' page on the tracking site
	Then I am presented with a login screen

Scenario Outline: Log in as a valid user
	Given '<UserType>' user is already registered
	And I’m on the login screen
	When I enter credentials of '<UserType>' user
	Then I am logged in successfully
Examples: 
	 | UserType    |
	 | RegularUser |