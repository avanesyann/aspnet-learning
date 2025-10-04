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

`public class HomeController : Controller`
`{`
`    private readonly EmailService _emailService;`

`    public HomeController()`
`    {`
`        _emailService = new EmailService(); // tightly coupled`
`    }`
`}`


- The Solution

`public class HomeController : Controller`
`{`
`    private readonly IEmailService _emailService;`

`    public HomeController(IEmailService emailService) // dependency is injected`
`    {`
`        _emailService = emailService;`
`    }`
`}`
