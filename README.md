# TermSpeak
TermSpeak is a mod that allows you to speak on your walkie-talkie while in the terminal.

If you have a walkie-talkie powered on in your inventory, it will automatically be enabled while you are in the terminal.

This mod is client-side only, you can use it on servers without the host having it installed.

## Installation
The mod is a standard BepInEx plugin, so just drop the `.dll` it in your `BepInEx/plugins` folder.

## Configuration
The configuration file is located in `BepInEx/config/me.kdcf.termspeak.cfg`.
All keys are described in the file.

## Building
To build the mod, make sure you copy/symlink the following files from the game directory into the `TermSpeak/lib` folder:
- `Assembly-CSharp.dll`
- `Unity.Netcode.Runtime.dll`

Then run `dotnet build` in the `TermSpeak` folder. The output will be in `TermSpeak/bin/Debug/net5.0/TermSpeak.dll`. 
You can discard the other files in the folder.