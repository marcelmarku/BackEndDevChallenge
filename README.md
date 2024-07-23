# BackEndDevChallenge

Setup Instructions:
Update the connection string for MathDatabase in the appsettings.json file to match your local environment. Do not store credentials and other identifying data

Apply Database Migration:
Run the UpdateMathProblem migration to update the database. 

Logging
The application will create a Logs table for logging system exceptions.

-------------------------------------------------
Fork the Repository:
Fork the public repository into your own GitHub account.

Set Up a Local Database:
Ensure you have a local SQL Server database ready to use.

Update Connection String:
Modify the connection string in CalculatorContext.cs to point to your local database.

Apply Database Migration:
Run the Entity Framework migration 20240722024700_InitialCreate.cs to set up the database schema.

After these steps are completed, running the application should allow easy testing using swagger.  Results should be saved in the local DB.
