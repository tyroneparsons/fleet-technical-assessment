# Fleet Technical Assessment
This repo contains the source for my submission for the technical assessment. 
I'll document my thought process here while constructing the solution and provide some info on my decisions and reasoning.

Traditionally I might have approached this challenge with a standard MVC approach style implementation. 
Creating a PatientsService to handle the request that sits behind a PatientsController that mediates the request.

In the initial call a modular monolith architecture was mentioned with a stand in service bus, so that the modulars can be split off into microservices as and when required at a later stage. 
While I am not as familiar with how that pattern is most optimally implemented, I am familiar with feature/domain/modular or CQRS style approaches.

I've decided to implement a similar architecture for the purposes of this challenge, with a stand in for the service bus using Mass Transit.

I have used a basic folder structure based on the recommendations from David Fowler: https://gist.github.com/davidfowl/ed7564297c61fe9ab814

I have started by creating the host Web API project. I'm using minimal API's for this task as they work well with this architecture, avoiding some of the historical boilerplate with traditional controllers.
I have then set up a modules folder to host my patient specific module that should hopefully be easily extractable out to a microservice in the future. I created a class library to contain the patients domain.