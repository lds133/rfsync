using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace RFSync
{
    class USB
    {
        [DllImport("SiUSBXp.dll", EntryPoint = "SI_GetNumDevices")]
        public static extern Int16 GetNumDevices(ref UInt32 lpdwNumDevices);
        

        [DllImport("SiUSBXp.dll", EntryPoint = "SI_GetProductString")]
        public static extern Int16 GetProductString(UInt32 dwDeviceNum, ref Byte lpvDeviceString,UInt32 dwFlags);
        
        [DllImport("SiUSBXp.dll", EntryPoint = "SI_Open")]
        public static extern Int32 Open(UInt32 dwDevice, ref UInt32 cyHandle);

        [DllImport("SiUSBXp.dll", EntryPoint = "SI_Close")]
        public static extern Int32 Close(ref UInt32 cyHandle);

        [DllImport("SiUSBXp.dll", EntryPoint = "SI_Read")]
        public static extern Int32 Read(UInt32 cyHandle,ref Byte lpBuffer, UInt32 dwBytesToRead, ref UInt32 lpdwBytesReturned , UInt32 lpOverlapped);

        [DllImport("SiUSBXp.dll", EntryPoint = "SI_Write")]
        public static extern Int32 Write(UInt32 cyHandle,ref Byte lpBuffer, UInt32 dwBytesToWrite, ref UInt32 lpdwBytesWritten, UInt32 lpOverlapped);
        
        [DllImport("SiUSBXp.dll", EntryPoint = "SI_SetTimeouts")]
        public static extern Int32 SetTimeouts(UInt32 dwReadTimeout, UInt32 dwWriteTimeout);

        [DllImport("SiUSBXp.dll", EntryPoint = "SI_GetTimeouts")]
        public static extern Int32 GetTimeouts(ref UInt32 dwReadTimeout, ref UInt32 dwWriteTimeout);

        [DllImport("SiUSBXp.dll", EntryPoint = "SI_CheckRXQueue")]
        public static extern Int32 CheckRXQueue(UInt32 cyHandle,ref UInt32 lpdwNumBytesInQueue, ref UInt32 lpdwQueueStatus);

       
        public const UInt16 RETURN_SERIAL_NUMBER = 0x00;
        public const UInt16 RETURN_DESCRIPTION = 0x01;
        public const UInt16 RETURN_LINK_NAME = 0x02;
        public const UInt16 RETURN_VID = 0x03;
        public const UInt16 RETURN_PID = 0x04;

        public const UInt16 SUCCESS = 0x00;
        public const UInt16 DEVICE_NOT_FOUND = 0xFF;
        public const UInt16 INVALID_HANDLE = 0x01;
        public const UInt16 READ_ERROR = 0x02;
        public const UInt16 RX_QUEUE_NOT_READY = 0x03;
        public const UInt16 WRITE_ERROR = 0x04;
        public const UInt16 RESET_ERROR = 0x05;
        public const UInt16 INVALID_BUFFER = 0x06;
        public const UInt16 INVALID_REQUEST_LENGTH = 0x07;
        public const UInt16 DEVICE_IO_FAILED = 0x08;

        public const UInt16 QUEUE_NO_OVERRUN = 0x00;
        public const UInt16 QUEUE_OVERRUN = 0x01;
        public const UInt16 QUEUE_READY = 0x02;

        public const UInt32 MAX_DEVICE_STRLEN = 256;
        public const UInt32 MAX_READ_SIZE = 65536;
        public const UInt32 MAX_WRITE_SIZE = 4096;

        public const UInt16 INVALID_HANDLE_VALUE = 0xFFFF;

    }
}
