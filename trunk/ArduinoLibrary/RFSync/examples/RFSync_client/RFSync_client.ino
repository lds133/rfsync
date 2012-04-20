#include <SPI.h>
#include <RFSync.h>

#define LOCALOFFSET 0
#define REMOTEOFFSET 5
#define TIMEOUT 5000


void setup() 
{
  Serial.begin(115200);
  RFSYNC.Start();
  
  Serial.println("* RFSYNC Client *");
}


void loop()
{
    long timeremote;
    long timelocal=millis();
    
    RFSYNC.Clear();
    Serial.print("Ping: ");
    RFSYNC.Put(LOCALOFFSET,(byte*)&timelocal,sizeof(long));
   
    while(!RFSYNC.Pump())
    {  if (millis()-timelocal>TIMEOUT)
       {   Serial.println("Timeout");
           return;
       }
    }
   
    if(RFSYNC.Get(REMOTEOFFSET,(byte*)&timeremote,sizeof(long)))
    {   Serial.println(millis()-timeremote,DEC);
    } else
    {   Serial.println("Not ping packet");
    }    
    
    delay(3000);
  
}


