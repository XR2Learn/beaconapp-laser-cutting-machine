INTERACT Change Log

## Version 21.12.00
### Added
- [PIXYZ] Support for Pixyz 2021.
- [VR] Laser Grab tool to be able to grab and pull physicalized objects from afar using your hands.

### Changed
- [EDITOR] "Add new player" menu to be more straightforward to use by merging HMD's to one item and with new icons.

### Fixed
- [VR] Missing references in the controller component of player prefabs.
- [VR] Measure tool not properly updating its length when pointing at an object.
- [VR] Null reference when trying to grab a root fixed joint with the desktop manipulator.
- [LICENSE] Fix an issue when trying to activate Interact with older licenses.
- [LIBRARY] Missing materials on UR10.
- [EDITOR] Null reference exceptions when trying to remove colliders from an object with no mesh.
- [EDITOR] Null reference when closing interact build panel.
- [EDITOR] OpenVR being loaded even when no HMD where in the scene.
- [EDITOR] Null reference when trying to open scenario graph in an invalid scene.
- [CAVE] Color mode not being serialized.
- [CAVE] Render texture not being serialized.

### Removed
- [EDITOR] Remove media importer.

## Version 21.08.00
### Added
- [SCENARIO] Completely revamped scenarization module.
- [SCENARIO] Add keypoint validation based on joint state.
- [SCENARIO] Add XdeAsbActionStep to trigger events between scenarios (activate, deactivate, reset).
- [CORE] Add compatibility with Unity 2020.3 LTS. Unity 2020.3 LTS is now the recommended version.
- [CORE] Add user preference to switch between imperial and metric units (Editor only).
- [VR] Grabbable property is now editable at runtime.
- [VR] Manus is now using bilateral manipulation behaviour.
- [VR] Add "Weld On Detach" option to XdeAsbOperatorGraspManipulator.
- [PHYSICS] All XdeManipulators now have Attached and Detached events.
- [PHYSICS] XdeRigidBody now has Attached and Detached events.

### Changed
- [CORE] Tune Pixyz default parameters. 
Import models with High tessellation by default, to provide better
results with "Pick Axis". Merge last level of hierarchy to reduce the number of gameobjects.

### Fixed
- [VR] Fix compatibility with single pass instanced.
- [CORE] Remove part on non-prefab item was broken.
- [LIBRARY] Fix conveyor window.
- [LIBRARY] Fix some robots joints orientation.
- [COLLAB] Fix connection issue with two desktop players.
- [PHYSICS] Beam: Correct gizmo for initial configuration.
- [PHYSICS] Fix unit joint's button behaviour when multi-selecting.
- [SCENARIO] Fix scale issue on animated ghost target.

## Version 21.05.03
### Fixed
- [ERGO] Fix regression in ergonomics calibration.
- [CORE] Fix "Apply Transform" throwing an exception on non-triangle mesh topologies.

## Version 21.05.02
### Added
- [CORE] Add ApplyTransform feature that convert a mesh with negative scale to unit scale.
This feature fixes a lot of issues related to dynamic batching, cable recognition, part target ... It is recommended that a GameObject have an unitary scale if possible.
Starting from Pixyz 2020.2.4.5, every objects are imported with negative scale. It is advised to use this ApplyTransform tool to set their transform to unit one.

### Changed
- [VR] Harmonize all prefabs' handtracker & graspmanipulators values.
- [LIBRARY] Convert all robots scale to unit scale.

### Fixed
- [VR] Add mesh collider & asb part on button for interaction with desktop manipulator.
- [CORE] Fix desktop manipulator not working with inversed normals.
- [CORE] Remove AsbPart when unphysicalize a GameObject.
- [CORE] Interact window in Unity Editor was constantly focused which leads to a performance issue.

## Version 21.05.01
### Added
- [LICENSE] User can now enter an hostname instead of IP for license server .

### Fixed
- [VR] Fix action triggered by controller while a tool was used in opposite controller.
- [POINTCLOUD] Fix missing dll issue.
- [CORE] Navigation mode was uneditable in Editor.

## Version 21.05.00
### Added
- [PHYSICS] Add a tool to compute initial configuration of a cable from CAD model.
- [EDITOR] Add a ruler tool in editor to measure distances.
- [EDITOR] Add a protactor tool in editor to measures angles.
- [VR] LeapMotion player is now using new manipulation behavior.
- [VR] Add a "None" navigation mode in order to lock navigation for a player. This is useful if you are using registration to manipulate real objects in your simulation.
- [LICENSE] For large deployment, you can now activate an Interact license from registry values.
- [LICENSE] User can now enter an hostname instead of IP for license server 

### Changed
- [CORE] Navigation mode (Teleport, fly, etc) is now a player parameter instead of global preference.

### Deleted
- [VR] Remove timer tool.

