This is a forum webapp with chat function, post, comment and rate, account and stuff TBA (will append more to this later), Code-first database
This is also an environment to practice working on an actual production-level project:
+ Emulating Scrum method
+ Using Taiga or Jira to keep track of tasks
+ Personal reminder to stay on track and work on time
+ Maybe will record personal video to emulate daily meeting, sprint review, etc.
+ Force self to code according to some popular design pattern (Repository, Unit of Work, Dependency Injection, Blue-Green, etc.)
After this is done, this project will be remade in Java via Spring Boot
Also DevOps stuff if I am fluent enough in these full-stack stuff first
OH and also, practice good git work ethics by:
+ Every task must be worked on and pushed to a separate branch for merging
+ Check merge request every time and merge accordingly
(this doesn't work that well with a single person working but will try my best to emulate such process)
Ok so both my personal acc and school acc is working on this repo, so how this will work is that:
+ I will do work on my school, push to separate branch following naming scheme (MyName_#TaskNo.-TaskSummary)
+ Then swap to my personal, check and approve merge request (will merge most of the time without conflict but f- it we ball)

Technology used (or to be used):
Language used: C#
Framework used: .NET 8
- MSSQL Server (will migrate to PostgreSQL or MySQL soon)
- Firebase
- SignalR or Kafka
- ASP.NET
- React and JS for front-end (not experienced, will update to more popular front-end technology as I go)
- .NET Mailkit
- AutoMapper
- VNPay and Momo UAT app/site for emulating payments (might checkout popular international payment like PayPal)

TODO list:
[x] Login and register function
[x] Implement basic security via jwt token and password hashing
[x] Create Taiga project and tasks
[] Setup an email sender for many related tasks
[] Add more related objects with medium-high relationship complexity 
[] CRUD functions and other misc. functions for all objects
[] Create a live chathub using either SignalR or Kafka
[] Upload pictures and files to another database like firebase
[] Setup CI/CD pipeline
[] Deploy the app in Swagger form
[] Develop a front-end 
[] Create separate environment for Development and Production
[] Research and implement way to keep secret key and API secure
[] Setup a global exception handler
[] Change some object's database ID scheme to use UID
[] Paging for GET functions
[] Research and implement payment functions and transactions management
[] TBA...

Technology to research:
+ All front-end tech (React, ReactJS, Angular, JS, whatever else...)
+ Automapper custom model uh... thing
+ Live chathub (Kafka and SignalR)
+ Integrate firebase
+ Momo payment and VNPay
