# Unity-ARFoundation-echo3D-demo-Twitter-Visualizer
Twitter Demo for echo3D.  Type a hashtag search query - eg. #election2020 or #BlackLivesMatter - (with no spaces for now!) into search bar and hit search to spawn up to 15 local tweets containing the hashtag. 

## Register
Don't have an API key? Make sure to register for FREE at [echo3D](https://www.echo3D.co/).


## Setup
### Twitter
1. Create a [Twitter account](www.twitter.com) if you don't have one already. This will allow you to access the Twitter developer site
2. Head to the [Twitter developer site](apps.twitter.com) and create a new application.
3. Fill out all the details and once you complete it you will recieve an **API Key** and an **API Secret Key** which will be used in the Unity app.

### Unity
1. Clone [echo3D-Twitter-Demo](https://github.com/echo3Dco/Unity-ARFoundation-echo3D-demo-Twitter-Visualizer) sample code.
2. Add and Open repo in Unity Hub as Unity 3D Project
3. Download [echo3D Unity SDK](https://bit.ly/echo3DUnitySDKDownload) and import the package into the project
4. [Set the echo3D API key](https://docs.echo3D.co/unity/using-the-sdk) in the echo3D.cs script inside the ```Assets/echo3D/echo3D.prefab``` using the the Inspector.
5. In ```Assets/echo3D/CustomBehaviour.cs```, comment out line 27 
```
this.gameObject.AddComponent<RemoteTransformations>().entry = entry;
```
and replace with:
```c#
// Set Rotation to parent
this.gameObject.transform.rotation = this.gameObject.transform.parent.transform.rotation;
// Set Scale
this.gameObject.transform.localScale = new Vector3(0.001f, 0.001f, 0.001f);
```
6. Open ```Assets/Scenes/Main```.
7. Click on the ```TweetManager``` game object in the heirarchy and in the ```Inspector > TweetManager(Script)``` add your Twitter **API Key** and **API Secret Key**.
8. In the ```TweetManager``` game object, in the ```Inspector > TweetGenerator(Script)``` 

   * Add ```Assets/Prefabs/TweetObject.Prefab``` to the ```Tweet Prefab``` field.
   * Add ```Assets/echo3D/echo3D.prefab``` to the ```Object To Spawn``` field.
   * Add ```ARCamera``` from the hierarchy to the ```Spawn Point``` field.

## Build & Run
[Build and run the AR application.](https://docs.echo3D.co/unity/adding-ar-capabilities#4-build-and-run-the-ar-application) Verify that the ```Assets/Scenes/Main``` scene is ticked in the Scenes in Build list and click ```Build And Run```.

## Learn More
Refer to our [documentation](https://docs.echo3D.co/unity/) to learn more about how to use Unity and echo3D.

## Support
Feel free to reach out at [support@echo3D.co](support@echo3D.co) or join our [support channel on Slack](https://go.echo3D.co/join).

## Screenshots
![demo gif](Images/Twitter.gif)<br>
note: text under search bar will not appear in current build.

![demo Screenshot](Images/echoArGrab.png)
