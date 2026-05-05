# GData

A comprehensive C# ASP.NET Core API for managing social media-like functionalities including users, posts, articles, comments, group chats, and tagging systems.

## Overview

GData is a RESTful API built with ASP.NET Core that provides backend services for a social platform. It features JWT-based authentication, extensive data management capabilities, and is designed with a clean architecture following repository and service patterns.

## Features

- **User Management**: User authentication, registration, and profile management with JWT tokens
- **Posts System**: Create, read, update, and delete posts with full comment support
- **Articles System**: Full article management with tagging and commenting capabilities
- **Group Chat**: Real-time group messaging functionality
- **Comments**: Nested comment systems for both posts and articles
- **Article Tags**: Comprehensive tagging system for articles
- **JWT Authentication**: Secure API endpoints with JWT bearer token authentication
- **CORS Support**: Cross-origin resource sharing configured for frontend integration
- **API Documentation**: Swagger/OpenAPI and Scalar API documentation
- **Database Migrations**: Entity Framework Core migrations support

## Technology Stack

- **Language**: C# (.NET 9.0)
- **Framework**: ASP.NET Core 9.0
- **Database**: PostgreSQL with Entity Framework Core 9.0
- **Authentication**: JWT Bearer tokens with Microsoft Identity Model
- **API Documentation**: Swagger/Swashbuckle and Scalar AspNetCore
- **Email**: MailKit and MimeKit
- **Package Manager**: NuGet