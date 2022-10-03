Feature: PlanMyJourneyTests


Scenario: Verify that a valid journey can be planned using the widget
	Given I enter locations
	| From Location                        | To Location                                |
	| London Bridge, London Bridge Station | Liverpool Street, Liverpool Street Station |
	When I click on plan my journey button
	Then I validate my journey results

Scenario Outline: Verify that the widget is unable to provide results when an invalid journey is planned
	Given I enter locations
	| From Location | To Location |
	| <From>         | <To>      |
	When I click on plan my journey button
	Then I should see a field validation error
	| Error   |
	| <Error> |
Examples: 
	| From     | To        | Error                                                                       |
	| 12345    | 546789    | Journey planner could not find any results to your search. Please try again |
	| 12340987 | 1234A0987 | Sorry, we can't find a journey matching your criteria                       |

Scenario: Verify that the widget is unable to plan a journey if no locations are entered into the widget. 
	When I click on plan my journey button
	Then I should see fieldvalidation errors
	| Error                       |
	| The From field is required. |
	| The To field is required.   |


Scenario: Verify change time link on the journey planner displays “Arriving” option and plan a journey based on arrival time
	Given I enter locations
	| From Location                        | To Location                                |
	| London Bridge, London Bridge Station | Liverpool Street, Liverpool Street Station |
	And I update Change time options
	| option   | Time  |
	| Arriving | 12:30 |
	When I click on plan my journey button
	Then I validate my updated journey results	

Scenario:On the Journey results page, verify that a journey can be amended by using the “Edit Journey” button
	Given I enter locations
	| From Location                        | To Location                                |
	| London Bridge, London Bridge Station | Liverpool Street, Liverpool Street Station |
	When I click on plan my journey button
	Then I validate my journey results
	When I click on edit journey button
	And I Edit the journey location details
	| From Location | To Location  |
	| Farringdon    | East Croydon |
	And I click on update my journey button
	Then I validate my journey results

Scenario: Verify that the “Recents” tab on the widget displays a list of recently planned journeys. (Note: This will only happen if all cookies are enabled).
	Given I enter locations
	| From Location                        | To Location                                |
	| London Bridge, London Bridge Station | Liverpool Street, Liverpool Street Station |
	When I click on plan my journey button
	Then I validate my journey results
	When I click on a result
	When I click on plan a journey reference link 
	And click on recent journey tab
	Then I should be able to see the recent jouneys


