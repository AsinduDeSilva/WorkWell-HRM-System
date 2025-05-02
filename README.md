# WorkWell Human Resource Management System

## Table of Contents
- [Overview](#overview)
- [Features](#features)
- [Prerequisites](#prerequisites)
- [Installation](#installation)
- [Database Setup](#database-setup)
- [Using SQL Scripts](#using-sql-scripts)
- [Database Configuration](#database-configuration)
- [Running the Application](#running-the-application)

## Overview
WorkWell is a desktop-based Human Resource Management System built using WPF (Windows Presentation Foundation) and C#. It provides a user-friendly interface for managing employee data, leave records, and other HR-related functionalities. It uses Entity Framework for ORM and SQL Server for database management.

## Features
- Employee registration and management
- Leave request and approval system
- Role-based access control (Admin, Manager, Employee)
- Gender and leave type enumeration support
- MVVM architecture for maintainability
- SQLite or SQL Server compatible data access layer using Entity Framework

## Prerequisites
Before running the application, ensure you have the following installed:
- Visual Studio 2019 or later
- .NET Desktop Development workload
- .NET 6 SDK or compatible framework (check your project's `csproj` file for the exact version)
- SQL Server (any edition) for database restoration
- SSMS (SQL Server Management Studio) for handling `.bak` files

## Installation
1. Clone or download the repository.
```bash
   git clone https://github.com/AsinduDeSilva/WorkWell-HRM-System.git
```
2. Open `WorkWell.sln` in Visual Studio.
3. Restore NuGet packages if prompted.
4. Build the solution to ensure all dependencies are correctly loaded.

## Database Setup
The solution includes a SQL Server `.bak` file: `WorkWellDb.bak`.

To restore the database:
1. Open SQL Server Management Studio.
2. Right-click on "Databases" → Restore Database.
3. Choose **Device**, then select the `.bak` file.
4. Set the database name (e.g., `WorkWellDb`).
5. Complete the restoration process.

## Using SQL Scripts
If you prefer using SQL scripts or need to regenerate the database manually:
1. Locate the `Migrations` folder in the `WorkWell` project.
2. Use the Entity Framework tools in the Package Manager Console:
   ```bash
   Update-Database
 