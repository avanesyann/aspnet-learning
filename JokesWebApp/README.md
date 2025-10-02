## MVC

MVC stands for Model-View-Controller. It's a design pattern used in software development to separate converns in an application, making it more modular, scalable, and easier to manage.

### :wrench: Model

- **What it does:** Represent the **data** and **business** logic of the application.
- **Responsibilities:**
	- Manages the data (retrieve, insert, update delete from a database).
	- Notifies the view when the data changes (in some implementations).
- **Example:** A `User` model that retrieves user data from a database.


### :sunrise_over_mountains: View

- **What it does:** Displays the **data to the user** and sends user actions to the controller.
- **Responsibilities:**
	- Renders data from the model into a UI (HTML, JSON, GUI, etc.).
	- Minimal to no business logic.
- **Example:** An HTML page showing a user profile.


### :video_game: Controller

- **What it does:** Acts as the middleman between the model and the view.
- **Responsibilities:**
	- Handles user input (clicks, form submissions).
	- Updates the model based on input.
	- Selects the appropriate view to render.
- **Example:** A function that handles a form submission, updates the database, and returns a confirmation page.



## How MVC Works Together:

1. User interacts with the **View**.
1. **Controller** receives the input and decides what to do.
1. **Controller** updates the **Model**.
1. **Model** updates its state.
1. **View** fetches updated data from the **Model** and re-renders.


## Simple Example

Suppose you're logging into a website:

- **Model:** Checks if the username/password match in the database.
- **View:** Shows the login form and later the success or error message.
- **Controller:** Processes the login request and tells the model to verify credentials.