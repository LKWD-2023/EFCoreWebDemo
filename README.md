(You already did this in class, but just as a reminder, before the first time using Entity Framework Core, you'll need to run the following command at the command line)

```
dotnet tool install --global dotnet-ef --version 6.0.16
```

# Entity Framework Web Instructions:

First, you'll need to add a reference to the following NuGet packages within your *data* project:


**MAKE SURE TO INSTALL THE LATEST OF VERSION 6 FOR ALL THESE**

* Microsoft.EntityFrameworkCore.SqlServer
* Microsoft.EntityFrameworkCore.Design
* Microsoft.Extensions.Configuration.FileExtensions
* Microsoft.Extensions.Configuration.Json

An alternative way do install these nuget packages that's much faster, is to right click on the Data project in visual studio and click "Edit Project File". This will bring
up the project file, where you can paste in the following (after the `</PropertyGroup>` section):

```
  <ItemGroup>
    <PackageReference Include="Microsoft.EntityFrameworkCore.Design" Version="6.0.16">
      <PrivateAssets>all</PrivateAssets>
      <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
    </PackageReference>
    <PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="6.0.16" />
    <PackageReference Include="Microsoft.Extensions.Configuration.FileExtensions" Version="6.0.0" />
    <PackageReference Include="Microsoft.Extensions.Configuration.Json" Version="6.0.0" />
  </ItemGroup>
```
Save and close this file and now all those nuget packages will be installed.

You can then go ahead and create your `DbContext` class, however, make sure to create a constructor that takes in a connection string. See how I did it here:

https://github.com/LKWD-2023/EFCoreWebDemo/blob/master/EFCoreWebDemo.Data/PeopleDbContext.cs#L14-L17

You can then create your classes that match the tables you want in your database, and then add them as a `DbSet<>` to your Context class:

https://github.com/LKWD-2023/EFCoreWebDemo/blob/master/EFCoreWebDemo.Data/Person.cs

https://github.com/LKWD-2023/EFCoreWebDemo/blob/master/EFCoreWebDemo.Data/PeopleDbContext.cs#L24

Then, you'll need to create a class that implements the interface `IDesignTimeDbContextFactory<NameOfYourDbContext>`. See mine here:

https://github.com/LKWD-2023/EFCoreWebDemo/blob/master/EFCoreWebDemo.Data/PeopleDataContextFactory.cs

You'll then need to change the directory on this line to match the name of your web project:

https://github.com/LKWD-2023/EFCoreWebDemo/blob/master/EFCoreWebDemo.Data/PeopleDataContextFactory.cs#L16

(at the end of that line where it says `EFCoreWebDemo.Web` by mine, change it to match the name of _your_ web project). You'll also need to change the return type of the `CreateDbContext` to match the name of your DbContext class.

Then, make sure to add your connection string in your web project within the `appsettings.json` file:

https://github.com/LKWD-2023/EFCoreWebDemo/blob/master/EFCoreWebDemo.Web/appsettings.json#L9-L11

Once you have all that set up, you can go to the command line, you can use the nuget command line for that:

<img width="700" alt="image" src="https://user-images.githubusercontent.com/126539919/234744150-28cd5b0c-be8f-4c43-8ee2-4d3c89f63eed.png">

and **make sure to go into the data projects directory**. You'll have to type something like `cd MyProject.Data` From there, you
can run both the `dotnet ef migrations add {SomeMigration}` and `dotnet ef database update` commands.

From there, you can create your repository classes as you did before, but instead of using the old style of database access code, you can now
use Entity Framework.
