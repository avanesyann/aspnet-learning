## REST

Representational State Transfer is an architectural style for building web services. It defines a set of rules and constraints on how clients (like browsers, mobile apps, or other backend services) communicate with servers.

*Think of REST as a philosophy about how APIs should be designed.*

### Key Ideas

1. Resources
Everything is treated as a resource: `users`, `videos`, `orders`

Each resource has a unique URL called an endpoint, like:
`GET /api/videos`
`GET /api/videos/5` 

2. REST uses HTTPS verbs to indicate actions
GET     - Read data
POST    - Create data
PUT     - Update (replace)
DELETE  - Remove data

3. Representation
A resources can be returned in many formats, typically:
- JSON
- XML
- Files

Example JSON from `/api/videos/1`
```
{
    "id": 1,
    "title": "Intro to ASP.NET",
    "tags": ["aspnet", "csharp"]
}
```


## Routing

Routing is the process of matching incoming HTTPS requests to appropriate action methods that handle those requests.


## DbContext

DbContext class is a class that represents a session with the database and provides a set of APIs for performing database operations.
It is responsible for maintaining a connection to the database, tracking changes to the data and performing crud operations (Create, Update, Delete, etc.).

In short, the DbContext class is a bridge between the domain models and the database.


## DbSet

A DbSet is a property of DbContext class that represents a collection of entities in the database.