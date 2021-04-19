using System;
using System.Runtime.InteropServices;

namespace AmebaZ_Flasher.Models
{
    static class AmebaZ_Addresses
    {
        // UM0111
        public static readonly UInt32 RAMAddr = 0x1000_0000;
        public static readonly UInt32 RAMSize = 0x0004_0000;
        public static readonly UInt32 RAMImage1Addr = 0x1000_2000; // Boot Addr
        public static readonly UInt32 RAMImage2Addr = 0x1000_5000;
        public static readonly UInt32 RAMMSPAddr = 0x1003_E000;
        public static readonly UInt32 RAMRDPAddr = 0x1003_F000;

        // Flash
        public static readonly UInt32 FlashAddr = 0x0800_0000;
        public static readonly UInt32 FlashSize = 0x0010_0000; // 1Mb

        // Flasher offsets
        public static readonly UInt32 FlasherRAM = RAMImage1Addr + 0x0000;
        public static readonly UInt32 FlasherData = 0x100022ac;// FlasherRAM + 0x0000;
        public static readonly UInt32 FlasherDataLen = 2048;
        public static readonly UInt32 FlasherBlockWriteSize = 0x10002ab8;//FlasherRAM + (FlasherDataLen / 4) + 0;
        public static readonly UInt32 FlasherWriteAddr = 0x10002abc;// FlasherRAM + (FlasherDataLen / 4) + 4;
        public static readonly UInt32 FlasherSetComplete = 0x10002ab4;// FlasherRAM + (FlasherDataLen / 4) + 8; // byte!!!
        public static readonly UInt32 FlasherBreakpoint = 0x100020d4;//0x100020d4 0x100020dc

        // Flash offsets
        public static readonly UInt32 BootAddr = 0x0000_0000;
        public static readonly UInt32 BootSize = 0x0000_8000;
        public static readonly UInt32 SystemDataAddr = 0x0000_9000;
        public static readonly UInt32 SystemDataSize = 0x0000_1000;
        public static readonly UInt32 UserDataAddr = 0x000F_5000;
        public static readonly UInt32 UserDataSize = 0x0000_A000;
        public static readonly UInt32 RDPAddr = 0x000F_F000;
        public static readonly UInt32 RDPSize = 0x0000_1000;
        public static readonly UInt32 OTA1Addr = 0x0000_B000;
        public static readonly UInt32 OTA1Size = 0x0007_5000;
        public static readonly UInt32 OTA2Addr = 0x0008_0000;
        public static readonly UInt32 OTA2Size = 0x0007_5000;

        // Boot
        public static readonly UInt32 Stack = 0x1003_EFFC;
        public static readonly UInt32 FirmwareAddr = 0x1000_2000;
    }

    [StructLayout(LayoutKind.Explicit, Pack = 1)]
    public struct Boot_Head
    {
        [MarshalAs(UnmanagedType.U8)]
        [FieldOffset(0)]
        public UInt64 Signature;

        [MarshalAs(UnmanagedType.U4)]
        [FieldOffset(8)]
        public UInt32 Size;

        [MarshalAs(UnmanagedType.U4)]
        [FieldOffset(12)]
        public UInt32 LoadAddr;

        /* reserved 0x10 ... 0x20 */
    }

    [StructLayout(LayoutKind.Explicit, Pack = 1)]
    public struct Image_Head
    {
        [MarshalAs(UnmanagedType.U8)]
        [FieldOffset(0)]
        public UInt64 Signature;

        [MarshalAs(UnmanagedType.U4)]
        [FieldOffset(8)]
        public UInt32 Size;

        [MarshalAs(UnmanagedType.U4)]
        [FieldOffset(12)]
        public UInt32 LoadAddr;

        /* reserved 0x10 ... 0x20 */
    }

    [StructLayout(LayoutKind.Explicit, Pack = 1)]
    public struct OTA_Head
    {
        [MarshalAs(UnmanagedType.U4)]
        [FieldOffset(0)]
        public UInt32 Version;

