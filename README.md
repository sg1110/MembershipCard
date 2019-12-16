# Membership Card System #

## Summary ##
A collection of web service API's for kiosk terminals.

## How to build the project: ##

### Build requirements: ###
- .net core 2.2
- docker

### Setup Database ### 
- To start the sql server running in docker go into database folder

```  
cd database/MembershipCard/  
```

and run runDb shell script 

```
./runDb.sh 
```

This will start microsoft/mssql-server-linux docker image and set up the password for the server that is referenced throughout this project
- To create the appropriate tables and sprocs that are used throughout this project go to create_db folder.

```  
cd create_db 
``` 

Connect to the mssql server and run the commands that are listed in create_db folder. These commands will create the database, table and the required sprocs.

- To stop the docker container running run the script mentioned below go back a folder and run the kill.sh script.

```
cd .. 
./kill.sh  
```

## How to build and run all tests: ##
- The project is set up with the cake build script that will build and run all tests found in this project.
- To run the cake build, go back to the main root directory and run the script
```
./build.sh
```
![Cake Script](images/cake-script.png?raw=true "cake-script-example")
- The cake script can be further configured and used as part of the deployment pipeline. Currently build configuration is set to as "Release". Release configuration builds a version of the app that can be deployed.

## How to use swagger: ##
- The project comes with swagger integrated, which has been modified to fully document the endpoints.
- This allows user to review the expected response codes and request data models.
- Swagger user interface also allows to interreact with the projects APIs and try them out.
- To run swagger, ensure that the project is running and go to the path 

https://localhost:5001/card/swagger/index.html

![Swagger example](images/swagger-ex.png?raw=true "swagger-example")

json version of the swagger interface can be found at
https://localhost:5001/card/v1/_interface

## Available requirement information from the project brief: ##
- Data cards contain and ID consisting of unique sequence of 16 alphanumeric characters.
- For registration information that will be provided is listed below:

* unique employee ID
* name
* email
* mobile number

- A four digit ping will be chosen by the employee for further security.
- The application will timeout after 10 minutes.

## Assumptions made, due to lack of information: ##
- It is assumed that when name is provided it will include first and last name (name and surname).
- In the brief it has not been specifically asked to provide and endpoint for setting a pin, thus this has not been implemented.
- In the brief it has been mentioned that the pin will be used, it is not a requirement to set a pin during registration, but it is important to know if the pin has been set before user tries to top up or log in, thus verify endpoint will inform both if the user has been registered and if they have set the pin.
- It has been assumed that pin will be used for security when logging in user, thus top up endpoint has been implemented with pin authentication. 
- It has been assumed that users will not store a significantly large amount of money in the card, thus a balance can only go up to 8 digit number (example - 10000000)
- It is a requirement that the application should time out after 10 minutes, but it is also a requirement to develop REST API service. REST API services should not be tracking user sessions and each request should be verified, authenticated and authorised, preferably using user or service tokens (depending on the API endpoint). However, as it is has been a requirement to implement a time out and no information is known about currently existing services that could provide user or service tokens, user pin will be stored in memory cache for 10 minutes, this will allow user to use top up endpoint for the next 10 minutes. 
- It is a requirement to implement an endpoint that will provide a goodbye message. A get endpoint that responds only with a goodbye message has been implemented. As it was not asked for the endpoint to log out the user this has not been implemented.
- Exception middleware - 


