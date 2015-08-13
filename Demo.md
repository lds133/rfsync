# Description #

This demo illustrates devices interaction while using FRSync Arduino library.

![http://rfsync.googlecode.com/svn/wiki/RF2400.png](http://rfsync.googlecode.com/svn/wiki/RF2400.png)

### Characters ###

  * Client
  * Server
  * Observer


### Act ###

  1. **Client** puts in common data array, system time information.
  1. **Server** takes from common data array information and put it in another position.
  1. **Client** takes time information from another position (where it was placed by server), compare it with current time and calculate time that synchronization takes.
  1. All synchronization processes observes by the **Observer**


# Hardware #

  * [RF-2400P module](http://www.inhaos.com/product_info.php?products_id=35) - 2 pieces
  * [Arduino board](http://arduino.cc) - 2 pieces
  * [RF-2410U dongle](http://www.inhaos.com/product_info.php?products_id=42)  - 1 piece
  * [PC](http://en.wikipedia.org/wiki/Personal_computer) running on [Microsoft Windows](http://windows.microsoft.com/en-US/windows/home) - 1 piece

# Software #

  * [Arduino IDE](http://arduino.cc/en/Main/Software)
  * RFSync Arduino [library](http://rfsync.googlecode.com/files/RFSync_0-01.zip)
  * Arduino [sketch](http://rfsync.googlecode.com/svn/trunk/ArduinoLibrary/RFSync/examples/RFSync_client/RFSync_client.ino) for the **Client**
  * Arduino [sketch](http://rfsync.googlecode.com/svn/trunk/ArduinoLibrary/RFSync/examples/RFSync_server/RFSync_server.ino) for the **Server**
  * [Drivers](Drivers.md) for RF-2410U
  * [RF-2410U firmware](http://rfsync.googlecode.com/files/RF2410U%20RFSYNC%200_01.hex)
  * [RF-2410U loader](http://rfsync.googlecode.com/files/RF2410U%20Loader.zip) application.
  * **Observer** [client application](http://rfsync.googlecode.com/files/RFSyncSetup_0-01.msi).

# Assembly instruction #

### Client & Server ###

  1. Connect RF-2400P modules to Arduino boards using next [connection diagram](Schematic.md).
  1. Set up RFSync library on Arduino IDE.
  1. Download client and server sketches on arduino boards.

### Observer ###

  1. Install drivers.
  1. Run Loader application and update firmware on RF-2410U dongle
  1. Install and run Observer [client application](GUI.md).

# Running demo #

  1. Power up the arduino boards.
  1. Look at the devices interaction process using UART interface.
  1. Press "Start" button in the [observer application](GUI.md).
  1. Look at the devices interaction in the observer application console.