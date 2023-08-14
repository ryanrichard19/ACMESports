<a href="https://www.medida.com/"><img src="./docs/images/medida-logo.svg" width="180px" align="right" /></a>

# Backend Team Lead position - Code Challenge

> We are a global digital marketing service provider who specializes in affiliate marketing & publishing. We are digital natives, data obsessed and focused on measurable outcomes. We are proud of our people and we have some of the most talented individuals you’ll ever meet working with us. Our values are at the heart of all decisions we make, from business goals to people initiatives and they have helped us to develop a world class team of experts, we are proud off. We’ve grown considerably in the past months and continually focus on growth via our global talent – you would be joining us at the most exciting time in our history.

Code challenge designed to evaluate technical skills of our [**Backend Team Lead** candidates](https://medida.breezy.hr/p/55df6c2fba1b01-backend-team-lead).

- [Backend Team Lead position - Code Challenge](#backend-team-lead-position---code-challenge)
  - [Introduction](#introduction)
  - [The Challenge](#the-challenge)
    - [Scenario](#scenario)
    - [Solution](#solution)
      - [Hints/Advices](#hintsadvices)
      - [Nice to have](#nice-to-have)
    - [Submission Guidelines](#submission-guidelines)
  - [License and Software Information](#license-and-software-information)
    - [License](#license)
  - [ATTENTION](#attention)
- [ACMESports API Design and Implementation Document](#acmesports-api-design-and-implementation-document)
  - [1. Understanding of the Assessment:](#1-understanding-of-the-assessment)
    - [Objective:](#objective)
    - [System Overview:](#system-overview)
    - [Challenges:](#challenges)
  - [2. Solutions for Potential Production Issues:](#2-solutions-for-potential-production-issues)
    - [2.1. High Latency:](#21-high-latency)
    - [2.2. Caching and Data Management:](#22-caching-and-data-management)
    - [2.3. Fault Tolerance:](#23-fault-tolerance)
    - [2.4. Unstable Third-party APIs:](#24-unstable-third-party-apis)
    - [2.5. Resilience Management:](#25-resilience-management)
  - [3. Logging and Telemetry:](#3-logging-and-telemetry)
    - [3.1 Logging:](#31-logging)
    - [3.2 Telemetry:](#32-telemetry)
    - [3.3 Anomaly Detection:](#33-anomaly-detection)
  - [4. Automation and API Maintenance:](#4-automation-and-api-maintenance)
  - [5. Contract Testing and Schema Verification:](#5-contract-testing-and-schema-verification)
    - [5.1 Pact for Contract Testing:](#51-pact-for-contract-testing)
    - [5.2. OpenAPI Schema Verification:](#52-openapi-schema-verification)
    - [5.3. Continuous Verification:](#53-continuous-verification)
  - [6. Security Considerations:](#6-security-considerations)
    - [6.1 Authentication \& Authorization:](#61-authentication--authorization)
    - [6.2 Data Protection:](#62-data-protection)
    - [6.3 API Security:](#63-api-security)
    - [6.4 Monitoring \& Anomaly Detection:](#64-monitoring--anomaly-detection)
    - [6.5 Third-party Dependencies:](#65-third-party-dependencies)
    - [6.6 Incident Response:](#66-incident-response)
  - [Project Implementation and Tooling](#project-implementation-and-tooling)
    - [Tooling:](#tooling)
      - [**1. .NET 7 Minimal API:**](#1-net-7-minimal-api)
      - [**2. Serilog:**](#2-serilog)
      - [**3. Polly:**](#3-polly)
    - [Patterns and Architectural Choices:](#patterns-and-architectural-choices)
      - [**1. Dependency Injection (DI):**](#1-dependency-injection-di)
      - [**2. Service Layer:**](#2-service-layer)
      - [**3. Custom Exception Handling:**](#3-custom-exception-handling)
      - [**4. OpenAPI-Generator Tool:**](#4-openapi-generator-tool)
  - [Getting Started](#getting-started)
    - [Prerequisites](#prerequisites)
    - [Setting Up the Project](#setting-up-the-project)
    - [Running Tests](#running-tests)

## Introduction

This challenge helps us to understand how you think through technical problems and how you might architect and implement solutions to those problems. It also helps us to assess your comfort level with various technologies and determine which projects you might start on, if you join our team.

We understand that everyone has existing personal and work commitments, so this challenge is not intended to use up a lot of your time. As such, there is no specific deadline or pre-set time allotment. Send us your submission once you’re comfortable with it. Some people prefer to dive right in and submit something right away, and others prefer spread out the time over a week or more - whatever works best for you and your schedule! Please note however, that our hiring process moves fast, so please send your submission as soon as possible.

If you have any questions or require any clarification, please [email](mailto:challenges@medida.com) us and we’ll get back to you as soon as we can.

## The Challenge

### Scenario

Our *fake client*, **ACME Sports**, wants to develop a RESTful API to return a list of NFL events in JSON format. The events data must be dynamic because the process will pull the NFL event data from a remote API that is frequently updated.

The remote API has been described using OpenAPI. You can see the OpenAPI specification opening this [file](./docs/apispecs/3rd-party-api/openapi.yaml).

In summary, the API provides us two endpoints for retrieving information events.

You can assume the remote API was well-documented so it will be returned the data in the exact same format, with the exact same field names and datatypes.

### Solution

For figuring out this challenge, you must implement the expected API, which has been described using OpenAPI as well, and you can see in [file](./docs/apispecs/challenge-api/openapi.yaml)

We are intentionally forgetting details about program languages or framework in order to write the solution, because we want that you can use the framework/object-oriented program language you are very familiar. For example, we know different frameworks that you could use for doing this challenge:

- **Python**: Django, Flask, FastAPI, Tornado, …
- **Java / Kotlin**: Spring Boot / Spring Framework, Quarkus, Micronauts, Microprofile, …
- **NodeJS**: Express.JS, Fastify, Nest.JS, …
- **.NET**: .NET core, .NET framework 4.x, 5.x…

Of course, a working implementation in code will be the best indicator of your abilities, however, if you prefer, you can describe how you would solve the problem in a PDF doc, you can attach drawings, doodles or screenshots, or you can write pseudo code - it’s up to you! Include any third-party libraries or frameworks you want to save time. **We like saving time too!**

#### Hints/Advices

- You should create a mock/stub of this remote API for typing your code. You can use [Postman](https://www.postman.com/), [Microcks](https://microcks.io/) or whatever you would rather with.

#### Nice to have

Below points are not mandatory, but we would like to see in your code 😀:

- You can include in your implementation some `architecture diagrams` in order to show us that you technically understood the challenge. In the case you are going to paint diagrams, you can use [Draw.io](https://drawio-app.com/use-draw-io-offline/) or [C4 models](https://c4model.com/) or whatever you prefer.
- Unit / integrations tests in order to check the APIs works fine! Here, you can use the Test Framework you are very familiar. Let us provide us some known frameworks (*):
  - **Java / Kotlin**: JUnit, TestNG
  - **NodeJS**: Jest, Mocha, Jasmine
  - **Python**: Pytest, …
- You can attach in your submission how to solve different issues in production environment we could face in a real scenario, such as high latency, caching, fault tolerance / resilient features or whatever you think we could consider.
  - If the remote API would be unstable, we don't want the user experience degrading because an API takes too long to respond.

**(*)** *If you dedice to implement unit tests, please, keep in mind you would need to mock objets for ensuring really are unit tests.*
**(*)** *If you dedice to implement integration tests, you can use 3rd party tools such as[**Testcontainers**], **Wiremock** for mocking the remote API.*

### Submission Guidelines

We strongly recommend to use `Git` in order to do the challenge, so maybe the best way for sending your submission would be creating a fork of this project, and pushing your code in a public/private `GitHub` repository. If you would dedice to push your code into a private Git repository, you must give right access to such repository to the `medidachallenges` GitHub username.

However, you can email to `challenges@medida.com` your submission. Please, don't attach any file in the email due to our SPAM restrictions. So, it would be better to send your code using [WeTransfer](https://wetransfer.com/) link or something like this.

Regardless what would it be the way you will choose, please use Git! We would like to see though the `Git Log` the development process you have been done.

## License and Software Information

© Medida

Medida publishes this software and accompanied documentation (if any) subject to the terms of the MIT license with the aim of opening some challenge wordings to the community. You will find a copy of the MIT license in the root folder of this package. All rights not explicitly granted to you under the MIT license remain the sole and exclusive property of Medida.

NOTICE: The software has been designed solely for the purpose of analyzing the code quality by checking the coding guidelines. The software is NOT designed, tested or verified for productive use whatsoever, nor or for any use related to high risk environments, such as health care, highly or fully autonomous driving, power plants, or other critical infrastructures or services.

If you want to contact Medida regarding the software, you can mail us at _challenges@medida.com_.

### License

[MIT](./LICENSE)

## ATTENTION
Do **NOT** try to PUSH direct to THIS repository!


# ACMESports API Design and Implementation Document

## 1. Understanding of the Assessment:
### Objective:
- Design, implement, and enhance an API system interacting with third-party sports services.
  
### System Overview:
- The system centralizes data from various sports APIs, presenting aggregated event data, scoreboards, and team rankings.
  
### Challenges:
- **External Dependencies:** Relying on third-party services introduces variability.
- **Data Aggregation:** Integration of diverse datasets for a comprehensive event view.
- **Error Handling:** Graceful error management ensures a smooth user experience.

## 2. Solutions for Potential Production Issues:

### 2.1. High Latency:
- Utilize load balancers, CDNs, and asynchronous operations.

### 2.2. Caching and Data Management:
- Implement in-memory caches such as Redis, use HTTP cache headers, and explore distributed cache systems.
- Factor in the frequency of data changes and the need for up-to-date data.
- Understand data change frequency, select cache locations wisely, and implement cache invalidation methods. Consider using stale-while-revalidate for optimal responsiveness.

### 2.3. Fault Tolerance:
- Basic retries have been implemented using Polly for exponential backoff.
- Further enhancements like circuit breakers, fallbacks, and timeouts are advised for comprehensive fault tolerance.

### 2.4. Unstable Third-party APIs:
- Implement throttling, monitoring, API mocks for testing, and ensure graceful degradation during slowdowns or failures.

### 2.5. Resilience Management:
- Incorporate the circuit breaker pattern, retry policies, fallback mechanisms, load balancing, and operation timeouts.

## 3. Logging and Telemetry:

### 3.1 Logging:
Key considerations for robust logging include:

- **Log Levels:** Ensure granularity by defining severity levels (`DEBUG`, `INFO`, `ERROR`).
- **Structured Logging:** Transition to machine-readable formats like JSON.
- **Centralized Solutions:** Tools like ELK and Graylog can centralize logs for easier analysis.
- **Log Rotation:** Prioritize log archiving and management.
- **Sanitization:** Safeguard sensitive information.
- **Correlation IDs:** Track transactions, especially in microservices architectures.

### 3.2 Telemetry:
For gaining pivotal insights in a production environment:

- **Monitor Metrics:** Focus on crucial metrics like response times or error rates.
- **Error Tracking:** Tools like Sentry offer real-time monitoring.
- **Application Insights:** Cloud services, such as Azure's, provide in-depth analytics.
- **User Analytics:** Understand user behavior with tools like Google Analytics.
- **Distributed Tracing:** Track requests across service touchpoints.
- **Alerting:** Automate notifications for specific events.
- **Dashboard Visualization:** Tools like Grafana provide a unified system view.
- **Budgeting & Learning:** Balance cost, ensure regulatory compliance, and learn from insights.

### 3.3 Anomaly Detection:
Monitor for anomalies in traffic patterns or usage which could be indicative of an issue or a potential security threat.

## 4. Automation and API Maintenance:
- Automate API changes with code generation tools.
- Seamlessly integrate with CI processes.
- Employ API versioning and maintain up-to-date documentation.

## 5. Contract Testing and Schema Verification:

### 5.1 Pact for Contract Testing:
- Use Pact for contract testing between services, ensuring adherence to agreed contracts.

### 5.2. OpenAPI Schema Verification:
- Regularly test the API against its OpenAPI schema for consistency.

### 5.3. Continuous Verification:
- Embed these tests in the CI/CD pipeline to ensure stability across deployments.

## 6. Security Considerations:

### 6.1 Authentication & Authorization:

- **Token-Based Authentication**: Utilize technologies like JWT (JSON Web Tokens) to securely transmit information between parties. 
- **OAuth**: Implement OAuth for token-based access, enabling third-party systems to interact without exposing user credentials.
- **Role-Based Access Control (RBAC)**: Define roles within your system and grant permissions accordingly to ensure that users can only perform actions relevant to their roles.

### 6.2 Data Protection:

- **Encryption**: Use encryption both at rest (e.g., databases) and in transit (e.g., HTTPS). Consider using industry standards like AES for data encryption and TLS for data transmission.
- **Sanitization**: Ensure that all input from users, especially those used in database queries, is sanitized to prevent SQL injections.
  
### 6.3 API Security:

- **Rate Limiting**: Implement rate limiting to prevent misuse of the API, which can lead to denial of service.
- **CORS**: Configure Cross-Origin Resource Sharing (CORS) properly to restrict which domains can access your API.
  
### 6.4 Monitoring & Anomaly Detection:

- **Real-time Monitoring**: Set up monitoring to get instant alerts for any suspicious activities.
- **Log Analysis**: Regularly analyze logs for any unusual access patterns or activities.
  
### 6.5 Third-party Dependencies:

- **Dependency Scanning**: Regularly scan project dependencies for vulnerabilities using tools like Snyk or Dependabot.
  
### 6.6 Incident Response:

- **Plan**: Have a well-defined incident response plan to handle potential security breaches. Regularly review and update the plan and conduct drills to ensure all stakeholders are aware of their roles in case of an incident.


## Project Implementation and Tooling

This project is based on .NET 7's Minimal API approach, focusing on creating lightweight and expressive endpoints without the burden of excessive boilerplate. The key choices made in the implementation are detailed below.

### Tooling:

#### **1. .NET 7 Minimal API:**
- **Purpose:** Building lightweight and expressive HTTP APIs with reduced boilerplate and improved efficiency.
- **Rationale:** The Minimal API feature in .NET 7 offers a way to streamline the process of setting up and managing web APIs, eliminating much of the ceremony and boilerplate associated with earlier versions of ASP.NET Core.

#### **2. Serilog:**
- **Purpose:** Enhanced logging mechanism to ensure that meaningful logs are captured during the application's runtime.
- **Rationale:** Serilog integrates seamlessly with the .NET environment and offers structured logging, making it easier to correlate logs and understand system behavior.

#### **3. Polly:**
- **Purpose:** Implement resilience and transient-fault-handling capabilities in the application, especially for HTTP requests.
- **Rationale:** When dealing with external systems or third-party APIs, it's crucial to handle potential failures gracefully. Polly offers a way to implement retries with exponential backoff, making sure our application can recover from transient faults.

### Patterns and Architectural Choices:

#### **1. Dependency Injection (DI):**
- **Description:** Using .NET's built-in DI mechanism to manage dependencies across the project.
- **Rationale:** DI simplifies object management and configuration, leading to more maintainable and testable code. It is especially beneficial when setting up services that require configurations or when setting up HTTP clients.

#### **2. Service Layer:**
- **Description:** Abstracting the core business logic and HTTP calls to third-party APIs inside dedicated services.
- **Rationale:** By abstracting this logic into a service layer, the application achieves a cleaner separation of concerns, making the codebase easier to manage and test.

#### **3. Custom Exception Handling:**
- **Description:** Defined custom exceptions (e.g., `NotFoundException`, `BadRequestException`) to handle various HTTP response scenarios.
- **Rationale:** Using specific exceptions for different error scenarios provides better clarity on the nature of the problem and offers a more granular approach to error handling.

#### **4. OpenAPI-Generator Tool:**
- **Description:** Initially considered to auto-generate client code, models, and other components from an OpenAPI definition.
- **Rationale:** Although OpenAPI tools can significantly speed up the development process, it was decided to revert to manual modeling due to excessive boilerplate. Instead, a minimalistic approach was adopted, cherry-picking only the necessary model definitions.

## Getting Started

### Prerequisites

1. **Git:** To clone the repository, ensure that you have [Git](https://git-scm.com/) installed on your machine.

2. **.NET 7 SDK:** The project is built using .NET 7, so ensure that you have the .NET 7 SDK installed. If not, download and install it from [here](https://dotnet.microsoft.com/download/dotnet/7.0).

### Setting Up the Project

1. **Clone the Repository:**

    ```bash
    git clone https://github.com/ryanrichard19/ACMESports.git
    ```

2. **Navigate to the ACMESportsAPI directory:**

    ```bash
    cd ACMESportsAPI
    ```

3. **Restore the Project:**

    Before running the project, it's essential to restore any .NET packages or dependencies.

    ```bash
    dotnet restore
    ```

4. **Modify Configuration (If Needed):**

    If there's a need to modify configurations such as database connection strings, API endpoints, or any other settings:

    - Open `appsettings.json` located in the root directory of `ACMESportsAPI`.
    - Make necessary changes and save the file.

5. **Run the Project:**

    ```bash
    dotnet run
    ```

    Upon successful execution, the API should be running, and you can access it via your browser or any API client.

### Running Tests

1. **Navigate to the ACMESportsTests directory:**

    ```bash
    cd ACMESportsTests
    ```

2. **Execute the Tests:**

    ```bash
    dotnet test
    ```

    This command will discover and execute all tests within the `ACMESportsTests` project.

    ### Running Integration Tests


1. **Boot up Prism using Docker-Compose**:
    ```bash
    docker-compose up

2. **Verify Prism is running**:

   Once Prism is up and running, you can access its mocked endpoints from your API tests or via tools like `curl` or Postman, typically at `http://0.0.0.0:4010`.

3. **Run your integration tests**
    ```bash
    pytest tests/integration_tests
