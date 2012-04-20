#include <SPI.h>
#include <RFSync.h>

#define LOCALOFFSET 0
#define REMOTEOFFSET 5
#define TIMEOUT 5000

void setup() 
{
  Serial.begin(115200);
  RFSYNC.Start();
  
  Serial.println("* RFSYNC Server *");
}


void loop()
{
    long timelocal = millis();
    long timeremote;
   
    while(!RFSYNC.Pump())
    {
        if (millis()-timelocal>TIMEOUT)
       {   Serial.println("Timeout");
           return;
       }
    }
    
    if (RFSYNC.Get(LOCALOFFSET,(byte*)&timeremote,sizeof(long)))
    {   Serial.print("Time: ");
        Serial.println(timeremote,DEC);
    } else
    {   Serial.println("Not ping packet");
    }

    RFSYNC.Put(REMOTEOFFSET,(byte*)&timeremote,sizeof(long));

}


