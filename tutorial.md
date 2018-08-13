# Tutorial, tips and etc #

Yes, https://docs.unity3d.com/Manual/index.html is a good resource. I'm not trying to regurgitate it here, and hope I can offer a more down-to-earth overview. 

## Concepts ##

### The Inspector vs Scripting ###

You can do a ton of stuff via the GUI inspector, but it can be fiddly to keep track of intentionality and so forth ("why is this variable set to ___ ?"). There are trade-offs to accomplishing certain things in the inspector vs. accomplishing them via scripting. Additionally, all custom update routines have to be implemented via scripts. 

### Scenes ###

.unity files (binary format -> beware the danger of overwriting someone's scene in version control systems)

Right now it probably doesn't matter much, but get in the practice of editing your own copy of a scene. (Clone by clicking it and pressing ctrl+d). 

Basically a scene keeps track of a (presumably tree) structure that contains all of the GameObjects that have been pre-determined (i.e. not allocated at run-time). It stores all of the public variable settings (set in inspector) for all of these objects (list viewable in Hierarchy window in inspector). This includes the positions of all of these objects (as every GameObject has a Transform component)

### GameObject(s) and the GameObject class ###

GameObject is the base class of every object that exists within a Scene. This is the case from the perspective of the engine's implementation because there are certain operations that are routinely called on every single GameObject, regardless of its specific function. 

Rather than using an interitance model, where subclasses of GameObject provide specialized functionality, Unity uses a component or *composition* system, in which each GameObject can have an arbitrary number of sub-components which contain data and functionality. 

#### Important functions / member variables in GameObject interface ####

- `GetComponent<Type>()`

	If a component does not exist, GetComponent returns null. 

- `SetActive(bool)` // sets the entire GameObject (and all of its components) to be active (enabled) or inactive (disabled). No updates will be processed by any attached script when a GameObject is disactivated. Such a state can be toggled in the inspector with a big checkmark thingy. 

- `gameObject.transform` // access the Transform struct of the GameObject, which has members position, rotation, and scale, which are all Vector3's

- Instantiate(Object object) 
	// usually called with a GameObject as parameter. Returns a clone of that object, and adds it to the Scene hierarchy at the top level by default. (There is an overload where you can specify a parent object to nest the new object under and existing one).

- Destroy(Object obj)
	// removes the object (usually called with a reference to a GameObject to destroy the *entire* thing and not just a component). Don't count on it to do anything after Destroy() is called (e.g. the destroyed object will not register as leaving a Collider).



### Built-in Components ###

#### Physics2D / Physics3D ####

### Scripts (Custom Components) ###

Any custom component must be a subclass of **Monobehaviour**. Classes not subclassed from Monobehaviour can be used for utility purposes, but can't be directly included as a component of a GameObject. 

Every Monobehaviour includes a reference to the GameObject it is attached to, which is referred to by the variable name 'gameObject'

Calling `GetComponent<Type>()` within a custom script is the same as calling the general purpose `GameObject.GetComponent<Type>()`, just as a shorthand for referring to the GameObject that the script is attached to. 

So you can say e.g. `CircleCollider2D circColl = GetComponent<CircleCollider2D>();`
instead of : `CircleCollider2D circColl = gameObject.GetComponent<CircleCollider2D>();`



#### Main lifecycle functions for MonoBehaviours:  ####

These functions are called on MonoBehaviours at specific times or intervals in their lifetimes.

- `Awake()` is called once in an object's lifetime. At the start of a scene, all `Awake()` functions will be called before any `Start()` function is called. Because of this, it is useful to get components and references to other objects during this function.


- `Start()` is mainly used to initialize variable settings for a component script. Since any custom component can access any other component of the same object, care should be taken that the initialization responsibilities are appropriately divided. `Start()` is called before any calls to update functions are made. 

- `Update()` An update function that is called every frame (i.e. as frequently as possible). Mainly used for game logic. 

- `FixedUpdate()` Normally used for physics updates. Called on a fixed time schedule.

- `LateUpdate()` An update function that is called *after* every object in the scene has had `Update()` called

#### Selection of callback functions for Monobehaviour ####

All of these function names begin with "On", and they are called when something or another has happened to the GameObject that this script is attached to.

- `OnEnable()` / `OnDisable()` // called when THIS PARTICULAR MONOBEHAVIOUR is enabled or disabled. Disabled scripts are not updated.

- `OnDestroy()`  // called just before an object is destroyed via the Destroy() function. 

- `OnCollisionEnter()` / `OnCollisionStay()` / `OnCollisionExit()`  // callbacks for 3D physics system





### Prefabs ###

#### What they are ####

A recipe which can be used to instantiate a GameObject which is pre-set with components, variables set, and even possibly nested GameObjects. 

Often, the *exact* settings of variables in the prefab are not sufficient for all of the possible use-cases for some object. Usually, the script used to instantiate a prefab will define some parameterized function that both instantiates a prefab AND sets its variables to some different value. 

#### How to create one ####

1.) Make a GameObject within some scene. (Go to Hierarchy window, click to create a new object, customize it as desired)
2.) Click and drag object from Hierarchy window to Assets window, placing it wherever. 
3.) now it's a prefab with a lil' cube symbol next to it, yay. 

#### How to edit one ####

Click the prefab name in the assets list, and then edit it as you would an instantiated object in the inspector. There are buttons you can click to apply changes made to the prefab to all objects that are copies of it. 

You can also update an object that's in the scene and then drag its name from the hierarchy onto the prefab you'd like to replace with new values. 

#### How to use one in the editor ####

Click and drag the prefab name from the Assets list either into the Hierarchy list (for some scene) or just drag it out into the world.

#### Nested Prefabs ####

When a GameObject-turned-prefab has child objects, these also become Prefabs, nested under their parent. 

#### Storing a prefab reference as a variable ####

If a custom script has a public GameObject variable, you can store a reference to a prefab in that variable. This variable can later be referenced in a script to `Instantiate()` a copy of the Prefab.


## Tips ##

## 2D and 3D ##

