# JobHunter - UnitTests
##Description
Project JobHunterTests contains a set of unit tests for 3 services of JobHunter .NetCore 3.1 and Angular 8 applicaiton. 
Tests checks authorization, registration and main part of the app - job service. Project is using created db context for the appplication. Tester can use migration tool to create real database or use it in memory, but this is not recommended becouse it's not relational and can provide problems. Tests are using predefined data from app assumptions sometimes.

ATTENTION!
A few tests don't works when all tests are running. Required running it separately. 
## SetUp
### Real database
1. MsSql Server is required.
2. Create database for app. 
3. You need to set Model project as startup. 
4. Change connection string in JobHunterContext. 
5. Open package manager console and run update-database command.
6. In test classes change connection string in constructor.
### In memory database (not recommended setup)
Comment injeciton of db context using sql server and uncomment injection of in memory databases.

## AuthServiceTests
Class tests Login and getting session data.
## RegistraionServiceTests
Testing user creation.
## JobService tests
Set of tests for CRUD job offers and its lifecycle.

# Contact
darreur@gmail.com
