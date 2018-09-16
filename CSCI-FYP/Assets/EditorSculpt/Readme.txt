EditorSculpt

ReadMe Text

h.tomioka

<About this asset>
EditorSculpt is a sculpt modeling tool that works inside Unity editor.
You can deform mesh with 3D brush such as "Move", "Draw", "Inflat", "Pinch", "Smooth", "Flatten" or other.

<Features>
- Sculpt with Autoremesh / Dynamic tessellation(automatically increase/decrease polygons in accordance with polygon size, and automatically modifing mesh topology while sculpting -AutoRemesh Sculpt only).

-Sculpt with real-time previewing Unity's material/shader.

- Vertex Color Paint.

-Texture Paint(New in ver1.1)

- Symmetry Sculpting.

-Export sculpted mesh as .obj fileformat.

<How to start>
1)Import the EditorSculpt package into the project to install.

2)Now "Tools/EditorSculpt" menu item is added in Unity editor "Tools" menu.

3)Select the mesh you want to sculpt, and select "Tools/EditorSculpt>Standard Sculpt" or "Tools/EditorSculpt>AutoRemesh Scult" menu in Unity editor menu.

 (Important!: Select "AutoRemesh Sculpt" will destroy mesh UVs.
 So, if you want to sculpt textured mesh, you may not select"AutoRemesh Sculpt" but "Standard Sculpt".)

4)Then the "EditorSculpt" window will appear, and in the window's popup menu to select brush you want to use.
Now you're ready to start sculpt.

5)If  you open the "EditorSculpt" window with no mesh is selected,"Sculpt Plane" and "Sculpt Sphere" and "Sculpt Cube"buttons are appears.
Pressing these buttons, you can create primitive mesh to sculpt.

<How to use it>
Sculpt:
In the "EditorSculpt" windw, you can use "BrushType" popup menu to select brush.
Also "BrushRadius" and "Brush Strength" field in the "EditorSculpt" window to control brush size and strength.
"DisplayMode"popup menu to select display mode.
you can select vertex color and vertex weight in addition to normal display mode.
"Symmetry" popoup to define symmetry axis.

Edit Mesh:
Expand "Edit Mesh" foldout in the "EditorSculpt" window to enable many kinds of "Edit Mesh"buttons.
You can edit/refine/deform mesh with pressing these buttons.

Save Mesh:
Expand "Save/Export" foldout in the "EditorSculpt" window to enable "Save" and "Export OBJ" buttons.
"Save" button to save sculpted mesh as Unity asset. and "Export" button to export ".obj"  file format.
".obj" file incldes vertex color information as default setting.

Advanced Options:
Expand "Show Advanced Options" foldout in the "EditorSculpt" window to eanable Advanced options to configuring detailed options.

<What is a Difference bitween "AutoRemesh Sculpt" and "Standart Sculpt">
"AutoRemesh Sculpt" automatically increase/decrease polygons in accordance with polygon size, and automatically modifing mesh topology while sculpting.
So you can sculpt meshes freely without editing polygon construction.
This feature is known as Autoremesh or Dynamic tessellation.
"Standard Sculpt" doesn't have that feature but it preserve mesh UVs insted.
(Important!: If you sculpt textured mesh, you should not use "AutoRemesh Sculpt" because it destroy mesh UVs.
In this case please use "Standard Sculpt" insted.
If you get troubled with "AutoRemesh Sculpt"'s these behaviour, 
You can fix that with "Undo" operation in the Unity menu ("Edit/Undo") .)

<Keyboad ShortCuts>
Shift - Smooth
Alt - Inverse brush behavour
Ctrl - Draw Mask
Ctrl+Alt - Erase Mask
Ctrl + Shift - Smooth Mask
Ctrl+Z - Undo Sculpt
Ctrl+Y - Redo Sculpt
