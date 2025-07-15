# Vertigo Roulette MiniGame

A modular, extensible Unity framework for state-driven mini-games, featuring addressable asset management, custom UI layouts, inventory systems, and a robust finite state machine architecture.  
This project is ready to serve as the foundation for roulette/progression minigames, or to be extended for other state-driven experiences.

---

## Table of Contents

- [Features](#features)
  - [State Machine System](#state-machine-system)
  - [Addressable Asset Management](#addressable-asset-management)
  - [Zone & Roulette Map System](#zone--roulette-map-system)
  - [Inventory System](#inventory-system)
  - [Custom UI Components](#custom-ui-components)
  - [Editor Extensions](#editor-extensions)
- [Project Setup](#project-setup)
- [How to Configure](#how-to-configure)
- [Extending the Framework](#extending-the-framework)
- [Best Practices](#best-practices)
- [Overview Video on Youtube](https://youtu.be/Erh6ycHHG3Q)
---

## Features

### State Machine System

- **Modular, Inspector-driven FSMs:**  
  Define states, transitions, and conditions as ScriptableObjects or MonoBehaviours.
- **Asynchronous States:**  
  Support for coroutine-based `IEnumerator` transitions and updates.
- **Reusable base types:**  
  - `StateComponentBase`
  - `StateTransitionBase`
  - `StateMachineComponentBase`
- **Custom Inspector & Editor:**  
  Fast, error-resistant setup and clear organization in the Unity Editor.

---

### Addressable Asset Management

- **Centralized Asset Loading:**  
  Cached, safe loading of Addressable assets by key.
- **Coroutine/Async Friendly:**  
  Yield on asset loads; pass result via strongly-typed result container.
- **Manual & Bulk Release:**  
  Release single or all cached handles for memory management.

---

### Zone & Roulette Map System

- **ZoneMap Configurations:**  
  - Fixed, looping, or "hopping" (special event) zone strategies.
- **Zone Instance Management:**  
  Zones track state, progression, and hold roulette data.
- **Roulette System:**  
  Editor-driven, weighted rewards; deterministic spin outcome (lands at chosen index).
- **Reward Handling:**  
  Handles "bombs", loss, and standard rewards.

---

### Inventory System

- **Player Inventory:**  
  Dynamic, serializable inventory with item stack counts.
- **Reward Delivery:**  
  Rewards are added automatically based on roulette results.
- **Inventory UI:**  
  Grid and tabbed display, filterable by item type.

---

### Custom UI Components

- **RadialLayout:**  
  Circular layout for reward wheels—children always scale and align perfectly.
- **AutoGridLayout:**  
  Dynamic grid with auto-sizing square cells and fixed column count.
- **CanvasPointDivider:**  
  Utility for dividing UI space into equidistant points for placement logic.

---

### Editor Extensions

- **Custom Inspector Drawers:**  
  For states, transitions, and regions.
- **Addressable Validation:**  
  Ensures all unique IDs are set and validated for addressable assets.
- **ReadOnly Attribute:**  
  Display fields as read-only in inspector for clarity.

---

## Project Setup

1. **Unity Version:**  
   Recommended: **2021.3+** (URP or Built-in)
2. **Dependencies:**  
   - [Addressables](https://docs.unity3d.com/Packages/com.unity.addressables@latest)
   - [DOTween](http://dotween.demigiant.com/)
   - [TextMeshPro](https://docs.unity3d.com/Packages/com.unity.textmeshpro@latest)
3. **Clone & Open:**  
   Clone the repo, open in Unity Hub.
4. **Addressables Setup:**  
   - Mark all runtime assets as **Addressable**.
   - Build Addressables via **Window > Asset Management > Addressables > Build > Build Player Content**.

---

## How to Configure

### 1. State Machines

- Add a `StateMachineComponentBase` to a GameObject.
- Add state configuration entries (child objects or asset references).
- Assign your `StateComponentBase` scripts and transitions in the inspector.

### 2. Zones & Roulette

- Create `ZoneConfiguration` assets for each unique zone.
- Add all zones to a `ZoneMapConfigurationFixed` or `ZoneMapConfigurationHopping` asset.
- Create and link your `MiniGameConfiguration` asset with the desired zone map configuration.

### 3. Rewards

- Create `ItemDefinition` assets, assign as Addressable.
- Create `RouletteRewardConfiguration` assets referencing item definitions.
- Configure probabilities, amounts, and flags as needed.

### 4. Inventory

- Assign or instantiate a `PlayerInventoryDisplay` in the UI.
- Configure tab types and visuals via the inspector.

### 5. UI

- Use `RadialLayout` for reward wheels.
- Use `CustomAutoGridLayout` for inventory displays.
- Use `CanvasPointDivider` via script for any equidistant UI placement needs.

---

## Extending the Framework

### Adding New States or Transitions

1. Create a new C# class inheriting from `StateComponentBase` or `StateTransitionBase`.
2. Implement required logic in `OnEnter`, `ShouldTransition`, etc.
3. Add your new type to your state machine in the Unity inspector.

### New UI Layouts

- Extend the provided layout scripts or write new MonoBehaviours as needed.
- Attach to any RectTransform for modular UI behavior.

### New Reward Types

- Extend `RouletteRewardConfiguration` or override reward delivery logic as needed.

---

## Best Practices

- **Always use Addressables** for runtime asset references.
- **Organize code with regions** and comments for serialized fields, properties, methods.
- **Release Addressables** only when no longer needed to avoid memory leaks.
- **Use base classes** for state/transition logic to ensure coroutine compatibility.
- **Leverage custom inspector tools** for safer, faster configuration.

---

_For questions or contributions, please open an issue or create a pull request._
