# Tiktik

# Overview
This project is part of a bigger project called Tiktik which is a N-Tier application which is intended for driving lessons registering and schedule between students and teachers. </br>
This repository contains the code we used for the server using ASP.NET CORE MVC framework to create a simple yet effective server for recieving, processing and returning messages to the User Inteface written in java, which is intended for an android application. view [TiktikUI](https://github.com/Yannnyan/TiktikUI) for more details.

# Architecture
1. Application user inteface written in java, for android users using async communication with the server and callbacks for updating the user view.
2. Server written in ASP.NET CORE MVC for processing and maintaining the bussiness logic of the application, and answering user requests.
3. Cloud database using Firebase free service, for writing the data, using async communication in the server side and Firebase built in API for C# which contains a ODM aswell.

# Bussiness Entities
- Lesson - The lesson contains the date and time, the comment for the place the student and teacher meets.
- Schedule - The Schedule contains a schedule for the planned lessons for the teacher
- Teacher - A Teacher class which has a Username, Password, Phone number, Email address, Name, Id, And a list of students. 
- Student - Has a Teacher, Username, password, Phone number, Email address, id.


# MVC
- View - The view is in another tier, bassically the android application.
- Model - The model in the system represents the bussiness entities and classes we user for parsing data.
- Controllers - Are simply the routes where the user can send REST API Messages to the server
- Services - Contains the logic of the application, That is were the server maintains the RAM memory about the hot data, i.e the active users, the teachers, the students caches, etc... 
- Database - Contains all the code for communicating with Firebase using C# firebase API.



# Things we struggled with
- We struggled to maintain a flowing version control with git, because we have different configuration files on each machine, and when we commited without a gitignore, we had some issues to run the code again.
- We had a struggle to maintain scrum's tough schedule
- We had disagreements about the implementation yet, it was good that we discussed these problems and fixes them with brainstorming.
- We had issues connecting the server to run on port forwarded way, such that every android user can connect to the server using it's public ip. </br>
To fix this issue and because we were connected to the university wifi we couldn't configure the router's ports, and therefore we using LAN(Local Address Network) ip given by NAT(Network Address Translation), and we settled for connecting only LAN devices. 
- We struggled to automate the process of publishing new server version everytime, we learned to use tools such as IIS(Internet Information Services) on windows, to controll the server when we need to close or reopen/ publish new version.