### Fixed
- [VR] Fix teleport when grasping an object.
- [POINTCLOUD] Fix missing dll issue.

## Version 21.01
### Added
- [CORE] Add importer for URDF files. Kinematics links, inertia and STL meshes are supported.
- [CORE] Add a tool to manipulate physicalized objects using desktop device.
- [CAVE] Start Optitrack server automatically according to CAVE configuration.
- [CORE] Hande multi-scenes when we build executables.
- [CORE] Handle multiselection for make grabbable.

### Changed
- [LICENSE] Remove Pixyz package from INTERACT. Pixyz is still highly recommended but not natively embedded anymore. Consider downloading it from Pixyz website.
- [CORE] Support latest Pixyz version 2020.2

### Fixed
- [VR] Fix wrong offset when using measure or teleport tool.
- [CAVE] Optitrack configuration was not serialized in VRX configuration file.
- [VR] Mode icons were not displayed.
- [PHYSICS] Interference detection with multiple submeshes was not reported correctly.
- [PHYSICS] Cable radius now taken into account.
- [COLLAB] Fix inputs that were duplicated for all users.
- [VR] Hands positions were jumping from rest pose to actual pose after teleport.
- [VR] Hands were not teleported at the right position when Manus was used.
- [VR] Fingers were sometimes in odd positions when closing the hand.
- [LICENSE] Old license system didn't deactivate tokens properly.
- [LICENSE] Trim license key to avoid activation error when copy-pasting.
- [CORE] Fix pointcloud shaders compilation with specific defines.
- [CORE] Only move desktop camera when we have focus.
- [CORE] Unphysicalize command did not remove UnitJointMonitor.
- [CORE] Avoid concatenation of "_Grouped" string multiple times.

## Version 20.09
### Added
- [VR] Improved manipulation: objects with different mass will behave accordingly when grabbed.
Bi-manual manipulation is now possible. Grap position has been improved, specially for constrained objects (door, robot, etc...) 
- [CORE] For desktop user, you can now use a camera that use the same commands as popular CAD software (CATIA, Solidworks, Sketchup)
- [CORE] You can group objects instead of merging them when preparing your physicalized scene.
- [PHYSICS] Add mapped joint. This is helpful to create a link between other joints using a custom relation.
- [POINTCLOUD] Add a gizmo to resize and scale when cropping pointcloud.
- [PHYSICS] Add shortcut - Shift+P - to physicalize objects.
- [UI] Add shortcut - Shift+M - to merge objects.

### Deleted
- [VR] GoTouch is no longer embedded in INTERACT package.
- [VR] 3DRudder is no longer embedded in INTERACT package.
- [VR] Removed the city environment.

### Changed
- [UI] Default manipulator is XYZTransform
- [UI] Default focus to OK button
- [CORE] You can now install Pixyz Plugin and Interact at the same time if you need advanced functions of Pixyz scene preparation.
- [CORE] Dramatically reduce the package size by removing uneeded assets and dependencies.
- [CORE] Documentation is now available online.
- [PHYSICS] Add manipulator no longer automatically physicalize object if a rigidbody is not selected.

### Fixed
- [CORE] Merge was not working reliably when several objects had the exact same name.
- [PHYSICS] Using pick axis with a prismatic joint was generating a new gameobject in the hierarchy.
- [UI] Fix NullReferenceException when using "Remove small colliders window" without selection.
- [UI] Fix NullReferenceException when using "Unphysicalize window" without selection.
- [LICENSE] Fix Pixyz button that was always disabled.
- [PHYSICS] Fix interferences detection with dilated lines.
- [VR] Update deprecated components for CAVE systems.


## Version 20.01.04f4
### Added
- [CORE] Unity 2019.4.X is now supported.
 
### Deleted
- [CORE] Disable embedded CTAA package. This should fix some performance issues with Oculus HMD.
 
### Fixed
- [CORE] License status fixed in license manager.
- [CORE] Improve support for HDRP (still in alpha, some features might not work in this rendering pipeline)
- [VR] Avatar hand manipulation fixed


## Version 20.01.04f3
### Added
- [UI] Add physicalize in contextual menu and add unphysicalize in interact menu.
- [License] You can now update your license.
- [License] Uninstall license in offline mode.
- [Physics] Add tooltip to explain our gravity vector convention.

### Deleted
- [CORE] Deleted embedded Post Processing package. This should fix compatibility with Unity Reflect and provides more flexibility to use the latest Post Processing package.
- [CORE] UseEmbeddedServer option was not working well. It is no longer available.
- [VR] Remove obsolete buttons on VR menu.

