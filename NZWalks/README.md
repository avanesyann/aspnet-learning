## The ASP.NET Web API Mental Model

> Client -> Controller -> DTOs -> Domain Models -> Repository -> DbContext (EF Core) -> Database

### Client

What it knows:
- URLs
- HTTP verbs
- JSON

What it does not know:
- DbContext
- EF Core
- Domain models

### Controller (Entry point)

It's the door to the API.

What it does:
- Receives HTTP requests
- Validates input
- Returns HTTP responses

What it should contain:
- Routes
- Status codes
- DTO mappings

What it should not contain:
- Database logic
- EF Core queries
- Business rules

### DTOs

DTOs define what the client is alllowed to send and see

They protect:
- the database
- the internal logic
- future changes

### Domain Models (Entities)

- EF Core tracks them
- They represent tables
- They contain relationships

Who uses them:
- Repositories
- DbContext

### Mapping (DTO <-> Domain)

Two ways:
- Manual mapping (clear, verbose)
- AutoMapper (clean, scalable)

Mapping is just translation, nothing more.

### Repository

They exist to hide EF Core from the rest of the app.

Repositories:
- talk to DbContext
- return domain models
- contain query logic

Repositories do not:
- Return DTOs
- Handle HTTP
- Validate requests

### DbContext (EF Core)

The bridge between C# and the database.

What it does:
- Tracks changes
- Translates LINQ -> SQL
- Saves data

### Dependency Injection (the glue)

It exists so classes don't create their own dependencies.

ASP.NET:
- creates objects
- injects them
- manages lifetime

We never `new` repositories or DbContexts manually.

### Request flow

Let's say client sends POST /regions:
1. Client sends JSON -> CreateDto
2. Controller receives DTO
3. DTO -> Domain Model (mapping)
4. Domain Model -> Repository
5. Repository -> DbContext
6. DbConetxt -> Database
7. Database generates id
8. Domain Model updated
9. Domain -> ReadDto
10. Controller returns response

### Response (what client gets)

HTTP response contains:
- status code
- headers
- body (DTO)




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


## AutoMapper

AutoMapper is an object-to-object mapping library. It allows to simplify mapping process between two objects with different structures.
In ASP.NET Core, it is commonly used to map between domain models and view models.

AutoMapper:
- Maps properties with the same name
- Maps nested objects
- Converts between types
- Applies custom rules

It does not:
- Replace DTOs
- Replace repositories
- Replace EF Core
- Do validation
- Automatically save to DB

### ReverseMap()

`ReverseMap()` creates the mapping in both directions.
Instead of writing:
```
CreateMap<Region, RegionReadDto>();
CreateMap<RegionReadDto, Region>();
```
We write:
`CreateMap<Region, RegionReadDto>().ReverseMap();`

That single line means:

Region -> RegionReadDto
RegionReadDto -> Region

**Use `ReverseMap()` only when both directions are logically valid.**



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


## Repository Pattern

The Repository Pattern is a design pattern used in ASP.NET to organize how you application accesses the database.
Think of it as a middle layer between controllers and Entity Framework.

**In one sentence:** It's a pattern where you create a separate class whose job is to talk to the database, so your controller doesn't have to.

### Why do we use it?

Without a repository, a controller does everything:
- queries the database
- updates entities
- deletes rows
- handles business rules
- maps DTOs
- etc.
This makes controllers huge and messy.

-> With a repository, we move all DB logic to a single clean class.


### AddScoped

`AddScoped` tells ASP.NET how long an object should live.

`builder.Services.AddScoped<IRegionRepository, RegionRepository>();`

This line means:
**Create one instance of RegionRepository per HTTP request and reuse it everywhere during that request.**


### The 3 lifestimes in ASP.NET

- Transient -> New instance every time
- Scoped -> One instance per request
- Singleton -> One instance for the entire app

When a client sends a request:
`GET /api/regions`

ASP.NET:
1. Creates a scope
2. Create one RegionRepository
3. Creates one ApplicationDbContext
4. Uses them everywhere in that request
5. Disposes them after the response



