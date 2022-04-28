### [Back to the index](./../../Devs.md)

# Using custom classes in the Il2Cpp domain (and their restrictions)

Melonloader uses [Il2CppAssemblyUnhollwer](https://github.com/knah/Il2CppAssemblyUnhollower) to inject classes into the Il2Cpp managed domain. 

## What is the "managed domain", and what is a "memory leak"?
---
You'll read "Il2Cpp managed domain" or the "Il2Cpp domain" many times in here, so let me give it a quick explanation. As you should know, C# has a garbage collector, also called GC. This means it has a region in memory where the language itself manages the space in memory, how much is used and by what. 

For example, if you create a variable in a method, then the method is done executing, the variable can be removed from memory again to make space for new ones. If you do not remove the unused variable form memory, this is called leaking, which is a bad thing. When your program has a memory leak it will constantly eat up more and more memory, which will lead to some weird crashes. 

Now back to Il2Cpp; it has its own GC. But it does not care if you use your injected classes in the C# domain, aka the region where the garbage collector is active. If you create an ibject from an injected class, you will have to create a reference/link inside the Il2Cpp domain to keep that object alive, but also create a method for this link to be removed so you dont leak memory.

## Example
---
The Il2CppAssemblyUnhollower, or in short Unhollower, has some special syntax for class injection. With Melonloader you can somewhat forego this syntax, but knowing about it is not a bad thing.

Lets take a look at an example, and I'll explain what each part does:
```cs
//using directives, we need the unhollower as well as the Il2CppSystem
using Il2CppSystem;
using Il2CppSystem.Collections.Generic;
using UnhollowerRuntimeLib;

//Example class, injectable into the Il2Cpp domain
public class Example : Object
{
    //A list to keep a strong reference to our own object inside the Il2Cpp domain.
    //This means that neither the object nor the list will be collected by the GC
    //as long as this object is added to its own list in the constructor.
    private List<Object> instance = new List<Object>();

    //Constructor for us to create new objects of the "Example" type.
    //we extend the base constructor, and pass a reference to the location of our 
    //constructor, so this one here will be called when an object is instantiated.
    //Basically we overload the IntPtr contstructor of the Object with a pointer to 
    //our own constructor for the "Example" class.
    public Example() : base(ClassInjector.DerivedConstructorPointer<Example>())
    {
        //This here points the constructor on the Il2Cpp side to the code which
        //actually creats our object, so the method body of the constructor.
        ClassInjector.DerivedConstructorBody(this);

        //add ourselves to a list inside the class to create a strong reference to this
        //object, so the Il2Cpp GC won't collect us. All references from Mono/C# to
        //Il2Cpp are weak by design, so we need this here as a workaround to keep us
        //from getting removed from memory. This is in essence a memory leak by design
        //but we mitigate it with the next method.
        instance.Add(this);
    }

    //A method to clear the List of Objects, as well as remove our reference to it, 
    //so that: 
    //  a) the reference from the list to this object here
    //and 
    //  b) the reference from us to the list 
    //are both removed, which enables the Il2Cpp GC to collect the objects. 
    //This should be called on all objects of this type when we exit the program, 
    //or don't need them anymore. This also means that we MUST NOT use custom classes
    //in a "foreach" loop, because the foreach loop will create a new object, but it 
    //cannot remove it anymore. These are some of the caveats to look out for. 
    public void Unload()
    {
        //Clear the list, aka remove the reference to out object
        instance.Clear();
        //Remove the reference to the list which contained the reference to our selves. 
        instance = null;
    }

    //This constructor is needed for Unity to instantiate this object, 
    //so only useful for inheriting from MonoBehaviours or GameObjects 
    public Example(System.IntPtr value) : base(value)
    {
        //same as in the other constructor, add a strong rerefence to this object 
        //so it does not get collected
        instance.Add(this);
    }
}
```
Before using this class you have to call `ClassInjector.RegisterTypeInIl2Cpp<Example>();`, best done at Application start.
```cs
//is called by MelonLoader on start of the mod
public override void OnApplicationStart()
{
    //registers the class constructor in the Il2Cpp class pointer store,
    //which means we are then able to call Example ex = new Example(); 
    //and actually get an object back.
    ClassInjector.RegisterTypeInIl2Cpp<Example>();
}
```
MelonLoader introduced an Attribute in some recent version to make this whole injecting process a bit easier. Just add `[RegisterTypeInIl2Cpp]` above the class definition, and it will be automatically registered. Though the Constructor part has to be the same still. Refer to the section in the [MelonLoader Wiki](https://melonwiki.xyz/#/modders/attributes?id=registertypeinil2cpp) for further information.

## Limitations!
---
Types/classes injected into Il2Cpp have many limitations, but knah is working on making them more and more funtional. See the [Unhollower Readme](https://github.com/knah/Il2CppAssemblyUnhollower#class-injection) for current information on what is possible, and what is not.

Currently, as of writing this here, only instance methods are available. 
* No static methods
* No static classes
* No virtual methods
* No interfaces (in the usual sense) (`class Test : ITest`)
* No public fields (`public int myInt`)
* No public properties (`public int MyInt {get; set; }`)
* No public events
* Not all types are suported for method signatures, means you could run into issues there.
  
Starting with Unhollower version 0.4.16.0, injected types can kind of implement interfaces. Refer to [the section in the Unhollower readme](https://github.com/knah/Il2CppAssemblyUnhollower#implementing-interfaces-with-injected-types) for info on how this works.

And starting with 0.4.15.0, you can have custom classes in unity asset bundles.
See the [relevant section in the unhollower documentation](https://github.com/knah/Il2CppAssemblyUnhollower#injected-components-in-asset-bundles).
