# Code Challenge - Rock, Paper, Scissors, Lizard, Spock game

## Prerequisites 
	- Visual Studio 2022, Rider or similar IDE
	- Docker

Once downloaded from **https://github.com/vukasin-djordjevic/RPSLS_CleanArchitecture.git**, find the _RPSSL_CleanArchitecture.sln__ file and open it using preferred IDE.

Select the _docker-compose_ option and click on the _Docker Compose_ command (one with the green arrow) : ![Screenshot 2024-11-18 001819](https://github.com/user-attachments/assets/0892d068-0b52-4df7-bcea-88150054ef16)

Once started, service becomes available for use.

## How to use it
If you prefere, you can access to it at **https://localhost:5001/swagger/index.html**. Of course, you can also use the Postman to test its functionalities.

## Available API endpoints
Base address for all API endpoints is **https://localhost:5001/api/RPSSL/V1** (in example, to play a new game, hit the address **https://localhost:5001/api/RPSSL/V1/play**)

### Choices
	Get all the choices that are usable for the UI.

  	GET: /choice
  	
   	Result: application/json
	[
	  {
	    “id”: integer [1-5],
	    "name" : string [12] (rock, paper, scissors, lizard, spock)
	  }
 	]

### Choice
	Get a randomly generated choice.

  	GET: /choice
  	
   	Result: application/json
	{
	  “id”: integer [1-5],
   	  "name" : string [12] (rock, paper, scissors, lizard, spock)
	}

### Play
	Play a round against a computer opponent.

  	POST: /play
  	
   	Data: application/json
	{
	  “player”: integer [1-5]
	}
 	
  	Result: application/json
	{
	  "results": string [12] (win, lose, tie),
	  "player": integer [1-5],
	  "computer": integer [1-5]
	} 
  
  ### Scoreboard
	Returns not more than last ten game results (if there is less thant ten results in the database, it will return all of them).

  	GET: /scoreboard
  	
   	Result: application/json
	[
	  {
	    "results": string [12] (win, lose, tie),
	    "player": integer [1-5],
	    "computer": integer [1-5],
	    "created": datetime
	  }
	]
 
  ### ResetScoreboard
	Deletes ALL the game results from the database.

  	DELETE: /resetScoreboard
  

## Brief info

Upon starting the solution, three docker containers will be created, _rpssl.api_ (container with the service), _rpssl.db_ (container with PostgreSQL database) and  _rpssl.seq_ (container with Seq by Datalust).

As previously mentioned, service listens at **https://localhost:5001**

Database is PostgreSQL and is available at port 5431. It uses local disk to keep the data between starts and stops of the service.

Logs are available at **http://localhost:8081/**. Logs are availble only during the running session.
