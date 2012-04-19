

#include "RFSync.h"
#include "RF2400.h"

RFSyncClass RFSYNC = RFSyncClass();



byte RFSyncClass::_REGSBANK1[] =
    {  0x00, 1, 0x0B,
       0x01, 1, 0x00,
       0x02, 1, 0x03,
       0x03, 1, 0x03,
       0x04, 1, 0x03,
       0x05, 1, RFSYNC_CHANNEL,
       0x06, 1, 0x3F,
       0x07, 1, 0x3E,
       0x08, 1, 0x00,
       0x09, 1, 0x00,
       0x0A, 5, 0x73,0x65,0x72,0x76,0x31,
       0x0B, 5, 0x63,0x6C,0x69,0x65,0x31,
       0x0C, 1, 0xC3,
       0x0D, 1, 0xC4,
       0x0E, 1, 0xC5,
       0x0F, 1, 0xC6,
       0x10, 5, 0x73,0x65,0x72,0x76,0x31,
       0x11, 1, RFSYNC_PAYLOADSIZE,
       0x12, 1, RFSYNC_PAYLOADSIZE,
       0x13, 1, 0x00,
       0x14, 1, 0x00,
       0x15, 1, 0x00,
       0x16, 1, 0x00,
       0x17, 1, 0x11,
       0x1C, 1, 0x00,
       0x1D, 1, 0x00,
    } ;

#define SET_CE 		digitalWrite(RFSYNC_PIN_CE,HIGH);
#define CLR_CE   	digitalWrite(RFSYNC_PIN_CE,LOW);
#define SET_CSN		digitalWrite(RFSYNC_PIN_CSN,HIGH);
#define CLR_CSN     digitalWrite(RFSYNC_PIN_CSN,LOW);
	
#define SYNK_MASK_NULL 		0
#define SYNK_MASK_LOCAL  	1
#define SYNK_MASK_REMOTE  	2

#define RFSYNC_CONFIG ((1<<EN_CRC) | (0<<CRCO) )
	
RFSyncClass::RFSyncClass()
{
	_payloadsize = RFSYNC_PAYLOADSIZE;
	_issending = false;
}

void RFSyncClass::Start()
{
    pinMode(RFSYNC_PIN_CE,OUTPUT);
    pinMode(RFSYNC_PIN_CSN,OUTPUT);

	memset(_syncdata,0,RFSYNC_SIZE);
	memset(_syncmask,SYNK_MASK_NULL,RFSYNC_SIZE);
	
	
	CLR_CE;
	SET_CSN;

	SPI.begin();
	SPI.setDataMode(SPI_MODE0);
	SPI.setClockDivider(SPI_2XCLOCK_MASK);
	
	WriteRegs(_REGSBANK1,sizeof(_REGSBANK1));
	
	
	SET_CE;
}

void RFSyncClass::WriteRegs(byte data[],byte size)
{
    byte reg,len;
    byte* pdata; 
    byte n=0;
	while(n<size)
	{	reg = data[n];
		len = data[n+1];
		pdata = &(data[n+2]);
		n+=(len+2);
		WriteRegister(reg,pdata,len);
	}
}


void RFSyncClass::WriteRegister(byte reg,byte* pdata,byte len)
{
    CLR_CSN;
	SPI.transfer(reg | W_REGISTER);
    SPIWrite(pdata,len);
    SET_CSN;
}

void RFSyncClass::ReadRegister(byte reg, byte * value, byte len)
{
    CLR_CSN;
    SPI.transfer(reg | R_REGISTER );
    SPIRead(value,len);
    SET_CSN;
}


byte RFSyncClass::GetReg(byte reg)
{
	byte result;
	ReadRegister(reg, &result,1);
	return result;
}

void RFSyncClass::PutReg(byte reg,byte value)
{
	WriteRegister(reg, &value,1);
}


void RFSyncClass::SPIWrite(byte *pdata,byte len)
{
	for(byte i = 0;i < len; i++)
		SPI.transfer(*(pdata+i));
}

void RFSyncClass::SPIRead(byte *pdata,byte len)
{
	for(byte i = 0;i < len;i++){
		*(pdata+i) = SPI.transfer(*(pdata+i));
	}
}

void RFSyncClass::WaitTillSendDone()
{
	byte status;
	while(_issending)
	{
		status =  GetReg(STATUS);
		if((status & ((1 << TX_DS)  | (1 << MAX_RT))))
		{
			_issending = false;
			CLR_CE;
			PutReg(CONFIG, RFSYNC_CONFIG | ( (1<<PWR_UP) | (1<<PRIM_RX) ) );
			SET_CE;
			PutReg(STATUS,(1 << TX_DS) | (1 << MAX_RT)); 
			break;
		}
	}

}


void RFSyncClass::Put(byte offset,byte* pdata,byte len)
{
	memset(&_packet,0,sizeof(RFSYNC_PACKET));
	_packet.offset = offset;
	_packet.size = len;
	memcpy(_packet.data,pdata,len);
	
	CLR_CE;
	
	_issending = true;
	PutReg(CONFIG, RFSYNC_CONFIG | ( (1<<PWR_UP) | (0<<PRIM_RX) ) );

	CLR_CSN;
	SPI.transfer( FLUSH_TX );
	SET_CSN;

	CLR_CSN;
	SPI.transfer( W_TX_PAYLOAD );
	SPIWrite((byte*)&_packet,RFSYNC_PAYLOADSIZE);
	SET_CSN;
	
	SET_CE;
	
	WaitTillSendDone();
	
}

boolean RFSyncClass::Get(byte offset,byte* pdata,byte len)
{
	boolean result=false;
	memcpy(pdata,(_syncdata+offset),len);
	for(int i=0;i<len;i++)
		if (*(_syncmask+i+offset) == SYNK_MASK_REMOTE)
		{	result = true;
			break;
		}
	memset((_syncmask+offset),SYNK_MASK_NULL,len);
	return result;
}


boolean RFSyncClass::IsDataReady() 
{
    if ( GetReg(STATUS) & (1 << RX_DR) ) return true;
    return !IsRXFifoEmpty();
}


boolean RFSyncClass::IsRXFifoEmpty()
{
	return (GetReg(FIFO_STATUS) & (1 << RX_EMPTY));
}


void RFSyncClass::Clear()
{
    CLR_CSN;
    SPI.transfer( FLUSH_TX ); 
    SPI.transfer( FLUSH_RX );
    SET_CSN;
}


void RFSyncClass::PowerDown()
{
	CLR_CE;
	PutReg(CONFIG, RFSYNC_CONFIG );
}

boolean RFSyncClass::Pump()
{
	if (!IsDataReady()) return false;
	
	memset(&_packet,0,sizeof(RFSYNC_PACKET));
	
	CLR_CSN;
	SPI.transfer( R_RX_PAYLOAD );
	SPIRead((byte*)&_packet,RFSYNC_PAYLOADSIZE);
	
	if (_packet.offset+_packet.size<=RFSYNC_SIZE) 
	{
		memcpy((_syncdata+_packet.offset),_packet.data,_packet.size);
		memset((_syncmask+_packet.offset),SYNK_MASK_REMOTE,_packet.size);
	}
	
	PutReg(STATUS,(1<<RX_DR));  
	
	return true;
}





















