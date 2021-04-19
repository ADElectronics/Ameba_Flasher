using JLinkAccess;
using Ameba.Common.Model;
using Ameba.Common.Helpers;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;

namespace AmebaZ_Flasher.Models
{
    public class AmebaZ : FlasherBase
    {
        CallbackLogDelegate cb_log = null, cb_err = null;
        byte[] buffer;

        #region Публичные свойства

        public UInt32 TIFSpeed { get; set; } = 12000;
        public JLink_TIF_Type TIFType { get; set; } = JLink_TIF_Type.SWD;
        public UInt32 FlashSize { get; private set; } = AmebaZ_Addresses.FlashSize;
        public bool VerifyFlashWrite { get; set; }

        #region IsOpen
        bool _IsOpen;
        public override bool IsOpen
        {
            get { return _IsOpen; }
            set { SetProperty<bool>(ref _IsOpen, value); }
        }
        #endregion

        #region IsConnected
        bool _IsConnected;
        public override bool IsConnected
        {
            get { return _IsConnected; }
            set { SetProperty<bool>(ref _IsConnected, value); }
        }
        #endregion

        #region IsInited
        bool _IsInited;
        public override bool IsInited
        {
            get { return _IsInited; }
            set { SetProperty<bool>(ref _IsInited, value); }
        }
        #endregion
        
        #region FlashID
        UInt32 _FlashID;
        public UInt32 FlashID
        {
            get { return _FlashID; }
            set { SetProperty<UInt32>(ref _FlashID, value); }
        }
        #endregion
        
        #region MAC
        byte[] _MAC;
        public byte[] MAC
        {
            get { return _MAC; }
            set { SetProperty<byte[]>(ref _MAC, value); }
        }
        #endregion

        #region LastFilePath
        string _LastFilePath;
        public string LastFilePath
        {
            get { return _LastFilePath; }
            set { SetProperty<string>(ref _LastFilePath, value); }
        }
        #endregion

        #endregion

        #region Публичные свойства - Образы

        #region Image1FlashAddr
        UInt32 _Image1FlashAddr;
        public UInt32 Image1FlashAddr
        {
            get { return _Image1FlashAddr; }
            set { SetProperty<UInt32>(ref _Image1FlashAddr, value); }
        }
        #endregion

        #region Image1SegSize
        UInt32 _Image1SegSize;
        public UInt32 Image1SegSize
        {
            get { return _Image1SegSize; }
            set { SetProperty<UInt32>(ref _Image1SegSize, value); }
        }
        #endregion

        #region Image1LoadAddr
        UInt32 _Image1LoadAddr;
        public UInt32 Image1LoadAddr
        {
            get { return _Image1LoadAddr; }
            set { SetProperty<UInt32>(ref _Image1LoadAddr, value); }
        }
        #endregion

        #region Image1IsSignatureOK
        bool _Image1IsSignatureOK;
        public bool Image1IsSignatureOK
        {
            get { return _Image1IsSignatureOK; }
            set { SetProperty<bool>(ref _Image1IsSignatureOK, value); }
        }
        #endregion

        #region OTA1FlashAddr
        UInt32 _OTA1FlashAddr;
        public UInt32 OTA1FlashAddr
        {
            get { return _OTA1FlashAddr; }
            set { SetProperty<UInt32>(ref _OTA1FlashAddr, value); }
        }
        #endregion

        #region OTA1SegSize
        UInt32 _OTA1SegSize;
        public UInt32 OTA1SegSize
        {
            get { return _OTA1SegSize; }
            set { SetProperty<UInt32>(ref _OTA1SegSize, value); }
        }
        #endregion

        #region OTA1LoadAddr
        UInt32 _OTA1LoadAddr;
        public UInt32 OTA1LoadAddr
        {
            get { return _OTA1LoadAddr; }
            set { SetProperty<UInt32>(ref _OTA1LoadAddr, value); }
        }
        #endregion

        #region OTA1IsSignatureOK
        bool _OTA1IsSignatureOK;
        public bool OTA1IsSignatureOK
        {
            get { return _OTA1IsSignatureOK; }
            set { SetProperty<bool>(ref _OTA1IsSignatureOK, value); }
        }
        #endregion

