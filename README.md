## Running the sample

##### To run the client app you can execute in terminal:

cd movieclient/ <br/>
npm install <br/>
npm start <br/>

##### To run the backend you can execute in terminal:

cd MovieCollectionAPI/ <br/>
dotnet run

By default, this app uses mssqllocaldb, if you want to use some other database you can change connectionstring in MovieCollectionAPI/appsettings.json or MovieCollectionAPI/appsettings.Development.json accordingly.

##### Running the backend with docker:

cd MovieCollectionAPI/ <br/>
docker-compose build <br/>
docker-compose up

You will have to change connectionstring in MovieCollectionAPI/appsettings.json to 

**Server=db;Database=MovieCollectionAPI;User=sa;Password=@someThingComplicated1234;**

before running the compose commands.
