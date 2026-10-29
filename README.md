# Student Record Manager — ASP.NET Core MVC + MySQL

A beginner-friendly college mini-project that manages student records. It demonstrates MVC, EF Core/MySQL, CRUD, form handling, validation, session/TempData state management, search, and a responsive Bootstrap interface.

## Requirements
- Windows 10/11
- .NET 8 SDK
- MySQL Server (or XAMPP with MySQL/MariaDB)
- Visual Studio 2022 (ASP.NET and web development workload) OR VS Code
- Internet for Bootstrap and client-side validation CDN files

## 1. Set up MySQL
1. Start MySQL from MySQL Workbench or XAMPP Control Panel.
2. Open a SQL editor/phpMyAdmin and run:
   ```sql
   CREATE DATABASE student_record_db;
   ```
3. Open `appsettings.json` and replace `YOUR_MYSQL_PASSWORD` with your MySQL password.
   - If your local MySQL root account has no password, use `password=;`.
   - Do not upload a real production password to GitHub.

The app uses EF Core migrations to create the `Students` table automatically.

## 2. Run the project (terminal)
Open PowerShell in the folder containing `StudentRecordMVC.csproj`, then run:
```powershell
dotnet --version
dotnet restore
dotnet tool install --global dotnet-ef --version 8.0.11
```
If `dotnet-ef` is already installed, update it instead:
```powershell
dotnet tool update --global dotnet-ef --version 8.0.11
```
Now create and apply the database migration:
```powershell
dotnet ef migrations add InitialCreate
dotnet ef database update
dotnet run
```
The terminal prints a local URL such as `https://localhost:7xxx` or `http://localhost:5xxx`. Open that exact URL in your browser.

If HTTPS shows a certificate warning in development, try the printed HTTP URL. Keep the terminal open while using the app.

## 3. Run in Visual Studio
1. Extract the ZIP.
2. Open `StudentRecordMVC.csproj` (or the folder) in Visual Studio 2022.
3. Edit the MySQL password in `appsettings.json`.
4. Open **Tools → NuGet Package Manager → Package Manager Console** and run:
   ```powershell
   Add-Migration InitialCreate
   Update-Database
   ```
   If those commands are unavailable, use the terminal steps above.
5. Press **F5** or **Ctrl+F5**.

## 4. Test the features
1. Add a student with a unique enrollment number.
2. Try an invalid email or age outside 16–60 to see validation.
3. Add a second student with the same enrollment number; duplicate validation should stop it.
4. Search by name, email, course, or enrollment number.
5. Open **View**, **Edit**, and **Delete** to test CRUD.
6. Resize the browser or use a phone-sized window to check responsiveness.
7. After a successful change, the success alert appears using TempData. The last action is also stored in session.

## 5. GitHub submission
In the project folder:
```powershell
git init
git add .
git commit -m "Student Record Manager mini project"
git branch -M main
git remote add origin https://github.com/YOUR-USERNAME/student-record-manager.git
git push -u origin main
```
Create the empty GitHub repository first. Replace `YOUR-USERNAME` with your username. If the remote repository already contains a README, do not blindly force-push; pull/merge the remote first.

## Common issues
- **Access denied for user root**: correct the password in `appsettings.json` and confirm MySQL is running.
- **Can't connect to MySQL**: verify port 3306, server status, and database name.
- **`dotnet` is not recognized**: install .NET 8 SDK and reopen PowerShell.
- **`dotnet ef` not found**: run the tool install/update command above, then reopen the terminal.
- **Migration command fails**: run commands from the folder containing `StudentRecordMVC.csproj`.
- **Page has no styling**: internet may be unavailable; Bootstrap is loaded from a CDN.

## Important notes
- This is an educational demo, not a production student-information system.
- The project intentionally does not include login/roles or sensitive student data.
- Keep secrets out of public repositories. For a classroom demo, use a local test password.
