using JLinkAccess;
using Ameba.Common.Model;
using Ameba.Common.Helpers;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace AmebaA_Flasher.Model
{
    public class AmebaA : FlasherBase
    {
        CallbackLogDelegate cb_log = null, cb_err = null;
        
        byte[] buffer;
        //byte[] buffer_otp = new byte[AmebaA_Addresses.OTPSize];

        #region Публичные свойства

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
        /*
        #region JedecID
        UInt32 _JedecID;
        public UInt32 JedecID
        {
            get { return _JedecID; }
            set { SetProperty<UInt32>(ref _JedecID, value); }
        }
        #endregion
        */
        #region MAC
        byte[] _MAC;
        public byte[] MAC
        {
            get { return _MAC; }
            set { SetProperty<byte[]>(ref _MAC, value); }
        }
        #endregion

        #region Images Properties

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

        #region Image2FlashAddr
        UInt32 _Image2FlashAddr;
        public UInt32 Image2FlashAddr
        {
            get { return _Image2FlashAddr; }
            set { SetProperty<UInt32>(ref _Image2FlashAddr, value); }
        }
        #endregion

        #region Image2SegSize
        UInt32 _Image2SegSize;
        public UInt32 Image2SegSize
        {
            get { return _Image2SegSize; }
            set { SetProperty<UInt32>(ref _Image2SegSize, value); }
        }
        #endregion

        #region Image2LoadAddr
        UInt32 _Image2LoadAddr;
        public UInt32 Image2LoadAddr
        {
            get { return _Image2LoadAddr; }
            set { SetProperty<UInt32>(ref _Image2LoadAddr, value); }
        }
        #endregion

        #region Image2InfraStart
        UInt32 _Image2InfraStart;
        public UInt32 Image2InfraStart
        {
            get { return _Image2InfraStart; }
            set { SetProperty<UInt32>(ref _Image2InfraStart, value); }
        }
        #endregion

        #region Image2CustomerSig
        string _Image2CustomerSig;
        public string Image2CustomerSig
        {
            get { return _Image2CustomerSig; }
            set { SetProperty<string>(ref _Image2CustomerSig, value); }
        }
        #endregion

        #region Image3FlashAddr
        UInt32 _Image3FlashAddr;
        public UInt32 Image3FlashAddr
        {
            get { return _Image3FlashAddr; }
            set { SetProperty<UInt32>(ref _Image3FlashAddr, value); }
        }
        #endregion

        #region Image3SegSize
        UInt32 _Image3SegSize;
        public UInt32 Image3SegSize
        {
            get { return _Image3SegSize; }
            set { SetProperty<UInt32>(ref _Image3SegSize, value); }
        }
        #endregion

        #region Image3LoadAddr
        UInt32 _Image3LoadAddr;
        public UInt32 Image3LoadAddr
        {
            get { return _Image3LoadAddr; }
            set { SetProperty<UInt32>(ref _Image3LoadAddr, value); }
        }
        #endregion

        #region Image3CustomerSig
        string _Image3CustomerSig;
        public string Image3CustomerSig
        {
            get { return _Image3CustomerSig; }
            set { SetProperty<string>(ref _Image3CustomerSig, value); }
        }
        #endregion

        #region ImageOTAFlashAddr
        UInt32 _ImageOTAFlashAddr;
        public UInt32 ImageOTAFlashAddr
        {
            get { return _ImageOTAFlashAddr; }
            set { SetProperty<UInt32>(ref _ImageOTAFlashAddr, value); }
        }
        #endregion

        #region ImageOTASegSize
        UInt32 _ImageOTASegSize;
        public UInt32 ImageOTASegSize
        {
            get { return _ImageOTASegSize; }
            set { SetProperty<UInt32>(ref _ImageOTASegSize, value); }
        }
        #endregion

        #region ImageOTALoadAddr
        UInt32 _ImageOTALoadAddr;
        public UInt32 ImageOTALoadAddr
        {
            get { return _ImageOTALoadAddr; }
            set { SetProperty<UInt32>(ref _ImageOTALoadAddr, value); }
        }
        #endregion

        #region ImageOTAInfraStart
        UInt32 _ImageOTAInfraStart;
        public UInt32 ImageOTAInfraStart
        {
            get { return _ImageOTAInfraStart; }
            set { SetProperty<UInt32>(ref _ImageOTAInfraStart, value); }
        }
        #endregion

        #region ImageOTACustomerSig
        string _ImageOTACustomerSig;
        public string ImageOTACustomerSig
        {
            get { return _ImageOTACustomerSig; }
            set { SetProperty<string>(ref _ImageOTACustomerSig, value); }
        }
        #endregion

        #region Image1CalibrationIsOK
        bool _Image1CalibrationIsOK;
        public bool Image1CalibrationIsOK
        {
            get { return _Image1CalibrationIsOK; }
            set { SetProperty<bool>(ref _Image1CalibrationIsOK, value); }
        }
        #endregion

        #region Image1SPICIsOK
        bool _Image1SPICIsOK;
        public bool Image1SPICIsOK
        {
            get { return _Image1SPICIsOK; }
            set { SetProperty<bool>(ref _Image1SPICIsOK, value); }
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

        #region Image2IsSignatureOK
        bool _Image2IsSignatureOK;
        public bool Image2IsSignatureOK
        {
            get { return _Image2IsSignatureOK; }
            set { SetProperty<bool>(ref _Image2IsSignatureOK, value); }
        }
        #endregion

        #region Image3IsSignatureOK
        bool _Image3IsSignatureOK;
        public bool Image3IsSignatureOK
        {
            get { return _Image3IsSignatureOK; }
            set { SetProperty<bool>(ref _Image3IsSignatureOK, value); }
        }
        #endregion

        #region ImageOTAIsSignatureOK
        bool _ImageOTAIsSignatureOK;
        public bool ImageOTAIsSignatureOK
        {
            get { return _ImageOTAIsSignatureOK; }
            set { SetProperty<bool>(ref _ImageOTAIsSignatureOK, value); }
        }
        #endregion

        #endregion

        #region LastFilePath
        string _LastFilePath;
        public string LastFilePath
        {
            get { return _LastFilePath; }
            set { SetProperty<string>(ref _LastFilePath, value); }
        }
        #endregion

        public bool VerifyFlashWrite { get; set; }
        public AmebaA_Restart_Type RestartType { get; set; } = AmebaA_Restart_Type.RAM_Start;
        public UInt32 TIFSpeed { get; set; } = 12000;
        public JLink_TIF_Type TIFType { get; set; } = JLink_TIF_Type.JTAG;
        public UInt32 FlashSize { get; private set; } = AmebaA_Addresses.FlashSize;

        #endregion

        #region Публичные события

        public override event CallbackLogDelegate CallbackLog;
        public override event CallbackLogDelegate CallbackError;

        #endregion

        #region Конструктор
        public AmebaA()
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

            if(ofd.ShowDialog() == true)
            {
                status = true;
                LastFilePath = ofd.FileName;

                ResetBuffer();
                using (FileStream fs = new FileStream(ofd.FileName, FileMode.Open, FileAccess.Read))
                {
                    CallbackLog?.Invoke(String.Format("Loading file {0} ...", ofd.FileName));
                    if (fs.Length <= FlashSize)
                    {
                        buffer = new byte[FlashSize];
                        fs.Read(buffer, 0, (int)fs.Length);
                    }
                    else if (fs.Length <= AmebaA_Addresses.FlashAddr * 2)
                    {
                        buffer = new byte[AmebaA_Addresses.FlashAddr * 2];
                        FlashSize = AmebaA_Addresses.FlashAddr * 2;
                        fs.Read(buffer, 0, (int)fs.Length);
                    }
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
                    if (fs.Length <= FlashSize)
                    {
                        buffer = new byte[FlashSize];
                        fs.Read(buffer, 0, (int)fs.Length);
                    }
                    else if (fs.Length <= AmebaA_Addresses.FlashAddr * 2)
                    {
                        buffer = new byte[AmebaA_Addresses.FlashAddr * 2];
                        FlashSize = AmebaA_Addresses.FlashAddr * 2;
                        fs.Read(buffer, 0, (int)fs.Length);
                    }
                }
            }

            return status;
        }

        public bool SaveToFile()
        {
            bool status = false;

            var sfd = new Microsoft.Win32.SaveFileDialog()
            {
                Filter = "BIN Files (*.bin)|*.bin"
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
                //JLinkARM.JLINKARM_SetSpeed(TIFSpeed);
                JLinkARM.JLINKARM_SetSpeed(1000);
                JLinkARM.JLINKARM_TIF_Select(TIFType);
                JLinkARM.JLINKARM_SelectDeviceFamily(JLink_Device_Family.Cortex_M3);
                //JLinkARM.JLINKARM_SetResetType(JLink_Reset_Type.Reset_Pin);
                //JLinkARM.JLINKARM_ExecCommand("endian little", IntPtr.Zero, 0);
                //JLinkARM.JLINKARM_SetResetDelay(2);
                JLinkARM.JLINKARM_Reset();

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

            if(IsOpen & JLinkARM.JLINKARM_IsConnected() > 0) // JLinkARM.JLINKARM_Connect() >= 0
            {
                JLinkARM.JLINKARM_SetSpeed(TIFSpeed);
                status = true;
            }

            IsConnected = status;
            return status;
        }

		public override void Disconnect()
        {
            if(IsConnected)
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
            //JedecID = 0x0;

            cb_log?.Invoke("Init SoC ...");

            JLinkARM.JLINKARM_Reset();
            //JLinkARM.JLINKARM_ResetNoHalt();

            // TEST
            //UInt32[] test = new UInt32[200];
            //WriteU32((UInt32)AmebaA_Flasher.RAMAddr, 4, new UInt32[] { 0x11aa22bb , 0x33cc44dd , 0x66778899, 0x11223344 });
            //WriteU32((UInt32)AmebaA_Flasher.RAMAddr, 0x11002200);
            //ReadU32((UInt32)AmebaA_Flasher.RAMAddr, ref data);
            //ReadU32((UInt32)AmebaA_Flasher.RAMAddr, 200, ref test);

            if (IsConnected)
            {
                // System Init
                WriteU32(0x40000304, 0x1FC00002);
                WriteU32(0x40000250, 0x400);
                WriteU32(0x40000340, 0x0);
                WriteU32(0x40000230, 0xdcc4);
                WriteU32(0x40000210, 0x11113);
                WriteU32(0x400002c0, 0x110011);
                WriteU32(0x40000320, 0xffffffff);

                WriteU32(0x40000014, 0x011); // Set CLK 166.66 MHz

                status = ReadU32(0x40000230, ref data);

                if (status)
                {
                    // enable spi flash peripheral clock
                    data |= 0x300;
                    WriteU32(0x40000230, data);

                    // enable spi flash peripheral
                    ReadU32(0x40000210, ref data);
                    data &= 0x0FFFFFF;
                    data |= 0x10;
                    WriteU32(0x40000210, data);

                    // select spi flash pinout (0 - internal), enable spi flash pins 
                    ReadU32(0x400002C0, ref data);
                    data &= 0xFFFFFFF8; // 0xFFFFFFF9
                    data |= 0x1;
                    WriteU32(0x400002C0, data);

                    WriteU32(0x40006008, 0x0); // disable SPI FLASH operation
                    WriteU32(0x4000602C, 0x0); // disable all interrupts
                    WriteU32(0x40006010, 0x1); // use first "slave select" pin
                    WriteU32(0x40006014, 0x2); // baud rate, default value
                    WriteU32(0x40006018, 0x0); // tx fifo threshold
                    WriteU32(0x4000601C, 0x0); // rx fifo threshold
                    WriteU32(0x4000604C, 0x0); // disable DMA 

                    WriteU32(AmebaA_Addresses.Addr + 0x08, 0);
                    WriteU32(AmebaA_Addresses.Addr + 0x00, 1);
                    WriteU32(AmebaA_Addresses.FirmwareAddr, (UInt32)AmebaA_Code.Flasher.Length, AmebaA_Code.Flasher);
                    // Если нажали сброс в пустую RAM, то запустит RTLConcoleRAM
                    WriteU32(AmebaA_Addresses.RAMBootAddr, (UInt32)AmebaA_Code.Console.Length, AmebaA_Code.Console);

                    JLinkARM.JLINKARM_WriteReg((UInt32)JLink_ARM_CM3_Register.FAULTMASK, 0x00);//0x01
                    JLinkARM.JLINKARM_WriteReg((UInt32)JLink_ARM_CM3_Register.R13, (UInt32)AmebaA_Addresses.Stack); // $sp
                    JLinkARM.JLINKARM_WriteReg((UInt32)JLink_ARM_CM3_Register.R15, (UInt32)AmebaA_Addresses.FirmwareAddr); // $pc 
                    JLinkARM.JLINKARM_Go();
                    //cb_log?.Invoke(String.Format("Is halted ? {0}", JLinkARM.JLINKARM_IsHalted()));
                    status = false;

                    if (WaitTarget())
                    {
                        ReadFlashID();
                        //ReadU32(AmebaA_Flasher.Addr + 0x0C, ref data);
                        //FlashID = (UInt32)((data & 0xFFFFFF) | (FlashWriteReadCMD(0xAB, 3, 1) << 24));
                        CheckFlashID();
                        status = true;
                    }
                }
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
                if (FlashID == 0x141520C2)
                    WriteU32(0x40000210, AmebaA_Code.Reg210B[(byte)RestartType]);
                else
                    WriteU32(0x40000210, AmebaA_Code.Reg210A[(byte)RestartType]);
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
            bool status = false;
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
            bool status = false;
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
            /*
            byte[] data_b = new byte[4];
            GCHandle addr_hndl = GCHandle.Alloc(data_b, GCHandleType.Pinned);
            IntPtr addr_data = addr_hndl.AddrOfPinnedObject();
            bool status = (JLinkARM.JLINKARM_ReadMem(Addr, 4, addr_data) == 0) ? true : false;
            data = (UInt32)(data_b[0] | (data_b[1] << 8) | (data_b[2] << 16) | (data_b[3] << 24));
            addr_hndl.Free();
            */

            UInt32[] data_r = new UInt32[2]; // TODO: Костыль, иначе по IntPtr всегда возвращается 0х0 ... 
            GCHandle addr_hndl = GCHandle.Alloc(data_r, GCHandleType.Pinned);
            IntPtr addr_data = addr_hndl.AddrOfPinnedObject();
            bool status = (JLinkARM.JLINKARM_ReadMemU32(Addr, 1, addr_data, IntPtr.Zero) >= 0);
            addr_hndl.Free();
            data = data_r[0];

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
            Image1SegSize = 0x00;
            Image1FlashAddr = 0x00;
            Image1LoadAddr = 0x00;

            Image2SegSize = 0x00;
            Image2FlashAddr = 0x00;
            Image2LoadAddr = 0x00;

            Image3SegSize = 0x00;
            Image3FlashAddr = 0x00;
            Image3LoadAddr = 0x00;

            ImageOTASegSize = 0x00;
            ImageOTALoadAddr = 0x00;
            ImageOTAFlashAddr = 0x00;

            Image1IsSignatureOK = false;
            Image2IsSignatureOK = false;
            Image3IsSignatureOK = false;
            ImageOTAIsSignatureOK = false;
            Image1CalibrationIsOK = false;

            Image2CustomerSig = String.Empty;
            Image3CustomerSig = String.Empty;
            ImageOTACustomerSig = String.Empty;

            for (byte i = 0; i < MAC.Length; i++)
                MAC[i] = 0xFF;
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
            buffer[AmebaA_Addresses.CalibrationDataAddr + 1] = (byte)(AmebaA_Code.CalibrationSignature & 0xFF);
            buffer[AmebaA_Addresses.CalibrationDataAddr] = (byte)(AmebaA_Code.CalibrationSignature >> 8);

            for (byte i = 0; i < MAC.Length; i++)
                buffer[AmebaA_Addresses.CalibrationDataAddr + 0x88 + i] = MAC[i];
        }

        public void CheckImages()
        {
            Boot_Head boot = BytesToStruct<Boot_Head>(buffer, (UInt32)(0x00 + Marshal.SizeOf<Flash_Head>()));
            Flash_Head image1_head = BytesToStruct<Flash_Head>(buffer, 0x00);
            Image_Head image2_head;
            Image_Head image3_head;
            Image_Head imageOTA_head;
            Image_Info image2_info;
            Image_Info imageOTA_info; 

            ResetAllAddresses();

            Image1SPICIsOK = Enumerable.SequenceEqual(image1_head.SPICPattern, AmebaA_Code.SPICCalibrationPattern);
            Image1IsSignatureOK = (boot.Signature == AmebaA_Code.BootSignature);

            if (Image1SPICIsOK)
            {
                Image1FlashAddr = 0x00;//AmebaA_Flasher.FlashAddr;
                Image1SegSize = (UInt32)(image1_head.Size + Marshal.SizeOf<Flash_Head>() + Marshal.SizeOf<Boot_Head>());
                Image1LoadAddr = image1_head.LoadAddr;
                Image2FlashAddr = (image1_head.Image2Addr & 0xffff) * 1024;

                image2_head = BytesToStruct<Image_Head>(buffer, Image2FlashAddr);
                Image2IsSignatureOK = (image2_head.Signature == AmebaA_Code.Image2Signature0) ||
                                        (image2_head.Signature == AmebaA_Code.Image2Signature1);

                if (Image2IsSignatureOK)
                {
                    Image2SegSize = (UInt32)(image2_head.Size + Marshal.SizeOf<Image_Head>());
                    Image2LoadAddr = image2_head.LoadAddr;

                    image2_info = BytesToStruct<Image_Info>(buffer, (UInt32)(Image2FlashAddr + Marshal.SizeOf<Image_Head>()));
                    Image2InfraStart = image2_info.InfraStart;
                    Image2CustomerSig = System.Text.Encoding.UTF8.GetString(image2_info.CustSign);

                    // TODO: Перепроверить...
                    Image3LoadAddr = Image2LoadAddr + Image2SegSize;
                    Image3FlashAddr = Image2FlashAddr + Image2SegSize;
                    image3_head = BytesToStruct<Image_Head>(buffer, Image3FlashAddr);
                    Image3SegSize = (UInt32)(image3_head.Size + Marshal.SizeOf<Image_Head>());

                    // TODO: Перепроверить...
                    Image3IsSignatureOK = (image3_head.Signature == AmebaA_Code.Image2Signature0) ||
                                            (image3_head.Signature == AmebaA_Code.Image2Signature1);
                }
            }

            UInt32 ota_addr = AmebaA_Addresses.SystemDataAddr;
            while (ota_addr < AmebaA_Addresses.SystemDataAddr + AmebaA_Addresses.FlashSectorSize)
            {
                if(buffer[ota_addr] != 0xFF)
                {
                    ImageOTAFlashAddr = (UInt32)(buffer[ota_addr + 3] << 24 | 
                                                buffer[ota_addr + 2] << 16 |
                                                buffer[ota_addr + 1] << 8 |
                                                buffer[ota_addr + 0]);
                    if(ImageOTAFlashAddr + Marshal.SizeOf<Image_Head>() < FlashSize)
                    {
                        imageOTA_head = BytesToStruct<Image_Head>(buffer, ImageOTAFlashAddr);
                        ImageOTAIsSignatureOK = (imageOTA_head.Signature == AmebaA_Code.Image2Signature0) ||
                                                (imageOTA_head.Signature == AmebaA_Code.Image2Signature1);

                        if (ImageOTAIsSignatureOK)
                        {
                            ImageOTASegSize = (UInt32)(imageOTA_head.Size + Marshal.SizeOf<Image_Head>() + 4 /* CRC */);
                            ImageOTALoadAddr = imageOTA_head.LoadAddr;
                            imageOTA_info = BytesToStruct<Image_Info>(buffer, (UInt32)(ImageOTAFlashAddr + Marshal.SizeOf<Image_Head>()));
                            ImageOTAInfraStart = imageOTA_info.InfraStart;
                            ImageOTACustomerSig = System.Text.Encoding.UTF8.GetString(imageOTA_info.CustSign); 
                        } 
                    }

                    break;
                }
                else
                {
                    ota_addr += 1;
                }
            }

            UInt16 calibr_sig = (UInt16)(buffer[AmebaA_Addresses.CalibrationDataAddr + 1] | (buffer[AmebaA_Addresses.CalibrationDataAddr] << 8));
            if (calibr_sig == AmebaA_Code.CalibrationSignature)
            {
                Image1CalibrationIsOK = true;

                for (byte i = 0; i < MAC.Length; i++)
                {
                    
                    MAC[i] = buffer[AmebaA_Addresses.CalibrationDataAddr + 0x88 + i];
                    OnPropertyChanged(new PropertyChangedEventArgs("MAC"));
                }
            }
            else
            {
                // Пример заводского MAC из eFuse OTP: 00:E0:4C:87:00:00	
                MAC[0] = 0x00;
                MAC[1] = 0xE0;
                MAC[2] = 0x4C;
                MAC[3] = 0x87;
                MAC[4] = 0x00;
                MAC[5] = 0x00;
                OnPropertyChanged(new PropertyChangedEventArgs("MAC"));
            }
        }

        #endregion

        #region Методы для работы с Flash памятью SoC
        public void ReadFlashID()
        {
            CallbackLog?.Invoke("Read Flash ID ...");

            FlashID = (UInt32)((FlashWriteReadCMD(0x9F, 1, 3) & 0xFFFFFF) | (FlashWriteReadCMD(0xAB, 3, 1) << 24));
            //UInt32 data = 0;
            //ReadU32(AmebaA_Flasher.Addr + 0x0C, ref data);
            //FlashID = data;
            CheckFlashID();
            //Thread.Sleep(50);
        }

        public void CheckFlashID()
        {
            // 0x131420C2 ( RTL8710AF )
            // 0x141520C2 ( RTL8710AM )
            // MX25L8006E
            // Memory Density = ((FlashID >> 16) & 0xFF)
            // Memory Type = ((FlashID >> 8) & 0xFF)
            // Manufacturer ID = (FlashID & 0xFF)
            // Electronic ID = ((FlashID >> 24) & 0xFF)

            if (FlashID == 0x141520C2 || FlashID == 0x1520C2)
                FlashSize = 0x200000;

            //buffer = new byte[FlashSize];
        }
/*
        public void ReadJedecID()
        {
            UInt32 id = 0x0;
            CallbackLog?.Invoke("Read Jedec ID...");

            WriteU32(RTL8195_Addresses.Addr + 0x04, (UInt32)AmebaA_Flasher_CMD.ReadID);
            WriteU32(RTL8195_Addresses.Addr + 0x08, 0x0);
            WriteU32(RTL8195_Addresses.Addr + 0x00, 0x1);

            if (WaitTarget())
            {
                ReadU32(RTL8195_Addresses.Addr + 0x0C, ref id);
                JedecID = id;
            }
        }
*/
        public bool FlashWrite()
        {
            CallbackLog?.Invoke("Full Flash Write ...");
            return FlashWrite(Image1FlashAddr, FlashSize, buffer);
        }

        public bool FlashWrite(UInt32 Addr, UInt32 Size)
        {
            return FlashWrite(Addr, Size, buffer);
        }

        public bool FlashWrite(UInt32 Addr, UInt32 Size, byte[] Data)
        {
            bool status = false;
            bool fullflasharray = false;
            Int32 Size_Left = (Int32)Size;

            if (IsInited & Data.Length >= Size & Size > 0)
            {
                Int32 len = 0;
                Int32 len_actual = 0;
                UInt32 offset = 0;
                UInt32 offset_flash = 0;

                fullflasharray = Data.Length == FlashSize;

                CallbackLog?.Invoke(String.Format("Flash Write at 0x{0:X}, size {1}", Addr, Size));
                if (Size > FlashSize)
                {
                    CallbackError?.Invoke(String.Format("Error! Size {0} > {1}", Size, FlashSize));
                    return status;
                }

                status = true;
                TimeSpan time = StopwatchUtil.Time(() =>
                {
                    while (Size_Left > 0 & status)
                    {
                        len = Size_Left;

                        if (len > AmebaA_Addresses.Size)
                            len = (Int32)AmebaA_Addresses.Size;

                        offset_flash = Addr + offset;
                        len_actual = len;

                        if ((len_actual & 0xff) > 0)
                            len_actual = (Int32)((len_actual & 0xffffff00) + 0x100);

                        byte[] Data_RAW = new byte[len_actual];
                        if(fullflasharray)
                            Array.Copy(Data, offset_flash, Data_RAW, 0, len_actual);
                        else
                            Array.Copy(Data, offset, Data_RAW, 0, len_actual);

                        GCHandle Data_RAW_hndl = GCHandle.Alloc(Data_RAW, GCHandleType.Pinned);
                        IntPtr Data_RAW_Ptr = Data_RAW_hndl.AddrOfPinnedObject();
                        JLinkARM.JLINKARM_WriteMem(AmebaA_Addresses.Addr + 0x20, (UInt32)len_actual, Data_RAW_Ptr);
                        Data_RAW_hndl.Free();

                        //WriteU8(RTL8195_Addresses.Addr + 0x20, (UInt32)len_actual, Data_RAW);

                        UInt32 len_check = offset_flash;
                        while (len_check < (offset_flash + len_actual /*len*/) & status)
                        {
                            UInt32 sector = (UInt32)len_check /*len_actual*/ / AmebaA_Addresses.FlashSectorSize;
                            //CallbackLog?.Invoke(String.Format("Erase Sector {0}, Offset {0:X}...", sector, len_check));
                            if (!EraseSector(sector))
                            {
                                CallbackError?.Invoke(String.Format("Erase Block Error! Sector {0}", sector));
                                status = false;
                                break;
                            }
                            len_check += AmebaA_Addresses.FlashSectorSize;
                        }

                        if (!StartWriteBlock(offset_flash, (UInt32)len_actual /*len*/) & status)
                        {
                            CallbackError?.Invoke(String.Format("Write Block Error! Offset {0:X} Len {1}", offset_flash, len_actual /*len*/));
                            status = false;
                            break;
                        }

                        if (VerifyFlashWrite & status)
                        {
                            if (!StartVerifyBlock(offset_flash, (UInt32)len_actual /*len*/))
                            {
                                CallbackError?.Invoke(String.Format("Verify Block Error! Offset {0:X} Len {1}", offset_flash, len_actual /*len*/));
                                status = false;
                                break;
                            }
                        }

                        offset += (UInt32)len_actual;// len;
                        Size_Left -= len_actual;// len;
                    }
                });

                CallbackLog?.Invoke(String.Format("Time {0:0.00} ms, Speed {1:0.00} KB/s", time.TotalMilliseconds, (double)((double)Size / time.TotalMilliseconds)));
            }

            if(status)
            {
                CallbackLog?.Invoke("Full Flash Write Done!");
            }

            return status;
        }

        public bool FlashRead()
        {
            return FlashRead(0x00, FlashSize, ref buffer);
        }

        public bool FlashRead(UInt32 Addr, UInt32 Size, ref byte[] Data)
        {
            bool status = false;
            Addr += AmebaA_Addresses.FlashAddr;

            if (IsInited & Data.Length >= Size)
            {
                CallbackLog?.Invoke(String.Format("Flash Read at 0x{0:X}, size {1}", Addr, Size));
                Stopwatch stopwatch = Stopwatch.StartNew();
                ReadU8(Addr, Size, ref Data);
                stopwatch.Stop();
                CallbackLog?.Invoke(String.Format("Time {0:0.00} ms, Speed {1:0.00} KB/s", stopwatch.Elapsed.TotalMilliseconds, (double)((double)Size / stopwatch.Elapsed.TotalMilliseconds)));
                CallbackLog?.Invoke("Flash Read Done!");
                status = true;
            }

            return status;
        }

        public bool EraseSector(UInt32 Sector)
        {
            bool status = false;
            if (IsInited)
            {
                //CallbackLog?.Invoke(String.Format("Erase Sector {0}...", Sector));
                UInt32 Addr = Sector * AmebaA_Addresses.FlashSectorSize;
                WriteU32(AmebaA_Addresses.Addr + 0x04, (UInt32)AmebaA_Flasher_CMD.EraseSector);
                WriteU32(AmebaA_Addresses.Addr + 0x08, 0x0);
                WriteU32(AmebaA_Addresses.Addr + 0x10, Addr);
                WriteU32(AmebaA_Addresses.Addr + 0x00, 0x1);
                status = WaitTarget();
            }

            return status;
        }

        public bool EraseAll()
        {
            bool status = false;

            if (InitTargert())
            {
                CallbackLog?.Invoke("Erase All ...");
                WriteU32(AmebaA_Addresses.Addr + 0x04, (UInt32)AmebaA_Flasher_CMD.EraseAll);
                WriteU32(AmebaA_Addresses.Addr + 0x08, 0);
                WriteU32(AmebaA_Addresses.Addr + 0x00, 1);
                status = WaitTarget();
            }
            return status;
        }

        public void FlashWaitReadyStatus()
        {
            UInt32 w = 0x0;

            do
            {
                ReadU32(0x40006028, ref w);
            }
            while ((w & 0x1) != 0x0);
        }

        public void FlashWriteEnable()
        {
            CallbackLog?.Invoke("Flash Write Enabled!");

            WriteU32(0x40006000, 0x100);
            WriteU32(0x40006004, 0x0);
            WriteU32(0x40006008, 0x1);
            WriteU32(0x40006060, 0x06);
            FlashWaitReadyStatus();
            WriteU32(0x40006008, 0x0);
        }

        public void FlashWriteDisable()
        {
            CallbackLog?.Invoke("Flash Write Disabled!");

            WriteU32(0x40006000, 0x100);
            WriteU32(0x40006004, 0x0);
            WriteU32(0x40006008, 0x1);
            WriteU32(0x40006060, 0x04);
            FlashWaitReadyStatus();
            WriteU32(0x40006008, 0x0);
        }

        public UInt32 FlashWriteReadCMD(UInt32 cmd, UInt32 wr, UInt32 rd)
        {
            UInt32 data = 0x0;
            UInt32 w = 0x0;

            if (rd != 0)
                w |= 0x200;

            if (wr != 0)
                w |= 0x100;

            WriteU32(0x40006000, w);
            WriteU32(0x40006004, rd);
            WriteU32(0x40006008, wr);
            WriteU32(0x40006060, cmd);

            if (rd != 0)
            {
                do
                {
                    //Thread.Sleep(1);
                    ReadU32(0x40006024, ref w);
                }
                while ((w & 0xFFF) == 0x0);
            }

            ReadU32(0x40006060, ref data);

            if (wr != 0)
            {
                FlashWaitReadyStatus();
            }

            WriteU32(0x40006004, 0);
            WriteU32(0x40006008, 0);

            return data;
        }

        public void FlashWriteCMD(byte cmd)
        {
            WriteU32(0x40006000, 0x100);
            WriteU32(0x40006004, 0x0);
            WriteU32(0x40006008, 0x1);
            WriteU32(0x40006060, cmd);
            FlashWaitReadyStatus();
            WriteU32(0x40006008, 0x0);
        }

        #endregion

        #region Специальные методы при работе с загрузчиком

        public bool WaitTarget()
        {
            bool status = false;
            UInt32 timeout = 10000;
            UInt32 data = 0;

            while((status != true) & (timeout > 0))
            {
                //Thread.Sleep(1);
                ReadU32(AmebaA_Addresses.Addr, ref data);
                status = (data == 0);
                timeout--;
            }
            return status;
        }

        public bool StartReadBlock(UInt32 offset, UInt32 len)
        {
            bool status = false;
            UInt32 data = 0x1;

            if (IsInited)
            {
                WriteU32(AmebaA_Addresses.Addr + 0x04, (UInt32)AmebaA_Flasher_CMD.Read);
                WriteU32(AmebaA_Addresses.Addr + 0x08, 0x0);
                WriteU32(AmebaA_Addresses.Addr + 0x10, offset);
                WriteU32(AmebaA_Addresses.Addr + 0x14, len);
                WriteU32(AmebaA_Addresses.Addr + 0x00, 0x1);

                if (WaitTarget())
                {
                    ReadU32(AmebaA_Addresses.Addr + 0x08, ref data);
                    status = (data == 0);
                }
            }

            return status;
        }

        public bool StartWriteBlock(UInt32 offset, UInt32 len)
        {
            bool status = false;
            UInt32 data = 0x1;

            if (IsInited)
            {
                WriteU32(AmebaA_Addresses.Addr + 0x04, (UInt32)AmebaA_Flasher_CMD.Write);
                WriteU32(AmebaA_Addresses.Addr + 0x08, 0x0);
                WriteU32(AmebaA_Addresses.Addr + 0x10, offset);
                WriteU32(AmebaA_Addresses.Addr + 0x14, len);
                WriteU32(AmebaA_Addresses.Addr + 0x00, 0x1);

                if (WaitTarget())
                {
                    ReadU32(AmebaA_Addresses.Addr + 0x08, ref data);
                    status = (data == 0);
                }
            }

            return status;
        }

        public bool StartVerifyBlock(UInt32 offset, UInt32 len)
        {
            bool status = false;
            UInt32 data = 0x1;

            if (IsInited)
            {
                WriteU32(AmebaA_Addresses.Addr + 0x04, (UInt32)AmebaA_Flasher_CMD.Verify);
                WriteU32(AmebaA_Addresses.Addr + 0x08, 0x0);
                WriteU32(AmebaA_Addresses.Addr + 0x10, offset);
                WriteU32(AmebaA_Addresses.Addr + 0x14, len);
                WriteU32(AmebaA_Addresses.Addr + 0x00, 0x1);

                if (WaitTarget())
                {
                    ReadU32(AmebaA_Addresses.Addr + 0x08, ref data);
                    status = (data == 0);
                }
            }

            return status;
        }

        #endregion

        /*
        #region Работа с OTP памятью
        public bool ReadOTP()
        {
            bool status = false; 
            FlashWriteCMD(0xB1); // enter secured OTP
            status = StartReadBlock(0, RTL8195_Addresses.OTPSize);
            if (status)
            {
                GCHandle addr_hndl = GCHandle.Alloc(buffer_otp, GCHandleType.Pinned);
                IntPtr addr_data = addr_hndl.AddrOfPinnedObject();
                JLinkARM.JLINKARM_ReadMem(RTL8195_Addresses.Addr + 0x20, RTL8195_Addresses.OTPSize, addr_data);
                addr_hndl.Free();
            }
            FlashWriteCMD(0xC1); // exit secured OTP
            return status;
        }
        #endregion
        */

        /*
        #region Методы по работе с файлами
        public bool FlashFromFile(string path, AmebaA_ImageType type)
        {
            bool status = false;

            if(InitTargert())
            {

            }

            return status;
        }

        public bool ReadToFile(string path, AmebaA_ImageType type)
        {
            bool status = false;

            if (InitTargert())
            {
                switch(type)
                {
                    case AmebaA_ImageType.All:

                        break;

                    default:
                        return status;
                }
            }

            return status;
        }
        #endregion
        */
    }
}
