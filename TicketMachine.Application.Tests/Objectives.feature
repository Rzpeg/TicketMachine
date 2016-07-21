Feature: Assignment Objectives
	In order to buy a ticket
	As a foregner
	I want to be able to search for stations while typing.

@Stanislav
Scenario: Partial station name search
	Given a list of four stations "DARTFORD", "DARTMOUTH", "TOWER HILL", "DERBY"
	When input "DART"
	Then should return: The characters of "F", "M" and the stations "DARTFORD", "DARTMOUTH"

Scenario: Full station name search
	Given a list of three stations "LIVERPOOL", "LIVERPOOL LIME STREET", "PADDINGTON"
	When input "LIVERPOOL"
	Then should return: The character of " " and the stations "LIVERPOOL", "LIVERPOOL LIME STREET"

Scenario: Non-existing station search
	Given a list of three stations "EUSTON", "LONDON BRIDGE", "VICTORIA"
	When input "LIVERPOOL"
	Then should return no possible stations and no possible characters