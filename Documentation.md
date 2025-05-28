# Course Project Documentation

## Title Page
**Course Project**

[Your Full Name]
[Your Course, Specialty, Faculty, Student Number]

Subject: Technical Programming Tools (TSP)
Instructor: Assoc. Prof. Dr. Eng. Dimitrichka Nikolaeva

## Project Assignment and Description
The project involves developing a Personal Diary web application that allows users to create, read, update, and delete diary entries. The application includes user authentication, role-based access control, and a modern, responsive user interface.

## Abstract
This project implements a Personal Diary web application using modern web development technologies. The application features a secure authentication system, intuitive user interface, and robust data management capabilities. The implementation follows clean architecture principles and includes comprehensive unit testing.

## Introduction
In today's digital age, personal journaling has evolved from traditional paper diaries to digital solutions. This project addresses the need for a secure, user-friendly digital diary application that provides both functionality and privacy. The relevance of this project lies in the growing demand for personal digital journaling tools that offer modern features while maintaining data security.

## Review of Existing Solutions. Conclusions. Goals and Tasks

### Existing Solutions
Several digital diary applications exist in the market:
1. Day One - A popular journaling app with cloud sync
2. Journey - A cross-platform journaling application
3. Penzu - A secure online journal

### Conclusions
**Advantages of Existing Solutions:**
- Cloud synchronization
- Cross-platform availability
- Rich text formatting

**Disadvantages:**
- Often require paid subscriptions
- Limited customization options
- Privacy concerns with cloud storage

### Goals and Tasks
**Goal:** Develop a secure, user-friendly personal diary application with modern features.

**Tasks:**
1. Design and implement a secure authentication system
2. Create a responsive user interface
3. Implement CRUD operations for diary entries
4. Add search and filtering capabilities
5. Implement role-based access control
6. Ensure data security and privacy
7. Write comprehensive unit tests

## Design and Description of the Proposed Solution

### 7.1 Requirements for the Software System
- User authentication and authorization
- CRUD operations for diary entries
- Search and filter functionality
- Responsive design
- Data validation
- Error handling
- Unit testing

### 7.2 Logical Model
The system follows a clean architecture pattern with the following layers:
- Presentation Layer (Blazor Client)
- Business Logic Layer (Services)
- Data Access Layer (Repositories)
- Domain Layer (Entities and Models)

### 7.3 System Architecture
The application uses a client-server architecture:
- Frontend: Blazor WebAssembly
- Backend: ASP.NET Core Web API
- Database: SQL Server
- Authentication: JWT-based

### 7.4 Data Organization
The database schema includes:
- Users table
- Diary entries table
- Audit information
- Soft delete functionality

### 7.5 Programming Language and Development Environment
- C# (.NET 9.0)
- Blazor WebAssembly
- Visual Studio 2022
- SQL Server
- Git for version control

### 7.6 Implementation Details

#### 7.6.1 Data Structure
Key entities:
- User (ID, Name, Email, Password, Permissions)
- DiaryEntry (ID, Title, Description, CreationDate, LastModifiedDate)

#### 7.6.2 Software Modules
1. Authentication Module
   - User registration
   - Login/logout
   - Password management

2. Diary Management Module
   - Entry creation
   - Entry editing
   - Entry deletion
   - Entry viewing

3. Search and Filter Module
   - Text search
   - Date filtering
   - Sorting options

#### 7.6.3 User Interface
- Modern, responsive design using MudBlazor
- Intuitive navigation
- Form validation
- Error notifications
- Loading indicators

### 7.7 Input Validation
- Required field validation
- Data type validation
- Length restrictions
- Format validation

### 7.8 Output Reports
- Diary entry list
- Search results
- User profile information

### 7.9 System Instructions

#### 7.9.1 User Manual
1. Registration and Login
   - Navigate to the registration page
   - Fill in required information
   - Verify email
   - Login with credentials

2. Creating Diary Entries
   - Click "Add New Entry"
   - Fill in title and description
   - Save entry

3. Managing Entries
   - View entries in the main page
   - Edit or delete entries using action buttons
   - Search and filter entries using the search bar

#### 7.9.2 Installation Instructions
1. Prerequisites:
   - .NET 9.0 SDK
   - SQL Server
   - Visual Studio 2022

2. Setup Steps:
   - Clone the repository
   - Restore NuGet packages
   - Update connection string
   - Run database migrations
   - Start the application

#### 7.9.3 Maintenance
- Regular database backups
- Log monitoring
- Security updates
- Performance optimization

#### 7.9.4 Hardware Requirements
- CPU: 2 GHz or faster
- RAM: 4 GB minimum
- Storage: 1 GB free space
- Internet connection

## Testing Results
The system includes comprehensive unit tests covering:
1. Repository layer tests
2. Business logic tests
3. UI component tests

Test coverage includes:
- CRUD operations
- Authentication
- Data validation
- Error handling

## References
[1] Microsoft. (2024). ASP.NET Core Documentation. https://docs.microsoft.com/en-us/aspnet/core/

[2] MudBlazor. (2024). MudBlazor Documentation. https://mudblazor.com/

[3] Clean Architecture. (2024). https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html

## Appendices
The project source code is organized in the following structure:
- PersonalDiary.API
- PersonalDiary.Client
- PersonalDiary.Business.Models
- PersonalDiary.Entities
- PersonalDiary.Services
- PersonalDiary.HttpRepositories
- PersonalDiary.Mappers
- Test Projects 