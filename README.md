# 🐱 StealAllTheCats

StealAllTheCats is a .NET Web API that fetches and stores cat images from [TheCatAPI](https://thecatapi.com), using **Entity Framework Core**, **Serilog**, and **AutoMapper**. The project follows a **multi-layered architecture** for scalability and maintainability.

## 🚀 Features
- **Fetch and Save Cats**: Retrieves 25 random cat images and stores them in a database.
- **Retrieve Cats by ID**: Fetch a cat image using its unique ID.
- **Paginated List of Cats**: Retrieve all stored cats with pagination.
- **Filter by Tags**: Get cats filtered by temperament tags.

## 🏗️ Solution Structure
The project is divided into multiple class libraries:

- **StealAllTheCats.API**: The ASP.NET Core Web API.
- **StealAllTheCats.Core**: Entities, DTOs, and interfaces.
- **StealAllTheCats.Application**: Business logic and services.
- **StealAllTheCats.Infrastructure**: Database and external API clients.

## 🛠️ Technologies Used
- **.NET 8**
- **Entity Framework Core** (SQL Server)
- **Serilog** (Logging)
- **HttpClient** (API Calls)
- **AutoMapper** (DTO Mapping)
- **Swagger** (API Documentation)
- **xUnit** (Unit Testing)
- **Dependency Injection** (IoC)

## 📦 Installation & Setup

### 1️⃣ Clone the Repository
```sh
git clone https://github.com/YOUR_USERNAME/StealAllTheCats.git
cd StealAllTheCats
