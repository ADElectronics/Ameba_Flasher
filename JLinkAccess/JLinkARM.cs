using System;
using System.Runtime.InteropServices;
using System.Text;

namespace JLinkAccess
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void CallbackLogDelegate(string data);

    public static class JLinkARM
    {

        #region General API

        //[DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        //public static extern Int32 JLINK_Configure(byte *pConfig);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINK_DownloadFile(ref byte sFileName, UInt32 Addr);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINK_GetAvailableLicense(ref byte pBuffer, UInt32 BufferSize);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern ref byte JLINK_GetPCode(Int32 PCodeIndex, ref UInt32 pNumBytes);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINK_PrInt32Config(ref byte pConfig, UInt32 Mask, ref byte pBuffer, UInt32 BufferSize);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINK_EraseChip();

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINK_SPI_Transfer(ref byte pDataDown, ref byte pDataUp, UInt32 NumBits, UInt32 Flags);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern ref IntPtr JLINK_GetpFunc(JLink_IFunc FuncIndex);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void JLINKARM_AddMirrorArea(UInt32 Addr);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void JLINKARM_AddMirrorAreaEx(UInt32 Addr, UInt32 Size);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void JLINKARM_BeginDownload(UInt32 Flags);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern byte JLINKARM_Clock();

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void JLINKARM_Close();

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_ClrBP(UInt16 BPIndex);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_ClrBPEx(Int32 BPHandle);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_ClrDataEvent(UInt32 Handle);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void JLINKARM_ClrError();

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void JLINKARM_ClrRESET();

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_ClrTCK();

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void JLINKARM_ClrTDI();

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void JLINKARM_ClrTMS();

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void JLINKARM_ClrExecTime();

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void JLINKARM_ClrTRST();

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_ClrWP(Int32 WPHandle);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_Communicate(ref IntPtr pWrite, Int32 WrSize, ref IntPtr pRead, Int32 RdSize);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_CommunicateEx(ref IntPtr pWrite, Int32 WrSize, ref IntPtr pRead, Int32 RdSize, byte IsCommand);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void JLINKARM_ConfigJTAG(Int32 IRPre, Int32 DRPre);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_Connect();

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void JLINKARM_Core2CoreName(UInt32 Core, ref byte pBuffer, UInt16 BufferSize);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_DisassembleInst(ref byte pBuffer, UInt32 BufferSize, UInt32 Addr);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_DisassembleInstEx(ref byte pBuffer, UInt32 BufferSize, UInt32 Addr, ref JLink_Disassembly_Info pInfo);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void JLINKARM_DownloadECode(ref byte pECode, UInt32 NumBytes);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_EnableCheckModeAfterWrite(Int32 OnOff);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void JLINKARM_EnableFlashCache(byte Enable);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void JLINKARM_EnableLog(ref byte pfLog);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void JLINKARM_EnableLogCom(ref byte pfLog);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void JLINKARM_EnablePerformanceCnt(UInt32 Index, UInt32 OnOff);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void JLINKARM_EnableSoftBPs(byte Enable);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_EndDownload();

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_ExecCommand(string pIn, /*ref byte*/IntPtr pOut, Int32 BufferSize);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void JLINKARM_ExecECode();

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_FindBP(UInt32 Addr);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_FreeMem(ref IntPtr pMem);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 JLINKARM_GetBPInfo(Int32 BPHandle);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_GetBPInfoEx(Int32 iBP, ref JLink_BP_Info pInfo);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern ref byte JLINKARM_GetCompileDateTime();

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void JLINKARM_GetConfigData(ref Int32 pIRPre, ref Int32 pDRPre);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_GetDebugInfo(UInt32 Index, ref UInt32 pInfo);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_GetDeviceFamily();

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 JLINKARM_GetDLLVersion();

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_GetEmbeddedFWString(ref byte sFWId, ref byte pBuffer, UInt32 BufferSize);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 JLINKARM_GetEmuCaps();

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void JLINKARM_GetEmuCapsEx(ref byte pCaps, Int32 BufferSize);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void JLINKARM_GetExecTime(ref UInt32 pExecTimeLow, ref UInt32 pExecTimeHigh);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void JLINKARM_GetFeatureString(ref byte pOut);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void JLINKARM_GetFirmwareString(ref byte s, Int32 BufferSize);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_GetHardwareVersion();

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_GetHWInfo(UInt32 BitMask, ref UInt32 pHWInfo);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_GetHWStatus(ref JLink_HW_Status pStat);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 JLINKARM_GetId();

        //[DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        //public static extern void JLINKARM_GetIdData(ref JTAG_ID_DATA pIdData);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_GetIRLen();

        //[DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        //public static extern Int32 JLINK_GetMemZones(ref JLINK_MEM_ZONE_INFO paZoneInfo, Int32 MaxNumZones);

        //[DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        //public static extern Int32 JLINKARM_GetMOEs(ref JLINKARM_MOE_INFO pInfo, Int32 MaxNumMOEs);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 JLINKARM_GetNumBPs();

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_GetNumBPUnits(UInt32 Type);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt16 JLINKARM_GetNumWPs();

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_GetNumWPUnits();

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern byte JLINKARM_GetOEMString(ref byte pOut);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 JLINKARM_GetPerformanceCnt(UInt32 Index);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern ref IntPtr JLINKARM_GetpSharedMem();

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_GetRegisterList(ref UInt32 paList, Int32 MaxNumItems);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern ref byte JLINKARM_GetRegisterName(UInt32 RegIndex);

        //[DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        //public static extern Int32 JLINKARM_GetResetTypeDesc(Int32 ResetType, ref byte* psResetName, ref byte* psResetDesc);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_GetScanLen();

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt16 JLINKARM_GetSelDevice();

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_GetSN();

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt16 JLINKARM_GetSpeed();

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void JLINKARM_GetSpeedInfo(ref JLink_Speed_Info pSpeedInfo);

        //[DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        //public static extern Int32 JLINKARM_GetWPInfoEx(Int32 iWP, ref JLINKARM_WP_INFO pInfo);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void JLINKARM_Go();

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void JLINKARM_GoAllowSim(UInt32 NumInsts);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void JLINKARM_GoEx(UInt32 MaxEmulInsts, UInt32 Flags);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_GoHalt(UInt32 NumClocks);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void JLINKARM_GoIntDis();

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern byte JLINKARM_Halt();

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_HasError();

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern byte JLINKARM_IsConnected();

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern byte JLINKARM_IsHalted();

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern byte JLINKARM_IsOpen();

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void JLINKARM_Lock();

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_MeasureCPUSpeed(UInt32 RAMAddr, Int32 PreserveMem);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_MeasureCPUSpeedEx(UInt32 RAMAddr, Int32 PreserveMem, Int32 AllowFail);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_MeasureRTCKReactTime(ref JLink_RTCK_Info pReactInfo);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_MeasureSCLen(Int32 ScanChain);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern string JLINKARM_Open();

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern string JLINKARM_OpenEx(CallbackLogDelegate pfLog, CallbackLogDelegate pfErrorOut);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_ReadCodeMem(UInt32 Addr, UInt32 NumBytes, ref IntPtr pData);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_ReadConfigReg(UInt32 RegIndex, ref UInt32 pData);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_ReadControlReg(UInt32 RegIndex, ref UInt32 pData);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_ReadDCC(ref UInt32 pData, UInt32 NumItems, Int32 TimeOut);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void JLINKARM_ReadDCCFast(ref UInt32 pData, UInt32 NumItems);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_ReadTerminal(ref byte pBuffer, UInt32 BufferSize);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_ReadDebugPort(UInt32 RegIndex, ref UInt32 pData);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_ReadDebugReg(UInt32 RegIndex, ref UInt32 pData);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_ReadEmu(ref IntPtr p, UInt32 NumBytes);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_ReadEmuConfigMem(ref byte p, UInt32 Off, UInt32 NumBytes);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 JLINKARM_ReadICEReg(Int32 RegIndex);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_ReadMem(UInt32 Addr, UInt32 NumBytes, /*[MarshalAs(UnmanagedType.LPArray)] byte[]*/IntPtr pData);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_ReadMemEx(UInt32 Addr, UInt32 NumBytes, IntPtr pData, UInt32 Flags);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_ReadMemHW(UInt32 Addr, UInt32 Count, IntPtr pData);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_ReadMemIndirect(UInt32 Addr, UInt32 NumBytes, IntPtr pData);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_ReadMemU8(UInt32 Addr, UInt32 NumItems, IntPtr pData, IntPtr pStatus);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_ReadMemU16(UInt32 Addr, UInt32 NumItems, IntPtr pData, IntPtr pStatus);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_ReadMemU32(UInt32 Addr, UInt32 NumItems, IntPtr pData, IntPtr pStatus);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_ReadMemU64(UInt32 Addr, UInt32 NumItems, IntPtr pData, IntPtr pStatus);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINK_ReadMemZonedEx(UInt32 Addr, UInt32 NumBytes, IntPtr pData, UInt32 Flags, ref byte sZone);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 JLINKARM_ReadReg(JLink_ARM_Register RegIndex);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_ReadRegs(ref UInt32 paRegIndex, ref UInt32 paData, ref byte paStatus, UInt32 NumRegs);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_Reset();

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void JLINKARM_ResetNoHalt();

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void JLINKARM_ResetPullsRESET(byte OnOff);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void JLINKARM_ResetPullsTRST(byte OnOff);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void JLINKARM_ResetTRST();

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void JLINKARM_SelectDeviceFamily(JLink_Device_Family Device);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern byte JLINKARM_SelectIP(ref byte sHost, Int32 Port);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void JLINKARM_SelectTraceSource(Int32 Source);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern byte JLINKARM_SelectUSB(Int32 Port);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_SetBP(UInt16 BPIndex, UInt32 Addr);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_SetBPEx(UInt32 Addr, UInt32 TypeFlags);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_SetCoreIndex(UInt16 CoreIndex);

        //[DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        //public static extern Int32 JLINKARM_SetDataEvent(JLINKARM_DATA_EVENT* pEvent, ref UInt32 pHandle);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void JLINKARM_SetDebugUnitBlockMask(Int32 Type, UInt32 Mask);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_SetEndian(Int32 v);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void JLINKARM_SetErrorOutHandler(ref byte pfErrorOut);

        //[DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        //public static extern Int32 JLINK_SetHookUnsecureDialog(JLINK_UNSECURE_DIALOG_CB_FUNC* pfHook);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_SetInitRegsOnReset(Int32 v);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void JLINKARM_SetLogFile(ref byte sFilename);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void JLINKARM_SetMaxSpeed();

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void JLINKARM_SetRESET();

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void JLINKARM_SetResetDelay(Int32 ms);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_SetResetPara(Int32 Value);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern JLink_Reset_Type JLINKARM_SetResetType(JLink_Reset_Type ResetType);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void JLINKARM_SetSpeed(UInt32 Speed);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_SetTCK();

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void JLINKARM_SetTDI();

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void JLINKARM_SetTMS();

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void JLINKARM_SetTRST();

        //[DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        //public static extern void JLINKARM_SetWaitFunction(JLINKARM_WAIT_FUNC* pfWait, ref IntPtr pContext);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void JLINKARM_SetWarnOutHandler(ref byte pfWarnOut);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_SetWP(UInt32 Addr, UInt32 AddrMask, UInt32 Data, UInt32 DataMask, byte Ctrl, byte CtrlMask);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern byte JLINKARM_SimulateInstruction(UInt32 Inst);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern byte JLINKARM_Step();

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void JLINKARM_StoreBits(UInt32 TMS, UInt32 TDI, Int32 NumBits);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_Test();

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void JLINKARM_Unlock();

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt16 JLINKARM_UpdateFirmware();

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 JLINKARM_UpdateFirmwareIfNewer();

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_UpdateReplaceFirmware(Int32 Replace, ref byte sInfo);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_WaitDCCRead(Int32 TimeOut);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_WaitForHalt(Int32 TimeOut);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_WriteConfigReg(UInt32 RegIndex, UInt32 Data);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_WriteControlReg(UInt32 RegIndex, UInt32 Data);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_WriteDCC(ref UInt32 pData, UInt32 NumItems, Int32 TimeOut);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void JLINKARM_WriteDCCFast(ref UInt32 pData, UInt32 NumItems);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_WriteDebugPort(UInt32 RegIndex, UInt32 Data);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_WriteDebugReg(UInt32 RegIndex, UInt32 Data);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_WriteEmu(ref IntPtr p, UInt32 NumBytes);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_WriteEmuConfigMem(ref byte p, UInt32 Off, UInt32 NumBytes);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void JLINKARM_WriteICEReg(UInt32 RegIndex, UInt32 Value, Int32 AllowDelay);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_WriteMem(UInt32 Addr, UInt32 Count, IntPtr pData);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_WriteMemDelayed(UInt32 Addr, UInt32 Count, ref IntPtr pData);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_WriteMemEx(UInt32 Addr, UInt32 NumBytes, ref IntPtr p, UInt32 Flags);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_WriteMemHW(UInt32 Addr, UInt32 Count, ref IntPtr p);

        //[DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        //public static extern Int32 JLINKARM_WriteMemMultiple(JLINK_WRITE_MEM_DESC* paDesc, UInt32 NumWrites);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern byte JLINKARM_WriteReg(/*JLink_ARM_Register*/UInt32 RegIndex, UInt32 Data);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_WriteRegs(ref UInt32 paRegIndex, ref UInt32 paData, ref byte paStatus, UInt32 NumRegs);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_WriteU8(UInt32 Addr, byte Data);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_WriteU16(UInt32 Addr, UInt16 Data);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_WriteU32(UInt32 Addr, UInt32 Data);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_WriteU64(UInt32 Addr, UInt64 Data);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINK_WriteMemZonedEx(UInt32 Addr, UInt32 NumBytes, ref IntPtr p, UInt32 Flags, ref byte sZone);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_WriteVectorCatch(UInt32 Value);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_WriteVerifyMem(UInt32 Addr, UInt32 NumBytes, ref IntPtr p, UInt32 Flags);

        #endregion

        #region BMI API

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_BMI_Get(ref UInt32 pBMIMode);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_BMI_Set(UInt32 BMIMode);

        #endregion

        #region CDC API

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_CDC_Read(ref byte pData, UInt32 NumBytes);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_CDC_SetBaudrate(Int32 BaudrateHz);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_CDC_SetHookFuncs(ref byte pData, Int32 NumBytes);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_CDC_SetRTSState(Int32 State);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_CDC_SetTimeoutLastCDCRead(Int32 Timeout);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_CDC_Write(ref byte pData, UInt32 NumBytes);

        #endregion

        #region CORE API

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 JLINKARM_CORE_GetFound();

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void JLINKARM_CORE_Select(UInt32 Core);

        #endregion

        #region CORESIGHT API

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_CORESIGHT_Configure(ref byte sConfig);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_CORESIGHT_ReadAPDPReg(byte RegIndex, byte APnDP, ref UInt32 pData);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_CORESIGHT_WriteAPDPReg(byte RegIndex, byte APnDP, UInt32 Data);

        #endregion

        #region CP15 API

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_CP15_IsPresent();

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_CP15_ReadEx(byte CRn, byte CRm, byte op1, byte op2, ref UInt32 pData);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_CP15_ReadReg(UInt32 RegIndex, ref UInt32 pData);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_CP15_WriteEx(byte CRn, byte CRm, byte op1, byte op2, UInt32 Data);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_CP15_WriteReg(UInt32 RegIndex, UInt32 Data);

        #endregion

        #region Device API

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_DEVICE_GetIndex(ref byte sDeviceName);

        //[DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        //public static extern Int32 JLINKARM_DEVICE_GetInfo(Int32 DeviceIndex, JLINKARM_DEVICE_INFO* pDeviceInfo);

        //[DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        //public static extern Int32 JLINKARM_DEVICE_SelectDialog(ref IntPtr hParent, UInt32 Flags, JLINKARM_DEVICE_SELECT_INFO* pInfo);

        #endregion

        #region Dialog API

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINK_DIALOG_Configure(ref byte pConfigIn, ref byte pConfigOut, UInt32 BufferSize);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINK_DIALOG_ConfigureEx(ref IntPtr hParent, UInt32 Mask, ref byte pConfigIn, ref byte pConfigOut, UInt32 BufferSize);

        #endregion

        #region EMU API

        //[DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        //public static extern Int32 JLINK_EMU_GPIO_GetProps(JLINK_EMU_GPIO_DESC* paDesc, UInt32 MaxNumDesc);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINK_EMU_GPIO_GetState(ref byte paIndex, ref byte paResult, UInt32 NumPorts);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINK_EMU_GPIO_SetState(ref byte paIndex, ref byte paState, ref byte paResult, UInt32 NumPorts);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINK_EMU_AddLicense(ref byte sLic);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINK_EMU_EraseLicenses();

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINK_EMU_GetLicenses(ref byte pBuffer, UInt32 BufferSize);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_EMU_GetCounters(UInt32 BitMask, ref UInt32 pCounters);

        //[DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        //public static extern void JLINKARM_EMU_GetDeviceInfo(UInt32 iEmu, JLINKARM_EMU_INFO* pInfo);

        //[DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        //public static extern Int32 JLINKARM_EMU_GetList(Int32 HostIFs, JLINKARM_EMU_CONNECT_INFO* paConnectInfo, Int32 MaxInfos);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 JLINKARM_EMU_GetMaxMemBlock();

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_EMU_GetNumConnections();

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 JLINKARM_EMU_GetNumDevices();

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_EMU_GetProductId();

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void JLINKARM_EMU_GetProductName(ref byte pBuffer, UInt32 BufferSize);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_EMU_HasCapEx(Int32 CapEx);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_EMU_HasCPUCap(UInt32 CPUCap);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern byte JLINKARM_EMU_IsConnected();

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 JLINKARM_EMU_SelectByIndex(UInt32 iEmu);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_EMU_SelectByUSBSN(UInt32 SerialNo);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_EMU_SelectIP(ref byte pIPAddr, Int32 BufferSize, ref UInt16 pPort);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void JLINKARM_EMU_SelectIPBySN(UInt32 SerialNo);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_EMU_TestNRSpeed(UInt32 NumReps, UInt32 NumBytes);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_EMU_TestNWSpeed(UInt32 NumReps, UInt32 NumBytes);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_EMU_COM_IsSupported();

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_EMU_COM_Read(UInt16 Channel, UInt16 NumBytes, ref IntPtr pData);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_EMU_COM_Write(UInt16 Channel, UInt16 NumBytes, ref IntPtr pData);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_EMU_FILE_Delete(ref byte sFile);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_EMU_FILE_GetList(ref byte sFile, ref byte pBuffer, UInt32 BufferSize);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_EMU_FILE_GetSize(ref byte sFile);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_EMU_FILE_Read(ref byte sFile, ref byte pData, UInt32 Offset, UInt32 NumBytes);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_EMU_FILE_Write(ref byte sFile, ref byte pData, UInt32 Offset, UInt32 NumBytes);

        #endregion

        #region ETB API

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern byte JLINKARM_ETB_IsPresent();

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 JLINKARM_ETB_ReadReg(UInt32 RegIndex);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void JLINKARM_ETB_WriteReg(UInt32 RegIndex, UInt32 Data, Int32 AllowDelay);

        #endregion

        #region ETM API

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern byte JLINKARM_ETM_IsPresent();

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 JLINKARM_ETM_ReadReg(UInt32 Reg);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void JLINKARM_ETM_StartTrace();

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void JLINKARM_ETM_WriteReg(UInt32 Reg, UInt32 Data, Int32 AllowDelay);

        #endregion

        #region Indicators API

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void JLINKARM_INDICATORS_SetState(Int32 NumStates, ref JLink_Indicator_CTRL pState);

        #endregion

        #region JTAG API

        //[DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        //public static extern void JLINKARM_JTAG_ConfigDevices(UInt32 NumDevices, JLINKARM_JTAG_DEVICE_CONF* paConf);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_JTAG_DisableIF();

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_JTAG_EnableIF();

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void JLINKARM_JTAG_GetData(ref byte pTDO, Int32 BitPos, Int32 NumBits);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 JLINKARM_JTAG_GetDeviceId(UInt16 DeviceIndex);

        //[DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        //public static extern Int32 JLINKARM_JTAG_GetDeviceInfo(UInt16 DeviceIndex, JLINKARM_JTAG_DEVICE_INFO* pDeviceInfo);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 JLINKARM_JTAG_Getbyte(Int32 BitPos);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 JLINKARM_JTAG_GetUInt16(Int32 BitPos);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 JLINKARM_JTAG_GetUInt32(Int32 BitPos);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_JTAG_StoreData(ref byte pTDI, Int32 NumBits);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void JLINKARM_JTAG_StoreGetData(ref byte pTDI, ref byte pTDO, UInt32 NumBits);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void JLINKARM_JTAG_StoreGetRaw(ref byte pTDI, ref byte pTDO, ref byte pTMS, UInt32 NumBits);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_JTAG_StoreInst(ref byte pTDI, Int32 NumBits);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_JTAG_StoreRaw(ref byte pTDI, ref byte pTMS, UInt32 NumBits);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void JLINKARM_JTAG_SyncBits();

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void JLINKARM_JTAG_SyncBytes();

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_JTAG_WriteData(ref byte pTDI, ref byte pTDO, Int32 NumBits);

        #endregion

        #region NET API

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void JLINKARM_NET_Close();

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_NET_Open();

        #endregion

        #region PCODE API

        //[DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        //public static extern Int32 JLINKARM_PCODE_Assemble(byte** ppDest, ref UInt32 pDestSize, ref byte pSrc, ref byte pfOnError);

        //[DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        //public static extern Int32 JLINKARM_PCODE_Exec(ref byte pPCode, UInt32 NumBytes, JLINKARM_EMU_PCODE_STATUS_INFO* pPCodeStat);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_PCODE_GetCaps(ref UInt32 pCaps);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_PCODE_GetS32Version(ref UInt32 pVersion);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_PCODE_GetDebugAPI(ref IntPtr pAPI);

        #endregion

        #region HSS API

        //[DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        //public static extern Int32 JLINK_HSS_GetCaps(JLINK_HSS_CAPS* pCaps);

        //[DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        //public static extern Int32 JLINK_HSS_Start(JLINK_HSS_MEM_BLOCK_DESC* paDesc, Int32 NumBlocks, Int32 Period_us, Int32 Flags);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINK_HSS_Stop();

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINK_HSS_Read(ref IntPtr pBuffer, UInt32 BufferSize);

        #endregion

        #region Powertrace API

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINK_POWERTRACE_Control(UInt32 Cmd, ref IntPtr pIn, ref IntPtr pOut);

        //[DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        //public static extern Int32 JLINK_POWERTRACE_Read(JLINK_POWERTRACE_DATA_ITEM* paData, UInt32 NumItems);

        #endregion

        #region RAW Trace API

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_RAWTRACE_Control(UInt32 Cmd, ref IntPtr pData);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_RAWTRACE_Read(ref byte pData, UInt32 NumBytes);

        #endregion

        #region RTT API

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINK_RTTERMINAL_Control(UInt32 Cmd, ref IntPtr p);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINK_RTTERMINAL_Read(UInt32 BufferIndex, ref byte sBuffer, UInt32 BufferSize);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINK_RTTERMINAL_Write(UInt32 BufferIndex, ref byte sBuffer, UInt32 BufferSize);

        #endregion

        #region STRACE API

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINK_STRACE_Config(ref byte sConfig);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINK_STRACE_Control(UInt32 Cmd, ref IntPtr pData);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINK_STRACE_Read(ref UInt32 paItem, UInt32 NumItems);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINK_STRACE_GetInstStats(ref IntPtr paItem, UInt32 Addr, UInt32 NumItems, UInt32 SizeOfStruct, UInt32 Type);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINK_STRACE_Start();

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINK_STRACE_Stop();

        #endregion

        #region SWD API

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void JLINK_SWD_GetData(ref byte pOut, Int32 BitPos, Int32 NumBits);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 JLINK_SWD_Getbyte(Int32 BitPos);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 JLINK_SWD_GetUInt16(Int32 BitPos);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 JLINK_SWD_GetUInt32(Int32 BitPos);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void JLINK_SWD_StoreGetRaw(ref byte pDir, ref byte pIn, ref byte pOut, UInt32 NumBits);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINK_SWD_StoreRaw(ref byte pDir, ref byte pIn, UInt32 NumBits);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void JLINK_SWD_SyncBits();

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void JLINK_SWD_SyncBytes();

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_SWD_DisableSWCLK();

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_SWD_EnableSWCLK();

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_SWD_SetDirIn();

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_SWD_SetDirOut();

        #endregion

        #region SWO API

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_SWO_Control(UInt32 Cmd, ref IntPtr pData);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_SWO_DisableTarget(UInt32 PortMask);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_SWO_EnableTarget(UInt32 CPUSpeed, UInt32 SWOSpeed, Int32 Mode, UInt32 PortMask);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_SWO_GetCompatibleSpeeds(UInt32 CPUSpeed, UInt32 MaxSWOSpeed, ref UInt32 paSWOSpeed, UInt32 NumEntries);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void JLINKARM_SWO_Read(ref byte pData, UInt32 Offset, ref UInt32 pNumBytes);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_SWO_ReadStimulus(Int32 Port, ref byte pData, UInt32 NumBytes);

        #endregion

        #region TIF API

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void JLINKARM_TIF_GetAvailable(ref UInt32 pMask);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 JLINKARM_TIF_Select(JLink_TIF_Type Interface);

        #endregion

        #region Trace API

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 JLINKARM_TRACE_Control(UInt32 Cmd, ref UInt32 p);

        //[DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        //public static extern UInt32 JLINKARM_TRACE_Read(JLINKARM_TRACE_DATA* pData, UInt32 Off, ref UInt32 pNumItems);

        #endregion

        #region WA API

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern byte JLINKARM_WA_AddRange(UInt32 Addr, UInt32 NumBytes);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern byte JLINKARM_WA_Restore();

        //[DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        //public static extern void JLINK_SetFlashProgProgressCallback(JLINK_FLASH_PROGRESS_CB_FUNC* pfOnFlashProgess);

        #endregion
    }
}
