# ZenFulcrum Browser BrowserAssets Tools

> by Lutonite

This tool was originally made for Welcome to the Game 2, but it should be working for any game which has the ZenFulcrum Embedded Browser and a browser_assets file.

This tool can be very useful if you want to mod the ingame HTML pages.

> **Make sure to make a backup of the game's browser_assets file before using any of the tools available here.**

## How to download

Go to the releases page and download both the unpacker and packer executables.

## How to unpack

Copy the two executables in a separate folder and put a copy of the browser_assets file from the game in the same folder.
The browser_assets file can usually be found in the game's `<Game>_Data > Resources > browser_assets`

Double click on the unpacker. Once over, you'll have a `sites` folder in the same directory.

## Modify the files

You can now edit the contents inside the folder, make sure to keep the same folder structure or you will probably have to change the game's script to load the pages differently.

## How to pack

Double click on the packer tool and it will start to pack the `sites` folder in `browser_assets` **replacing the previous content**. (Make a backup!!)

The packer will **only** pack everything in the `sites` folder.
