# BluetoothBattery2

A small Windows utility that shows Bluetooth device battery level in the Windows notification area.

---

## Overview

- Shows the battery level of a selected Bluetooth device in the system tray (numeric only)
- Customizable font, icon offset and font size
- Bilingual UI: Chinese and English
- Periodically refreshes device battery information (interval in **seconds**)

---

## Requirements

- Operating system: Windows
- Dependencies: PowerShell (version 7 or newer is recommended)

---

## How it works

- Uses PowerShell commands (such as `Get-PnpDevice`) to read Bluetooth device battery information
- Draws a custom tray icon with the numeric battery percentage overlaid

---

## UI Overview

- **Device Name**  
  Select the Bluetooth device to monitor from the dropdown.

- **Refresh Interval (s)**  
  Refresh interval in **seconds**. The default value is `600` seconds (10 minutes); very small values are not recommended.

- **Font family**  
  Font used to draw the numeric battery value on the tray icon.

- **Minimize to tray when the close button is clicked**  
  When checked, clicking the close button hides the window to the tray instead of exiting.

- **Icon layout settings**  
  - `X offset`
  - `Y offset`
  - `Font size offset`

  Click the "Apply" button to preview current layout changes. In the dialog:
  - Choose **Yes** to write settings to config and rebuild the icon cache.
  - Choose **No** to preview only and restore previous offsets and font size.

- **Language**  
  Options:
  - `Chinese`
  - `English`

---

## How to Use

1. Click the **Reload** button and wait until the device label shows **Device Name** again.
2. Choose your Bluetooth device from the first dropdown.
3. Set the refresh interval in seconds (default `600`).
4. Choose the font family and adjust icon layout if needed.
5. Click **Save && Refresh** to apply settings.

The tray icon shows the current battery percentage (1–99).

- Hovering over the icon shows a tooltip containing:
  - Device name
  - Battery level
  - Time since last refresh
  - Refresh interval (seconds)
  - Font name
- Double-click the tray icon to show or hide the main window.

---

## Language Settings

- The selected language is stored in `settings.txt`.
- On next startup, the previous language is restored automatically.
- UI labels, message boxes, and tray tooltips all follow the current language.

---

## Notes

- This tool relies on PowerShell commands such as `Get-PnpDevice` to read Bluetooth device battery information, and only works on systems that support these APIs.
- The current target framework is `net6.0-windows`. Newer .NET SDKs may warn that this framework is out of support; consider upgrading in the future.
