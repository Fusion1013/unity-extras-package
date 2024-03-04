# Unity Extras Package
## Overview
This package contains a lot of nice-to-have components, editor tools and data structures for use with unity.
## Components
### Attributes
#### Always Select
When clicking on a nested game object, a script with this tag will always be selected. Example:
```cs
[AlwaysSelect]
public class Foo : MonoBehaviour { }
```
#### Read Only
A serialized field with this attribute will be grayed out in the inspector and can not be edited. Example:
```cs
[ReadOnly] public float fooBar = 1f;
```
![bild](https://github.com/Fusion1013/unity-extras-package/assets/35802522/fdab01f7-d264-4c68-9750-05a9bf4dea26)
#### Searchable Enum
Adds a search function to the enum dropdown. Useful for enums with a lot of constants. Example:
```cs
[SearchableEnum] public KeyCode fooBar;
```
![bild](https://github.com/Fusion1013/unity-extras-package/assets/35802522/811db32f-d1c1-4b3d-83de-6ef5254812e6)
### Events
#### Game Event Objects
A game event object is a scriptable object that functions as an event. It can be subscribed/unsubscribed to and invoked. Does not carry data.
It displays what components are listening to it in the inspector, and allows for manual invocation from the inspector. Has a debug window that shows invocation history. Example:
```cs
public GameEventObject eventObject;

private void OnEnable() => eventObject += OnEventInvoked;
private void OnDisable() => eventObject -= OnEventInvoked;
private void InvokeEvent() => eventObject.Invoke("Invocation Description");
```
![bild](https://github.com/Fusion1013/unity-extras-package/assets/35802522/a28c6f5b-30ae-4618-bee0-9fe1c9f83ba6)
#### Game Event Listener
Listens to a game event object and invokes a UnityEvent.
### Hierarchy Headers
Displays a header in the hierarchy menu.

![bild](https://github.com/Fusion1013/unity-extras-package/assets/35802522/74489d43-5b48-4b8d-b7a1-200cdf9f95e8)

![bild](https://github.com/Fusion1013/unity-extras-package/assets/35802522/ea8d7b78-7fc5-4025-a7ba-2561573839ec)
### Buttons
Allows for adding buttons to methods that display in the inspector. Version 1:
```cs
public QuickButton helloWorldButton = new QuickButton("HelloWorld");
private void HelloWorld() => Debug.Log("Hello World");
```
Version 2:
```cs
[EditorButton] private void HelloWorld() => Debug.Log("Hello World");
```
![bild](https://github.com/Fusion1013/unity-extras-package/assets/35802522/8568d8aa-ad08-4fe5-a849-44cf80f23ff9)
### Scene Asset
Allows for referencing a scene through the inspector. Example:
```cs
public SceneAsset sceneAsset;
```
![bild](https://github.com/Fusion1013/unity-extras-package/assets/35802522/c1bc26bc-48d3-45e6-b64a-9537082a0dff)
### Scriptable Object Variables/References
#### Variables
Allows for storing a variable on a scriptable object. Has an event for when the value of the object changes. There are currently 8 supported variable types:
- Bool
- Float
- GameObject
- Int
- SceneAsset
- String
- Vector2
- Vector3
Example:
```cs
public StringVariable stringVariable;
private void SetValue(string newValue) => stringVariable.Value = newValue;
```
![bild](https://github.com/Fusion1013/unity-extras-package/assets/35802522/2e0f3c97-bf1f-4c2a-9c85-ceca2f3b1868)
#### References
Allows for switching between variables and constants in the inspector. Example:
```cs
public IntReference intReference;
```
![bild](https://github.com/Fusion1013/unity-extras-package/assets/35802522/91a8320e-1c89-4744-81fc-b848060f5333)
## Editor Extensions
### Matrix 4x4 Drawer
Property drawer for a 4x4 matrix in the inspector. Allows for editing the matrix directly in the inspector.

![bild](https://github.com/Fusion1013/unity-extras-package/assets/35802522/17d49662-3319-4cf2-9342-1b8f6f7851ac)
### Normals Visualizer
Visualize normals on a mesh.

![bild](https://github.com/Fusion1013/unity-extras-package/assets/35802522/16165006-a0ed-4c9b-8dcb-373f917e372a)

![bild](https://github.com/Fusion1013/unity-extras-package/assets/35802522/9dcf33a2-94d0-467e-83b5-8cddfaa7f883)
### Transform Editor
Adds functionality to the transform component.

![bild](https://github.com/Fusion1013/unity-extras-package/assets/35802522/d59fa2e2-0cdf-415e-948f-8f77947cc1e3)

**Snap:** Snap the gameobject along the cartesian coordinate axis.
