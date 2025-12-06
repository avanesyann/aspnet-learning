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


## DTO

A Data Transfer Object is a simple C# class used to send data to the client or receive data from the client.

Think of them as **shaped data** - a safe and clean version of your database model.

### Why do we need them?

Because we NEVER want to expose our database models (entities) directly.

#### Bad (no DTO)

`public IActionResult CreateVideo(Video video)`

Problems:
- You expose all fields (including ones the client shouldn't change, like Id, CreatedAt, etc.)
- Security issues

#### Good (using DTO)

`public IActionResult CreateVideo(VideoCreateDto dto)`

Example DTO:
```
public class VideoCreateDto
{
    public string Title { get; set; }
    public List<string> Tags { get; set; }
}
```

Database entity stays internal:
```
public class Video
{
    public int Id { get; set; }
    public string Title { get; set; }
    public List<string> Tags { get; set; }
    public DateTime CreatedAt { get; set; }
}
```


## CreatedAtAction()

When you create a new resource, best API practice is:
- Return 201 Created
- Include the URL of the newly created resource (Location header)
- Include the created object in the response body
`CreatedAtAction` is a helper method that handles all three of these requirements.

The typical usage is:
```
return CreatedAtAction(
    nameof(GetById),        // Action name to generate URL for
    new { id = video.Id },  // Route values (URL parameters)
    video                   // Response body
);
```

1. `nameof(GetById)` tells ASP.NET > Use the route template from the GetById action to build the Location header.
If `GetById` is `[HttpGet("{id}")]`, ASP.NET knows the URL looks like: `/api/videos/52`.

2. `new { id = video.Id }`
These are the route parameters needed to construct the URL.
If your `GetById` route is:
```
[HttpGet("{id}")]
public IActionResult GetById(int id) { ... }
```

Then ASP.NET needs `{ id = 52 }` to build: `Location: /api/videos/52`.

3. `video` (the created object) becomes the response body.


## DTO Roles (Simple Version)

1. Create/Update DTOs -> What the client *sends* to you

These represent **input**.

They contain only the fields the client is *allowed* to set.

2. Read DTOs -> What the client *receives* from you

These represent **output**.

They contain fields you want to *show* to the client.


- When creating, the client does not send the ID -> they don't know it.
- When reading, the client must see the ID -> so they can update, delete, or fetch more.


## Asynchronous Programming

Asynchronous programming is a way of writing code that does not block the thread while waiting for something slow to finish.

### Synchronous (blocking)

Your code waits for a slow operation to finish.

Example:
```
var data = GetDataFromDatabase();   // waits here
return data;
```

While waiting:
- Your thread is blocked
- That thread cannot handle another request.
- If many requests come, your server slows down or crashes.

### Asynchronous (non-blocking)

Your code starts the slow operation, but does not wait.

Instead, the thread is released to do other work. 

Then when the result is ready, it "comes back" and continues.

Example:
```
var data = await GetDataFromDatabaseAsync();    // doesn't block
return data;
```

While waiting:
- The thread is free to handle other requests.
- Your server handles more traffic with fewer resources.


> Get the data.
> While you're fetching it, I'll handle other requests.
> Call me back when you're done.


#### Summary (super short)

Thread = a worker that executes code
Sync code = the worker waits doing nothing
Async code = the worker is freed to handle other tasks while waiting