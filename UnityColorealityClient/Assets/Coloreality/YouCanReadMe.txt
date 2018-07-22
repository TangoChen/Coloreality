
  /*
  *  Coloreality Unity Package v0.1.1.0
 *	Github: https://github.com/TangoChen/Coloreality
*/


Simple instructions:

-Set up on Unity 5.6+:

To work on Cardboard project, please use Unity 5.6 or later versions and enable Unity Cardboard support.
Go to "Build Settings..." under File menu => Player Settings... => on Other Settings, check Virtual Reality Supported => Click "+" and select "Cardboard".


-Prefabs:

ColorealityLeapMain.prefab - Main prefab to receive data, also includes network GUI function.
Simulated Leap objects should be put under LeapObjects.

ColorealityCameraView.prefab - Show camera image as a background.
Please add a layer named Background(Or whatever you like), and apply this layer on ColorealityCameraView.prefab and its children.
Culling Mask of Camera component in ColorealityCameraView should be set to this Background layer only.

This is for avoiding mixing background image and foreground objects. You can achieve it in some other ways you like, too.
e.g. Putting ColorealityLeapMain and ColorealityCameraView objects far enough to each other.
Make sure your objects move inside proper space that will not bump into background image object. 


Yet more to change & improve..


Jingzhou Chen

Blog:	 http://TangoChen.com
Twitter: @TangoChen
YouTube: http://youtube.com/Tan9oChen