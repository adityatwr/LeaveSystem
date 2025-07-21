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


1. **Initialize db objects:**
   - Use the `/DbScripts/Initialize_systemDB.sql` script to create the database, a user, tables and insert employee on the server
2.  **Connection String Configure:**
   - In `appsettings.json`, update the `DefaultConnection` to point to your SQL Server instance.

    ```json
    "ConnectionStrings": {
      "DefaultConnection": "Server=YOUR_SERVER;Database=LeaveApprovalDb;User Id=YOUR_USER;Password=YOUR_PASSWORD;Trusted_Connection=True;TrustServerCertificate=True;"
    }
    ```
3. **Check whether you are able to connect to db:**
   - Sometimes, db refused to connect due to certificates, add _TrustServerCertificate=True_ to default connection string.

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
    ║ LeaveApprovalSystem.Core  ║
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

