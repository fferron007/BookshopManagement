# Bookshop Management Solution

This solution is a Bookshop Management application built with .NET Core using WPF for the presentation layer, Entity Framework for data access, and NLog for logging. This README provides steps to set up and run the project on a new machine.

## Prerequisites

1. **.NET SDK**: Make sure .NET SDK is installed. You can download it from [https://dotnet.microsoft.com/download](https://dotnet.microsoft.com/download).
2. **SQL Server**: Install SQL Server or use SQL Server Express if you don’t already have a SQL Server instance.
3. **Visual Studio** (recommended): Ensure that Visual Studio with .NET desktop development is installed.

## Step-by-Step Setup

1. **Clone the Repository**

   ```bash
   git clone https://github.com/yourusername/bookshop-management.git
   cd bookshop-management
   ```

2. **Open the Solution in Visual Studio**

   - Open Visual Studio.
   - Select **File > Open > Project/Solution**.
   - Locate the `BookshopManagement.sln` file in the cloned repository and open it.

3. **Restore NuGet Packages**

   - In Visual Studio, go to **Tools > NuGet Package Manager > Manage NuGet Packages for Solution**.
   - Click on **Restore** to ensure all necessary packages are installed.

4. **Set Up Database Configuration**

   - Open the `appsettings.json` file in the **BookshopManagement.DAL** project.
   - Update the `ConnectionStrings` section with your SQL Server connection details. Example:

     ```json
     {
       "ConnectionStrings": {
         "BookshopDatabase": "Server=YOUR_SERVER_NAME;Database=BookshopDatabase;User Id=YOUR_USERNAME;Password=YOUR_PASSWORD;"
       }
     }
     ```

5. **Run Database Migrations**

   - Open the **Package Manager Console** in Visual Studio (**Tools > NuGet Package Manager > Package Manager Console**).
   - Make sure **BookshopManagement.DAL** is selected as the default project in the console.
   - Run the following command to apply migrations and create the database:

     ```bash
     Update-Database
     ```

6. **Configure Logging (Optional)**

   The project uses NLog for logging, which is configured to save logs to `c:\temp\Bookshop_Management\Log_{date}.txt`. You can modify this path in the `NLog.config` file located in the **BookshopManagement.Common** project if needed.

7. **Build and Run the Project**

   - In Visual Studio, select **Build > Build Solution** to compile the project.
   - Set **BookshopManagement.PL** as the startup project (right-click the project in Solution Explorer and select **Set as Startup Project**).
   - Press **F5** or click **Start** to run the application.

## Project Structure

- **BookshopManagement.PL** - Presentation layer (WPF)
- **BookshopManagement.BL** - Business logic
- **BookshopManagement.DAL** - Data access layer (Entity Framework)
- **BookshopManagement.Common** - Common utilities and logging configuration
- **BookshopManagement.Tests** - Unit tests for the solution

## Usage

1. **Book Management**: Manage books with options to add, edit, and delete records.
2. **Sales Management**: Process book sales, manage stock, and generate sales reports.
3. **Logging**: Errors and significant operations are logged to a file for easy troubleshooting.

## Troubleshooting

If you encounter issues:

- Ensure `appsettings.json` has the correct database connection string.
- If `appsettings.json` isn’t copied to the output directory, set **Copy to Output Directory** to **Copy if newer**.
- If `Update-Database` fails, confirm SQL Server is running and accessible with the credentials provided.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