        #region OTA2FlashAddr
        UInt32 _OTA2FlashAddr;
        public UInt32 OTA2FlashAddr
        {
            get { return _OTA2FlashAddr; }
            set { SetProperty<UInt32>(ref _OTA2FlashAddr, value); }
        }
        #endregion

        #region OTA2SegSize
        UInt32 _OTA2SegSize;
        public UInt32 OTA2SegSize
        {
            get { return _OTA2SegSize; }
            set { SetProperty<UInt32>(ref _OTA2SegSize, value); }
        }
        #endregion

        #region OTA2LoadAddr
        UInt32 _OTA2LoadAddr;
        public UInt32 OTA2LoadAddr
        {
            get { return _OTA2LoadAddr; }
            set { SetProperty<UInt32>(ref _OTA2LoadAddr, value); }
        }
        #endregion

        #region OTA2IsSignatureOK
        bool _OTA2IsSignatureOK;
        public bool OTA2IsSignatureOK
        {
            get { return _OTA2IsSignatureOK; }
            set { SetProperty<bool>(ref _OTA2IsSignatureOK, value); }
        }
        #endregion

        // System Data

        #region SysOTA2FlashAddr
        UInt32 _SysOTA2FlashAddr;
        public UInt32 SysOTA2FlashAddr
        {
            get { return _SysOTA2FlashAddr; }
            set { SetProperty<UInt32>(ref _SysOTA2FlashAddr, value); }
        }
        #endregion

        #region SysOTA2ValidSignature
        UInt32 _SysOTA2ValidSignature;
        public UInt32 SysOTA2ValidSignature
        {
            get { return _SysOTA2ValidSignature; }
            set { SetProperty<UInt32>(ref _SysOTA2ValidSignature, value); }
        }
        #endregion

        #region SysOTA1ForceGPIO
        UInt32 _SysOTA1ForceGPIO;
        public UInt32 SysOTA1ForceGPIO
        {
            get { return _SysOTA1ForceGPIO; }
            set { SetProperty<UInt32>(ref _SysOTA1ForceGPIO, value); }
        }
        #endregion

        #region SysRDPFlashAddr
        UInt32 _SysRDPFlashAddr;
        public UInt32 SysRDPFlashAddr
        {
            get { return _SysRDPFlashAddr; }
            set { SetProperty<UInt32>(ref _SysRDPFlashAddr, value); }
        }
        #endregion

        #region SysRDPLen
        UInt32 _SysRDPLen;
        public UInt32 SysRDPLen
        {
            get { return _SysRDPLen; }
            set { SetProperty<UInt32>(ref _SysRDPLen, value); }
        }
        #endregion

        #region SysSPIMode
        UInt16 _SysSPIMode;
        public UInt16 SysSPIMode
        {
            get { return _SysSPIMode; }
            set { SetProperty<UInt16>(ref _SysSPIMode, value); }
        }
        #endregion

        #region SysSPISpeed
        UInt16 _SysSPISpeed;
        public UInt16 SysSPISpeed
        {
            get { return _SysSPISpeed; }
            set { SetProperty<UInt16>(ref _SysSPISpeed, value); }
        }
        #endregion

        #region SysFlashID
        UInt16 _SysFlashID;
        public UInt16 SysFlashID
        {
            get { return _SysFlashID; }
            set { SetProperty<UInt16>(ref _SysFlashID, value); }
        }
        #endregion

        #region SysFlashSize
        UInt16 _SysFlashSize;
        public UInt16 SysFlashSize
        {
            get { return _SysFlashSize; }
            set { SetProperty<UInt16>(ref _SysFlashSize, value); }
        }
        #endregion

        #region SysLogBaudrate
        UInt32 _SysLogBaudrate;
        public UInt32 SysLogBaudrate
        {
            get { return _SysLogBaudrate; }
            set { SetProperty<UInt32>(ref _SysLogBaudrate, value); }
        }
        #endregion

        // USB ...

        // ADC ...

        #endregion

        #region Публичные события

