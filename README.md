Vertigo Roulette MiniGame
A modular, extensible Unity framework for state-driven mini-games, featuring addressable asset management, custom UI layouts, inventory systems, and a robust finite state machine architecture.
This project is ready to serve as the foundation for roulette or progression-based minigames, or to be extended for other state-driven experiences.

Table of Contents
Features

State Machine System

Addressable Asset Management

Zone & Roulette Map System

Inventory System

Custom UI Components

Editor Extensions

Project Setup

How to Configure

Extending the Framework

Best Practices

License

Features
State Machine System
Modular, Inspector-driven FSMs:

Define states, transitions, and conditions as ScriptableObjects or MonoBehaviours.

Supports asynchronous (IEnumerator) transitions and updates.

Reusable base types:

StateComponentBase, StateTransitionBase, StateMachineComponentBase

Editor Integration:

Custom editors and drawers for rapid setup and reconfiguration.

Addressable Asset Management
Centralized Asset Loading:

Safe, cached loading of any addressable Unity asset.

Coroutine Integration:

Yield on asset loads with contextual result passing (strongly typed).

Manual & Bulk Release:

Release single or all assets via AddressableAssetManager.

Zone & Roulette Map System
ZoneMap Configuration:

Multiple strategies: fixed lists, "hopping" logic, looped maps.

Zone Instance Management:

Zones track state, progression, and hold roulette instance data.

Roulette Configuration:

Editor-driven, weighted reward configuration.

Spin Simulation:

Roulette spins are fully animated and deterministic (land on specific result).

Reward Handling:

Supports “bomb” or loss states.

Inventory System
Player Inventory:

Dynamic, serializable inventory.

Reward Delivery:

Add rewards to player inventory from roulette results.

Inventory UI:

Grid and tabbed display, filter by item type.

Custom UI Components
RadialLayout:

Place children in a perfect circle (for rewards, etc.), always scales with parent.

AutoGridLayout:

Dynamic grid layout, square or fixed columns, auto cell sizing.

CanvasPointDivider:

Utility for dividing UI space into equidistant points (for zone display, etc.).

Editor Extensions
Inspector Customization:

Region-folded code and custom drawers for clean, error-resistant setup.

Addressable Validation:

Ensures addressable GUIDs are assigned at edit time.

ReadOnly Attribute:

Easily mark fields as read-only in inspector.

Project Setup
Unity Version:

Recommended: 2021.3+ (URP or Standard)

Dependencies:

Addressables

DOTween

TextMeshPro

Clone and Open

Clone repository, open in Unity Hub.

Addressables Setup:

Mark all assets (sprites, scenes, prefabs, ScriptableObjects) required at runtime as Addressable.

Build Addressables content via Window > Asset Management > Addressables > Build > Build Player Content.

How to Configure
1. State Machines
Add a StateMachineComponentBase to a GameObject.

Add state configuration entries (as children or references).

Assign State scripts and transitions in the inspector.

Use the editor tools to quickly add or reorder transitions.

2. Zones & Roulettes
Create ZoneConfiguration assets for each unique zone type.

Create a ZoneMapConfigurationFixed or ZoneMapConfigurationHopping asset and add all ZoneConfiguration assets.

Link your MiniGameConfiguration to your desired zone map configuration.

3. Rewards
Create ItemDefinition ScriptableObjects, assign as Addressable.

Create RouletteRewardConfiguration entries referencing item definitions.

Setup weights, amounts, and any special flags (e.g. bomb).

4. Inventory
Assign or instantiate a PlayerInventoryDisplay in the UI.

Configure tab types as needed (via enum or inspector).

5. UI
Use RadialLayout for reward wheels, assign as parent to reward display prefabs.

Use CustomAutoGridLayout for inventory displays.

Use CanvasPointDivider via script for any point-distribution layout needs.

Extending the Framework
Add New States or Transitions
Create a C# class inheriting StateComponentBase or StateTransitionBase.

Implement logic for OnEnter, ShouldTransition, etc.

Add as a new state or transition in the FSM inspector.

Add New UI Layouts
Create new layout MonoBehaviours as needed, or extend provided ones.

All layout scripts are modular and can be attached to any RectTransform.

New Reward Types
Extend RouletteRewardConfiguration or implement custom handling in the reward system.

Best Practices
Keep asset references addressable for runtime safety.

Group states and transitions by game feature, using regions and editor comments for maintainability.

Use provided base classes for all state/transition logic to ensure coroutine compatibility.

Release addressables only when sure they are no longer used.

Leverage custom attributes for inspector clarity.

License
Replace with your license, e.g. MIT, or proprietary if internal.

For further questions or contributions, please open an issue or create a pull request.

