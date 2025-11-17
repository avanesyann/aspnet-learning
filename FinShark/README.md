## Web API

Following through Teddy Smith's ASP.NET Core Web API tutorial.


### Endpoints

An endpoint is a URL in your API that clients (front-end, mobile apps, other services) can send requests to.

Think of it as:

A specific address inside your API that performs a specific action.

Example: `https://example.com/api/videos/5`

This endpoint might return the video with ID = 5.

#### Why do we call it “endpoint”?

Because it’s the end point (final destination) of a request as it travels through:

- routing
- middleware pipeline
- controller
- action

Everything happens so the request can eventually hit an endpoint.