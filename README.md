School project for subject "Distributed systems" at FIT BUT. The goal was to create a program for "Beer exchange" ("Pivní burza") which can handle a large amount of requests. 
"Beer exchange" is a pub in Brno where you can buy beer for the current price on the exchange. 
The price of beer is changing based on demand and supply. 

We created this project as a collection of three programs. Two of these programs are on my GitHub profile. 
The first one is the UI for the user. It is written in PHP and not available on my GitHub profile. The second program is this one. It is a console application that is responsible for getting messages from Kafka and writing them to InfluxDB in the correct order. 
The last program is for testing the previous programs under stress. It creates a lot of requests in a very short amount of time and verifies the result.

The structure of this project is very straightforward. It runs in an infinite loop and in each iteration, it gets messages from Kafka and writes them to InfluxDB.

Notes I left for a teacher when submitting this project:
To start a program type "dotnet run". To run it you will need .NET CORE 3.1.
Program for reading messages from Kafka and writing them to InfluxDB.
You can configure login credentials for these tools in appsettings.json file.
The format of the message which program expects in a message obtained from Kafka tool is JSON serialization of KafkaMessage class (KafkaMessage.cs file).

![image](https://github.com/xsojka04/ConsolePdb/assets/52315948/b252e25f-2120-4766-80c2-d08024cdd90a)

