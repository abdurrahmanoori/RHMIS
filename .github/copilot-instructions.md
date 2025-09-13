If a new property is added to a domain model, make sure to propagate the change wherever necessary: update the DTO, add a migration, update the database, modify the integration tests, adjust the database seeder, and apply changes in any other relevant places.



If I ask you to add a new entity, implement it in the same way as the Patient entity. That means writing all its operations (commands, queries, controller), creating a database seeder, adding a migration, updating the database, writing integration tests, and handling any other necessary tasks.



After completing any work, try to run all the tests without any error and build the project and make sure it builds successfully.



If you encounter any problem, avoid introducing solutions that could cause breaking changes in the project.



## React Application Instruction

I am new to React.js and using TypeScript. If I ask you to develop a feature or make changes (in the frontend or anywhere else), please:

- Keep the code as simple as possible, not overly complex.
- Avoid introducing breaking changes to the project.
- Remember that I don’t know much about React yet, so clarity and simplicity are important.
