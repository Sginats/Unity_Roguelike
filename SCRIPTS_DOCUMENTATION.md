# Scripts Documentation

This document provides detailed information about the C# scripts used in the Unity Mr. Bean project.

## TV Functionality Scripts

### TVController.cs
Main controller for TV functionality including power, channel switching, and volume control.

**Public Properties:**
- `GameObject tvScreen` - The TV screen GameObject to show/hide
- `GameObject[] channels` - Array of channel GameObjects
- `AudioSource tvAudioSource` - Audio source for TV volume control
- `Slider volumeSlider` - UI Slider for volume control
- `Toggle powerToggle` - Toggle for TV power
- `Toggle[] channelToggles` - Array of toggles for channel selection

**Public Methods:**
- `TogglePower(bool value)` - Turn TV on/off
- `SwitchChannel(int channelIndex)` - Switch to specified channel (0-based index)
- `ChangeVolume(float value)` - Change volume (0.0 - 1.0)

**Usage:**
1. Attach to a GameObject in your scene
2. Assign TV screen and channel GameObjects
3. Assign audio source and UI elements
4. Connect Toggle components to `TogglePower` method
5. Connect channel toggles to `SwitchChannel` method

### CharacterHoverAudio.cs
Plays audio and provides visual feedback when hovering over characters.

**Public Properties:**
- `AudioSource audioSource` - Audio source for playing sounds
- `AudioClip hoverSound` - Sound to play on hover
- `bool scaleOnHover` - Enable/disable scale animation on hover
- `float hoverScale` - Scale multiplier on hover (default: 1.1)

**Usage:**
1. Attach to any UI GameObject (Image, Button, etc.)
2. Assign audio source and hover sound
3. Configure visual feedback options
4. GameObject will automatically play sound on mouse hover

## UI Scripts

### UIAnimator.cs
Provides simple animations for UI elements including pulse, rotation, and fade effects.

**Public Properties:**
- `bool pulse` - Enable pulse animation
- `bool rotate` - Enable rotation animation
- `bool fadeInOut` - Enable fade in/out animation
- `float pulseMinScale` - Minimum scale for pulse (0.8-1.5)
- `float pulseMaxScale` - Maximum scale for pulse (0.8-1.5)
- `float pulseSpeed` - Speed of pulse animation
- `float rotationSpeed` - Speed of rotation
- `float minAlpha` - Minimum alpha for fade (0.3-1.0)
- `float fadeSpeed` - Speed of fade animation

**Usage:**
1. Attach to any UI GameObject
2. Enable desired animation types
3. Adjust speed and range parameters
4. Animation runs automatically

### DropdownHandler.cs
Handles dropdown menu functionality and option selection.

**Public Properties:**
- `TMP_Dropdown dropdown` - Reference to dropdown component
- `GameObject feedbackText` - Optional text to show selected option
- `string[] optionLabels` - Array of options to populate dropdown

**Public Methods:**
- `SetDropdownValue(int index)` - Programmatically set dropdown value

**Usage:**
1. Attach to GameObject with TMP_Dropdown component
2. Populate optionLabels array in inspector
3. Optionally assign feedback text GameObject
4. Dropdown automatically populates on Start

## Existing Scripts

### ToggleScript.cs
Handles character visibility toggles and transformations.

**Key Methods:**
- `ToggleBean(bool value)` - Show/hide Bean character
- `ToggleFlip(int x)` - Flip character horizontally
- `ToggleTeddy/Car/Granny(bool value)` - Show/hide other characters
- `ChangeCharacterImage(int index)` - Change displayed character sprite
- `ChangeRotation()` - Rotate character based on slider
- `ChangeSize()` - Scale character based on slider

### NameScript.cs
Handles text input and display with random greetings.

**Key Methods:**
- `GetText()` - Get input field text and display random greeting
- `ReverseText()` - Reverse displayed text

### SFX_Script.cs
Manages sound effect playback.

**Key Methods:**
- `PlaySFX(int ix)` - Play sound effect by index

### DragScript.cs
Implements drag and drop functionality for UI elements.

**Interface Implementations:**
- `IPointerDownHandler` - Handle pointer down events
- `IBeginDragHandler` - Handle drag start
- `IDragHandler` - Handle ongoing drag
- `IEndDragHandler` - Handle drag end

### JiggleScript.cs
Provides jiggle animations for position, rotation, and scale.

**Key Features:**
- Position jiggling with configurable amount and frequency
- Rotation jiggling with 3D rotation support
- Scale jiggling with size variation
- Independent control of each animation type

### SceneChangerScript.cs
Handles scene transitions and application quit.

**Key Methods:**
- `LoadWithDelay(string sceneName)` - Load scene with 1.5s delay
- `QuitApplication()` - Quit the application

## Implementation Examples

### TV Setup Example
```csharp
// In Unity Editor:
// 1. Create empty GameObject "TVManager"
// 2. Add TVController component
// 3. Create TV Screen GameObject
// 4. Create Channel1 and Channel2 GameObjects
// 5. Add 3 character images to each channel
// 6. Add CharacterHoverAudio to each character image
// 7. Create UI Toggles for power and channels
// 8. Create UI Slider for volume
// 9. Assign all references in TVController inspector
```

### Character Hover Setup Example
```csharp
// In Unity Editor:
// 1. Select character Image GameObject
// 2. Add CharacterHoverAudio component
// 3. Assign audio source (or leave empty to find automatically)
// 4. Assign hover sound clip
// 5. Enable scaleOnHover for visual feedback
// 6. Adjust hoverScale if needed
```

## Notes

- All scripts use Unity's event system for UI interactions
- Scripts are designed to work together but can be used independently
- Error checking and null reference handling included
- Debug.Log statements included for testing and debugging
- Compatible with Unity 2022.3+
