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
    - [2.2. Caching:](#22-caching)
    - [2.3. Fault Tolerance:](#23-fault-tolerance)
    - [2.4. Unstable Third-party APIs:](#24-unstable-third-party-apis)
  - [3. Logging and Telemetry:](#3-logging-and-telemetry)
    - [Logging:](#logging)
    - [Telemetry:](#telemetry)
  - [4. Resilience Management:](#4-resilience-management)
  - [5. Caching Strategy:](#5-caching-strategy)
  - [6. Automation and OpenAPI Schema:](#6-automation-and-openapi-schema)

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
  
### 2.2. Caching:
- Implement in-memory caches like Redis, use HTTP cache headers, and consider distributed cache systems.
  
### 2.3. Fault Tolerance:
- Apply retries with exponential backoff, circuit breakers, fallbacks, and timeouts.
  
### 2.4. Unstable Third-party APIs:
- Implement throttling, monitoring, API mocks for testing, and ensure graceful degradation during slowdowns or failures.

## 3. Logging and Telemetry:
### Logging:
- Utilize log levels, structured logging, centralized solutions, log rotation, sanitization, and correlation IDs.
  
### Telemetry:
- Monitor metrics, error tracking, application insights, user analytics, distributed tracing, alerting, and employ dashboard visualization. Ensure cost-effectiveness, compliance, and retrospective learning.

## 4. Resilience Management:
- Incorporate the circuit breaker pattern, retry policies, fallback mechanisms, load balancing, and operation timeouts.

## 5. Caching Strategy:
- Understand data change frequency, choose cache locations wisely, and implement cache invalidation methods. Consider using stale-while-revalidate for optimal responsiveness.

## 6. Automation and OpenAPI Schema:
- Use code generation tools for API changes, integrate with CI processes, adopt API versioning, and keep documentation synchronized.