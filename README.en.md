# Capstone Project: Space Invaders Recreation

## Summary

*   [1. Project Overview](#1-project-overview)
*   [2. Detailed Documentation](#2-detailed-documentation)
*   [3. Key Functional Requirements Summary](#3-key-functional-requirements-summary)
*   [4. Technologies](#4-technologies)
*   [5. Installation and Execution](#5-installation-and-execution)
    *   [5.1. Prerequisites](#51-prerequisites)
    *   [5.2. Database Configuration](#52-database-configuration)
        *   [5.2.1. Using Docker Compose (Recommended)](#521-using-docker-compose-recommended)
        *   [5.2.2. Manual PostgreSQL Configuration](#522-manual-postgresql-configuration)
    *   [5.3. Applying Database Migrations](#53-applying-database-migrations)
    *   [5.4. Project Execution](#54-project-execution)
*   [6. Project Structure](#6-project-structure)
*   [Project Structure Diagram](#project-structure-diagram)


## 1. Project Overview

This repository contains the source code and documentation for the final project of the Programming 3 discipline. The goal is to recreate the classic game **Space Invaders** as a desktop application, using C# and the Uno Platform, focusing on applying concepts of Object-Oriented Programming, Data Structures, and event handling.

## 2. Detailed Documentation

The project documentation is maintained on two platforms to ensure easy and comprehensive access to all relevant information:

- **[Documentation via Writerside](https://capstone-8f3123.gitlab.io/intro.html)**: The main source for technical documentation, user guides, and architecture details. It is automatically generated from the files in the `/Writerside` directory.

- **[GitLab Wiki](https://gitlab.com/jala-university1/cohort-4/oficial-pt-programa-o-3-cspr-231.ga.t2.25.m1/se-o-a/gustavo.jesus/capstone/-/wikis/home)**: Used for development notes.


## 3. Key Functional Requirements Summary

| ID | Requirement | Priority |
| --- | --- | --- |
| RF01 | Ship Control (Horizontal Movement) | High |
| RF02 | Laser Firing (Vertical) | High |
| RF04 | Enemy Generation and Movement | High |
| RF06 | Lives and Damage System | High |
| RF07 | Game Over Conditions | High |
| RF19 | Menus and Screens (Initial, Game Over) | High |
| RF21 | Score Persistence | High |

## 4. Technologies

*   **Language**: C#
*   **Platform**: Uno Platform (for desktop application)
*   **UI**: XAML
*   **Data Persistence**: Entity Framework Core with PostgreSQL
*   **Architecture Pattern**: MVVM (Model-View-ViewModel)
*   **Version Control**: Git

## 5. Installation and Execution

This guide details the necessary steps to set up the development environment, install dependencies, and run the Space Invaders project.

### 5.1. Prerequisites

Ensure that the following software is installed on your machine:

*   **SDK .NET 9.0 (or higher)**: Essential for compiling and running .NET applications. You can download it from [dotnet.microsoft.com/download](https://dotnet.microsoft.com/download).
*   **Docker Desktop (or Docker Engine)**: Required to run the PostgreSQL database. Available at [docker.com/products/docker-desktop](https://www.docker.com/products/docker-desktop).
*   **Visual Studio 2022 (or Visual Studio Code or Rider)**: Recommended IDEs for C# and Uno Platform development. While not strictly mandatory, they greatly facilitate the process.
    *   **Visual Studio**: Make sure you have the "Desktop development with .NET" and ".NET Multi-platform App UI development" workloads installed.
*   **Uno Platform Templates**: To create and manage Uno Platform projects, you will need to install the templates. Run the following command in the terminal:

    ```c#
    dotnet new install Uno.Templates
    ```

    For more details, consult the official documentation: [platform.uno/docs/articles/get-started.html](https://platform.uno/docs/articles/get-started.html)

### 5.2. Database Configuration (PostgreSQL with Docker)

The project uses PostgreSQL for data persistence (scores), managed via Docker to simplify configuration.

1.  **Navigate to the project root directory** in your terminal (where the `docker-compose.yml` file is located):

2.  **Start the database service** using Docker Compose:

    ```c#
    docker-compose up -d db
    ```

    This command will download the PostgreSQL image (if you don't have it yet), create, and start a database container configured for the project. The database will be accessible on port `5432`.

3.  **Verify that the container is running** (optional):

    ```c#
    docker ps
    ```

    You should see a container named `capstone-programming-3-db-1` (or similar) in the list.

### 5.3. Project Configuration and Compilation

1.  **Navigate to the SpaceInvaders project directory**:

    ```c#
    cd SpaceInvaders/SpaceInvaders
    ```

2.  **Restore project dependencies**:

    ```c#
    dotnet restore
    ```

3.  **Compile the project**:

    ```c#
    dotnet build
    ```

    This command will compile the source code and check for errors.

### 5.4. Project Execution

After successful compilation and with the database running, you can run the application.

1.  **Run the desktop application**:

    ```c#
    dotnet run --project SpaceInvaders/SpaceInvaders.csproj -f net9.0-desktop
    ```

    The Space Invaders application should start in a new window.

#### Note on Audio in Linux

If you are running the project in a Linux environment, make sure you have `mpg123` installed for audio playback. The project's `SoundService` uses this external utility to ensure audio compatibility on Linux.

To install `mpg123` (example for Debian/Ubuntu based systems):

```c#
sudo apt-get update
sudo apt-get install mpg123
```

With these steps, you will be ready to develop and test the Space Invaders project on your machine.

## 6. Project Structure

The project is organized to follow the MVVM (Model-View-ViewModel) pattern, promoting a clear separation of responsibilities. The main folders are:

*   **`SpaceInvaders/Models`**: Contains domain classes, such as `Player`, `Alien`, and `Score`.
*   **`SpaceInvaders/ViewModels`**: Responsible for presentation logic and UI state. It interacts with services to obtain and manipulate data to be displayed in the Views.
*   **`SpaceInvaders/Services`**: Implements business logic decoupled from the UI, such as `PlayerService` and `ScoreService`, and coordinates data access.
*   **`SpaceInvaders/Presentation`**: Defines the user interface (UI) in XAML. Contains the `Views` (pages like `MainPage.xaml` and `GameOver.xaml`) that bind to the `ViewModels`.
*   **`SpaceInvaders/Data`**: Includes the Entity Framework Core `DbContext` and migrations, managing all communication with the database.
*   **`SpaceInvaders/Factories`**: Contains classes for creating complex objects, such as `AlienFactory` and `WaveFactory`, abstracting instantiation logic.
*   **`SpaceInvaders/Interfaces`**: Defines contracts (abstractions) for services and other classes, allowing dependency injection and facilitating testing.
*   **`SpaceInvaders/Constants`**: Stores constant values used throughout the application, such as file paths and game settings.
*   **`SpaceInvaders/Assets`**: Contains all static resources, such as sprites, fonts, and sound files.
*   **`SpaceInvaders/Styles`**: Stores XAML resource dictionaries that define the application's visual styles.

## Project Structure Diagram

Below is a diagram illustrating the organization of the main folders and the relationship between them:

```plantuml
@startuml
skinparam componentStyle rectangle

package "SpaceInvaders" {
    folder "Presentation" {
        [MainPage.xaml]
        [GameOver.xaml]
        [ScorePage.xaml]
    }
    
    folder "ViewModels" {
        [MainViewModel]
        [GameOverViewModel]
        [ScoreViewModel]
    }
    
    folder "Services" {
        [PlayerService]
        [ScoreService]
        [SoundService]
    }
    
    folder "Interfaces" {
        [IPlayerService]
        [IWaveFormationStrategy]
    }
    
    folder "Factories" {
        [AlienFactory]
        [WaveFactory]
    }
    
    folder "Models" {
        [Player]
        [Alien]
        [Score]
    }
    
    folder "Data" {
        [SpaceInvadersDbContext]
        [Migrations]
    }
    
    folder "Constants" {
        [GameConstants]
        [SpritePaths]
    }
    
    folder "Assets" {
        [Images]
        [Sounds]
    }
    
    folder "Styles" {
        [AppStyles.xaml]
    }
}

Presentation -down-> ViewModels : displays data and forwards events
ViewModels -down-> Services : invokes business logic
Services ..> Interfaces : implements
Services -down-> Data : accesses and persists data
Services -down-> Factories : uses to create objects
Factories -down-> Models : creates instances of
ViewModels -right-> Models : manipulates as DataContext
@enduml
