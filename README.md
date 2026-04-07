# MoneyMate

A comprehensive full-stack financial management application built with C# .NET backend and React + TypeScript frontend.

---

## Table of Contents

- [Features](#features)
- [Tech Stack](#tech-stack)
- [Project Structure](#project-structure)
- [Prerequisites](#prerequisites)
- [Installation](#installation)
- [Running the Application](#running-the-application)
- [API Documentation](#api-documentation)
- [Database Setup](#database-setup)
- [Environment Variables](#environment-variables)
- [Project Architecture](#project-architecture)
- [Contributing](#contributing)
- [License](#license)

---

## Features

### User Management
- User registration & authentication
- JWT token-based authorization
- Change password functionality
- User profile management

### Financial Management
- Transactions - Track income and expenses
- Budgets - Create and monitor spending budgets
- Categories - Organize transactions by category
- Savings Goals - Set and track savings objectives
- Payment Methods - Manage payment options
- Multi-Currency Support - Handle multiple currencies

### Advanced Features
- Shared savings goals with multiple contributors
- Real-time budget tracking
- Savings goal sharing codes
- Transaction filtering and search
- Dark/Light theme support
- Responsive design (mobile, tablet, desktop)

---

## Tech Stack

### Backend
- Framework: .NET 8 / ASP.NET Core
- Language: C#
- Database: PostgreSQL
- ORM: Entity Framework Core
- Authentication: JWT (JSON Web Tokens)
- API Design: RESTful API

### Frontend
- Framework: React 18
- Language: TypeScript
- Styling: Tailwind CSS
- UI Components: Shadcn UI
- HTTP Client: Axios
- State Management: React Context API
- Build Tool: Vite

### Tools & Services
- Version Control: Git
- Code Editor: Visual Studio Code
- Database Tool: DataGrip
- API Testing: Postman / Swagger UI

---

## Project Structure

```
AcademyConsoleApp/
├── frontend/                          # React application
│   ├── src/
│   │   ├── api/                      # API service calls
│   │   │   ├── api.ts               # Axios instance with interceptors
│   │   │   ├── userService.ts       # User API calls
│   │   │   ├── transactionService.ts # Transaction API calls
│   │   │   ├── budgetService.ts     # Budget API calls
│   │   │   ├── categoryService.ts   # Category API calls
│   │   │   ├── savingService.ts     # Savings API calls
│   │   │   ├── paymentMethodService.ts     # PaymentMethods API calls
│   │   │   └── currencyService.ts     # Currencies API calls
│   │   │
│   │   ├── components/               # Reusable React components
│   │   │   ├── ProtectedRoute.tsx   # Auth-protected routes
│   │   │   ├── PublicRoute.tsx      # Public routes
│   │   │   ├── theme-switch.tsx     # Theme toggle
│   │   │   ├── layout/
│   │   │   │   ├── topNav.tsx       # Top navigation bar
│   │   │   │   └── profile-dropdown.tsx
│   │   │   └── ui/                  # Shadcn UI components
│   │   │       ├── button.tsx
│   │   │       ├── input.tsx
│   │   │       ├── dialog.tsx
│   │   │       ├── form.tsx
│   │   │       ├── card.tsx
│   │   │       ├── table.tsx
│   │   │       ├── tabs.tsx
│   │   │       ├── sidebar.tsx
│   │   │       └── ...other components
│   │   │
│   │   ├── context/                  # React Context providers
│   │   │   ├── AuthContext.tsx      # Authentication context
│   │   │   ├── ThemeContext.tsx     # Theme context
│   │   │   └── CurrencyContext.tsx  # Currency context
│   │   │
│   │   ├── hooks/                    # Custom React hooks
│   │   │   ├── useAuth.ts           # Authentication hook
│   │   │   ├── useTheme.ts          # Theme hook
│   │   │   ├── useCurrency.ts       # Currency hook
│   │   │   └── use-dialog-state.tsx # Dialog state hook
│   │   │
│   │   ├── pages/                    # Page components
│   │   │   ├── login.tsx             # Login page
│   │   │   ├── register.tsx          # Registration page
│   │   │   ├── dashboard.tsx         # Dashboard
│   │   │   ├── transactions.tsx      # Transactions page
│   │   │   ├── budgets.tsx           # Budgets page
│   │   │   ├── savings.tsx           # Savings goals page
│   │   │   ├── categories.tsx        # Categories page
│   │   │   └── profile.tsx           # User profile page
│   │   │
│   │   ├── lib/                      # Utility functions
│   │   │   ├── utils.ts             # Helper utilities
│   │   │   └── cookies.ts           # Cookie management
│   │   │
│   │   ├── App.tsx                   # Root component
│   │   ├── App.css                   # Root styles
│   │   ├── index.css                 # Global styles
│   │   └── main.tsx                  # React app entry point
│   │
│   ├── public/                        # Static assets
│   ├── index.html                     # HTML entry point
│   ├── .gitignore                     # Git ignore rules
│   ├── vite.config.ts                 # Vite configuration
│   ├── tailwind.config.js             # Tailwind CSS config
│   ├── tsconfig.json                  # TypeScript config
│   ├── package.json                   # Dependencies
│   └── README.md                      # Frontend README
│
└── SampleCkWebApp/                   # .NET Backend
    ├── docs/
    │   ├── ARCHITECTURE.md           # Architecture documentation
    │   ├── SWAGGER.md                # API documentation
    │   └── USERS_API.md              # User endpoints
    │
    ├── src/
    │   ├── SampleCkWebApp.Application/
    │   │   ├── Users/
    │   │   │   ├── AuthService.cs
    │   │   │   ├── UserService.cs
    │   │   │   ├── AuthValidator.cs
    │   │   │   ├── DependencyInjection.cs
    │   │   │   ├── Interfaces/
    │   │   │   └── Mappings/
    │   │   │
    │   │   ├── Transactions/
    │   │   │   ├── TransactionService.cs
    │   │   │   ├── TransactionValidator.cs
    │   │   │   ├── DependencyInjection.cs
    │   │   │   ├── Interfaces/
    │   │   │   └── Mappings/
    │   │   │
    │   │   ├── Budgets/
    │   │   │   ├── BudgetService.cs
    │   │   │   ├── BudgetValidator.cs
    │   │   │   ├── DependencyInjection.cs
    │   │   │   ├── Interfaces/
    │   │   │   └── Mappings/
    │   │   │
    │   │   ├── Categories/
    │   │   │   ├── CategoryService.cs
    │   │   │   ├── CategoryValidator.cs
    │   │   │   ├── DependencyInjection.cs
    │   │   │   ├── Interfaces/
    │   │   │   └── Mappings/
    │   │   │
    │   │   ├── Savings/
    │   │   │   ├── SavingService.cs
    │   │   │   ├── SavingValidator.cs
    │   │   │   ├── DependencyInjection.cs
    │   │   │   ├── Interfaces/
    │   │   │   └── Mappings/
    │   │   │
    │   │   ├── UserSaving/
    │   │   │   ├── UserSavingService.cs
    │   │   │   ├── UserSavingValidator.cs
    │   │   │   ├── DependencyInjection.cs
    │   │   │   ├── Interfaces/
    │   │   │   └── Mappings/
    │   │   │
    │   │   ├── PaymentMethods/
    │   │   │   ├── PaymentMethodService.cs
    │   │   │   ├── PaymentMethodValidator.cs
    │   │   │   ├── DependencyInjection.cs
    │   │   │   ├── Interfaces/
    │   │   │   └── Mappings/
    │   │   │
    │   │   ├── Currencies/
    │   │   │   ├── CurrencyService.cs
    │   │   │   ├── CurrencyValidator.cs
    │   │   │   ├── DependencyInjection.cs
    │   │   │   ├── Interfaces/
    │   │   │   └── Mappings/
    │   │   │
    │   │   └── Common/
    │   │       ├── PasswordService.cs
    │   │       ├── Interfaces/
    │   │
    │   ├── SampleCkWebApp.Domain/
    │   │   ├── Entities/              # Domain models (User, Transaction, etc)
    │   │   ├── Enums/                 # Enumerations (TransactionType, etc)
    │   │   └── Errors/                # Error definitions
    │   │
    │   ├── SampleCkWebApp.Infrastructure/
    │   │   ├── Data/
    │   │   │   ├── ApplicationDbContext.cs
    │   │   ├── Migrations/        # Database migrations
    │   │   ├── Users/                 # User repositories
    │   │   ├── Transactions/          # Transaction repositories
    │   │   ├── Budgets/               # Budget repositories
    │   │   ├── Categories/            # Category repositories
    │   │   ├── Savings/               # Savings repositories
    │   │   ├── UserSavings/           # UserSaving repositories
    │   │   ├── PaymentMethods/        # PaymentMethod repositories
    │   │   └── Currencies/            # Currency repositories
    │   │
    │   ├── SampleCkWebApp.Contracts/
    │   │   └── DTOs/                  # Data transfer objects
    │   │       ├── User/
    │   │       ├── Transaction/
    │   │       ├── Budget/
    │   │       ├── Category/
    │   │       ├── Saving/
    │   │       ├── PaymentMethod/
    │   │       └── Currency/
    │   │
    │   └── SampleCkWebApp.WebApi/
    │       ├── Controllers/           # API endpoints
    │       │   ├── Users/
    │       │   ├── Transactions/
    │       │   ├── Budgets/
    │       │   ├── Categories/
    │       │   ├── Savings/
    │       │   └── PaymentMethods/
    │       ├── Program.cs             # Application startup & config
    │       ├── DependencyInjection.cs # DI registration
    │       ├── appsettings.json       # Configuration
    │       ├── appsettings.development.json
    │       ├── Dockerfile             # Docker configuration
    │       └── SampleCkWebApp.WebApi.csproj
    │
    └── SampleCkWebApp.sln            # Solution file
```

---

## Prerequisites

### Backend
- .NET 8 SDK - [Download](https://dotnet.microsoft.com/download)
- PostgreSQL 13+ - [Download](https://www.postgresql.org/download/)
- Visual Studio 2022 or JetBrains Rider (recommended)

### Frontend
- Node.js 18+ - [Download](https://nodejs.org/)
- npm 9+ or yarn 3+

### Tools
- Git - [Download](https://git-scm.com/)
- Postman (optional) - [Download](https://www.postman.com/)

---

## Installation

### Backend Setup

1. Navigate to backend folder:
   ```bash
   cd SampleCkWebApp
   ```

2. Restore NuGet packages:
   ```bash
   dotnet restore
   ```

3. Create and configure database:
   - See Database Setup section

4. Run migrations:
   ```bash
   dotnet ef database update -p SampleCkWebApp.Infrastructure -s SampleCkWebApp.WebApi
   ```

5. Build the solution:
   ```bash
   dotnet build
   ```

### Frontend Setup

1. Navigate to frontend folder:
   ```bash
   cd frontend
   ```

2. Install dependencies:
   ```bash
   npm install
   ```

---

## Running the Application

### Start Backend

```bash
cd SampleCkWebApp
dotnet run --project src/SampleCkWebApp.WebApi/SampleCkWebApp.WebApi.csproj
```

Backend runs on: http://localhost:5000

Swagger UI: http://localhost:5000/swagger/index.html

### Start Frontend

```bash
cd frontend
npm run dev
```

Frontend runs on: http://localhost:5173

### Access the Application

Open browser and go to: http://localhost:5173

---

## API Documentation

### Swagger UI
- URL: http://localhost:5000/swagger/index.html
- Auto-generated from code comments
- Test endpoints directly in UI

### API Endpoints Summary

#### User Management
- POST /api/users/register - Register new user
- POST /api/users/login - Login user
- POST /api/users/change-password - Change password
- GET /api/users/me - Get current user
- GET /api/users - Get all users
- GET /api/users/{id} - Get user by ID
- PUT /api/users/{id} - Update user
- DELETE /api/users/{id} - Delete user

#### Transactions
- GET /api/transactions - Get all transactions (paginated)
- GET /api/transactions/user/{userId}/paginated - Get user transactions with filters
- GET /api/transactions/{id} - Get transaction by ID
- GET /api/transactions/user/{userId}/all - Get all user transactions
- GET /api/transactions/user/{userId}/income/monthly - Get monthly income
- GET /api/transactions/user/{userId}/income/range - Get income by date range
- GET /api/transactions/user/{userId}/expense/monthly - Get monthly expenses
- GET /api/transactions/user/{userId}/expense/range - Get expenses by date range
- GET /api/transactions/user/{userId}/savings/monthly - Get monthly savings
- GET /api/transactions/user/{userId}/savings/range - Get savings by date range
- POST /api/transactions - Create transaction
- PUT /api/transactions/{id} - Update transaction
- DELETE /api/transactions/{id} - Delete transaction

#### Budgets
- GET /api/budgets - Get all budgets
- GET /api/budgets/{id} - Get budget by ID
- GET /api/budgets/user/{userId} - Get user budgets
- GET /api/budgets/user/{userId}/month/{month}/year/{year} - Get budgets for month
- GET /api/budgets/user/{userId}/summary - Get budget summary
- GET /api/budgets/user/{userId}/month/{month}/year/{year}/summary - Get monthly summary
- POST /api/budgets - Create budget
- PUT /api/budgets/{id} - Update budget
- DELETE /api/budgets/{id} - Delete budget

#### Savings Goals
- GET /api/savings - Get all savings
- GET /api/savings/{id} - Get savings by ID
- GET /api/savings/user/{userId} - Get user savings
- GET /api/savings/user/{userId}/non-completed - Get non-completed savings
- POST /api/savings - Create savings goal
- PUT /api/savings/{id} - Update savings goal
- POST /api/users/{userId}/savings - Join savings goal
- DELETE /api/savings/{id} - Delete savings goal
- DELETE /api/users/{userId}/savings/{savingId} - Remove user from savings

#### Payment Methods
- GET /api/paymentmethods - Get all payment methods
- GET /api/paymentmethods/{id} - Get payment method by ID
- POST /api/paymentmethods - Create payment method
- PUT /api/paymentmethods/{id} - Update payment method
- DELETE /api/paymentmethods/{id} - Delete payment method

#### Categories
- GET /api/categories - Get all categories
- GET /api/categories/{id} - Get category by ID
- POST /api/categories - Create category
- PUT /api/categories/{id} - Update category
- DELETE /api/categories/{id} - Delete category

#### Currencies
- GET /api/currencies - Get all currencies
- GET /api/currencies/{id} - Get currency by ID
- POST /api/currencies - Create currency
- PUT /api/currencies/{id} - Update currency
- DELETE /api/currencies/{id} - Delete currency


See full documentation in /docs/SWAGGER.md

---

## Database Setup

### 1. Create PostgreSQL Database

Using pgAdmin:
1. Open pgAdmin
2. Right-click "Databases" -> Create -> Database
3. Name: MoneyMate
4. Click Save

Using Command Line:
```bash
psql -U postgres -c "CREATE DATABASE MoneyMate;"
```

### 2. Update Connection String

Edit SampleCkWebApp/src/SampleCkWebApp.WebApi/appsettings.json:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=MoneyMate;User Id=postgres;Password=YOUR_PASSWORD;Port=5432;"
  }
}
```

Replace:
- YOUR_PASSWORD with your PostgreSQL password

### 3. Run Migrations

```bash
cd SampleCkWebApp
dotnet ef database update -p SampleCkWebApp.Infrastructure -s SampleCkWebApp.WebApi
```

Database is now ready!

---

## Environment Variables

### Backend - appsettings.json

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=AcademyConsoleApp;User Id=postgres;Password=password;Port=5432;"
  },
  "Jwt": {
    "SecretKey": "your-secret-key-min-32-characters-long",
    "ExpirationMinutes": 60
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information"
    }
  }
}
```

---

## Project Architecture

### Backend Architecture - Onion Layer Pattern

```
API Layer (Controllers) - HTTP Requests/Responses
    |
    v
Contracts Layer (DTOs) - Data contracts between layers
    |
    v
Application Layer (Services & Validators) - Business logic & rules
    |
    v
Domain Layer (Entities & Enums) - Core business models
    |
    v
Infrastructure Layer (Repositories) - Data access & persistence
    |
    v
Database (PostgreSQL) - Data storage
```

### Key Design Patterns

- Repository Pattern - Data access abstraction
- Dependency Injection - Loose coupling
- Service Layer - Business logic separation
- DTO Pattern - Data transfer
- Error Handling - ErrorOr pattern
- Validator Pattern - Input validation

### Frontend Architecture

React Component Structure:
```
App (Root)
├── ThemeProvider (Context)
├── AuthProvider (Context)
├── CurrencyProvider (Context)
├── Router (Routes)
│   ├── Pages (Full pages)
│   │   ├── Login
│   │   ├── Register
│   │   ├── Dashboard
│   │   ├── Transactions
│   │   ├── Budgets
│   │   ├── Savings
│   │   ├── Profile
│   │   └── Categories
│   └── Components (Reusable)
│       ├── ProtectedRoute
│       ├── PublicRoute
│       ├── TopNav
│       ├── ProfileDropdown
│       ├── ThemeSwitch
│       └── UI Components
├── Hooks (Custom)
│   ├── useAuth
│   ├── useTheme
│   └── useCurrency
└── API Services
    ├── userService
    ├── transactionService
    ├── budgetService
    ├── categoryService
    ├── savingService
    ├── paymentMethodService
    └── currencyService
```

---

## Key Features Implementation

### Authentication Flow
1. User registers/logs in
2. Backend generates JWT token
3. Token stored in localStorage
4. Axios interceptor adds token to requests
5. Protected routes check authentication

### Transaction Management
1. User creates transaction
2. Category & payment method selected
3. Transaction saved to database
4. Category totals updated
5. Budget tracking updated

### Savings Goals
1. User creates savings goal
2. Goal shared via code
3. Other users join with code
4. Track contributions
5. Contributors see progress

### Budget Tracking
1. Create budget per category per month
2. Transactions auto-update budget spent
3. Real-time progress tracking
4. Budget alerts (if implemented)

---

## Testing

### Backend Testing

```bash
cd SampleCkWebApp

# Run tests (when added)
dotnet test
```

### Frontend Testing

```bash
cd frontend

# Run tests (when added)
npm run test

# Run with coverage
npm run test:coverage
```

---

## Common Tasks

### Create New Migration

```bash
cd SampleCkWebApp
dotnet ef migrations add MigrationName -p SampleCkWebApp.Infrastructure -s SampleCkWebApp.WebApi
dotnet ef database update -p SampleCkWebApp.Infrastructure -s SampleCkWebApp.WebApi
```

### Reset Database

```bash
# Remove all migrations (careful!)
dotnet ef database drop -p SampleCkWebApp.Infrastructure -s SampleCkWebApp.WebApi

# Reapply
dotnet ef database update -p SampleCkWebApp.Infrastructure -s SampleCkWebApp.WebApi
```

### Build for Production

Backend:
```bash
dotnet publish -c Release
```

Frontend:
```bash
npm run build
```

---

## Troubleshooting

### Database Connection Error
```
Error: Could not connect to database
Solution: Check appsettings.json connection string
Solution: Verify PostgreSQL is running
Solution: Check username/password
```

### API Port Already in Use
```
Error: Port 5000 already in use
Solution: Change port in launchSettings.json
Solution: Kill process using port (Windows: netstat -ano)
```

### Frontend API Connection Error
```
Error: Cannot connect to backend
Solution: Check VITE_API_URL is correct
Solution: Verify backend is running
Solution: Check CORS configuration
```

### Token Expired - Auto Logout
```
This is expected behavior
User redirected to login
Token removed from localStorage
Re-login to get new token
```

---


## Author

Your Name - [GitHub](https://github.com/aidajk77)

---

## License

This project is licensed under the MIT License - see LICENSE file for details.

---

## Contributing

1. Fork the repository
2. Create feature branch (git checkout -b feature/AmazingFeature)
3. Commit changes (git commit -m 'Add AmazingFeature')
4. Push to branch (git push origin feature/AmazingFeature)
5. Open Pull Request

---

## Deployment

### Backend Deployment (Azure/AWS)
- Update connection string for production database
- Set JWT secret to secure value
- Update CORS settings
- Enable HTTPS
- Configure logging

### Frontend Deployment (Vercel/Netlify)
- Build: npm run build
- Set production API URL
- Configure environment variables
- Deploy dist folder

---

## Support

For issues and questions:
- Create GitHub Issue
- Contact: your-email@example.com

---

Last Updated: 2026-04-07
Version: 1.0.0