using System;
using System.Runtime.InteropServices;

namespace JLinkAccess
{
    [StructLayout(LayoutKind.Explicit, Pack = 1)]
    public struct JLink_Disassembly_Info
    {
        [MarshalAs(UnmanagedType.U4)]
        [FieldOffset(0)]
        UInt32 Size; // Required. Size = sizeof(JLink_Disassembly_Info)

        [MarshalAs(UnmanagedType.U1)]
        [FieldOffset(4)]
        byte Mode; // Mode to use for disassembling instruction. 0: Current CPU Mode; 1: ARM Mode; 2: Thumb Mode

        [MarshalAs(UnmanagedType.U1)]
        [FieldOffset(5)]
        byte Condition; // [0]: 1 = Use condition for this instruction. [7:1]: Condition to use (e.g. [4:1] in IT Block on Cortex-M).

        [MarshalAs(UnmanagedType.U1)]
        [FieldOffset(6)]
        byte Dummy0; // Reserved for future use

        [MarshalAs(UnmanagedType.U1)]
        [FieldOffset(7)]
        byte Dummy1; // Reserved for future use
    }

    [StructLayout(LayoutKind.Explicit, Pack = 1)]
    public struct JLink_Indicator_CTRL
    {
        [MarshalAs(UnmanagedType.U2)]
        [FieldOffset(0)]
        UInt16 IndicatorId;

        [MarshalAs(UnmanagedType.U2)]
        [FieldOffset(2)]
        UInt16 Override; // 1: Controlled by Host (PC), 0: controlled by emulator (default)

        [MarshalAs(UnmanagedType.U2)]
        [FieldOffset(4)]
        UInt16 InitialOnTime; // 1ms

        [MarshalAs(UnmanagedType.U2)]
        [FieldOffset(6)]
        UInt16 OnTime; // 1ms

        [MarshalAs(UnmanagedType.U2)]
        [FieldOffset(8)]
        UInt16 OffTime; // 1ms
    }

    [StructLayout(LayoutKind.Explicit, Pack = 1)]
    public struct JLink_BP_Info
    {
        [MarshalAs(UnmanagedType.U4)]
        [FieldOffset(0)]
        UInt32 Size;

        [MarshalAs(UnmanagedType.U4)]
        [FieldOffset(4)]
        UInt32 Handle;

        [MarshalAs(UnmanagedType.U4)]
        [FieldOffset(8)]
        UInt32 Addr;

        [MarshalAs(UnmanagedType.U4)]
        [FieldOffset(12)]
        UInt32 Type;

        [MarshalAs(UnmanagedType.U4)]
        [FieldOffset(16)]
        UInt32 ImpFlags;

        [MarshalAs(UnmanagedType.U4)]
        [FieldOffset(20)]
        UInt32 UseCnt;

        [MarshalAs(UnmanagedType.U1)]
        [FieldOffset(24)]
        byte Internal;

        [MarshalAs(UnmanagedType.U1)]
        [FieldOffset(25)]
        byte Disabled;
    }

    [StructLayout(LayoutKind.Explicit, Pack = 1)]
    public struct JLink_HW_Status
    {
        [MarshalAs(UnmanagedType.U2)]
        [FieldOffset(0)]
        UInt16 VTarget;

        [MarshalAs(UnmanagedType.U1)]
        [FieldOffset(2)]
        byte TCK;

        [MarshalAs(UnmanagedType.U1)]
        [FieldOffset(3)]
        byte TDI;

        [MarshalAs(UnmanagedType.U1)]
        [FieldOffset(4)]
        byte TDO;

        [MarshalAs(UnmanagedType.U1)]
        [FieldOffset(5)]
        byte TMS;

        [MarshalAs(UnmanagedType.U1)]
        [FieldOffset(6)]
        byte TRES;

        [MarshalAs(UnmanagedType.U1)]
        [FieldOffset(7)]
        byte TRST;
    }

    [StructLayout(LayoutKind.Explicit, Pack = 1)]
    public struct JLink_JTAG_ID_Data
    {
        [MarshalAs(UnmanagedType.I2)]
        [FieldOffset(0)]
        Int16 NumDevices;

        [MarshalAs(UnmanagedType.U1)]
        [FieldOffset(2)]
        UInt16 ScanLen;

        [MarshalAs(UnmanagedType.LPArray, SizeConst = 3)]
        [FieldOffset(4)]
        Int32[] aId;

        [MarshalAs(UnmanagedType.LPArray, SizeConst = 3)]
        [FieldOffset(16)]
        byte[] aScanLen;

        [MarshalAs(UnmanagedType.LPArray, SizeConst = 3)]
        [FieldOffset(19)]
        byte[] aIrRead;

        [MarshalAs(UnmanagedType.LPArray, SizeConst = 3)]
        [FieldOffset(22)]
        byte[] aScanRead;
    }

    [StructLayout(LayoutKind.Explicit, Pack = 1)]
    public struct JLink_Speed_Info
    {
        [MarshalAs(UnmanagedType.U4)]
        [FieldOffset(0)]
        UInt32 Size;

        [MarshalAs(UnmanagedType.U4)]
        [FieldOffset(4)]
        UInt32 BaseFreq; // Clock frequency in Hz

        [MarshalAs(UnmanagedType.U2)]
        [FieldOffset(8)]
        UInt16 MinDiv; // Minimum divider. Max JTAG speed is BaseFreq / MinDiv.

        [MarshalAs(UnmanagedType.U2)]
        [FieldOffset(10)]
        UInt16 SupportAdaptive;
    }

    [StructLayout(LayoutKind.Explicit, Pack = 1)]
    public struct JLink_RTCK_Info
    {
        [MarshalAs(UnmanagedType.U4)]
        [FieldOffset(0)]
        UInt32 Size;

        [MarshalAs(UnmanagedType.U4)]
        [FieldOffset(4)]
        UInt32 Min;

        [MarshalAs(UnmanagedType.U4)]
        [FieldOffset(8)]
        UInt32 Max;

        [MarshalAs(UnmanagedType.U4)]
        [FieldOffset(12)]
        UInt32 Average;
    }
}
