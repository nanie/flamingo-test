# README

## Overview
This project is a submission for a development test, addressing the provided requirements and implementing some of the stretch goals. Below is a summary of the features, structure, and additional information about the implementation.

---

## Features

### **Core Requirements**
1. **DOTween and Unity Animation Clips**
   - DOTween was used to create animations for scene transition.
   - Unity Animation Clips were utilized to enhance the player experience with "squish" animations during jumps.
   - DOTween was also used to animate the mini-game UI, making movements feel more dynamic and responsive.

2. **Modular and Easily Modifiable Board Layout**
   - The board is generated in runtime from the json definition of the level using a zenject factory
   - The board can be easily created with the board creation tool

3. **Load Quizzes from JSON Files**
   - Quizzes are loaded from provided JSON files, enabling dynamic and extensible content updates.

---

### **Stretch Goals**
1. **Architecture for Multiple Game Modes**
   - The project structure supports multiple game modes.
   - Due to time constraints, only one mode was implemented.

2. **Scene Transition Mechanism**
   - A splash animation was implemented using DOTween.
   - A service (`SimpleLoadSceneService`) handles scene transitions.
     - Views can request animations to play before loading a scene.
     - The service ensures the animation completes before transitioning.

3. **Dynamic Asset Loading via Addressables**
   - The `ImageLibrary` service loads sprite assets and is structured to allow easy modification for addressables in the future.

4. **Extensible Quiz Implementation**
   - Each mini-game data consists of one `View` and one text asset.
   - Views derive from the abstract class `MinigameView`, making it simple to add new question and answer types by creating new `View` classes.

5. **Zenject Integration**
   - Zenject was used for dependency injection.
   - Initially relied heavily on signals, but transitioned to a more service-oriented approach to improve maintainability.
   - In future updates I would probably use less signals, if use it at all. 

6. **Custom Map Creation Tool**
   - A map creation tool was developed to facilitate the creation of new boards:
   
[![Map Creation Tool Video](https://img.youtube.com/vi/k06WSznRpdc/0.jpg)](https://www.youtube.com/watch?v=k06WSznRpdc)

   Next steps for the tool:
   - Validate the board to assure its a valid board
   - Show stats of the maps (minigame density, tile count...)
   - Load created maps to modify existant boards
---

### **Unfinished Features**
- A localization service was started but remains incomplete due to time constraints. This would have allowed easy translation of game text for multiple languages.

---

## Project Structure

### **Key Services and Systems**
- **Board**: Creates the board from the JSON configuration. It has dependencies on `GameLoop` and `GameState`.
- **GameLoop**: Controls the game modes and signals. It has dependencies on `TextMeshPro` and `DOTween`.
- **GameState**: Maintains the selection data and player options. It has a dependency on `SceneLoader`.
- **ImageLibrary**: Provides sprites to be loaded by name. It has no dependencies.
- **Localization**: Provides localized texts to be used in any text. It has a dependency on `TextMeshPro`.
- **MainMenu**: Manages the main menu views. It has no services but depends on `GameState`, `TextMeshPro`, and `DOTween`.
- **Minigame**: Manages the creation and events of mini-games. It has dependencies on `GameState`, `GameLoop`, `ImageLibrary`, `Localization`, `TextMeshPro`, and `DOTween`.
- **Player**: Controls player movement and animations. It has dependencies on `GameLoop` and `DOTween`.
- **SceneLoader**: Manages scene loading. It has a dependency on `DOTween`.

### **Tools**
- **Map Creation Tool**: Simplifies the creation of new game boards through an intuitive editor interface.

---

## Installation and Setup
1. Clone the repository.
2. Open the project in Unity (tested with Unity version 2022.3.18f1).
3. Run the scene `Menu` to start the project OR play directly the `Board` scene, running directly this scene the `Default Board` selected in the `BoardInstaller` will be loaded.

---

## Future Improvements
1. **Localization Service**
   - Complete the implementation of the localization service to support multiple languages.

2. **Multiple Game Modes**
   - Add additional game modes such as hotseat and vs AI.

3. **Full Addressables Integration**
   - Transition the `ImageLibrary` service to use Unity Addressables for efficient asset management.

4. **Zenject Best Practices**
   - Refine the use of Zenject based on lessons learned, moving toward a service-centric approach.
     
5. **Tool improvements**
   - Add more features to the board creation tool.
---

## Notes
This project demonstrates a strong focus on modularity, extensibility, and adherence to best practices. While some stretch goals were not fully implemented, the groundwork is laid for easy future expansion.
