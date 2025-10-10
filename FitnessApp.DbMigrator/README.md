# DbMigrator

This application is for handling database migration and seeding for dev or prod use.
The idea is to have a way to migrate locally to test migration before production, seed locally for testing purposes, or migrate/seed in CI/CD for prod.

## Run all (migrate + seed)
```
dotnet run --project FitnessApp.DbMigrator
```
## Just migrate
```
dotnet run --project FitnessApp.DbMigrator migrate
```
## Just seed
```
dotnet run --project FitnessApp.DbMigrator seed
```
## Reset database (careful!)
```
dotnet run --project FitnessApp.DbMigrator reset
```
You’ll get a confirmation prompt before deletion.


## Reverting migration
This takes a second argument for the name of the migration you want to revert to.

> CAUTION: This is only for local development testing for reverting test migrations
```
dotnet run --project ./FitnessApp.DbMigrator revert LastGoodMigration
```

## With custom connection string
```
dotnet run --project FitnessApp.DbMigrator -- \
  --ConnectionStrings:DefaultConnection="Host=prod-db;Database=fitness;..."
```

## Example for CI/CD
```
- name: Backup Database
  run: pg_dump ${{ secrets.DB_CONNECTION_STRING }} > backup.sql

- name: Run Migrations
  run: dotnet run --project ./FitnessApp.DbMigrator/FitnessApp.DbMigrator.csproj migrate

# if fails:
# restore with psql < backup.sql
```

This will:
1. Use the production database connection string from GitHub Secrets.
2. Run migrations.
3. Seed initial data.
4. Create a backup before migration to ensure that if something goes wrong it can be restored

## Notes for Contributors

This project depends on the FitnessApp.Data project for:

- FitnessAppDbContext
- DatabaseSeeder

Always verify your migrations with:
```
dotnet ef migrations list --project ./FitnessApp.Data
```

To add a new migration:
```
dotnet ef migrations add MigrationName --project ./FitnessApp.Data -
```


## Troubleshooting
| Issue                                     | Fix                                                                                                |
| ----------------------------------------- | -------------------------------------------------------------------------------------------------- |
| **`ERROR: Connection string not found!`** | Ensure `ConnectionStrings__DefaultConnection` or `appsettings.json` is set.                        |
| **Migrations don’t run**                  | Make sure the DbContext in `FitnessApp.Data` matches your database provider and connection string. |
| **Permission denied in CI/CD**            | Ensure your secret has correct credentials and the DB allows remote connections.                   |


