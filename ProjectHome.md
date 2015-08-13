Code to establish data synchronization on Arduino boards through radio interface using RF-2400P radio modules from Inhaos.
Including firmware code and windows [GUI client application](GUI.md) for RF-2410U USB module.

The module should be connected to arduino board using next [connection diagram](Schematic.md).

Please find next [demo](Demo.md) as illustration.

## Arduino library functions ##

The library creates class instance named RFSYNC.

### Start ###
Initializes RF module, call this function in the very beginning.

Example:
```
RFSYNC.Start();
```

### Put ###
Put data to the common data cloud. Calling this function cause sending RF packet.

Example:
```
int data=0x1234;
RFSYNC.Put(0,(byte*)&data,sizeof(int));
```

### Get ###
Get data from synchronized local copy of common data. Returns true if data is taken from the cloud.

Example:
```
int data;
if(RFSYNC.Get(0,(byte*)&data,sizeof(int)))
   Serial.println(data,DEC);
```


### Pump ###
Receive synchronize data from RF FIFO buffer if any. Call this function often enough to do not miss synchronize packets from other devices. Returns true if data was received. When received data will be stored in local copy of common data, and can be retrieved later using **Get** function.

Example:
```
long time=millis();
while(!RFSYNC.Pump())
{  if (millis()-time>10000)
   {   Serial.println("Timeout");
       break;
   }
}
```