## LT Photo Album API

### Local Development

##### Setup

> Install SQL Server Express 2019
``` https://www.microsoft.com/en-us/Download/details.aspx?id=101064 ```

> Create Database in SQL Server
``` LTPA_DEV ```

> Local App Settings
``` Server=localhost\SQLEXPRESS;Database=LTPA_DEV;Trusted_Connection=True; ```

> Install dotnet ef CLI tools
``` > dotnet tool install --global dotnet-ef ```

> Run local migrations (root folder)
``` > dotnet ef database update --context MigrationContext --startup-project .\Data ```

##### Adding Migrations

> Use Migration Context (root folder)
``` > dotnet ef migrations add NameOfTheMigrationBeingCreated --context MigrationContext --startup-project .\Data ```
