# Medical Practice Version 2
This is a personal project of mine.
So far the scope is to perform full CRUD functions on the User entity in the database, complete with a React.js frontend.

## Version
Ver 0.021 08.05.2024

## Installation
1. Create a 'Medical Practice' directory
2. Click into Medical Practice.
3. Extract Contents of MedicalPracticeV2 into the Medical Practice directory.
4. See 'Setup_MPv2Database.doc' to set up database locally and hook it up to the ASP.NET Core API (MPv2_API).

### After DB setup
1. To run the project simply click the 'http Run'
2. For MPv2_FrontEnd, on ther terminal in VSCode, type...
    ```bash
    npm start
    ```
3. API come with SwaggerUI to see the http requests to and from the database.
4. However you can copy the URLs to be used with Postman.
5. The URLs are structured as:

   *https://localhost:7290/api/{name of the entity}*

   E.g.:

    *https://localhost:7290/api/User/10001*

   Gets User with ID '10001'
   
   
