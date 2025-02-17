# KutShut - FunAPI Translation

### C# .NET MVC CRUD Web Application

---

#### Key features

> ---
>
> ---
>
> - **Five unique menu design styles**
>
> ---
>
> ![screenshot](/wwwroot/screenshot/KutShort3.png) > ![screenshot](/wwwroot/screenshot/KutShort5.png)
>
> ---
>
> ---
>
> - **Power user select FunAPI enabled translation**
> - **An elegant dashboard interface**
> - **View multiple dataset on single page**
>
> ---
>
> ![screenshot](/wwwroot/screenshot/KutShort6.png)
>
> ---
>
> ---
>
> - **Simplified pagination**
> - **Search as you type**
>
> ---
>
> ![screenshot](/wwwroot/screenshot/KutShort7.png)
>
> ---
>
> ---
>
> - **Fascinating CRUD operations interface**
>
> ---
>
> ![screenshot](/wwwroot/screenshot/KutShort8.png)
>
> ---
>
> ---
>
> - **An API END POINT for data fetching (\***Leets/FunAPIJsonData(int? funTransID, string? funTransTextAPI)**\*)**
>
> ---
>
> ![screenshot](/wwwroot/screenshot/KutShort9.png)
>
> ---

---

### Prerequisites

Prior to commencing, please verify that you have the following pre-requisites:

1. [Visual Studio Code](https://code.visualstudio.com/download/)
2. [.NET SDK](https://dotnet.microsoft.com/en-us/download/dotnet)
3. Install required extensions
   > - c#
   > - c# Dev kit
4. Database of choice

---

---

### Step 1: Setting Up the Project

1. Open Visual Studio Code and create a new project.
2. Open the terminal window, name your project (e.g., `FunTrans`) and configure it as needed
   1. PS C:\Users\\"user\'s folder'\
   2. Make and select new folder then new MVC application
   3. mkdir FunTrans
   4. cd FunTrans
   5. dotnet new mvc

---

### Step 2: Making the Model

The model represents the data structure.  
Define the a `Models` folder to hold the model classes `FunAPITranslateModel` and `FunAPITranslateMultiModel` classes.

(`Models → FunAPITranslateModel.cs`):  
(`Models → FunAPITranslateMultiModel.cs`):

```csharp
using System.ComponentModel.DataAnnotations;

public class FunAPITranslate
{
    public int Id { get; set; }
    [Required]
    public string? textSource { get; set; }
    [Required]
    public string? textTarget { get; set; }
    [Required]
    public string? textAPI { get; set; }
    public DateTime Downdated { get; set; } = DateTime.UtcNow;
    public DateTime Updated { get; set; } = default!;
}
```

### Model to present more than one dataset per page

```csharp
public class FunAPITranslateMultiModel
{
  public List<FunAPITranslate>? QueryHasConditionData { set;get;}
  public List<FunAPITranslate>? QueryHasNoConditionData { set;get;}
}

```

---

### Step 3: Setting Up the Database

1. Install the database of choice packages for the Solution (Example for MySql )

   ```
   Microsoft.EntityFrameworkCore
   Microsoft.EntityFrameworkCore.Tools
   Pomelo.EntityFrameworkCore.MySql
   ```

2. Define a `Data` folder to hold the data class `ApplicationDbContext` class to handle database interactions:

   ```csharp
   using Microsoft.EntityFrameworkCore;

    public class ApplicationContext : DbContext
    {
        public DbSet<FunAPITranslate> FunAPITranslates { get; set; } = null!;

         public ApplicationContext() => Database.EnsureCreated();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=localhost;user=root;password=;database=translatelang;",
            new MySqlServerVersion(new Version(10, 4, 32)), options => options.EnableRetryOnFailure());
        }
    }
   ```

### Step 5: Creating the Controller

In the `Controllers`folder add the controller `LeetsController.cs` :

```

```

---

### Step 6: Create Views for the Actions

In the `Views` folder add the `Leets` folder.  
Add all nine views `ChangeFunAPI.cshtml`,`ChangeFunAPIData.cshtml`,`DashboardFunAPI.cshtml`,`FetchFunAPI.cshtml`,`Index.cshtml`,`RemoveFunAPI.cshtml`,`RemoveFunAPIData.cshtml`,`UploadFunAPI.cshtml` and `WelcomeFunAPI.cshtml`:

### Step 7: Run the Application

1. Press `Crtl + F5` to run the applicatoin
2. Navigate to `http://localhost:5182/`
3. Test all CRUD operations.

> - **An API END POINT for data fetching (\***Leets/FunAPIJsonData(int? 1/2/3/4/5, string? Yoda API/Pirate API/Valspeak/Minon )**\*)**
