# Coloreality
##### [Check Video](http://www.youtube.com/embed/nu9lzzVj8iY)
##### A project for enabling Leap Motion tracking indirectly, by transferring tracking data wirelessly from PC to target devices.
Also by adding a rear camera view aligned with hand tracking objects on the phone with Cardboard. It makes a kind of Augmented Reality and Virutal Reality combined experience. (Can be called as Mixed Reality experience, depending on your definition. Or by extending addtional capabilities.)

##### Set up:
Server application on PC, with Leap Motion device connected.
Client application on smartphone(iOS/Android).
Both PC and smartphone connect to the same network(e.g. same Wi-fi network).

Click [Start Server] on PC application.
On the phone application, input IP and port as displayed on PC. Then click [Connect].

##### What's the demo inside:
With a Coloreality PC Server application(use either **ColorealityServer_PC.zip** or **ColorealityServer_PC_Unity.zip** under **executables/**) and Unity Cardboard demo(need to build with **ColorealityUnityPlugin.unitypackage** on Unity) working together, you can see the demo with:
- Simple skeletal hand view made by cubes & spheres.
- Hand-following cubes with random colors and size scaled by hand gripping. (As tea pot in video.)
- A line connecting both hands. Only shows when two hands detected. (Similar to lightning effects in video. I cannot include third party assets.)

*Tap phone screen to switch demos.

# src - Coloreality.sln
  - ### Coloreality.csproj
    The main library provides all project functionalities. Including both server and client function classes.
Requires **LeapCSharp.NET4.0.dll** from Leap Motion SDK.
  - ### ColorealityUnity.csproj
    A very similar library extracted from **Coloreality.csproj** for Unity projects, without scripts under *LeapWrapper/Sender/*.
  - ### ColorealityServer.csproj
    A PC server using main library.
Requires **LeapCSharp.NET4.0.dll** and **LeapC.dll** from [Leap Motion SDK](https://developer.leapmotion.com/get-started/). You can find under *LeapSDK/lib/* of downloaded SDK folder.

# ColorealityUnityPlugin.unitypackage
 - #### Set up on Unity 5.6:
   To work on a Cardboard project, please use Unity 5.6 or later versions and enable Unity Cardboard support.
Go to "Build Settings..." under File menu => Player Settings... => on Other Settings, check Virtual Reality Supported => Click "[+]" and select "Cardboard".

 - #### Prefabs:
   **ColorealityLeapMain.prefab** - Main prefab to receive data, also includes network GUI function.
   Simulated Leap objects should be put under LeapObjects.

   **ColorealityCameraView.prefab** - Show camera image as a background.
   --- Please add a layer named Background(Or whatever you like), and apply this layer on ColorealityCameraView.prefab and its children. Culling Mask of Camera component in ColorealityCameraView should be set to this Background layer only.
   
   This is for avoiding mixing background image and foreground objects. You can achieve it in some other ways you like, too.
e.g. Putting ColorealityLeapMain and ColorealityCameraView objects far enough to each other.
Make sure your objects move inside proper space that will not bump into background image object.

# ColorealityUnityServer.unitypackage
For building server program with Unity.
*Since the referenced dlls and Leap functions is for Leap Motion Orion that currently only works on PC(Windows 7 64-bit or higher). This is not yet ready to build a Unity server on Mac.
 - #### Set up on Unity:
   The server application requires some Leap Motion dlls. Please download **UNITY CORE ASSETS** on https://developer.leapmotion.com/unity to get dlls under **LeapMotion/Core/Plugins/**.

   Import this unity package. There you can see **ColorealityServerGUI.scene**, a simple scene with only one script **ColorealityServerManager.cs** on a Camera object.
   
      *Recommended to set **Run in Background**. (Go to "Build Settings..." under File menu => Player Settings... => under Resolution and Presentation you can see the option.)