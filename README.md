
# Services App 

## Brief

The project brief outlines the overall objective: 

> To create an application that generates objects upon a set of predefined rules, utilising a service-orientated architecture.

The application must be composed of at least 4 services - the core of which serving as the front-end of the app and communicating with the other services which will serve as part of the back-end.
Service 3 must create an object based on the results of service 1 and 2 services combined - and then pass this to the front-end.

Core modules that this project will fixate on: software development with C#, agile development, version control, continuous integration, cloud fundamentals.

### Requirements

The requirements of the project are as follows:

* An Asana board (or equivalent Kanban board tech)
  * This could also provide a record of any issues or risks faced creating the project.
* Clear Documentation of the Design Phase, Architecture and Risk Assessment.
* An Application fully integrated using the Feature-Branch model into a Version Control System which will subsequently be built through a CI server and deployed to a cloud-based environment.
* If a change is made to a code base, adequately configured pipelines recreate and redeploy the changed application.
* The project must follow the Service-oriented architecture that has been asked for.
* The applications services should be deployed using Azure as a cloud platform.

## My Approach

In order to achieve this, I have decided to produce a Holiday Generator.


|Service| Host Resource| Objective
|---|---|---|
Service 1 - Days| Azure App Service (Web App)| Generates the period of days that the holiday should be for. Randomly selects a number from an array that holds periods as integers, i.e. [3, 7, 14, etc.]
Service 2 - Month| Azure App Service (Web App)| Generates the month that the holiday should be in. Randomly selects a month from an enumeration consisting of the 12 months of the year, i.e. [JAN, FEB, MAR, etc.]
Service 3 - Merge| Azure App Service (Web App)| Takes the period of days from service 1 and the month from service 2 using GET requests. Generates a random country, based on the month. If the month is a cold month (e.g. DEC), randomly select a country from an array that holds a list of hot countries. If the month is a warm month (e.g. AUG), randomly select a country from an array that holds a list of cold countries. Returns the country, the month and the period of days combined.
Service 4 - Front-End | Azure App Service (Web App)| Makes a GET request to take the result from service 3, stores the information in a database and then outputs the results to the web page for the user to see.
Database | Azure SQL Database| Stores the results of service 4. The creation of the database entries take place in service 4.

#### Service 1 - Days
This service will generate the period of days that the holiday should be for. This service will randomly select a number from an array that holds periods as integers, i.e. [3, 7, 14, etc.]

#### Service 2 - Month
This service will generate the month that the holiday should be in. This service will randomly select a month from an enumeration consisting of the 12 months of the year, i.e. [JAN, FEB, MAR, etc.]

#### Service 3 - Merge
This service will take the period of days from service 1 and the month from service 2 using GET requests. It will then generate a random country, based on the month. 
If the month is a cold month (e.g. DEC), it will randomly select a country from an array that holds a list of hot countries. If the month is a warm month (e.g. AUG), it will randomly select a country from an array that holds a list of cold countries. 
This service will then return the country, the month and the period of days combined.

#### Service 4 - Front-End
This will make a GET request to take the result from service 3, store the information in a database and then output the results to the web page for the user to see.


## Architecture
### Database Structure

### Database Structure

Below is an Entity Relationship Diagram showing the structure of the database. It shows the tables and the relationship between them. 

insert image

### CI Pipeline

Below is the continuous integration pipeline diagram that displays the project's associated services and frameworks. It shows how 

insert image

## Project Tracking

Asana was used to create a Kanban Board to track the progress of the project. The KanBan Board can be found [here]
(Ben although the project is set to public I might need to add you as a member so you can fully access the board)

The BackLog consists of the Epics and User Stories. 
add image

Sprints then break down these stories with the additions of child tasks. Two sprints were devised, unfinished work from Iteration 1 was moved to Iteration 2.

[Iteration 1]()
add image

[Iteration 2]()
add image

The Boards were designed with columns that move a task along the board from left to right in stages of: 
* New
* Active 
* Two stages of Completion: 
  * Resolved (for bug fixes)  
  * Closed (for the completion of tasks)

## Risk Assessment

insert risk assessment

## Testing

[XUnit](https://xunit.net/) has been used to run unit tests. This tool makes use of an Assertion Library so that tests can be written to compare the result of a function to a known output. The tests for all of the projects (Days, Month, Merge, Front-End) are run in the pipeline. The following image is the section of the pipeline which runs the tests:

[pipeline config image]()

### Console Output

Below is a screenshot of the console output from running the tests which displays the number of tests passed or failed:

[console output image]()

### Coverage Report

Below is a screenshot of the test coverage report that has been generated to display a breakdown of the successfully tested code in the app. The report is located in the HolidayGeneratorTest project

[coverage report image]()


## Front-End Design

-

## Future Improvements
Deploying the 3 services (Days, Month and Merge) as Azure Function Apps would be a more economical approach. This is because although an App Service may be stopped, it still incurs charges for the selected plan. Azure Functions are 


## Author

Amena Rafiq

## License

[Apache License 2.0](https://github.com/AmenaRafiq/RecipeBlogApp/blob/main/LICENSE)