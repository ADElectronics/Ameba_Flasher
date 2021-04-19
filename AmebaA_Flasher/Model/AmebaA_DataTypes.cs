using System;
using System.Runtime.InteropServices;

namespace AmebaA_Flasher.Model
{
    static class AmebaA_Addresses
    {
        public static readonly UInt32 Addr = 0x1000_8000;
        public static readonly UInt32 Size = 0x0004_0000;//421888;//0x0006_7000;
        public static readonly UInt32 Stack = 0x1FFF_FFFC;
        public static readonly UInt32 FirmwareAddr = 0x1000_1000;

        public static readonly UInt32 RAMAddr = 0x1000_0000;
        public static readonly UInt32 RAMBootAddr = 0x1000_0bc8;
        public static readonly UInt32 RAMSize = 0x0007_0000;
        public static readonly UInt32 SDRAMAddr = 0x3000_0000;
        public static readonly UInt32 SDRAMSize = 0x0020_0000;
        public static readonly UInt32 FlashAddr = 0x9800_0000;
        public static readonly UInt32 FlashSize = 0x0010_0000; // обычный размер для 8710AF
        public static readonly UInt32 FlashSectorSize = 4096;
        public static readonly UInt32 FlashBlockSize = 65536;

        public static readonly UInt32 SystemDataAddr = 0x0000_9000;
        public static readonly UInt32 SystemDataSize = 0x0000_1000;
        public static readonly UInt32 CalibrationDataAddr = 0x0000_A000;
        public static readonly UInt32 CalibrationDataSize = 0x0000_1000;
        public static readonly UInt32 OTPSize = 512 / 8;
    }
    /*
    static class AmebaA_Firmware
    {
        public static readonly UInt32 SystemDataAddr = 0x9000;
        public static readonly UInt32 CalibrationDataAddr = 0xA000;
        public static readonly UInt32 Image2Addr = 0xB000;
    }
    */
    public enum AmebaA_ParticleImageBits : UInt16
    {
        Image1 = 0x1,
        System = 0x2,
        Calibration = 0x4,
        Image2 = 0x8,
        OTA = 0x10,
        Image3 = 0x20
    }

    public enum AmebaA_ImageType : byte
    {
        All = 0x0,
        Image1,
        Image2,
        Image3,
        OTA,
        System,
        Calibration,
        Saved
    }

    public enum AmebaA_Flasher_CMD : UInt32
    {
        ReadID = 0x0,
        EraseAll,
        EraseSector,
        Read,
        Write,
        Verify
    }

    public enum AmebaA_Restart_Type : UInt32
    {
        Flash = 0x0,
        RAM_Start,
        RAM_Wakeup,
        RAM_Patch0,
        RAM_Patch1,
        RAM_Patch2
    }

    [StructLayout(LayoutKind.Explicit, Pack = 1)]
    public struct Boot_Head
    {
        [MarshalAs(UnmanagedType.U4)]
        [FieldOffset(0)]
        public UInt32 RAMStartAddr;

        [MarshalAs(UnmanagedType.U4)]
        [FieldOffset(4)]
        public UInt32 RAMWakeUpAddr;

        [MarshalAs(UnmanagedType.U4)]
        [FieldOffset(8)]
        public UInt32 RAMPatch0Addr;

        [MarshalAs(UnmanagedType.U4)]
        [FieldOffset(12)]
        public UInt32 RAMPatch1Addr;

        [MarshalAs(UnmanagedType.U4)]
        [FieldOffset(16)]
        public UInt32 RAMPatch2Addr;

        [MarshalAs(UnmanagedType.U4)]
        [FieldOffset(20)]
        public UInt32 Signature; // 0x8816_7923

        [MarshalAs(UnmanagedType.U4)]
        [FieldOffset(24)]
        public UInt32 Reserved; // 0xFFFF_FFFF
    }

    [StructLayout(LayoutKind.Explicit, Pack = 1)]
    public struct Flash_Head
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        [FieldOffset(0)]
        public UInt32[] SPICPattern; // SPICCalibrationPattern

        [MarshalAs(UnmanagedType.U4)]
        [FieldOffset(16)]
        public UInt32 Size;

        [MarshalAs(UnmanagedType.U4)]
        [FieldOffset(20)]
        public UInt32 LoadAddr;

        [MarshalAs(UnmanagedType.U4)]
        [FieldOffset(24)]
        public UInt32 Image2Addr; // unit is 1024 bytes or 0xFFFF_0000, Ameba = 0xFFFF_002C -> 0x0000_B000

        [MarshalAs(UnmanagedType.U4)]
        [FieldOffset(28)]
        public UInt32 Reserved; // 0xFFFFFFFF

    }

    [StructLayout(LayoutKind.Explicit, Pack = 1)]
    public struct Image_Head
    {
        [MarshalAs(UnmanagedType.U4)]
        [FieldOffset(0)]
        public UInt32 Size;

        [MarshalAs(UnmanagedType.U4)]
        [FieldOffset(4)]
        public UInt32 LoadAddr;

        [MarshalAs(UnmanagedType.U8)]
        [FieldOffset(8)]
        public UInt64 Signature; // Image2Signature0, Image2Signature1
    }

    [StructLayout(LayoutKind.Explicit, Pack = 1)]
    public struct Image_Info
    {
        [MarshalAs(UnmanagedType.U4)]
        [FieldOffset(0)]
        public UInt32 InfraStart;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
        [FieldOffset(4)]
        public byte[] RTKWin; // RTKWin, b0, b1, b1, b0, w$8195, b1, b1

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        [FieldOffset(24)]
        public byte[] CustSign; // Customer Signature-modelxxx
    }
}
