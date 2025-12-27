# RadwaMinta

A comprehensive full-stack e-commerce and showcase platform for RadwaMinta, a company specializing in the production, processing, and export of high-quality herbs, spices, and seeds since 2006.

## Overview

RadwaMinta is a modern web application that provides a complete business solution featuring product catalog management, customer reviews, quality showcases, admin dashboard, and multi-language support. The platform is built with a robust architecture implementing best practices in software engineering.

## Tech Stack

### Backend
- **.NET 8.0** - Modern C# framework
- **ASP.NET Core Web API** - RESTful API development
- **Entity Framework Core 8.0** - ORM with SQL Server
- **JWT Bearer Authentication** - Secure token-based authentication
- **AutoMapper** - Object-object mapping
- **BCrypt.Net-Next** - Password hashing
- **MailKit/MimeKit** - Email service integration
- **Swagger/OpenAPI** - API documentation
- **Specification Pattern** - Advanced querying architecture

### Frontend
- **Angular 19** - Modern TypeScript framework
- **Angular SSR** - Server-Side Rendering
- **Tailwind CSS** - Utility-first CSS framework
- **ngx-translate** - Internationalization (i18n)
- **RxJS** - Reactive programming
- **Owl Carousel** - Responsive carousel component
- **JWT Decode** - Token management

### Database
- **Microsoft SQL Server** - Relational database management

## Architecture

### Backend Architecture

The backend follows a clean architecture pattern with clear separation of concerns:

- **Repository Pattern** - Data access abstraction
- **Unit of Work Pattern** - Transaction management
- **Specification Pattern** - Encapsulated query logic
- **Service Layer** - Business logic implementation
- **DTO Pattern** - Data transfer objects for API contracts
- **Dependency Injection** - Loose coupling and testability

### Key Components

#### Controllers
- `AdminController` - Administrative operations
- `AuthController` - Authentication and authorization
- `ContactController` - Contact form submissions
- `ExperienceController` - Experience counter management
- `MediaController` - Social media and WhatsApp integration
- `ProductController` - Product catalog operations
- `QualityController` - Quality showcase management
- `ReviewController` - Customer review management

#### Services
- Authentication service with JWT tokens
- Email service for notifications and OTP delivery
- Token service for JWT management
- Product, Review, Quality, Media, and Experience services

#### Entities
- Base entity pattern with generic key support
- Admin user management
- Product and category hierarchy
- Review system
- Contact message handling
- Password reset with OTP
- Token revocation tracking

## Features

### Public Features
- Product catalog with categories
- Product detail pages with images
- Customer review system
- Quality showcase gallery
- Contact form with email notifications
- Experience counter display
- Social media integration
- WhatsApp contact integration
- Multi-language support (Arabic/English)
- Responsive design with Tailwind CSS

### Admin Features
- Secure authentication with JWT
- Password reset with OTP via email
- Admin dashboard
- Product management (CRUD operations)
- Review management
- Quality content management
- Media settings configuration
- Contact message viewing

### Technical Features
- JWT-based authentication
- Token revocation mechanism
- Email service with HTML templates
- File upload and static file serving
- CORS configuration
- API documentation with Swagger
- Data seeding for initial setup
- Specification-based querying

## Prerequisites

Before running the application, ensure you have the following installed:

