## Why use underscore in view names?

1. Convention for shared/reusable files

- Files that aren’t full pages (like layouts, partial views, or components) often get an underscore prefix.
- It makes it clear at a glance: “This is not meant to be navigated directly as a standalone page.”

2. To avoid accidental routing

- By default, MVC routing looks for views that match controller actions.
- Files with _ are not expected to be returned directly by controller actions (though technically they can).

3. Organizational clarity

- When you open the Views folder, you can quickly spot the shared/layout/partial files.
- Example: _Layout.cshtml, _ValidationScriptsPartial.cshtml, _ViewImports.cshtml.

**In short:**

We put an underscore **by convention** to show “this is not a standalone page, but a helper/shared file.”

*The default location for any partial view is the `Shared` folder.*



## Creating Tables

Anything to do with the database is inside `Data\ApplicationDbContext.cs`

To create a table we create a model, next we create a `DbSet` for that model and do a migration in tha package manager console (add-migration).
After the migration we type `update-database` so that the entity core will check if there are any migrations that have not been pushed to the db.
(EF keeps track of added migrations in the `__EFMigrationsHistory` table)

**Whenever anything has to be updated in a database we add a migration.**

*`SaveChanges()` sends all pending changes (inserts, updates, deletes) to the database.*



## Seeding

Seeding means automatically inserting initial data into your database when it’s first created or migrated.

Think of it like planting default values — categories, roles, admin users, countries, etc. — so your app starts with some data instead of an empty database.


### How to Seed Data in EF Core

#### Method 1: Model-based seeding (via `OnModelCreating`)

1. Open your `ApplicationDbContext.cs`

Add this inside your class:

```
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);

    modelBuilder.Entity<Category>().HasData(
        new Category { Id = 1, Name = "Electronics" },
        new Category { Id = 2, Name = "Books" },
        new Category { Id = 3, Name = "Games" }
    );
}
```

💡 You must specify the Id values manually for seeding, because EF Core needs to track them during migrations.

Now, open the Package Manager Console and run:

`Add-Migration SeedCategories`
`Update-Database`


EF Core will detect the new seed data and insert it into your Categories table automatically.