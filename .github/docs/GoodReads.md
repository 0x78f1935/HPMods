### [Back to the index](./../../Devs.md)

# If you experience issues with a mod, DO NOT CONTACT EEK ABOUT IT. They have nothing to do with it. Rather ask around in the community or ask the mods creator!

### Eek is not responsible for compatibility of mods, mods (not) working or any other issue experienced when modding the game in any way.
-----------

You can also refer to the links here for help.

## Some links to interesting reads:
* [Quick start on installing MelonLoader](https://github.com/0x78f1935/HPMods#quickstart-melonloader)
* [A writeup of the basics of creating a mod. (With links to all tools needed/suggested)](https://github.com/0x78f1935/HPMods/blob/master/Devs.md)
* [The MelonLoader Wiki on creating mods](https://melonwiki.xyz/#/modders/quickstart?id=quick-start)
* [In-depth info on method patching using HarmonyX](https://harmony.pardeike.net/articles/patching.html)
* [The MelonLoader Wiki on the main differences with Il2Cpp games. (Applies to House Party!)](https://melonwiki.xyz/#/modders/il2cppdifferences?id=il2cpp-differences)
* [Il2CppAssemblyUnhollower on injecting custom classes, and their restrictions. (Needed for custom Components)](https://github.com/knah/Il2CppAssemblyUnhollower#class-injection) 
* [An example by me on creating injectable types](https://github.com/CamelCaseName/HPcscPlus/blob/main/Items.cs)
* [Official C# documentation on reflection, mostly applicable to the use in MelonMods](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/reflection)
* [Modding channel on Eek! Games discord](https://discord.com/channels/288848581161517066/966402990560927744)

If you want to get into creating your own mods, I suggest first reading through it all (especially the MelonLoader Wiki), and maybe following along by creating a simple mod as an exercise.

This mod could for example detect some game state, then print output to the console. Through all the documents linked in the ones I linked you should also be able to get a grasp of the restrictions imposed by il2cpp on custom stuff, but normal Unity engine shenanigans work quite well.

Feel free to [contact Lenny](https://discord.com/channels/288848581161517066/966402990560927744) if you have any serious questions. You'll probably find me in the [House Party discord server](https://discord.com/invite/eekgames).