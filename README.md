# skudatabase

This project is purposefully over engineered version of a very simple application which is suppose allow users to configure an very generic SKU database. 

## Objectives

The objectives of this projects is to explain product design, architectural patterns, design patterns, separation of concerns and many other best practices used in typical .Net full stack project.

1. Use of Respository and UnitOfWork Design Patterns to isolate and even switch between multiple kinds of databases. (Decoupling application/Business logic from the database)
2. Use of Domain Entities and Services layer to demonstrate how the business logic layer can be controled securely in an isolated environment.
3. Use of Result Pattern in the Services layer to efficently handle errors. 
4. Use of Data transfer objects to encapsulate domain entities from external sources to control the use/updation of data (Request Response Pattern) and to simplyfy data structure for presentation (View Models).
6. Use of Middlware/Middle Layer/API to facilitate interactions over Web.
7. Use of Multiple Frontend applications which consume the services/middleware apis to demonstrate a seemless user experience with the system accross platforms. 