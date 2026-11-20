# LMS - Learning Management System

A web-based Learning Management System built with ASP.NET Core MVC. Teachers can create courses and publish lessons, materials, and assignments, while students can enroll in courses, access learning content, and submit their work.

## Features

- Role-based access with separate Teacher and Student experiences
- Session-based authentication
- Course management organized by categories
- Student enrollment in courses
- Lessons and downloadable course materials
- Assignments and student submissions
- File uploads for materials and submissions
- Card-based dashboards for each role
- Dark and light theme support

## Tech Stack

- ASP.NET Core MVC (C#)
- Entity Framework Core (Code First with Migrations)
- SQL Server
- Razor Views, Bootstrap, HTML, CSS, JavaScript

## Data Model

`User`, `Role`, `Course`, `Category`, `Enrollment`, `Lesson`, `Assignment`, `Submission`, `Material`

## Getting Started

### Prerequisites

- .NET SDK (see `LmsProject.csproj` for the target framework)
- SQL Server or SQL Server Express

### Setup

1. Clone the repository:
   ```bash
   git clone https://github.com/ashrakatwaeel143-ops/LMS.git
   cd LMS
   ```
2. Update the `DefaultConnection` string in `appsettings.json` to point to your own SQL Server instance.
3. Create the database:
   ```bash
   dotnet ef database update
   ```
4. Run the project:
   ```bash
   dotnet run
   ```

## Project Structure

- `Controllers/` - request handling and application logic
- `Models/` - domain entities
- `Data/` - database context
- `Migrations/` - EF Core migrations
- `Views/` - Razor views (UI)
- `wwwroot/` - static files (CSS, JS, uploads)

