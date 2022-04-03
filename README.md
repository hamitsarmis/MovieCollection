## Running the sample

#### To run the client app you can execute in terminal:

cd movieclient/ <br/>
npm install <br/>
npm start <br/>

#### To run the backend you can execute in terminal:

cd MovieCollectionAPI/ <br/>
dotnet run

By default, this app uses mssqllocaldb, if you want to use some other database you can change connectionstring in MovieCollectionAPI/appsettings.json or MovieCollectionAPI/appsettings.Development.json accordingly.

#### Running the backend with docker:

You will have to change connectionstring in MovieCollectionAPI/appsettings.json to 
**Server=db;Database=MovieCollectionAPI;User=sa;Password=@someThingComplicated1234;**
before running the compose commands below:

cd MovieCollectionAPI/ <br/>
docker-compose build <br/>
docker-compose up

#### User Interface is generated with Angular 13.

Bootstrap used for creating components easily.
In the view of collections:<br/>
- All collections are listed
- User clicks on a collection and its movies and names are listed.
- If user logged in and the collection is theirs, they can see the add / remove buttons to add or remove movies from collections.<br/>
Also, they can change the name of collections or delete them.
- There is also a "new collection" button to create new collections

#### Unit tests can be found in MovieCollectionAPI.Tests project. Here is a rule that is tested:

Every user should be able to see collections of other people, but to be able to change only their own collection.

#### Additionally:
Besides having an API endpoint, swagger UI provided with Security Requirement and Security Definition for people who don’t know how to use Postman. <br/>
Managing movies (Create Update Delete) operations are only allowed for admin role. But, only backend codes are written because of time limitation.
