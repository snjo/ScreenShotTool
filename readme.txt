Screenshot Tool
by Andreas Aakvik Gogstad
https://github.com/snjo/ScreenShotTool/

--------------------------------------------------------------------------------------

Capture Modes

Screenshots are performed using hotkeys defined in Options.

Default hotkeys:

Region				Printscreen
Window				Alt + PrintScreen
Screen				Ctrl + PrintScreen
All screens			No preset
Open last folder	No preset

--------------------------------------------------------------------------------------

Region capture

During region capture, the current screen the mouse cursor is on will display a region selection interface.

Click and drag the mouse to create a selection. This can be refined with the keys shown below.

To confirm the region, press Enter to save to file, or C to copy the selection to the clipboard. Press Escape to exit.

If you select the option "Complete capture when releasing mouse", the output is instead decided by the otions for saving to file or clipboard (Options > Modes: Region)

When adjusting the size of the region, arrows will indicate what edges are affected by arrow key presses.
When adjusting the position of the region, arrows are shown in all directions.


	Enter		Save image to file and exit Region capture
	C			Copy image to clipboard and exit Region captrue
	Esc			Exit region capture, discard selection
	S			Size adjustment mode (Default)
	P			Position adjustment mode
	Ctrl+Arrow 	Key	Size adjustemt side selection
	Arrow Key	Size / Position adjustement
	Shift		Hold Shift to adjust region by 10 pixels instead of 1

Region framerate

The specified framerate in Options sets the max amount of times per second the region UI will update.
If this is too high, the system can't catch up, and lag may be worse than if you set a lower framerate.
A good system should be able to handle 60fps. Default is 30fps.
(Options > Mode: Region)

--------------------------------------------------------------------------------------

Window capture

Captures the active window.
Saves to file or copies to clipboard based on options (Options > Modes: Window)

--------------------------------------------------------------------------------------

Screen capture

Captures the screen the mouse cursor is currently in.
Saves to file or copies to clipboard based on options (Options > Modes: Screen)

--------------------------------------------------------------------------------------

All Screens capture

Captures all screen in a single image.
Saves to file or copies to clipboard based on options (Options > Modes: All Screens)

--------------------------------------------------------------------------------------

Capture output

File name variables

When saving to file, the name of the active application, time or incrementing numbers can be included in the file or folder name.
The default file name is "$w $d $t $c", which will output something like "MyWindow 2023-12-31 16:02 003"

	$w	Active Window Title
	$d	Date
	$t	Time
	$ms	Milliseconds
	$c	Counter number (auto increments)

Title tweaks

The title of the active window when using $w can be adjusted to make file names nicer.

Window title max length

Limits the name length to prevent long file names.

Split Window title using string

If a window title is dynamically altered by the file it's viewing, extract just the part you're interested in.

Which of the split name element to use is set by the option "Keep split text in index", where 0 is the first element.

	Example: "ImageEditor - mypicture.png - v12.2"

	Split with " - "

	Keep split text in index: 0
	Result: $w outputs "ImageEditor"

	Keep split text in index: 1
	Result: $w outputs "mypicture.png"

Crop Window capture

If on, this option crops the captured window from top, left, right and bottom.
This can be used to remove the window title bar and edges. In the Windows 11 default theme, values of 32,10,10,10 will remove these.

--------------------------------------------------------------------------------------

Application options

Start Hidden

Start the application without appearing on screen or on the task bar. Interact with it via the System Tray icon or hotkeys.
You can add the application to start automatically by placing a shortcut in the Startup folder: *%appdata%Menu*

System tray tooltips

System tray tooltip options will display a notification in the lowe right of the screen when capturing, creating folders, or there's a problem.

Thumbnails

Specify the size of the thumbnails used in the application. This does not affect the saved files.
"Crop thumbnails" will make the thumbnails retain their aspect ration, but parts of the image are cut in the capture list thumbnail.
