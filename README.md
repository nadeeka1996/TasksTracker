# TasksManagement System

A full-stack task management application built with **.NET 8 (Web API)** and **Angular 19**. The app supports user authentication and CRUD operations for tasks with history tracking.

---

## 🧱 Project Structure

```
/TasksManagement               # .NET 8 backend (Web API)
└── TasksManagement.Presentation
└── TasksManagement.Application
└── TasksManagement.Domain
└── TasksManagement.Infrastructure

/task-management-app                    # Angular 19 frontend
└── src
└── angular.json
└── package.json
```

---

## ⚙️ Prerequisites

### Backend (.NET 8)
- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)
- SQL Server
- Visual Studio 2022+ or VS Code

### Frontend (Angular 19)
- [Node.js](https://nodejs.org/) (v18+)
- [Angular CLI](https://angular.io/cli) (`npm install -g @angular/cli@19`)

---

## 🚀 How to Run the Project

### 1. Clone the Repository

```bash
git clone https://github.com/nadeeka1996/TasksManagement.git
cd TasksManagement
```

---

### 2. Configure the Backend

- Open `appsettings.json` in `TasksManagement.Presentation`
- Update the connection string:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\mssqllocaldb;Database=TasksDb;Trusted_Connection=True;"
}
```

- Run migrations scripts:

```bash
cd TasksManagement.Infrastructure
dotnet ef database update
```
#### ✅ Optionally, you can run the script file DbSchema.sql.

### 3. Run the Backend API

```bash
dotnet run --project TasksManagement.Presentation
```

> API runs on `https://localhost:7295` by default.

---

### 4. Setup and Run the Angular Frontend

```bash
cd task-management-app
npm install
ng serve --open
```

> Frontend runs on `http://localhost:4200`.

---

### 5. Log in with Default User or Create New via Swagger

#### ✅ Option 1: Use Default User (Recommended for First-Time Setup)

When you run the provided sample SQL data script, a **default user** is created:

```json
{
  "name": "John Doe",
  "email": "john.doe@example.com",
  "password": "123456"
}
```

You can use this user to log in to the system immediately.

---

#### ✳️ Option 2: Create a New User via Swagger (Optional)

If you want to register a new user:

1. **Open Swagger UI**  
   Navigate to: `https://localhost:7295/swagger`

2. **Register a New User**  
   Use `POST /api/auth/register` with:

   ```json
   {
     "name": "Jane Smith",
     "email": "jane@example.com",
     "password": "Password@123"
   }
   ```
---


## 📦 Technologies Used

### Backend
- .NET 8
- Entity Framework Core
- FluentValidation
- SQL Server

### Frontend
- Angular 19
- Angular Material
- TypeScript

---

## ✅ TODO / Features

- [x] User login
- [x] Task CRUD

---

