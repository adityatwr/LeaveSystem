# Leave Approval System

A web-based leave management system built with ASP.NET MVC, Entity Framework, and SQL Server.

## Features

- **Employee self-service:**  
  - Apply for annual leave  
  - View, edit, or retract pending leave requests

- **Manager functionality:**  
  - Approve or reject leave requests from subordinates  
  - View pending and processed leave requests over time

- **Business Rules:**  
  - Work days: Monday to Friday (excluding public holidays)
  - Leaves cannot be applied on weekends or in the past

- **Technical Stack:**  
  - ASP.NET MVC 5.2+
  - Entity Framework (Code First)
  - MS SQL Server 16+
  - Bootstrap (for UI)
  - SOLID principles, n-tier architecture, repository pattern, dependency injection

---

## Getting Started

### Prerequisites

- [.NET 8 SDK]
- [SQL Server 2012+]
- [Visual Studio 2022+]

### Database Setup


1. **Create the Database and User follwed by Connection String Configure:**
    - Use the `/DbScripts/Create_db_and_user.sql` script to create the database and a SQL Server user on a server
    - In `appsettings.json`, update the `DefaultConnection` to point to your SQL Server instance.

    ```json
    "ConnectionStrings": {
      "DefaultConnection": "Server=YOUR_SERVER;Database=LeaveApprovalDb;User Id=YOUR_USER;Password=YOUR_PASSWORD;TrustServerCertificate=True;"
    }
    ```

2. **Create Database Tables:**
    - Use the provided SQL scripts in `/DbScripts/CreateTables.sql` to create and seed the `Employees` and `LeaveRequests` tables.

3. **To insert default or sample employee records, use the `/DbScripts/Insert_Employee.sql` script**

---

### Running the Application

1. Build and run the solution in Visual Studio or use CLI:
    ```sh
    dotnet run
    ```

2. Open your browser and navigate to [https://localhost:5157](https://localhost:5157).

---

## Architecture

- **Presentation Layer:** ASP.NET MVC (`LeaveApprovalSystem.Web`)
- **Domain Layer:** Business logic (`LeaveApprovalSystem.Domain`)
- **Data Access Layer:** Entity Framework repository pattern (`LeaveApprovalSystem.Data`)
- **Core Layer:** Entity definitions and shared contracts (`LeaveApprovalSystem.Core`)

**Design Patterns:**  
- SOLID Principles  
- Repository Pattern  
- Dependency Injection


## Architecture Overview


    ┌─────────────────────────┐
    │     Client Devices      │
    │  (Browser / Mobile App) │
    └──────────┬──────────────┘
               │ HTTPS/API Calls
               ▼
    ┌─────────────────────────┐
    │ LeaveApprovalSystem.Web │
    │  (MVC / API Server)     │
    └──────────┬──────────────┘
               │ calls into
               ▼
    ┌─────────────────────────┐
    │ LeaveApprovalSystem.Dom │
    │ ain (Business Logic)    │
    └──────────┬──────────────┘
               │ uses IRepository<T>
               ▼
    ┌─────────────────────────┐
    │ LeaveApprovalSystem.Dat │
    │ a (EF DbContext & Repos)│
    └──────────┬──────────────┘
               │ SQL Authentication
               ▼
    ┌─────────────────────────┐
    │     SQL Server DBMS     │
    │  – LeaveSystemDB        │
    │  – User: leavesystem    │
    └─────────────────────────┘

    ╔═══════════════════════════╗
    ║ LeaveApprovalSystem.Core ║
    ║ • Interfaces              ║
    ║ • IRepository<T>          ║
    ║ • Enums & Value Objects   ║
    ║ • Base Entities & Helpers ║
    ╚═══════════════════════════╝
       ▲                 ▲
       │ referenced by   │ used by
       └─────────────────┴───────────


---

## Customization

- To add new leave types, edit the `LeaveType` enum in the core project.
- To add new business rules (e.g., leave overlap, minimum notice), modify the relevant service logic in the domain layer.

---

## Troubleshooting

- **Cannot connect to SQL Server:**  
  Ensure SQL Server is running and your credentials are correct.
- **Dates default to Today's date:**  
  Make sure you set default dates in your controller before returning the view.

