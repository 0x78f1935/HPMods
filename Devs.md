# How to compile

Assuming you have the mod sultion running in visual studio.

1. \<ProjectName\> Build Settings > `Debug` tab
2. `Start external program` -> Add `steam.exe`
3. `Command line arguments` -> Add `steam://rungameid/611790 --melonloader.debug`
4. Open the tab `Build` and change the `output path` to the `mod` folder: `C:\Steam\steamapps\common\House Party\Mods\`

You can now hit the `Start` button in order to start the game with your compiled bindary.

# Writing Mods

See [MelonLoader Wiki](https://melonwiki.xyz/#/modders/quickstart) or refer to the mods inside this repository as an example.

# More soon