        public override event CallbackLogDelegate CallbackLog;
        public override event CallbackLogDelegate CallbackError;

        #endregion

        #region Конструктор
        public AmebaZ()
        {
            MAC = new byte[6];

            ResetAllAddresses();
            ResetBuffer();
        }
        #endregion

        #region Работа с файлами

        public bool LoadFromFile()
        {
            bool status = false;

            var ofd = new Microsoft.Win32.OpenFileDialog()
            {
                Filter = "BIN Files (*.bin)|*.bin"
            };

            if (ofd.ShowDialog() == true)
            {
                status = true;
                LastFilePath = ofd.FileName;

                ResetBuffer();
                using (FileStream fs = new FileStream(ofd.FileName, FileMode.Open, FileAccess.Read))
                {
                    CallbackLog?.Invoke(String.Format("Loading file {0} ...", ofd.FileName));     
                    if (fs.Length <= FlashSize)
                    {
                        buffer = new byte[FlashSize/*fs.Length*/];
                        fs.Read(buffer, 0, (int)fs.Length);
                    }
                    else
                    {
                        CallbackError?.Invoke(String.Format("Loading file {0} Error! File size ({1}) > Flash Size ({2}).", ofd.FileName, fs.Length, FlashSize));
                    }
                   /* 
                    UInt32[] image = new UInt32[buffer.Length / 4];
                    for (UInt32 i = 0; i < image.Length; i++)
                        image[i] = (UInt32)(buffer[i * 4 + 3] << 24 | buffer[i * 4 + 2] << 16 | buffer[i * 4 + 1] << 8 | buffer[i * 4 + 0]);
                   */
                }
            }

            return status;
        }

        public bool ReloadFromFile()
        {
            bool status = false;

            if (!String.IsNullOrEmpty(LastFilePath))
            {
                status = true;

                ResetBuffer();
                using (FileStream fs = new FileStream(LastFilePath, FileMode.Open, FileAccess.Read))
                {
                    CallbackLog?.Invoke(String.Format("Loading file {0} ...", LastFilePath));

                }
            }

            return status;
        }

