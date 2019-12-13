# Membership Card System #

## Summary ##

A collection of web service API's for kiosk terminals.

## Available information: ##

- Data cards consist of unique sequence of 16 alphanumeric characters.
- For registration information will be provided:
* unique employee ID
* name
* email
* mobile number
- A four digit ping will be chosen by the employee for further security.
- The application will timeout after 10 minutes.

## Assumptions made, due to lack of requirement information: ##

- It is assumed that when name is provided it will include first and last name (name and surname).
- In the brief it has not been specifically asked to provide and enpoint for setting a pin, thus this has not been implemented.
- In the brief it has been mentioned that the pin will be used, it is not a requirement to set a pin during registration, but it is important to know if the pin has been set before user tries to top up or log in, thus verify endpoint will inform both if the user has been registered and if they have set the pin.
- It has been assumed that pin will be used for security when loging in user, thus top up endpoint has been implemented with pin authentication. 
- It has been assumed that users will not store a significantly large amount of money in the card, thus a balance can only go up to 8 digit number (example - 10000000)
- It is a requirement that the appplication should time out after 10 minutes, but it is also a requirement to develop REST API service. REST API services should not be tracking user sessions and each request should be validated, authenticated and authorised, preferably using user or service tokens (depending on the API endpoint). However, as it is has been a requirement to implement a time out and no information is known about currently existing identity services, user pin will be stored in memory cache for 10 minutes, this will allow user to use top up endpoint for the next 10 minutes.
- It is a requirement to implement an enpoint that will provide a welcome and goodbye message.....(finish later)



## User Stories: ##

- As a card user I would like to register a card with the new system.
- As a card user I would like to be informed if my details have not been registered.
- As a card user I would like to be informed if my details have not been registered.
- As an employee I would like to top up my card with credit, so I could purhcase food in the future.
- As a card user I would like to see a greeting message with my name when I tap my card.
- As a card user I would like to see a goodbye message with my name when I tap my card the second time.
- As a card user I will need to present my pin for authentication.


## List of REST API's required to meet user stories: ##
- Verify enpoint - to check if the card has been registered and if pin has been set.
- Register endpoint - to register new card.
- Log in endpoint - to be used to authenticate if the card user is who they say they are.
- Name endpoint - to be used to retrieve name associated with the presented card (service endpoint). 
- Top Up endpoint - to be used to top up card balance (user endpoint).
- Status endpoint - to be integrated for testing purposes to check if the APIs are responding (can be implemented with Runscope).


WIP WIP WIP WIP

### Registration data model assumptions ###
- Phone number has been assumed to be up to 22 digits.
- Phone number has been chosen to be inputed as a string, so it's limit is restriced to 22  upon request and so it will be easier to validate later on.
- Phone number has not been validated whether it is accurate or not as it is unclear, which countrys phone numbers should be accepted.