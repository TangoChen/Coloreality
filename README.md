# Coloreality
##### [Check Video](http://www.youtube.com/embed/nu9lzzVj8iY)
##### A project for enabling Leap Motion tracking on target devices by transferring tracking data from PC.
Also by adding a rear camera view aligned with hand tracking objects on the phone with Cardboard. It makes a kind of Augmented Reality and Virutal Reality combined experience. (Can be called as Mixed Reality experience, depending on your definition. Or by extending addtional capabilities.)

##### Set up demo:
- Install Client application on mobile phone(iOS/Android) with **UnityColorealityClient** project. 
- Run **ColorealityServer.exe**(unzip from /executables/ColorealityServer_PC.zip) on PC, with Leap Motion device connected.
- Connect both PC and phone to the same Wi-fi network.
- Click **Start Server** on PC application.
- On the phone app, input IP and port as displayed on PC. Then tap **Connect**.

##### Demo includes:
- Simple skeletal hand view made by cubes & spheres.
- Hand-following cubes with random colors and size scaled by hand gripping. (As tea pot in video.)
- A line connecting both hands. Only shows when two hands detected. (Similar to lightning effects in video. I cannot include third party assets.)

*Tap screen to switch demos.

# src - Coloreality.sln
  - ### Coloreality.csproj
    The Colorealty.dll project.
  - ### ColorealityServer.csproj
    A PC server using main library.
Requires **LeapCSharp.NET4.0.dll** and **LeapC.dll** from [Leap Motion SDK](https://developer.leapmotion.com/get-started/) included as dependencies. You can find under *LeapSDK/lib/* of downloaded SDK folder.

# UnityColorealityClient
 - #### Set up on Unity 5.6+:
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