# TV Simulation - Unity 2D Project

## 🖥️ Overview
An interactive TV simulation where users can turn the TV on/off, switch between channels with unique content, adjust volume, and interact with characters that respond with sound effects.

## 🎮 Controls

### Power Control
- **Power Toggle**: Turn the TV ON or OFF
  - When OFF: Screen goes black, all audio stops
  - When ON: Current channel displays with music

### Channel Navigation
- **Next Button**: Switch to the next channel
- **Previous Button**: Switch to the previous channel
- **Channel Dropdown**: Select a specific channel directly
- Channels switch with smooth transition effects

### Volume Control
- **Volume Slider**: Adjust the volume from 0% to 100%
- Affects both background music and sound effects

### Character Interaction
- **Mouse Hover**: Hover over characters on the TV screen to hear unique sounds
- Characters only respond when the TV is powered ON
- Visual feedback: Characters scale up slightly on hover

## ✨ Features

### TV Power System
- Toggle UI element controls TV power state
- Black screen when TV is OFF
- Channel content displayed when TV is ON
- All audio automatically stops when powering off

### Channel System
- Multiple channels with unique content:
  - **Channel 1**: [Custom background + 3+ interactive characters]
  - **Channel 2**: [Custom background + 3+ interactive characters]
  - Add more channels as needed
- Each channel features:
  - Unique background image
  - Looping background music
  - Interactive characters with hover sounds

### Sound System
- **Music AudioSource**: Plays channel background music
- **SFX AudioSource**: Plays character hover sounds
- Volume slider controls both audio sources
- Real-time volume percentage display

### Interactive Characters
- 3+ characters per channel
- Unique hover sound for each character
- Visual hover effects (scale animation)
- Only interactive when TV is powered ON

### UI Elements
The project uses all required UI components:
- ✅ **Toggle**: Power switch
- ✅ **Button**: Channel navigation (Next/Previous)
- ✅ **Slider**: Volume control
- ✅ **Dropdown**: Channel selection
- ✅ **Text**: Channel name, volume display, status
- ✅ **Image**: TV screen, channel backgrounds, characters
- ✅ **Scroll View**: [Optional: Channel info, credits]
- ✅ **Input Field**: [Optional: Comments, feedback]

### UI Animations
- **Button Hover Effect**: Buttons scale up on mouse hover
- **Toggle Click Animation**: Toggle animates when clicked
- **Character Hover**: Characters scale up when hovering
- **Channel Transition**: Smooth fade effect when switching channels

## 🗂️ Project Structure

```
Assets/
├── Scenes/
│   └── TV.unity                    # Main TV simulation scene
├── Scripts/
│   ├── TVController.cs             # Main TV logic controller
│   ├── CharacterHoverAudio.cs      # Character interaction script
│   ├── ButtonHoverEffect.cs        # Button hover animation
│   ├── ToggleAnimator.cs           # Toggle click animation
│   ├── UIAnimator.cs               # General UI animations
│   └── DropdownHandler.cs          # Dropdown management
├── Images/
│   ├── [Channel backgrounds]
│   ├── [Character sprites]
│   └── [TV interface graphics]
├── Audio/
│   ├── [Background music files]
│   └── [Character sound effects]
├── Prefabs/
│   └── [UI prefabs, character prefabs]
└── Animations/
    └── [UI animation clips]
```

## 🎯 How to Run the Project

### In Unity Editor
1. Open the project in Unity 6000.0 or later
2. Open the scene: `Assets/Scenes/TV.unity`
3. Click the Play button in the Unity Editor
4. Interact with the TV interface:
   - Toggle power ON
   - Switch channels using buttons or dropdown
   - Adjust volume with the slider
   - Hover over characters to hear sounds

### Build & Run
1. Open `File > Build Settings`
2. Ensure `TV` scene is in the build
3. Select target platform (Windows, Mac, Linux)
4. Click `Build` and choose output folder
5. Run the generated executable

## 🎨 Customization

### Adding New Channels
1. In the TV scene, create a new GameObject for the channel
2. Add background Image and character Images
3. Add CharacterHoverAudio script to each character
4. Assign unique hover sounds to each character
5. In TVController, add new ChannelData entry:
   - Channel Name
   - Channel GameObject
   - Background Sprite
   - Background Music
   - Character array

### Changing Sounds
- Replace audio files in `Assets/Audio/` or `Assets/Sounds/`
- Assign new clips in TVController or CharacterHoverAudio components

### Modifying UI
- All UI elements are in the TV scene Canvas
- Adjust positions, colors, and sizes in the Inspector
- Modify animation settings in animator scripts

## 🔧 Technical Details

### Main Scripts

**TVController.cs**
- Manages TV power state
- Handles channel switching with transitions
- Controls audio playback
- Updates UI elements dynamically
- Manages character interaction states

**CharacterHoverAudio.cs**
- Detects mouse hover events
- Plays character-specific sounds
- Provides visual feedback (scale animation)
- Respects TV power state

**ButtonHoverEffect.cs**
- Animates buttons on hover
- Optional hover sound effects

**ToggleAnimator.cs**
- Animates toggle on click
- Provides audio feedback

### Requirements
- Unity 6000.0 or later
- TextMeshPro package (included)
- Unity UI package (included)

## 📝 Notes

- All scripts follow the project coding guidelines
- Self-explanatory variable and method names
- Public methods include comments
- Constants used instead of magic numbers
- Input System ready (com.unity.inputsystem package installed)

## 🎓 Learning Resources

This project demonstrates:
- UI system implementation in Unity
- Audio management with multiple AudioSources
- Event-driven programming with UI callbacks
- Coroutines for smooth transitions
- Component-based architecture
- Serializable data structures (ChannelData)

## 📜 License

This project is created for educational purposes.

---

**Version**: 1.0  
**Unity Version**: 6000.0  
**Last Updated**: 2024
