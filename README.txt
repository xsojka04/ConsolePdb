Shool project for subject "Distributed systems" at FIT BUT. The goal was to create program for "Beer exchange" ("Pivní burza") which can handle big amount of requests. 
"Beer exchange" is a pub in Brno where you can buy beer for current price on the exchange. 
The price of beer is changing based on demand and supply. 

We created this project as a colletion of three programs. Two of these programs are on my GitHub profile. 
The first one is UI for user. It is written in PHP and not available on my GitHub profile. Second program is this one. It is a console application which is responsible for getting messages from Kafka and writing them to InfluxDB in correct order. 
Last program is for testing the previous programs under stress. It creates a lot of requests in a very short amount of time and verifies the result.

The structure of this project is very straight forward. It runs in infinite loop and in each iteration it gets messages from Kafka and writes them to InfluxDB.

Notes I left for a teacher when submitting this project:
To start a program type "dotnet run". In order to run it you will need .NET CORE 3.1 .
Program for reading messages from Kafka and writing them to InfluxDB.
You can configure login credentials for these tools in appsettings.json file.
Format of message which program expects in message obtained from Kafka tool is JSON serialization of KafkaMessage class (KafkaMessage.cs file).
