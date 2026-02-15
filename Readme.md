![Img](https://github.com/user-attachments/assets/4cac0065-29f9-4acd-872f-db5121458b1f)

[Flexy.Tools](https://github.com/FlexyTools/Flexy.Docs/tree/main) / [Framework](https://github.com/FlexyTools/Flexy.Docs/tree/main/Framework) / Flexy.UI


# Flexy.UI

**Clean, scalable, and production-proven UI screen management for Unity**

Create UI Screens, Popups, and Loaders with a consistent workflow  
Visual setup, minimal code, no enums, no prefab path wiring  
Suitable for both rapid prototyping and long-term projects

[GitHub](https://github.com/FlexyTools/Flexy.UI)
| [Docs](https://github.com/FlexyTools/Flexy.Docs/blob/main/Framework/Flexy.UI/Readme.md)


## Overview

Flexy.UI is a UI screen management system for Unity focused on safe iteration and clean structure

You work with standard Unity uGUI  
UI layout is created visually  
UI behavior is written in clean MonoBehaviour code  
UI elements are connected to screens using binders  
Used and evolved in production projects since 2012


## Key Benefits

- Fast iteration on individual UI screens
- No enum or string identifiers
- No prefab path wiring
- No Resources folder requirements
- Screens run in a valid runtime context
- The same UI setup works for prototypes and final builds


## How It Works

- Duplicate an existing UI Screen prefab
- Create a Screen MonoBehaviour
- Bind UI elements visually using binders
- Open the screen from UI events or runtime code

This is sufficient to add a working screen


## Key Features

- MonoBehaviour-centric workflow
- Visual UI setup using binders
- Minimal required code per screen
- UI screens can be tested in isolation
- History-aware screen navigation
- Runtime-safe screen execution
- Explicit screen and game stage transitions
- Screen prefabs loaded on demand
- No refactors when moving to Asset Bundles


## Who It Is For

Good fit if you:
- Iterate on UI screens frequently
- Expect UI to change during development
- Build prototypes and long-term projects
- Work solo or in a team
- Prefer visual setup over hardcoded references

Not a good fit if you:
- Need a no-code UI solution
- Expect a visual UI layout builder
- Do not want to write C# code


## Showcase Project
- Includes a buildable template project

The project can be opened, built, and run  
This allows testing Flexy.UI in real project conditions


## Technical Details

- Uses modern C# features (C# 10)
- Designed to work with Domain Reload disabled
- Tested with Unity 2022.3 through Unity 6.3
- Depends on Flexy.GameFlow and Unity uGUI
- Render pipeline agnostic
- Platform agnostic

<br/>

[Flexy.Tools](https://github.com/FlexyTools/Flexy.Docs/tree/main) / [Framework](https://github.com/FlexyTools/Flexy.Docs/tree/main/Framework) / Flexy.UI