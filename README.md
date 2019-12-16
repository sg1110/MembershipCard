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

- To stop the docker container running run kill.sh script. If this step is being followed from the step mentioned above please use the command mentione below to go back a folder.

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
- Swagger user interface allows to interact with the projects APIs and try them out.
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

- A four digit pin will be chosen by the employee for further security.
- The application will timeout after 10 minutes.

## Assumptions made, due to lack of information: ##
- It is assumed that when name is provided it will include first and last name (name and surname).
- In the brief it has not been specifically asked to provide and endpoint for setting a pin, thus this has not been implemented.
- In the brief it has been mentioned that the pin will be used, it is not a requirement to set a pin during registration, but it is important to know if the pin has been set before user tries to top up or log in, thus verify endpoint will inform both if the user has been registered and if they have set the pin.
- It has been assumed that pin will be used for security when logging in user, thus top up endpoint has been implemented with pin authentication. 
- It has been assumed that users will not store a significantly large amount of money in the card, thus a balance can only go up to 8 digit number (example - 10000000)
- It is a requirement that the application should time out after 10 minutes, but it is also a requirement to develop REST API service. REST API services should not be tracking user sessions and each request should be verified, authenticated and authorised, preferably using user or service tokens (depending on the API endpoint). However, as it is has been a requirement to implement a time out and no information is known about currently existing services that could provide user or service tokens, user pin will be stored in memory cache for 10 minutes, this will allow user to use top up endpoint for the next 10 minutes. 
- It is a requirement to implement an endpoint that will provide a goodbye message. A get endpoint that responds only with a goodbye message has been implemented. As it was not asked for the endpoint to log out the user this has not been implemented.

## To do: ## 
- Build script that will automatically set up the database with all tables and stored procedures.
- OAuth3 - identity - service - a service that would be responsible for issuing tokens and managing their expiration times. User access tokens should be issued after the log in, once the user has managed to successfully log in with their pin and card id. These services should be able to issue both user and service tokens, so the appropriate calls can be made when required.
- Log out endpoint - currently the log out endpoint only responds with a goodbye message. To improve this endpoint it should be changed to POST request that is also responsible for calling a service responsible for invalidating access tokens.
- Time outs - when a service issuing tokens is implemented, retries and timeouts calling this service should also be set. This will ensure that if there is a network or connectivity blip/issue the service gets called again and can be resolved successfully.
- Logs - currently no logging service has been implemented as it is unclear if the client would prefer the logs to be structured in any specific way. Logging service should be implemented and added to log all exceptions and when the operation on each API endpoint has been completed successfully
- Phone number validation - currently the phone number is not validated as it has not been specified, which countries phone numbers are accepted.
- Storing pin securely - due to lack of time pin is currently stored only as a string. To improve the security of storing a users pin the process should be changed to employ salted pin hashing.
- Exception handling middleware - an exception handling middleware should be implemented to return standard error responses. As it is unclear what format the client would like to see the errors formatted in, this has not been implemented.
- Testing - further extend integration tests to ensure that all unhappy path scenarios are documented and tested. All unhappy path scenarios have been tested through postman (such as validating request messages) and some through Xunit, but some are still missing. Since required and max string length attributes for request models are part of .net core features (and they should be already tested by .net core developers) these attributes have only been tested with few cases instead of all during integration tests.

## Current project limitations: ##
- Memory cache - using memory cache prevents this system from being highly scalable. Different instances of each application will contain different in memory caches, thus preventing top up endpoint from being properly used as each instance will only know certain cached pins. This can be fixed by using a distributed cache or even better - by using Oauth services and user and service tokens.
- Database connection string - currently is stored as a plain string in appsettings. Connection string of a real database should never be pushed into a github repository. If using aws services connection string could be stored in the parameter store or if deploying to on premise servers it could be injected through Octopus. For development purposed connection string could be stored a user secrets (https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-3.1&tabs=linux).

## User Stories: ##
- As a card user I would like to register a card with the new system.
- As a card user I would like to be informed if my details have not been registered.
- As a card user I would like to be informed if my details have not been registered.
- As a card user I would like to top up my card with credit, so I could purchase food in the future.
- As a card user I would like to see a greeting message with my name when I tap my card.
- As a card user I would like to see a goodbye message when I tap my card the second time.
- As a card user I will need to present my pin for authentication.

## List of REST API's required to meet user stories: ##
- Verify endpoint - to check if the card has been registered and if pin has been set.
- Register endpoint - to register new card.
- Log in endpoint - to be used to authenticate if the card user is who they say they are.
- Name endpoint - to be used to retrieve name associated with the presented card (service endpoint). 
- Top Up endpoint - to be used to top up card balance (user endpoint).
- Status endpoint - to be integrated for testing purposes to check if the APIs are responding (can be implemented with Runscope).
- To follow REST API best practises each standpoint should only be concerned about the task it is meant to resolve, should not track users state and should not redirect to any other pages.
- Single responsibility principle has been followed to ensure each API is only concerned with one clear purpose.
- The selection of API's has been developed to meet the business requirements and proposed journey for how to connect these APIs is shown below.
- API should be connected each card is first checked if it has been registered and if pin is present. 
- If card has not been registed it should redirect the user to registration page. If pin is missing it should be redirected to register pin page (as this has not been a requirement to implement, register pin endpoint is not shown in the journey below).
- User should be asked to log in first, before they are redirected to welcome screen and a top up page.
- Service call to get name should be made once it is clear that the registration has been completed sucessfully or it is known that the card has been registered.
- Welcome screen should show card owners name and a greeting message. This welcome message can either be on the same page as a top up screen or the welcome message can be shown and the card owner be redirected to a top up screen next.
- As it is a requirement to implement an endpoint for a goodbye message there is a log out API that can be used to retrieve the log out message. However, as the endpoint only sends back a goodbye string, in the future it is advised to either add further functionality to the log out endpoint or just automatically display a log out screen without a need to call a back end API.
![journey](images/journey.png?raw=true "journey")

## Database assumptions ##
- Phone number has been assumed to be up to 22 digits to accommodate most of the available phone numbers.
- Phone number has been chosen to be inputted as a string, so its limit is restricted to 22 upon request.
- Phone number has not been validated whether it is accurate or not as it is unclear, which countries phone numbers should be accepted.
- Currently card balance is stored in the same table as other card owner details as it has not been specified that the balance could exist in more than one currency.
- If each card is required to support multiple currencies a new table for card balance should be created. This will allow to specify a balance for a selection of different currencies.
- There is only one table since all details mentioned in this project relate to the card object.

## Testing: ##
- Log out endpoint - as it only returns a string it has only been tested using postman.
- This project has been tested by writing integration tests to ensure that all different scenarios go successfully through all controllers and reach the database.
- Full end to end integration test should be carried out once this collection of APIs is implemented by the client.
- In the future to ensure that the real database is not touched during testing an example database can be set up for testing purposes only, which can be achieved with a build script that is mentioned in the to do section.
- Due to lack of time unit tests were not written as the functionality of each function has been successfully and thoroughly tested using integration tests.
- Mocks and fakes should be used in the future where necessary. For example - when token issuance service is implemented instead of calling the service that issues a token it should be faked to return a token. This will intercept the service call and ensure that a successful response is always received. This can be especially useful when tests are ran as part of the deployment process and there might be flaky network between external dependencies and to reduce time it takes to run the tests.
![test1](images/test1.png?raw=true "test2")
![test2](images/test2.png?raw=true "test2")



## Other ##
### Status endpoint
- Not documented on swagger as its primary use is for API monitoring purposes (example - Runscope).