        [MarshalAs(UnmanagedType.U4)]
        [FieldOffset(4)]
        public UInt32 Number;

        [MarshalAs(UnmanagedType.U4)]
        [FieldOffset(8)]
        public UInt32 OTA1Signature; // OTAx

        [MarshalAs(UnmanagedType.U4)]
        [FieldOffset(12)]
        public UInt32 OTA1HeadLen;

        [MarshalAs(UnmanagedType.U4)]
        [FieldOffset(14)]
        public UInt32 OTA1CRC;

        [MarshalAs(UnmanagedType.U4)]
        [FieldOffset(18)]
        public UInt32 OTA1ImageLen;

        [MarshalAs(UnmanagedType.U4)]
        [FieldOffset(22)]
        public UInt32 OTA1Offset;

        [MarshalAs(UnmanagedType.U4)]
        [FieldOffset(26)]
        public UInt32 OTA1FlashAddr;

        [MarshalAs(UnmanagedType.U4)]
        [FieldOffset(30)]
        public UInt32 OTA2Signature; // OTAx

        [MarshalAs(UnmanagedType.U4)]
        [FieldOffset(34)]
        public UInt32 OTA2HeadLen;

        [MarshalAs(UnmanagedType.U4)]
        [FieldOffset(38)]
        public UInt32 OTA2CRC;

        [MarshalAs(UnmanagedType.U4)]
        [FieldOffset(42)]
        public UInt32 OTA2ImageLen;

        [MarshalAs(UnmanagedType.U4)]
        [FieldOffset(46)]
        public UInt32 OTA2Offset;

        [MarshalAs(UnmanagedType.U4)]
        [FieldOffset(50)]
        public UInt32 OTA2FlashAddr;
    }

    [StructLayout(LayoutKind.Explicit, Pack = 1)]
    public struct System_Data
    {
        [MarshalAs(UnmanagedType.U4)]
        [FieldOffset(0)]
        public UInt32 OTA2FlashAddr;

        [MarshalAs(UnmanagedType.U4)]
        [FieldOffset(4)]
        public UInt32 Image2Valid;

        [MarshalAs(UnmanagedType.U4)]
        [FieldOffset(8)]
        public UInt32 OTA1Force;

        [MarshalAs(UnmanagedType.U4)]
        [FieldOffset(12)]
        public UInt32 OTAReserved;

        [MarshalAs(UnmanagedType.U4)]
        [FieldOffset(16)]
        public UInt32 RDPFlashAddr;

        [MarshalAs(UnmanagedType.U4)]
        [FieldOffset(20)]
        public UInt32 RDPLen;

        [MarshalAs(UnmanagedType.U4)]
        [FieldOffset(24)]
        public UInt32 RDPReserved1;

        [MarshalAs(UnmanagedType.U4)]
        [FieldOffset(28)]
        public UInt32 RDPReserved2;

        [MarshalAs(UnmanagedType.U2)]
        [FieldOffset(32)]
        public UInt16 SPIMode;

        [MarshalAs(UnmanagedType.U2)]
        [FieldOffset(34)]
        public UInt16 SPISpeed;

        [MarshalAs(UnmanagedType.U2)]
        [FieldOffset(36)]
        public UInt16 FlashID;

        [MarshalAs(UnmanagedType.U2)]
        [FieldOffset(38)]
        public UInt16 FlashSize;

        [MarshalAs(UnmanagedType.U4)]
        [FieldOffset(42)]
        public UInt32 FlashReserved1;

        [MarshalAs(UnmanagedType.U4)]
        [FieldOffset(46)]
        public UInt32 FlashReserved2;

        [MarshalAs(UnmanagedType.U4)]
        [FieldOffset(50)]
        public UInt32 LogBaudrate;

        /* reserved ... usb ... adc ...*/
    }
}
