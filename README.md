Interactive Streaming and Cinema Ticketing Platform

A final-year project developed for the Bachelor’s Degree in Computer Engineering at the University of Madeira.

## Description
GetWatch was built to explore the design and implementation of a complete, scalable web application using modern software engineering principles. Our motivation was to create an interactive platform that mirrors real-world services like Netflix or ticketing systems, but with a layered architecture showcasing advanced design patterns.

We built this project to deepen our understanding of how to integrate frontend frameworks (Blazor Server) with backend systems (Entity Framework, repositories, and services), while applying object-oriented design and software architecture best practices.

The platform solves the problem of managing complex interactions between users, media content, shopping carts, and ticket customization, all while maintaining a clean, testable codebase. It also simulates real-life concerns like command undo/redo, validation, and transaction safety in a learning context.

Throughout the project, we learned how to:

- Apply and combine key design patterns (e.g., Command, Proxy, Mediator, Strategy)

- Build dynamic, server-side UIs using Blazor

- Manage data persistence using Entity Framework Core with SQLite

- Write clean, modular, and maintainable C# code in a multi-layered architecture

- Handle real-world scenarios like cart management, ticketing, and payment handling

This project represents a synthesis of our technical knowledge, creativity, and teamwork.

## Table of Contents 

- [Technologies Used](#tecUsed)
- [Architecture & Design Patterns](#arch)
- [Core Features](#features)
- [Getting Started](#gettingStarted)
- [Credits](#credits)
- [License](#license)

## Technologies Used

<div align="center">
	<code><img width="50" src="https://raw.githubusercontent.com/marwin1991/profile-technology-icons/refs/heads/main/icons/git.png" alt="Git" title="Git"/></code>
	<code><img width="50" src="https://raw.githubusercontent.com/marwin1991/profile-technology-icons/refs/heads/main/icons/html.png" alt="HTML" title="HTML"/></code>
	<code><img width="50" src="https://raw.githubusercontent.com/marwin1991/profile-technology-icons/refs/heads/main/icons/css.png" alt="CSS" title="CSS"/></code>
	<code><img width="50" src="https://raw.githubusercontent.com/marwin1991/profile-technology-icons/refs/heads/main/icons/c%23.png" alt="C#" title="C#"/></code>
	<code><img width="50" src="https://raw.githubusercontent.com/marwin1991/profile-technology-icons/refs/heads/main/icons/_net_core.png" alt=".NET Core" title=".NET Core"/></code>
	<code><img width="50" src="https://raw.githubusercontent.com/marwin1991/profile-technology-icons/refs/heads/main/icons/blazor.png" alt="Blazor" title="Blazor"/></code>
</div>

## Architecture & Design Patterns

The project integrates multiple design patterns to follow software engineering best practices:

### Repository + Unit of Work
- Abstracts data access logic and groups changes into transactions for consistency.

### Entity Framework Core
- Handles data persistence with a relational model representing users, tickets, carts, purchases, etc.

### Mapper
- Converts database entities to domain models and vice versa.

### Proxy
- Adds access control and validation to the shopping cart.

### Command + CommandManager
- Encapsulates cart operations (add/remove items) and supports undo/redo functionality.

### Decorator
- Dynamically adds features (e.g., IMAX, 3D, seat count) to cinema tickets, affecting their base price.

### Strategy
- Allows different filtering/sorting strategies for browsing media content.

### Chain of Responsibility
- Handles form validation through chained handlers, e.g., in user registration or card input.

### Builder
- Simplifies the creation of complex objects like User and SupportTicket.

### Factory
- Used for generating CartItem objects based on product type (BluRay, Rent, Ticket) and repository creation.

### Mediator
- Coordinates actions between components, like executing cart operations through the GetWatchMediator

## Core Features

- User registration and login

- Movie browsing and session viewing

- Purchase and rental of media (Blu-rays, digital rentals)

- Dynamic cart with undo/redo support

- Ticket customization using decorators

- Support ticket creation

- Payment method management

- Purchase history tracking

## Getting Started

### Clone the repository
- git clone https://github.com/your-username/GetWatch.git
- cd GetWatch

### Restore dependencies
- dotnet restore

### Run the project
- dotnet run

## Credits

Project developed by:

- [Angelo Bras](#https://github.com/angelobras1794)

- [Manuel Tomas](#https://github.com/Getmeapint)

- [Afonso Rodrigues](#https://github.com/AfonsoRod)

- [Marcos Freitas](#https://github.com/MarcosASFreitas2004)

## License

MIT License

Copyright (c) 2025 GetWatch Dev Team

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
