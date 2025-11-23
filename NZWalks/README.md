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