#ifndef _RFSYNC_H_
#define _RFSYNC_H_

#include <Arduino.h>
#include <SPI.h>

#define RFSYNC_PIN_CE 		8
#define RFSYNC_PIN_CSN 		7
#define RFSYNC_CHANNEL		1
#define RFSYNC_PAYLOADSIZE	10 
#define RFSYNC_SIZE         30




#pragma pack(push)  
#pragma pack(1)     
typedef struct
{
	byte offset;
	byte size;
	byte data[RFSYNC_SIZE];
}RFSYNC_PACKET;
#pragma pack(pop)  



class RFSyncClass 
{

public:
	
	RFSyncClass();
	void Start();
	void Put(byte offset,byte* pdata,byte len);
	boolean Get(byte offset,byte* pdata,byte len);
	boolean Pump();
	void Clear();
	void PowerDown();
	
	
private:	
	static 	byte _REGSBANK1[];
	byte _payloadsize;
	byte _syncdata[RFSYNC_SIZE];
	byte _syncmask[RFSYNC_SIZE];
	RFSYNC_PACKET _packet;
	boolean _issending;	
	
	void WriteRegs(byte data[],byte size);
	void SPIWrite(byte *pdata,byte len);
	void SPIRead(byte *pdata,byte len);
	void WaitTillSendDone();
	boolean IsRXFifoEmpty();
	boolean IsDataReady(); 	
	void WriteRegister(byte reg,byte* pdata,byte len);
	void ReadRegister(byte reg,byte* pdata,byte len);
	byte GetReg(byte reg);
    void PutReg(byte reg,byte value);	


	
	
};

extern RFSyncClass RFSYNC;

#endif 
