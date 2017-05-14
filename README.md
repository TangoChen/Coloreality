# Coloreality
##### [Check Video](http://www.youtube.com/embed/nu9lzzVj8iY)
##### A project for enabling Leap Motion tracking indirectly, by transferring tracking data wirelessly from PC to target devices.
Also by adding a rear camera view aligned with hand tracking objects on the phone with Cardboard. It makes a kind of Augmented Reality and Virutal Reality combined experience. (Can be called as Mixed Reality experience, depending on your definition. Or by extending addtional capabilities.)

##### What's the demo inside:
With Coloreality PC Server(built on ColorealityServer project) and Unity package demo working together:
- Simple skeletal hand view made by cubes & spheres.
- Hand-following cubes with random colors and size scaled by hand gripping. (As tea pot in video.)
- A line connecting both hands. Only shows when two hands detected. (Similar to lightning effects in video. I cannot include third party assets.)
- It can connect with multiple devices.

##### What could be wrong and buggy for now:
- Auto disconnecting. (Sometimes due to high sending rates.)
- Tracker bars for adjusting Leap object position/scale is not sensitive and does not change value sometimes.

##### What could be more in future:
- Have sending-interval configurations control on PC server. Now you can adjust DEFAULT_SEND_INTERVAL in Globals.cs for all connections.
- Not limited on only Leap Motion accessing, but also some more options. Such as Kinect...
- Separated parameter settings for each connection.

# src - Coloreality.sln
  - ### Coloreality
    The main library provides all project functionalities. Including both server and client function classes.
Requires **LeapCSharp.NET4.0.dll** from Leap Motion SDK.
  - ### ColorealityUnity
    The smaller library extracted from Coloreality above for Unity projects, without server function classes and other unneeded parts.
  - ### ColorealityServer
    A PC server using main library.
Requires **LeapCSharp.NET4.0.dll** and **LeapC.dll** from Leap Motion SDK.

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