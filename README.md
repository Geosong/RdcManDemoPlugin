# RdcManDemoPlugin

This project is a demonstration of the RdcManPluginFix library.

Be sure to install Microsoft's [Remote Desktop Connection Manager](https://www.microsoft.com/en-gb/download/details.aspx?id=44989) (RDCMan) and to do a `git submodule init` `git submodule update` to ensure this demo works.

The project is set up to run **RDCMan.exe** when you run it, this will let you place breakpoints in the plugin to see how it works.

## Events

### OnContextMenuEvent

Triggered by Right-Clicking any of the servers/groups in the left panel of the screen or Right-Clicking the background of the panel itself.

This demo adds another option to a servers context menu which is labelled _Server Details_ and it will display a message box containing the servers display name and actual name.

### OnDockServerEvent

Triggered by closing an undocked server window.

This demo produces a popup congratulating the user on docking a server.

### OnUndockServerEvent

Triggered when a user selects the **Undock** or **Undock and connect** options on a servers context menu.

This demo changes the colour of the menu bar of the newly undocked servers window to pink.

### PostLoadEvent

Triggered after all `PreLoadEvent` methods have been called and when all `.rdg` files have been loaded.

This demo adds a new group called **Test** with a new server called **localhost** to the left panel of the screen.

### PreLoadEvent

Triggered when the plugin is loaded by **RDCMan.exe**.

This demo loads the last time the settings were saved in a previous session and adds it to the title bar of the main window.

### SaveSettingsEvent

Triggered when the user presses **OK** on the **Options** window which is opened from the **Tools** menu or when the user closes the application.

This demo saves the timestamp of when the settings were last saved and uses that in the title bar of the window.

### ShutdownEvent

Triggered when the user exits the application.

This demo produces a message box which says _Goodbye!_ to the user.

## Additional Information

More information on how to create plugins for **RDCMan** can be found [here](https://github.com/Geosong/RdcManPluginFix).