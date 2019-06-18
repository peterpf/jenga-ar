# JengaAR

JengaAR is an android app to play jenga in augmented reality.
Following requirements must be met in order to play and develop the app:


## Requirements

### Software

- Windows
- UnityHub
    - Unity3D
    - Vurofia module
    - Android build-support module
- Vuforia license key
- Android studio (for developing)


### Hardware

- Android version 5 or above
- Fairly good camera
- Built-in accelerometer/gyroscope
- Multi-touch support (optionally)


## Setup

After cloning the repository, downloading UnityHub and installing Unity3D alongside the required modules, open the project with Unity3D.


### Vuforia

In Unity3D, set the vuforia license key by going to *Window* → *Vuforia Configuration* → *Global* → *App License Key*.


### Build Settings

In order to get the android app running on your phone, there are two ways to configure the build settings:

Firstly, click on *File* → *Build Settings* and a popup appears. Select **Android** as platform. Then we go:


### The Gamer's Way

Just hit the **Build** button and an `.apk` file will be generated. Copy it to your phone and install it.


### The Developer's Way

Select the checkbocks *Export Project*, this will generate a folder which can be opened by *Android Studio*. This will give you the power to see system logs and debug the app.


## Project Structure

As in most Unity3D projects, the *Assets* folder is of greater interest and therefore we will go into further details about its file structure:

```
Assets
├── Editor
├── Materials
|   └── Textures
├── Models
├── Prefabs
|   └── Parts
├── Resources
├── Scripts
├── StreamingAssets
├── Targeting Base
└── Vuforia
```

The important folders are:

- **Models** Containg the 3D model of a single jenga block
- **Prefabs** Containing predefined game objects like a tower layer
- **Scripts** Here plays the action
- **Vuforia** Modifying vuforia events can be done here

Details about the scripts folder can be found in the section below.

### Scripts

### TowerController

This script describes the behaviour of the tower and handles input events.

Basically, on every `Update` we check for input events from the **phone** or the **computer** -- *touch* or *mouseClicked* respectively.
If such an event occurs, we perform ray tracing to find an object that is 'hit' by the input event. In case that **block** object gets clicked, its color is changed and a flag is set to update its physical behaviour on `FixedUpdate`s.

Once a block is selected it can be manipulated by moving the phone around - stimulating its accelerometer. The readings of the acceloremeter sensor are amplified via the globally defined `thrust` variable. It's value should be set to around `400f` to achieve a good physical behaviour of the blocks.

If the user clicks a second time on the object, the block will be deselected.


### CameraFocusController

The script's sole purpose is to enable autofocus for cameras in order to get a sharper picture of the target.


### DefaultTrackableEventHandlers

There have been slights modifications to the script `DefaultTrackableEventHandlers.sc` located in `Vuforia/Scripts/`.

In order to prevent unwanted behaviour, gravity is disabled once the image target is lost; and enabled again once it's found.

This is done by adjusting the code in both event handlers:

- `OnTrackingFound`,
- `OnTrackingLost`.