## Navigation Properties

A navigation property is a property on an entity that points to another related entity.

In simple terms: They let you navigate between related tables using objects, not SQL.


Databases use:

- foreign keys
- joins

C# uses:

- objects
- references
- collections

Navigation properties are the bridge between those worlds.


### Two types of navigation properties
#### Reference navigation (one object)

`public Region Region { get; set; }`

Used in:

- many-to-one
- one-to-one

#### Collection navigation (many objects)

`public ICollection<Country> Countries { get; set; }`

Used in:

- one-to-many
- many-to-many


### Do navigation properties create columns in DB?

No.

Only foreign keys create columns.

Navigation properties exist:

- in C#
- in EF Core
- not in the database



## Model Validations

Model validation is how ASP.NET verifies that incoming data is acceptable before your code runs.

It answers one question:
> Is the data the client sent valid enough for us to process?

If the answer is no, your controller method should not run.


**Important rule:** Validation belongs on DTOs, not domain models


### How validation works

When a request arrives:

1. ASP.NET reads JSON
2. Tries to bind it to a DTO
3. Applies validation rules
4. Sets `ModelState.IsValid`
5. If invalid -> returns 400 Bad Request
6. If valid -> Controller runs



## Filtering

Filtering means returning only the data that matches some condition.

APIs rarely return all data.

Clients usually want:

- Regions with a specific code
- Items within a range
- Users with a status
- Records that match a search term

Filtering:

- reduces data size
- improves performance
- makes APIs usable


### Who is responsible for what?

**Client:** Specifies what to filter by

**Controller:** Reads filter parameters and passes them to repository

**Repository:** Applies filtering logic using LINQ

**Database:** Executes filtered SQL


Clients usually filter using query parameters:
```
GET /api/regions?code=EU
GET /api/regions?name=Europe
GET /api/regions?isActive=true
```



## Sorting

Sorting is the act of ordering data based on one or more fields.

Instead of just returning items, we decide in what order they appear.

Without sorting:

- Data is usually in *database insertion order*
- Responses can feel random
- Client often has to sort manually

> Client -> Controller -> Repository -> DbContext -> Database

```
GET /api/regions?sort=name_asc
GET /api/regions?sort=name_desc
GET /api/regions?sort=code_asc
```



## Pagination

Pagination is splitting a large result set into smaller, fixed-size chunks (pages).

Instead of returning everything, the API returns a portion of the data.

Example:

- Page 1 -> items 1-10
- Page 2 -> items 11-20
- Page 3 -> items 21 - 30


> Filter -> Sort -> Paginate -> Map -> Return

```
GET /api/regions?pageNumber=1&pageSize=10
GET /api/regions?pageNumber=2&pageSize=10
```



## Authentication

Authentication is the process of verifying the identity of a user.

Examples:
- Logging in with email + password
- Logging in with Google
- Using JWT tokens
- Using fingerprint / Face ID



## Authorization

Authorization happens after authentication.

It answers the question: "What can you access?"

Examples:
- Admin can delete users | User cannot
- User can see their own info | Not others'
- Only managers can access `/admin`



## JWT tokens

JSON Web Token is a compact, self-contained token used to prove that a user is authenticated.

In simple words: It's a digital ID card for the user.


### Why do we use JWT?

Without JWT (classic session)
- Server stores session in memory
- Doesn't scale well

With JWT
- Server does NOT store session
- Token contains all needed info
- Easy to scale


### JWT flow

Step 1: Login
Step 2: Server verifies credentials
Step 3: Server sends JWT to client
Step 4: Client stores token
- LocalStorage
- SessionStorage
- Memory
Step 5: Client sends token with every request
Step 6: Server validates token
- Signature
- Expiration
- Claims


### JWT vs Session

JWT is:
- Stateless
- Stored on client
- Scales well
- Good for APIs

Session is:
- Stateful
- Stored on server
- Harder to scale
- Good for MVC apps