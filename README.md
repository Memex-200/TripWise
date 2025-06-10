# TripWise: Comprehensive Travel Application Graduation project 



## About the Repo

It is a .NET-based travel planning solution using Clean Architecture principles. This repository contains the backend services and domain logic for a travel planning system.



##  Prerequisites

- .NET 8 SDK

- Entity Framework Core CLI

- Database Server: Hosted at  monsterasp.net (SQL Server )

- [Visual Studio 2022+](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)

  

## Solution Structure

The solution consists of 5 projects following domain-driven design:

```markdown
TripWise/
├── .github/workflows/    # GitHub Actions CI/CD
├── TripWise.Api/         # API Entry Point
│   ├── Controllers/
│   ├── appsettings.json
│   └── Program.cs
├── TripWise.Application/ # Business Logic
│   ├── DTOs/
│   └── Interfaces/
├── TripWise.Domain/      # Domain Models
│   └── Entities/
├── TripWise.EntityFrameworkCore/ # Database
│   ├── Migrations/
│   ├── EntitiesConfigurations/
│   └── ApplicationDbContext.cs
└── TripWise.Infrastructure/ # Implementation
    ├── Repositories/
    └── Services/
```



## Getting Started

### 1. Clone Repository

```bash
git clone https://github.com/your-username/TripWise.git
cd TripWise
```

### 2. Install Dependencies

```bash
dotnet restore
```

### 3. Run the API

```bash
dotnet run --project TripWise.Api
```

### Cloud Deployment:

  [TripWise](http://tripwiseeeee.runasp.net/swagger/index.html)



## Contributing



We welcome contributions! Please follow these steps:

1. Fork the repository
2. Create a new feature branch: `git checkout -b feature-name`
3. Commit your changes: `git commit -am 'Add some feature'`
4. Push to the branch: `git push origin feature-name`
5. Submit a pull request



## License

This project is licensed under the MIT License - see [LICENSE](https://license/) for details.



## Contact

For support or questions, please open an issue on GitHub or contact:

- Email:  asmaakhaledm.55@gmail.com
- Project Link: https://github.com/Memex-200/TripWise

