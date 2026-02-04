# Fleet Technical Assessment
This repository is my submission for the technical assessment. While arguably over-engineered for the scope of the requirements, I chose this approach to demonstrate how to implement a Modular Monolith architecture that is prepared for future scaling. I've written in some overall decisions as well as left my initial notes made while working through the project at the tailend.

## Running & Testing

### Run the API
From the https launch profile in Visual Studio

--or--

From the repository root:

```bash
dotnet run --project --debug src/Fleet.WebApi -c Debug
```

The API exposes the Patients endpoint:

```http
GET /patients/{id}
```

### Calling the API with the `.http` file
This solution includes an HTTP requests file for manual testing.

- **Step 1** Run the API (see above)
- **Step 2** Open the `Fleet.WebApi.http` file in Visual Studio
- **Step 3** Execute the requests from the editor

### Run automated tests
From the https Test Explorer in Visual Studio

-- or --

From the repository root:

```bash
dotnet test
```

## Architecture Overview
The solution follows a Modular Monolith design pattern, prioritizing high cohesion and low coupling.

- **Vertical Slicing:** Instead of traditional horizontal layers, the project is organized around features. This ensures that all logic for a specific capability (e.g., "Get Patient") resides within the same functional boundary.
- **Decoupled Communication:** The Host API does not call domain logic directly. Instead, it uses MassTransit as an in-process mediator/service bus. This allows any module to be extracted into a standalone Microservice in the future with minimal code changes, simply by swapping the transport layer from "In-Memory" to RabbitMQ or Azure Service Bus.
- **Minimal APIs:** Leverages .NET 9 Minimal APIs for a lightweight, performant routing layer, avoiding the overhead and boilerplate of traditional Controllers.

## Initial Strategy
Traditionally, I might have approached this challenge using a standard MVC pattern—creating a PatientsService to handle business logic behind a PatientsController. However, based on our initial discussion regarding a modular monolith architecture, I decided to showcase a more robust approach.

## Architectural Decisions
- **Service Bus Simulation:** I implemented MassTransit to serve as a stand-in for a distributed service bus. This ensures the modules are ready to be split into microservices whenever required.
- **Project Structure:** I followed the folder structure recommendations from David Fowler to ensure the solution remains organized and idiomatic.
- **Minimal APIs:** I chose Minimal APIs for the host project as they integrate seamlessly with this architecture and eliminate historical controller boilerplate.

## Implementation Details
- **Module Encapsulation:** I created a dedicated Modules folder containing the Patients domain as a class library. This keeps the domain logic strictly separated from the Web Host.
- **Repository Pattern:** I implemented a PatientRepository to satisfy the in-memory storage requirement. This abstraction makes it trivial to swap the storage provider for a real database (EF Core) or a cache (Redis) later. I opted against an EF Core In-Memory provider to keep dependencies lean.
- **Data Simulation:** I eventually moved to a more realistic, normalized data model. Using ConcurrentDictionary allowed me to simulate database indices, ensuring thread-safe, asynchronous execution for lookups.
- **Feature Consumers:** The "Get Patient" logic is implemented as a MassTransit Consumer. I defined explicit contracts for all possible outcomes, including PatientDetail and PatientNotFound results.
- **Typed Results & Error Handling:** I introduced an ErrorDetails object for 404 cases. This ensures responses are strongly typed, which is beneficial for generating clean OpenAPI/Swagger documentation.

## Evolution of the Design
During development, I deliberated on where endpoint registration should live. I ultimately decided on an Endpoint Registration Extension within the module. This allows the host project to remain "thin," simply calling app.MapPatientsModule() without needing to know the internal details of the module's routes.

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

I ended up creating an endpoint registration extension method and simulating the join on the database level using concurrent dictionaries to serve as indices and allow for async execution.

I downgraded Mass Transit so that I can maintain the Apache license requirement when building for release.