## To do: ## 
- Build script that will automatically set up the database with all tables and stored procedures
- OAuth - service - a service that would be responsible for issuing tokens and managing their expiration times. These tokens should be issued after the log in, once the user has managed to successfully log in with their pin and card id.
- Log out endpoint - currently the log out endpoint only responds with a goodbye message. To improve this endpoint it should be changed to POST request that is also responsible for calling a service for invalidating access tokens.
- Time outs - when a service issuing tokens is implemented, retries and timeouts calling this service should also be set. This will ensure that if there is a network or connectivity blip/issue the service gets called again and can be resolved successfully.
- Logs - currently no logging service has been implemented as it is unclear if the client would prefer the logs to be structured in any specific way. Logging service should be implemented and added to log all exceptions and when the operation on each API endpoint has been completed successfully
- Phone number validation - currently the phone number is not validated as it has not been specified, which countries phone numbers are accepted.
- Storing pin securely - due to lack of time pin is currently stored only as a string. To improve security storing of pin should be changed to employ salted pin hashing.
- Exception handling middleware - an exception handling middleware should be implemented to return standard error responses. As it is unclear what format the client would like to see the errors formatted in, this has not been implemented.

## Current project limitations: ##
- Memory cache - using memory cache prevents this system from being highly scalable. Different instances of each application will contain different in memory caches, thus preventing from top up endpoint from being used properly and remembering all cached pins. This can be fixed by using a distributed cache or even better - by using Oauth services and user and service tokens.
- Database connection string - currently is stored as a plain string in appsettings. Connection string of a real database should never be pushed into a github repository. If using aws services connection string could be stored in the parameter store or if deploying to on premise servers it could be injected through Octopus. For development purposed connection string could be stored a user secrets (https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-3.1&tabs=linux).

## User Stories: ##
- As a card user I would like to register a card with the new system.
- As a card user I would like to be informed if my details have not been registered.
- As a card user I would like to be informed if my details have not been registered.
- As an employee I would like to top up my card with credit, so I could purchase food in the future.
- As a card user I would like to see a greeting message with my name when I tap my card.
- As a card user I would like to see a goodbye message with my name when I tap my card the second time.
- As a card user I will need to present my pin for authentication.

## List of REST API's required to meet user stories: ##
- Verify endpoint - to check if the card has been registered and if pin has been set.
- Register endpoint - to register new card.
- Log in endpoint - to be used to authenticate if the card user is who they say they are.
- Name endpoint - to be used to retrieve name associated with the presented card (service endpoint). 
- Top Up endpoint - to be used to top up card balance (user endpoint).
- Status endpoint - to be integrated for testing purposes to check if the APIs are responding (can be implemented with Runscope).
- To follow REST API best practises each standpoint should only be concerned about the task it is meant to resolve, should not track users state and should not redirect to any other pages.
- Single responsibility principle has been followed to ensure each API is only concerned with one task.
- The selection of API's has been developed to meet the business requirements and proposed journey for how to connect these APIs is show below
![journey](images/journey.png?raw=true "journey")

## Database assumptions ##
- Phone number has been assumed to be up to 22 digits to accommodate most of the available phone numbers.
- Phone number has been chosen to be inputted as a string, so its limit is restricted to 22 upon request.
- Phone number has not been validated whether it is accurate or not as it is unclear, which countries phone numbers should be accepted.
- Currently card balance is stored in the same table as card as it has not been specified that the balance could exist in more than one currency.
- There is only one table since all details mentioned in this project relate to the card object.

## Testing: ##
- Log out endpoint - as it only returns a string it has been tested only using postman.
- This project has been tested by writing integration tests to ensure that all different scenarios go successfully through all controllers and reach the database.
- Full end to end integration test should be carried out once this collection of APIs is implemented by the client.
- In the future to ensure that the real database is not touched during testing an example database can be set up during testing, which can be achieved with a build script that is mentioned in to do section.
- Due to lack of time unit tests were not written as the functionality of each function has been successfully tested using integration tests.
- Mocks and fakes should be used in the future where necessary. For example - when token issuance service is implemented instead of calling the service that issues tokens it should be faked to return a token. This will intercept the service call and ensure that a successful response is always received. This can be especially useful when tests are ran as part of the deployment process and there might be flaky network between external dependencies and to reduce time it takes to run the tests.
![test1](images/test1.png?raw=true "test2")
![test2](images/test2.png?raw=true "test2")



## Other ##
### Status endpoint
- Not documented on swagger as its primary use is for API monitoring purposes (example - Runscope).