        public bool SaveToFile()
        {
            bool status = false;

            var sfd = new Microsoft.Win32.SaveFileDialog()
            {
                Filter = "BIN Files (*.bin)|*.bin",
                OverwritePrompt = false
            };

            if (sfd.ShowDialog() == true)
            {
                status = true;

                CallbackLog?.Invoke(String.Format("Save to file {0} ...", sfd.FileName));
                using (FileStream fs = new FileStream(sfd.FileName, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    fs.Write(buffer, 0, (Int32)FlashSize);
                }
            }

            return status;
        }

        #endregion

        #region Базовые методы для работы с SoC

        public override bool OpenDebugger()
        {
            bool status = false;
            if (cb_log == null & CallbackLog != null)
                cb_log = new CallbackLogDelegate(CallbackLog);
            if (cb_err == null & CallbackError != null)
                cb_err = new CallbackLogDelegate(CallbackError);

            cb_log?.Invoke("Open Debugger ...");
            try
            {
                string err = JLinkARM.JLINKARM_OpenEx(cb_log, cb_err);

                if (err != null)
                {
                    status = false;
                    cb_err?.Invoke("JLINKARM_OpenEx Error!");
                    return status;
                }
                JLinkARM.JLINKARM_SetSpeed(TIFSpeed);
                //JLinkARM.JLINKARM_SetSpeed(1000);
                JLinkARM.JLINKARM_TIF_Select(TIFType);
                JLinkARM.JLINKARM_SelectDeviceFamily(JLink_Device_Family.Cortex_M4);
                JLinkARM.JLINKARM_Reset();
                JLinkARM.JLINKARM_Go();

                status = true;
            }
            catch (Exception ex)
            {
                cb_err?.Invoke(ex.Message);
            }

            IsOpen = status;
            return status;
        }

        public override bool OpenDebugger(JLink_TIF_Type TIF, UInt32 Speed)
        {
            TIFSpeed = Speed;
            TIFType = TIF;
            return OpenDebugger();
        }

        public override bool Connect()
        {
            bool status = false;

            if (IsOpen & JLinkARM.JLINKARM_IsConnected() > 0)
            {
                JLinkARM.JLINKARM_SetSpeed(TIFSpeed);
                status = true;
            }

            IsConnected = status;
            return status;
        }

        public override void Disconnect()
        {
            if (IsConnected)
            {
                JLinkARM.JLINKARM_Close();
                IsOpen = false;
                IsConnected = false;
                IsInited = false;
            }
        }

        public override bool InitTargert()
        {
            bool status = false;
            UInt32 data = 0;

            FlashID = 0x0;

            cb_log?.Invoke("Init SoC ...");

            //JLinkARM.JLINKARM_Reset();
            JLinkARM.JLINKARM_Halt();
            ReadFlashID();

            // RAM test
            /*
            UInt32[] test = new UInt32[200];
            for (UInt16 i = 0; i < test.Length; i++)
                test[i] = i;
            WriteU32((UInt32)AmebaZ_Addresses.RAMAddr, (UInt32)test.Length, test);
            WriteU32((UInt32)AmebaZ_Addresses.RAMAddr, 0x11002200);

            UInt32[] test_r = new UInt32[200];
            ReadU32((UInt32)AmebaZ_Addresses.RAMAddr, 200, ref test_r);
            */

            if (IsConnected)
            {
                // System Init
                ReadU32(0x40000210, ref data);
                data &= 0xe7ff_ffff; // 27, 28
                data |= 0x0400_0000; // 26
                WriteU32(0x40000210, data);

                // Load Flasher
                WriteU32(AmebaZ_Addresses.RAMImage1Addr, (UInt32)AmebaZ_Code.Flasher.Length, AmebaZ_Code.Flasher);

                JLinkARM.JLINKARM_WriteReg((UInt32)JLink_ARM_CM4_Register.FAULTMASK, 0x00);
                JLinkARM.JLINKARM_WriteReg((UInt32)JLink_ARM_CM4_Register.R13, (UInt32)AmebaZ_Addresses.Stack); // Stack pointer
                JLinkARM.JLINKARM_WriteReg((UInt32)JLink_ARM_CM4_Register.R15, (UInt32)AmebaZ_Addresses.FirmwareAddr); // Program counter
                JLinkARM.JLINKARM_Go();
                status = true;
            }

            IsInited = status;
            return status;
        }

        public override void Restart()
        {
            if (IsConnected)
            {
                cb_log?.Invoke("Restart SoC ...");

                JLinkARM.JLINKARM_Reset();
                JLinkARM.JLINKARM_Go();

                IsInited = false;
            }
        }

        #endregion

        #region Базовые методы для чтения и записи памяти
        public override bool WriteU8(UInt32 Addr, UInt32 Count, byte[] data)
        {
            GCHandle addr_hndl = GCHandle.Alloc(data, GCHandleType.Pinned);
            IntPtr addr_data = addr_hndl.AddrOfPinnedObject();
            bool status = (JLinkARM.JLINKARM_WriteMem(Addr, Count, addr_data) >= 0) ? true : false;
            addr_hndl.Free();
            return status;
        }

        public override bool WriteU8(UInt32 Addr, byte data)
        {
            bool status = (JLinkARM.JLINKARM_WriteU8(Addr, data) == 0 ? true : false);
            return status;
        }

        public override bool ReadU8(UInt32 Addr, UInt32 Count, ref byte[] data)
        {
            GCHandle addr_hndl = GCHandle.Alloc(data, GCHandleType.Pinned);
            IntPtr addr_data = addr_hndl.AddrOfPinnedObject();
            bool status = (JLinkARM.JLINKARM_ReadMem(Addr, Count, addr_data) >= 0) ? true : false;
            addr_hndl.Free();
            return status;
        }

        public override bool ReadU8(UInt32 Addr, ref byte data)
        {
            GCHandle addr_hndl = GCHandle.Alloc(data, GCHandleType.Pinned);
            IntPtr addr_data = addr_hndl.AddrOfPinnedObject();
            bool status = (JLinkARM.JLINKARM_ReadMem(Addr, 1, addr_data) >= 0) ? true : false;
            addr_hndl.Free();
            return status;
        }

        public override bool WriteU32(UInt32 Addr, UInt32 Count, UInt32[] data)
        {
            GCHandle addr_hndl = GCHandle.Alloc(data, GCHandleType.Pinned);
            IntPtr addr_data = addr_hndl.AddrOfPinnedObject();
            UInt32 size = Count * 4;
            bool status = (JLinkARM.JLINKARM_WriteMem(Addr, size, addr_data) >= 0) ? true : false;
            addr_hndl.Free();
            return status;
        }

        public override bool WriteU32(UInt32 Addr, UInt32 data)
        {
            bool status = (JLinkARM.JLINKARM_WriteU32(Addr, data) == 0) ? true : false;
            return status;
        }

        public override bool ReadU32(UInt32 Addr, UInt32 Count, ref UInt32[] data)
        {
            GCHandle addr_hndl = GCHandle.Alloc(data, GCHandleType.Pinned);
            IntPtr addr_data = addr_hndl.AddrOfPinnedObject();
            bool status = (JLinkARM.JLINKARM_ReadMemU32(Addr, Count, addr_data, IntPtr.Zero) >= 0) ? true : false;
            addr_hndl.Free();
            return status;
        }

        public override bool ReadU32(UInt32 Addr, ref UInt32 data)
        {
            UInt32[] data_r = new UInt32[2]; // TODO: Костыль, иначе по IntPtr всегда возвращается 0х0 ... 
            GCHandle addr_hndl = GCHandle.Alloc(data_r, GCHandleType.Pinned);
            IntPtr addr_data = addr_hndl.AddrOfPinnedObject();
            bool status = (JLinkARM.JLINKARM_ReadMemU32(Addr, 1, addr_data, IntPtr.Zero) >= 0);
            addr_hndl.Free();
            data = data_r[0];
            /*
            byte[] data_b = new byte[8];
            GCHandle addr_hndl = GCHandle.Alloc(data_b, GCHandleType.Pinned);
            IntPtr addr_data = addr_hndl.AddrOfPinnedObject();
            bool status = (JLinkARM.JLINKARM_ReadMemHW(Addr, 8, addr_data) == 0);
            //data = (UInt32)(data_b[7] | data_b[6] << 4 | data_b[5] << 8 | data_b[4] << 12 | data_b[3] << 16 | data_b[2] << 20 | data_b[1] << 24 | data_b[0] << 28);
            data = (UInt32)(data_b[0] | data_b[1] << 4 | data_b[2] << 8 | data_b[3] << 12 | data_b[4] << 16 | data_b[5] << 20 | data_b[6] << 24 | data_b[7] << 28);
            addr_hndl.Free();
            */
            return status;
        }

        public override bool VerifyU32(UInt32 Addr, UInt32 Count, UInt32[] data)
        {
            bool status = false;
            return status;
        }
        public override bool VerifyU32(UInt32 Addr, UInt32 data)
        {
            bool status = false;
            return status;
        }
        #endregion

        #region Работа с образами и их валидностью

        static T BytesToStruct<T>(byte[] rawData, UInt32 addr) where T : struct
        {
            T result = default(T);
            byte[] data_cut = new byte[Marshal.SizeOf<T>()]; // TODO: имхо, костыль, иначе Marshal.PtrToStructure ведёт себя некорректно
            Array.Copy(rawData, addr, data_cut, 0, Marshal.SizeOf<T>());
            GCHandle handle = GCHandle.Alloc(data_cut, GCHandleType.Pinned);

            try
            {
                IntPtr rawDataPtr = handle.AddrOfPinnedObject();
                result = Marshal.PtrToStructure<T>(rawDataPtr);
            }
            finally
            {
                handle.Free();
            }

            return result;
        }

        public void ResetAllAddresses()
        {

        }

        public void ResetBuffer()
        {
            if (buffer == null)
                buffer = new byte[FlashSize];

            for (UInt32 i = 0; i < FlashSize; i++)
                buffer[i] = 0xff;
        }

        public void UpdateCalibrationRegion()
        {

        }

        public void CheckImages()
        {
            Boot_Head boot = BytesToStruct<Boot_Head>(buffer, AmebaZ_Addresses.BootAddr);
            Image1FlashAddr = AmebaZ_Addresses.BootAddr; 
            Image1IsSignatureOK = (AmebaZ_Code.BootSignature == boot.Signature);

            if(Image1IsSignatureOK)
            {
                Image1LoadAddr = boot.LoadAddr;
                Image1SegSize = boot.Size;
            }
            else
            {
                UInt32 bootsyssize = AmebaZ_Addresses.BootSize + AmebaZ_Addresses.SystemDataSize + 0x2000 /* reserved x2 */;

                CallbackLog?.Invoke(String.Format("Boot Signature is not found, try to shift image at 0x{0:X} ...", bootsyssize));
                Array.Copy(buffer, 0, buffer, AmebaZ_Addresses.OTA1Addr, bootsyssize);
                for (UInt32 i = 0; i < bootsyssize; i++)
                    buffer[i] = 0xff;

                // TODO: Проверять ли сразу на сигнатуры и двигать файл далее ?
            }

            System_Data sys = BytesToStruct<System_Data>(buffer, AmebaZ_Addresses.SystemDataAddr);
            SysOTA2FlashAddr = sys.OTA2FlashAddr;
            SysOTA1ForceGPIO = sys.OTA1Force;
            SysSPIMode = sys.SPIMode;
            SysSPISpeed = sys.SPISpeed;
            SysFlashID = sys.FlashID;
            SysFlashSize = sys.FlashSize;
            SysRDPFlashAddr = sys.RDPFlashAddr;
            SysRDPLen = sys.RDPLen;
            SysLogBaudrate = sys.LogBaudrate;

            Image_Head ota1 = BytesToStruct<Image_Head>(buffer, AmebaZ_Addresses.OTA1Addr);
            OTA1FlashAddr = AmebaZ_Addresses.OTA1Addr;
            OTA1IsSignatureOK = (AmebaZ_Code.ImageSignature == ota1.Signature);

            if(OTA1IsSignatureOK)
            {
                OTA1LoadAddr = ota1.LoadAddr;
                OTA1SegSize = ota1.Size;
            }
            
            Image_Head ota2 = BytesToStruct<Image_Head>(buffer, AmebaZ_Addresses.OTA2Addr);
            OTA2FlashAddr = AmebaZ_Addresses.OTA2Addr;
            OTA2IsSignatureOK = (AmebaZ_Code.ImageSignature == ota2.Signature);

            if (OTA2IsSignatureOK)
            {
                OTA2LoadAddr = ota2.LoadAddr;
                OTA2SegSize = ota2.Size;
            }
        }

        #endregion

        #region Методы для работы с Flash памятью SoC
        public void ReadFlashID()
        {
            CallbackLog?.Invoke("Read Flash ID ... (fake!!!)");
            FlashID = 0x141520C2;

            CheckFlashID();
        }

        public void CheckFlashID()
        {
            // Memory Density = ((FlashID >> 16) & 0xFF)
            // Memory Type = ((FlashID >> 8) & 0xFF)
            // Manufacturer ID = (FlashID & 0xFF)
            // Electronic ID = ((FlashID >> 24) & 0xFF)

            //if (FlashID == 0x141520C2)
            //    FlashSize = 0x200000;
        }

        public bool FlashWrite()
        {
            CallbackLog?.Invoke("Full Flash Write ...");
            return FlashWrite(0x0000 /*AmebaZ_Addresses.FlashAddr*/, FlashSize, buffer);
            //return FlashWrite(0x0000 /*AmebaZ_Addresses.FlashAddr*/, 1024/*FlashSize*/, buffer);
        }

        public bool FlashWrite(UInt32 Addr, UInt32 Size)
        {
            return FlashWrite(Addr, Size, buffer);
        }

        public bool FlashWrite(UInt32 Addr, UInt32 Size, byte[] Data)
        {
            bool status = false;
            Int32 Size_Left = (Int32)Size;
            Int32 len_actual = 0;
            UInt32 offset = 0;

            if (IsInited & Data.Length >= Size & Size > 0)
            {
                CallbackLog?.Invoke(String.Format("Flash Write at 0x{0:X}, size {1}", Addr, Size));
                status = true;
                TimeSpan time = StopwatchUtil.Time(() =>
                {
                    //JLinkARM.JLINKARM_SetBP(0, AmebaZ_Addresses.FlasherBreakpoint);
                    JLinkARM.JLINKARM_SetBPEx(AmebaZ_Addresses.FlasherBreakpoint, (0x000000F0 | 0xFFFFFF00 | 0x1));
                    //Thread.Sleep(25);
                    //JLinkARM.JLINKARM_GoEx(0x8000_0000, 1);

                    while (status)
                    {
                        len_actual = Size_Left;

                        if (len_actual > AmebaZ_Addresses.FlasherDataLen)
                            len_actual = (Int32)AmebaZ_Addresses.FlasherDataLen;

                        int halted = JLinkARM.JLINKARM_WaitForHalt(2000);
                        if (halted == 0)
                        {
                            CallbackError?.Invoke(String.Format("Wait for flasher Timeout!"));
                            status = false;
                            break;
                        }
                        else if (halted < 0)
                        {
                            CallbackError?.Invoke(String.Format("SoC Halt Error!"));
                            status = false;
                            break;
                        }

                        byte[] Data_RAW = new byte[len_actual];
                        Array.Copy(Data, offset, Data_RAW, 0, len_actual);

                        GCHandle Data_RAW_hndl = GCHandle.Alloc(Data_RAW, GCHandleType.Pinned);
                        IntPtr Data_RAW_Ptr = Data_RAW_hndl.AddrOfPinnedObject();
                        JLinkARM.JLINKARM_WriteMem(AmebaZ_Addresses.FlasherData, (UInt32)len_actual, Data_RAW_Ptr);
                        Data_RAW_hndl.Free();

                        WriteU32(AmebaZ_Addresses.FlasherWriteAddr, Addr + offset);
                        WriteU32(AmebaZ_Addresses.FlasherBlockWriteSize, (UInt32)len_actual); // старт записи

                        Size_Left -= len_actual;
                        offset += (UInt32)len_actual;

                        if(Size_Left == 0)
                            break;

                        //JLinkARM.JLINKARM_Go();
                        JLinkARM.JLINKARM_GoEx(0x8000_0000, 1);
                    }
                });

                WriteU8(AmebaZ_Addresses.FlasherSetComplete, 0x01); // выходим из загрузчика
                //JLinkARM.JLINKARM_Go();
                JLinkARM.JLINKARM_GoEx(0x8000_0000, 1);
                CallbackLog?.Invoke(String.Format("Time {0:0.00} ms, Speed {1:0.00} KB/s", time.TotalMilliseconds, (double)((double)Size / time.TotalMilliseconds)));
            }

            return status;
        }

        public bool FlashRead()
        {
            //return FlashRead(AmebaZ_Addresses.FlashAddr, 0x2000/*FlashSize*/, ref buffer);
            return FlashRead(AmebaZ_Addresses.FlashAddr, FlashSize, ref buffer);
        }

        public bool FlashRead(UInt32 Addr, UInt32 Size, ref byte[] Data)
        {
            bool status = false;

            if (Data.Length >= Size)
            {
                CallbackLog?.Invoke(String.Format("Flash Read at 0x{0:X}, size {1}", Addr, Size));
                Stopwatch stopwatch = Stopwatch.StartNew();
                ReadU8(Addr, Size, ref Data);
                stopwatch.Stop();
                CallbackLog?.Invoke(String.Format("Time {0:0.00} ms, speed {1:0.00} KB/s", stopwatch.Elapsed.TotalMilliseconds, (double)((double)Size / stopwatch.Elapsed.TotalMilliseconds)));
                CallbackLog?.Invoke("Flash Read Done!");
                status = true;
            }

            return status;
        }

        public bool EraseSector(UInt32 Sector)
        {
            bool status = false;

            return status;
        }

        public bool EraseAll()
        {
            bool status = false;

            return status;
        }

        #endregion

    }
}