- **.NET 8.0 SDK** - [Download](https://dotnet.microsoft.com/download/dotnet/8.0)
- **Node.js** (v18 or higher) - [Download](https://nodejs.org/)
- **Angular CLI 19** - Install globally with `npm install -g @angular/cli@19`
- **SQL Server** - SQL Server 2019 or later, or SQL Server Express
- **Visual Studio 2022** or **Visual Studio Code** - Recommended IDEs

## Installation & Setup

### Backend Setup

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd Radwa-Minta/Back-End/RadwaMintaWebAPI
   ```

2. **Configure the database connection**
   Edit `appsettings.json` or `appsettings.Development.json`:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=localhost;Database=RadwaMintaWebDataBase;Trusted_Connection=True;TrustServerCertificate=True;"
     }
   }
   ```

3. **Configure JWT settings**
   Update the JWT configuration in `appsettings.json`:
   ```json
   {
     "Jwt": {
       "Key": "YourSuperSecretKeyThatShouldBeAtLeast32BytesLong",
       "Issuer": "RadwaMintaWebAPIApi",
       "Audience": "RadwaMintaClient",
       "DurationInDays": 7,
       "AccessTokenExpirationMinutes": "60"
     }
   }
   ```

4. **Configure email settings**
   Update email configuration for SMTP:
   ```json
   {
     "EmailSettings": {
       "SmtpServer": "smtp.gmail.com",
       "SmtpPort": 587,
       "EnableSsl": true,
       "SenderEmail": "your-email@gmail.com",
       "SenderPassword": "your-app-password",
       "SenderName": "RadwaMinta"
     },
     "ContactSettings": {
       "RecipientEmail": "recipient@example.com"
     }
   }
   ```

5. **Restore NuGet packages**
   ```bash
   dotnet restore
   ```

6. **Run database migrations**
   ```bash
   dotnet ef database update
   ```
   Note: The application includes automatic data seeding on startup.

7. **Run the API**
   ```bash
   dotnet run
   ```
   The API will be available at `https://localhost:5001` or `http://localhost:5000`
   Swagger UI: `https://localhost:5001/swagger`

### Frontend Setup

1. **Navigate to the frontend directory**
   ```bash
   cd Radwa-Minta/Front-End
   ```

2. **Install dependencies**
   ```bash
   npm install
   ```

3. **Configure API endpoint**
   Update `src/app/shared/environments/environment.ts` with your API base URL:
   ```typescript
   export const environment = {
     production: false,
     apiUrl: 'https://localhost:5001/api'
   };
   ```

4. **Run the development server**
   ```bash
   npm start
   ```
   The application will be available at `http://localhost:4200`

5. **Build for production**
   ```bash
   npm run build
   ```

## Project Structure

```
Radwa-Minta/
├── Back-End/
│   └── RadwaMintaWebAPI/
│       ├── Controllers/          # API endpoints
│       ├── Services/             # Business logic
│       ├── Interfaces/           # Service contracts
│       ├── Models/
│       │   ├── Entities/         # Domain models
│       │   └── DbContexts/       # EF Core context
│       ├── DTOs/                 # Data transfer objects
│       ├── Contracts/            # Repository and UoW interfaces
│       ├── Specifications/       # Query specifications
│       ├── MappingProfiles/      # AutoMapper profiles
│       ├── Data/
│       │   ├── SeedingData/      # Database seeding
│       │   └── *.json            # Seed data files
│       ├── Migrations/           # EF Core migrations
│       ├── Templates/            # Email HTML templates
│       ├── Helpers/              # Utility classes
│       └── wwwroot/              # Static files
│
└── Front-End/
    └── src/
        ├── app/
        │   ├── core/             # Core module
        │   │   ├── guards/       # Route guards
        │   │   ├── interceptors/ # HTTP interceptors
        │   │   └── services/     # Core services
        │   ├── pages/            # Feature pages
        │   │   ├── home/
        │   │   ├── products/
        │   │   ├── product-details/
        │   │   ├── quality/
        │   │   ├── contact/
        │   │   ├── login/
        │   │   ├── dashboard/
        │   │   └── ...
        │   └── shared/           # Shared components
        │       ├── components/
        │       ├── directives/
        │       ├── interfaces/
        │       └── environments/
        └── public/
            └── i18n/             # Translation files
```

## API Documentation

The API documentation is available via Swagger UI when running the application in development mode. Access it at:
- Development: `https://localhost:5001/swagger`
- Production: `https://your-domain.com/swagger`

### Authentication

The API uses JWT Bearer token authentication. To authenticate:

1. Login via `POST /api/Auth/login` with credentials
2. Receive a JWT token in the response
3. Include the token in subsequent requests:
   ```
   Authorization: Bearer {your-token}
   ```

### Main Endpoints

- **Authentication**: `/api/Auth/*`
- **Products**: `/api/Product/*`
- **Reviews**: `/api/Review/*`
- **Quality**: `/api/Quality/*`
- **Contact**: `/api/Contact/*`
- **Media**: `/api/Media/*`
- **Experience**: `/api/Experience/*`
- **Admin**: `/api/Admin/*`

## Configuration

### Backend Configuration

Key configuration sections in `appsettings.json`:

- **ConnectionStrings**: Database connection string
- **Jwt**: JWT token settings (Key, Issuer, Audience, Expiration)
- **EmailSettings**: SMTP configuration for email services
- **ContactSettings**: Recipient email for contact form
- **WhatsappSettings**: WhatsApp contact number
- **URLs**: Base URL configuration

### Frontend Configuration

- **Environment files**: `src/app/shared/environments/`
- **Translation files**: `public/i18n/ar.json` and `public/i18n/en.json`
- **API base URL**: Configure in environment files

## Development Guidelines

### Backend

- Follow the repository and unit of work patterns
- Use DTOs for all API contracts
- Implement specifications for complex queries
- Use dependency injection for all services
- Follow async/await patterns for I/O operations

### Frontend

- Follow Angular style guide conventions
- Use services for API communication
- Implement route guards for protected routes
- Use interceptors for common HTTP operations
- Follow component-based architecture

## Database Migrations

To create a new migration:
```bash
dotnet ef migrations add MigrationName --project RadwaMintaWebAPI
```

To apply migrations:
```bash
dotnet ef database update --project RadwaMintaWebAPI
```

To revert the last migration:
```bash
dotnet ef database update PreviousMigrationName --project RadwaMintaWebAPI
```

## Testing

### Backend Testing
```bash
dotnet test
```

### Frontend Testing
```bash
ng test
```

## Deployment

### Backend Deployment
1. Build the project:
   ```bash
   dotnet publish -c Release -o ./publish
   ```
2. Deploy the `publish` folder to your hosting server
3. Ensure SQL Server connection string is configured
4. Run database migrations on the production database

### Frontend Deployment
1. Build for production:
   ```bash
   npm run build
   ```
2. Deploy the `dist/radwaminta/browser` folder to your web server
3. For SSR deployment, use the `dist/radwaminta/server` folder with Node.js

## UI/UX Design

The project design is available on Figma:
[Figma Design Link](https://www.figma.com/design/6SEqXvHzPWExdIgs9ilFnt/Radwaminta?node-id=0-1&t=3CmI9TRoIDtooOYl-1)

## Security Considerations

- JWT tokens are used for authentication
- Passwords are hashed using BCrypt
- HTTPS is enforced in production
- CORS is configured appropriately
- SQL injection protection via EF Core parameterized queries
- Token revocation mechanism implemented

## Contributing

1. Create a feature branch from `main`
2. Make your changes following the project's coding standards
3. Test your changes thoroughly
4. Submit a pull request with a clear description

## License

This project is proprietary software for RadwaMinta.

## Support

For issues and questions, please contact the development team or create an issue in the repository.

## Version

Current version: 1.0.0

---

Built with modern web technologies and best practices. RadwaMinta - Excellence in Herbs, Spices, and Seeds since 2006.
