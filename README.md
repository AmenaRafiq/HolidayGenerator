
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

In order to achieve this, I have decided to produce a Holiday Generator App. The services used in this application would generate random periods, months and countries to form a random potential holiday.

### User Stories
The following user stories served as the starting product backlog items, from which child tasks were devised.

> As a User I want to be able to see a randomly generated month so that I can plan when to book my holiday.

> As a User I want to be able to see a randomly generated period of days so that I can plan for how long to go on holiday.

> As a User I want to be able to see a randomly generated country so that I can plan where to go on my holiday.

## Architecture

### Service Architecture

[Service Architecture](https://raw.githubusercontent.com/AmenaRafiq/HolidayGenerator/main/docs/readme_pictures/servicearchitecture.PNG)

| Service | Host Resource | Objective |
|---|---|---|
Service 1: Days| Azure App Service (Web App)| Generates the period of days that the holiday should be for. Randomly selects a number from an array that holds periods as integers, i.e. [3, 7, 14, etc.]
Service 2: Month| Azure App Service (Web App)| Generates the month that the holiday should be in. Randomly selects a month from an enumeration consisting of the 12 months of the year, i.e. [JAN, FEB, MAR, etc.]
Service 3: Merge| Azure App Service (Web App)| Takes the period of days from service 1 and the month from service 2 using GET requests. Generates a random country, based on the month. If the month is a cold month (e.g. DEC), randomly select a country from an array that holds a list of hot countries. If the month is a warm month (e.g. AUG), randomly select a country from an array that holds a list of cold countries. Returns the country, the month and the period of days combined.
Service 4: Front-End | Azure App Service (Web App)| Makes a GET request to take the result from service 3, stores the information in a database and then outputs the results to the web page for the user to see.
Database | Azure SQL Database| Stores the results of service 4. The creation of the database entries take place in service 4.

### Database Structure

Below is a somewhat vain Entity Relationship Diagram showing the structure of the database, which has just one table to store the results. The front-end application creates entries in the database each time new objects are generated and passed to it, i.e. every time the page refreshes. 

[Database ER diagram](https://raw.githubusercontent.com/AmenaRafiq/HolidayGenerator/main/docs/readme_pictures/DB-ER.PNG)

### CI Pipeline

Below is the continuous integration pipeline diagram that depicts how the project's associated services and frameworks work together. 

[architecture image](https://raw.githubusercontent.com/AmenaRafiq/HolidayGenerator/main/docs/readme_pictures/architecture.PNG)

It shows how the tasks in the Kanban board are used to produce the code, which is stored in a Git repository. The development of the project's assets are organised by separate git branches; once the feature is complete, the branch is merged into the main branch. Usually the feature branch is then deleted - I have kept them in the repository for marking purposes only. A GitHub Actions pipeline was used to test, build and deploy the project to Azure resources that were set up and configured by a terraform script.

Here is a snippet of the terraform script, the full script can be viewed at [main.tf](https://github.com/AmenaRafiq/HolidayGenerator/blob/main/terraform/main.tf)

[Terraform snippet](https://raw.githubusercontent.com/AmenaRafiq/HolidayGenerator/main/docs/readme_pictures/terraform-snippet.PNG)

An Azure App Service is provisioned for the front-end app with configurations to give it access to the database and the merge app for the code's GET requests. 

Here is a snippet of the pipeline, the full script can be viewed at [main.yml](https://github.com/AmenaRafiq/HolidayGenerator/blob/main/.github/workflows/main.yml)

[Pipeline snippet](https://raw.githubusercontent.com/AmenaRafiq/HolidayGenerator/main/docs/readme_pictures/pipelinesnippet.PNG)

This snippet shows two jobs: test and build-and-deploy-days. Inside each job, steps are run on Linux machines, configured using workflow variables. The test job runs the test project [HolidayGeneratorTest](https://github.com/AmenaRafiq/HolidayGenerator/tree/main/code/HolidayGeneratorTest). The following job builds and deploys the [Days](https://github.com/AmenaRafiq/HolidayGenerator/tree/main/code/DaysService) app to Azure. The Month, Merge and Front-End apps are built and deployed with very similar jobs. The whole pipeline runs every time the main branch is pushed to, to allow for continuous deployment and integration.

## Project Tracking

Asana was used to create a Kanban Board to track the progress of the project. The Kanban Board can be found [here](https://app.asana.com/0/1200434306149251/board)
(Although the project is set to public, I might need to add you as a member so you can fully access the board)

The board was designed with columns that move a task along the board from left to right until completion: 
* Backlog
* To Do (used for current sprint items)
* Doing
* Done

Here is a snippet of the board to show the columns:
[Asana board snippet](https://raw.githubusercontent.com/AmenaRafiq/HolidayGenerator/main/docs/readme_pictures/asanaboardsnippet.PNG)

Here is a snippet of some of the work items, in which it is shown that the user stories were also part of the backlog: 
[Asana board work items snippet](https://raw.githubusercontent.com/AmenaRafiq/HolidayGenerator/main/docs/readme_pictures/sampleworkitems.PNG)


## Risk Assessment

insert risk assessment

## Testing

[XUnit](https://xunit.net/) has been used to run unit tests. This tool makes use of an Assertion Library so that tests can be written to compare the result of a function to a known output. The tests for all of the projects (Days, Month, Merge, Front-End) are run in the pipeline (as was shown in the pipeline section above). All four of the application's controllers have been tested to ensure the backend is fully functional so there are no faults for the front-end of each app. 

### Console Output

Below is a screenshot of the console output from running the tests which displays the number of tests passed or failed:

[console output image](https://raw.githubusercontent.com/AmenaRafiq/HolidayGenerator/main/docs/readme_pictures/testrun.PNG)

### Coverage Report

Below is a screenshot of the test coverage report that has been generated to display a breakdown of the successfully tested code in the app.

[coverage report image](https://raw.githubusercontent.com/AmenaRafiq/HolidayGenerator/main/docs/readme_pictures/codecoverage1.PNG)
[coverage report image](https://raw.githubusercontent.com/AmenaRafiq/HolidayGenerator/main/docs/readme_pictures/codecoverage2.PNG)


## Front-End Design

-

## Future Improvements

Deploying the 3 services (Days, Month and Merge) as Azure Function Apps would be a more economical approach. This is because although an App Service may be stopped, it still incurs charges for the selected plan. Azure Functions are 


## Author

Amena Rafiq

## License

[Apache License 2.0](https://github.com/AmenaRafiq/RecipeBlogApp/blob/main/LICENSE)