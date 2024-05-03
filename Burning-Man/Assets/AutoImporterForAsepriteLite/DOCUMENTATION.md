# Auto Importer for Aseprite

## This tool allows you to have a seamless workflow when using the pixel art editor [Aseprite](https://www.aseprite.org/) and Unity

Note: This tool requires that you have Aseprite installed. It does not provide an Aseprite version and is not affiliated with the Aseprite team. Altough you can easily share the project with Unity Collab with the exported png files, so that other people on your team don't need Aseprite installed (see bellow for how to turn on that option).

Video preview: https://youtu.be/tpkwpB3mWbE

## Main features

- (LITE and PRO) Automatic import of Aseprite files (.ase and .aseprite). This is done using a custom scripted importer. That means that unity will reimport the images whenever a change is made to the original Aseprite asset
  This allows for a smooth workflow, where you can even make a change in Aseprite, save it, and have it immediately reflected in Unity, even during play mode.

- (LITE and PRO) Ability to export the images to regular PNG sprites, so that other people on your team can view them too.

- (PRO) Automatic import of animations. The animation tags you have in Aseprite are automatically translated to an .anim animtions in Unity.
  The direction and duration of frames is perserved in Unity, but can be overriden too by using a custom direction or a custom animation curve.

- (LITE and PRO) Customizable settings - control a number of settings that manage the way the sprites and animations are imported, all in the editor of the Aseprite file. For example:
  - Option to split images into layers (not working with animations currently)
  - Customize output sprite naming
  - Customize aseprite export options, such as dithering options and color mode
  - Sprite options, such as: pixels per unit, pivot point, extrusion, mesh type and border
  - Texture options, such as: wrap mode, filter mode, and transparency
  - (PRO) Animation options, such as: relative path to game object, type of component - Sprite Renderer or Image, direction of animation, loop, custom curves

## How to use?

### 1. Locate your Aseprite executable:
   
   The path depends on how and where you installed Aseprite, but the most common paths are:
   
  Windows:
   
    C:\Program Files (x86)\Aseprite\Aseprite.exe
    or
    C:\Program Files\Aseprite\Aseprite.exe

  MacOS:
    
    /Users/<your username>/Library/Application Support/Steam/steamapps/common/Aseprite/Aseprite.app/Contents/MacOS/aseprite
    or
    /Applications/Aseprite.app/Contents/MacOS/aseprite
  If that doesn't work type `which aseprite` in the terminal and it should display the path

  Linux:

You probably have an idea of where it is, but type `which aseprite` in the terminal to be sure

  Steam:
  
If you've installed from Steam - see this https://community.aseprite.org/t/find-aseprite-executable-when-aseprite-is-installed-from-steam/1268

### 2. In Unity, open the settings menu (Window -> Aseprite Auto Importer Settings).
### 3. Insert the path to your executable in the field and close the window

That's it! Your .ase and .aseprite files that are in the project will automatically get imported as sprites and animations (PRO version)
You can customize them using the inspector of the aseprite assets. To save some settings as the deafault ones you can go to the little settings icon on the top right, then click
on the "Save current to..." button and save your preset somewhere. Then go to the preset and click on the "Add to AsepriteAutoImporter default".

## If you need to use Collab or want to share the project to other people

Turn on the "Export to file" option under Colab options. This will generate actual PNG assets that you can work with.

## Settings

- Scale - controls the export scale
- Split Layers - enable this to export layers individually
- File name format - this controls how the created sprites are named. You can use different tags in the name that will be replaced with relevant info. Available tags are:
  - {fullname}: Original sprite full filename (path + file + extension).
  - {path}: Path of the filename. E.g. If the sprite filename is C:\game-assets\file.ase this will be C:\game-assets.
  - {name}: Name (including extension) of the filename. E.g. If the sprite filename is C:\game-assets\file.ase this will be file.ase.
  - {title}: Name without extension of the filename. E.g. If the sprite filename is C:\game-assets\file.ase this will be file.
  - {extension}: Extension of the filename. E.g. If the sprite filename is C:\game-assets\file.ase this will be ase.
  - {layer}: Current layer name.
  - {tag}: Current tag name.
  - {innertag}: Smallest/inner current tag name.
  - {outertag}: Largest/outer current tag name.
  - {frame}: Current frame (starting from 0). You can use {frame1} to start from 1, or other formats like {frame000}, or {frame001}, etc.
  - {tagframe}: The current frame in the current tag. It's 0 for the first frame of the tag, and so on. Same as {frame}, it accepts variants like {tagframe000}.
- Dithering algorithm type, Dithering matrix type and Color mode - controls the aseprite export settings. See aseprite documentation for more info
- Pixels per unit, Pivot point, Extrusion, Mesh Type, Border, Texture wrap mode, Filter mode, Alpha is transparency - standard Unity sprite and texture settings
- Animation options (PRO)
  - Relative path - Path to the game object that contains the Sprite/Image component.
    The relativePath is formatted similar to a pathname, e.g. "root/spine/leftArm".
    If relativePath is empty it refers to the game object the animation clip is attached to.
  - Target component type - Weather the target component is an Image or a Sprite Renderer
  - Direction - Set this to true in order to use the wrap mode and reverse direction from these options. Otherwise the settings will be implied from the animation settings in the ase file
  - Loop time - whether the animation will loop (default is to not loop)
  - Custom Curve - use a custom curve for the timing of your frames. If not set, the timing will be taken from the aseprite frame duration settings
