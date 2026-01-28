# 🏰 TileVania: 2D Platformer & Input Architecture

![System](https://img.shields.io/badge/System-New%20Input%20System-red)
![Architecture](https://img.shields.io/badge/Architecture-Event%20Driven-purple)

> **Note:** This project serves as a technical demonstration of **Unity's New Input System**, decoupling player logic from hardware inputs to create a flexible control scheme.

## 📖 Project Overview
TileVania is a classic 2D platformer developed to explore advanced Unity 2D tools. Unlike traditional input polling, this project utilizes an **Event-Driven Architecture** to handle gameplay mechanics, ensuring that input logic is separated from character behavior.

## ⚙️ Key Technical Features

### 1. Event-Driven Input System (Decoupled Logic)
Integrated **Unity's New Input System** to replace the legacy `Input.GetAxis()` polling method.
* **Abstraction:** Gameplay mechanics (Jump, Move, Climb) are triggered by input events, making it easy to switch between Keyboard, Gamepad, or Touch controls without rewriting code.
* **Separation of Concerns:** Input reading and Character logic are fully decoupled.

```csharp
// Example: Using Input Action Context instead of Update loop
void OnJump(InputValue value)
{
    if (!isAlive) { return; }
    if (value.isPressed && myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
    {
        rb.velocity += new Vector2(0f, jumpSpeed);
    }
}

### 2. Responsive UI & Canvas Management
[cite_start]Utilized **Unity UI Toolkit** and Canvas scalers to create a responsive interface.
* Designed a **HUD** that adapts seamlessly to different screen resolutions and aspect ratios.
* Implemented efficient **Scene Management** for reliable "Game Over" and "Restart" loops, ensuring proper memory cleanup.

### 3. Advanced 2D Physics & State Management
* **Composite Colliders:** Optimized physics calculations by merging individual tile colliders into a single geometry, significantly improving performance.
* **State Handling:** Managed player states (*Running, Climbing, Dying*) to strictly synchronize animations with physics interactions.

## 🛠️ Tech Stack
* **Engine:** Unity 6 LTS
* [cite_start]**Input:** Unity New Input System Package
* **Level Design:** Tilemaps, Rule Tiles, Composite Colliders
* **Camera:** Cinemachine (Target Tracking & Confiners)