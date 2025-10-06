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



## Creating Tables

Anything to do with the database is inside `Data\ApplicationDbContext.cs`

To create a table we create a property of type `DbSet` and do a migration (add-migration).
After the migration we type `update-database` so that the entity core will check if there are any migrations that have not been applied.
(EF keeps track of added migrations in the `__EFMigrationsHistory` table)