### Fixed
- [Core] Fix a critical issue that prevented building when using Unity 2019.3.9+ version. Previous versions were not affected.
- [Core] Merging of objects with specials chars was not working.
- [Core] Fix a crash sometimes happening on our reference demo. 
- [Library] Staubly TX2 robot was not working.
- [Physics] Fix right hand kinematics.
- [VR] Wrong orientation when using teleport with left hand. 
- [VR] Fix navigation in fly mode.
- [PointCloud] Crop gizmo was not rotated correctly.
- [Physics] Changes on Collision Matrix were not always saved.
- [Physics] Fix exception when using arrows with a large number of contact points.
- [Physics] User is no longer allowed to enter a negative value for timestep.
- [Ergo] Fix interaction with cotation panel.
- [Collab] First user was the only one able to interact in VR.


## Version 20.01.04

* [Misc] Add importer of XRTwin scenarios

## Version 2019.12

* [Core] New collaboration framework (XSM2)
* [Core] Ability to import media (images, video, pdf)
* [Core] Added new teleportation mode (fly-over)
* [Core] Ability to load and export CAVE configuration files
* [Core] Updated SteamVR 
* [Core] Updated XDE physics engine
* [VR] New user type : controllers with virtual hands
* [Graphics] Optimized rendering for better performance (HBAO, CTAA, triplanar shader)
* [Physics] New feature : joint axis detection
* [Physics] New feature : remove small colliders on an assembly (screws, bolts)
* [Physics] Add diagnostic tool to check scene conformity
* [UI] Create external camera
* [UI] Possibility to assign collision layers when physicalizing part
* [Misc] Add importer of XRTwin scenarios
* [Library] Add awesome materials in the library
* [Fix] Merge parts with line boundaries


## Version 2019.06

* [Graphics] Add Object and polygon counters (Scene stats)
* [UI] Customization of the hierarchy window
* [Core] Compatibility with Unity 2018.3
* [Core] Updated PiXYZ 2019.1
* [Core] Updated XDE 19.04
* [Core] Ability to import 2D drawings (layout)
* [Physics] Add unphysicalize command (right click)
* [Physics] Add keyboard type manipulator
* [Physics] Possibility to define custom attach points for cables
* [Scenario] Possibility to define actuators
* [PointCloud] Add convex decomposition (convert mesh to convex mesh)
* [PointCloud] We now import .ply format
* [Library] Add Sick lidar scanner
* [Ergo] Improve body tracking and add ergonomics cotation panel
* [Misc] Add bug reporter and automatic support tickets


## Version 2018.12

* [UI] Redesign Editor interfaces
* [Core] Add Assembly module allowing to build assembly scenario
* [Physics] Update XDE (18.11)
* [Physics] Add ball joint
* [Physics] Add material library for friction
* [Physics] Add cable configuration window
* [Physics] Add spring and joint effort
* [Physics] Add sound on collision (CollisionSound component)
* [Physics] Add Transform manipulator
* [PointCloud] Cutting plane, Measures and Teleport now compatible with pointclouds
* [PointCloud] Cut and Crop point cloud in Editor
* [PointCloud] Interference detection pointcloud/CAD
* [VR] Multi mode now possible (ex: cutting plane and screenshots)
* [VR] Paint brush tool
* [Ergo] Add support for ART Human
* [Robot] Ability to import/export native FANUC programs
* [Robot] Visual helpers (ghosts) for programming robots
* [Robot] Add Staubli TX2 90L
* [Misc] MultiPlayer : User selection pop up

## Version 2018.06

* Integrate Unity FBX Exporter
* Add tracking devices (ART, OptiTrack, SpaceMouse, ViveTrackers)
* Add Merge part options (by material and by name)
* Add UR3 and UR5 robots in library
* Add measure X,Y,Z
* Export notes in report when simulation stops
* Add ABB IRB 6700 to library


## Version 2018.03

* Update XDE (18.01)
* Update PiXYZ (1.5.8.29)
* Add support for Windows Mixed Reality Headsets
* Add compatibility with 3DRudder Motion Controller
* Add Factory and city environments
* Improve Point cloud importer (>5 billion)
* Add part grabber with controller
* Add directional teleporter
* Add assign material for multiple part
* Improve performance of selection effect

## Version 2018.01

* Add screenshot functionality
* Add stopwatch in toolbox
* Add loading screen while physics is initializing
* Now supporting GoTouchVR haptic fingers
* Improve virtual keyboard and notes
* Enable cutting plane with Standard Shader
* Bug Fix Point cloud visualizer

## Version 2017.11

* Update XDE 17.10
* Import large point clouds
* Add Measure feature
* Add Sawyer robot in Industrial Library
* Desktop Player : Display view of other player with num keys
* Better Leap/Vive colocalization
* Fix Stat Display stereo settings


## Version 2017.08

* Add Part Manipulator
* Add Part explosion
* Add Manipulator feature
* Add Culling Mask for head (avoid seeing the inside of the skull)
