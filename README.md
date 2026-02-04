# Fleet Technical Assessment

## Thought Process
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

I then mocked up the call directly in the minimal API host so I have a baseline to start building from. 
I put in a placeholder for the unit tests so I can add in the tests as they go.

I started abstracting the logic out. I created a very simple patients repository that uses a simple in memory list as a store. 
I have gone with a repository pattern here to satisfy the in memory requirement and to make it simple to swap it out for a cache or db etc later. I considered using an in memory entity framework store, but I thought this would be over engineering this too much and introduce additional dependencies. 
The repository pattern also makes the unit tests easier to write.

I created a Patients Module extensions to register the required service easily using DI. At this point I am also not treating the source data as normalized in the way it might be in a real world system, with links to a normalized GP Practice for example.

I moved on to created a consumer feature for the Get Patient feature using MassTransit, so that this can be moved out to a real service bus at a later stage. I defined all of the possible outcomes including the not found result.

I added an error details object for the not found case so that all the response are nicely typed should you layer on Swagger docs later. This should probably be moved out to a shared portion of the solution to be reused elsewhere, possibly with a solid registry of errors and types.

I am debating if the endpoint registration should sit within the module or not. An endpoints extension could be exposed to make registering all endpoints simple. I am also debating if I should more realistically handle the repository with normalized in memory objects so that the repository to DTO in the consumer makes more realistic sense.

