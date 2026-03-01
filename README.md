# Dangeon RPG
## Overview
This project is a 2.5D Dungeon RPG game developed using C# with Godot.
It started as part of a course and was significantly extended with custom systems and architectural improvements.

## 🎯 Gameplay Overview
- Explore a dungeon environment
- Fight enemies using melee combat
- Manage movement and attack states
- Interact with UI feedback and game systems

## 🎮 Features
- Modular **Character System** using inheritance (Player & Enemy)
- **Combat System** with hitboxes and hurtboxes
- **Finite State Machine (FSM)** for player and enemy behaviors
- Reusable AI logic shared between entities
- Basic **UI system**
- Dungeon environment with **lighting and particle effects**

## 🧠 Architecture & Design
The project emphasizes **clean architecture and reusability**:
- **Object-Oriented Programming (OOP)**
  - Base `Character` class
  - Derived `Player` and `Enemy` classes
- **State Machine Pattern**
  - Handles movement, attacks, and enemy AI
  - Designed to be reusable across different entity types
- **Component-based structure** for gameplay systems

## 🔧 My Contributions & Improvements
- Refactored the state machine to work for both Player and Enemy
- Improved code modularity and reusability
- Extended the combat system
- Added UI and gameplay polish
- Improved architecture and maintainability

## 📸 Screenshots / Demo

## 🛠️ Technologies Used
- **Language:** C#
- **Engine:** Godot Engine
- **Paradigms:** OOP, State Machines, Modular Design

## 🚀 Getting Started
### Prerequisites
- Install Godot (with .NET support)
- .NET SDK installed

### Run the Project

```bash
git clone https://github.com/your-username/your-repo.git
```
Open godot and import the project.  
Run the project and then run the game.

## 🤝 Credits
- Course base project: (add course name if you want to credit it)
- All extensions and refactoring implemented by me

## 📄 License
- This project is for educational and portfolio purposes.