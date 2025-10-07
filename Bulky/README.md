## Routing

Routing is the mechanism that **maps incoming requests (URLs) to specific code that should handle them**.

When a user types a URL (like `/products/5`), the routing system decides which *controller and action (or method)* should handle that request.

The URL pattern for routing is considered after the domain name.
- `https://localhost:55555/Category/Index/3`
- `https://localhost:55555/{controller}/{action}/{id}`


## Dependency Injection

Dependency Injection is a design pattern used to achieve loose coupling between classes.

It means:
Instead of a class creating the objects (dependencies) it needs by itself, those objects are provided ("injected") from the outside - usually by a DI container.

- The Problem Without DI
```
public class HomeController : Controller
{
    private readonly EmailService _emailService;

    public HomeController()
    {
        _emailService = new EmailService(); // tightly coupled
    }
}
```

- The Solution
```
public class HomeController : Controller
{
    private readonly IEmailService _emailService;

    public HomeController(IEmailService emailService) // dependency is injected
    {
        _emailService = emailService;
    }
}
```


## Entity Framework

Entitiy Framework is an Object-Relational Mapper (ORM) for .NET.
It allows you to **interact with a database using C# objects**, instead of writing SQL queries manually.

Normally, to get data from a database, we'd write SQL:
`SELECT * FROM Products WHERE Id = 1;`

Then we'd manually convert the result into a Product object.

With **Entity Framework**, we just write:
`var product = context.Products.FirstOrDefault(p => p.Id == 1);`

EF automatically:
- Builds the SQL query
- Sends it to the database
- Converts the result into a Product object
- Tracks changes so you can later SaveChanges() to update the database