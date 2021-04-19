using Prism.Mvvm;
using Ameba.Common.Controls;
using Ameba.Common.Helpers;
using Ameba.Common.Model;
using AmebaA_Flasher.View;
using System;
using System.Windows;
using System.Windows.Input;
using AmebaA_Flasher.Model;

namespace AmebaA_Flasher.ViewModel
{
    public class MainWindowViewModel : BindableBase
    {
		LogViewer logger;
		public AmebaA RTL { get; private set; } = new AmebaA();
        public FastConnect RTL_FC { get; private set; } = new FastConnect();
        public ImageMemViewer IMG { get; private set; } = new ImageMemViewer(6, 0x0000, AmebaA_Addresses.FlashSize);

        #region ImagesToWrite
        UInt16 _ImagesToWrite;
        public UInt16 ImagesToWrite
        {
            get { return _ImagesToWrite; }
            set { SetProperty<UInt16>(ref _ImagesToWrite, value); }
        }
        #endregion

        #region Команды
        public ICommand Сommand_Connect { get; set; }
        public ICommand Сommand_Disconnect { get; set; }
        public ICommand Сommand_InitTargert { get; set; }
        public ICommand Сommand_RestartTargert { get; set; }
        public ICommand Сommand_EraseAll { get; set; }
        public ICommand Сommand_ReadIDRegs { get; set; }
        public ICommand Сommand_ReadFullFlash { get; set; }
        public ICommand Сommand_WriteFullFlash { get; set; }
        public ICommand Сommand_WriteParticleFlash { get; set; }
        public ICommand Сommand_LoadFullFlash { get; set; }
        public ICommand Сommand_ReloadFullFlash { get; set; }
        public ICommand Сommand_SaveFullFlash { get; set; }
        public ICommand Сommand_ShowConfigurator { get; set; }
        public ICommand Сommand_WriteConfig { get; set; }
        public ICommand Сommand_ReadConfig { get; set; }
        public ICommand Сommand_EraseConfig { get; set; }
        public ICommand Сommand_UpdateCalibration { get; set; }
        #endregion

        public MainWindowViewModel()
        {
            if(!InDesignMode.Check())
            {
                RTL.CallbackLog += RTL_CallbackLog;
                RTL.CallbackError += RTL_CallbackError;

                Application.Current.MainWindow.Loaded += MainWindow_Loaded;
            }

            Сommand_UpdateCalibration = new RelayCommand((p) =>
            {
                CalibrationWindow ucw = new CalibrationWindow();

                ucw.Owner = Application.Current.MainWindow;
                ucw.DataContext = this;
                ucw.ShowDialog();

                RTL.UpdateCalibrationRegion();
                RTL.CheckImages();
            }, (p) => (true));

            Сommand_WriteConfig = new RelayCommand((p) =>
            {
                if(p != null)
                {
                    switch((string)p)
                    {
                        case "0":
                            byte[] data = RTL_FC.GetByteArray();
                            RTL.FlashWrite(RTL_FC.FlashAddr, (UInt32)data.Length, data);
                            break;

                        default:
                            break;
                    }
                }
            }, (p) => (RTL.IsInited));

            Сommand_ReadConfig = new RelayCommand((p) =>
            {
                if (p != null)
                {
                    switch ((string)p)
                    {
                        case "0":
                            byte[] data = new byte[RTL_FC.FlashSize];
                            if (RTL.FlashRead(RTL_FC.FlashAddr, RTL_FC.FlashSize, ref data))
                            {
                                RTL_FC.FromByteArray(data);
                            }
                            break;

                        default:
                            break;
                    }
                }
            }, (p) => (RTL.IsInited));

            Сommand_EraseConfig = new RelayCommand((p) =>
            {
                if (p != null)
                {
                    switch ((string)p)
                    {
                        default:
                            break;
                    }
                }
            }, (p) => (RTL.IsInited));

            Сommand_Connect = new RelayCommand((p) =>
			{
                RTL.OpenDebugger();
                RTL.Connect();
            }, (p) => (!RTL.IsOpen));

            Сommand_Disconnect = new RelayCommand((p) =>
            {
                RTL.Disconnect();
            }, (p) => (RTL.IsConnected));

            Сommand_InitTargert = new RelayCommand((p) =>
            {
                RTL.InitTargert();
            }, (p) => (RTL.IsConnected));

            Сommand_RestartTargert = new RelayCommand((p) =>
            {
                RTL.Restart();
            }, (p) => (RTL.IsConnected));

            Сommand_EraseAll = new RelayCommand((p) =>
            {
                RTL.EraseAll();
            }, (p) => (RTL.IsConnected));

            Сommand_ReadIDRegs = new RelayCommand((p) =>
            {
                //RTL.ReadJedecID();
                RTL.ReadFlashID();
            }, (p) => (RTL.IsInited));

            Сommand_ReadFullFlash = new RelayCommand((p) =>
            {
                if(RTL.InitTargert())
                {
                    if (RTL.FlashRead())
                    {
                        RTL.CheckImages();
                        UpdateImagesPreview();
                    }
                }
            }, (p) => (RTL.IsConnected));

            Сommand_WriteFullFlash = new RelayCommand((p) =>
            {
                if (RTL.InitTargert())
                {
                    RTL.FlashWrite();
                }   
            }, (p) => (RTL.IsConnected));

            Сommand_WriteParticleFlash = new RelayCommand((p) =>
            {
                if (RTL.InitTargert())
                {
                    ImagesSelectWindow isw = new ImagesSelectWindow();

                    isw.Owner = Application.Current.MainWindow;
                    isw.DataContext = this;
                    isw.ShowDialog();

                    if (((ImagesToWrite & (UInt16)AmebaA_ParticleImageBits.System) > 0) & RTL.Image1SPICIsOK)
                    {
                        RTL.FlashWrite(AmebaA_Addresses.SystemDataAddr, AmebaA_Addresses.SystemDataSize);
                    }

                    if (((ImagesToWrite & (UInt16)AmebaA_ParticleImageBits.Calibration) > 0) & RTL.Image1CalibrationIsOK)
                    {
                        RTL.FlashWrite(AmebaA_Addresses.CalibrationDataAddr, AmebaA_Addresses.CalibrationDataSize);
                    }

                    if (((ImagesToWrite & (UInt16)AmebaA_ParticleImageBits.Image1) > 0) & RTL.Image1IsSignatureOK)
                    {
                        RTL.FlashWrite(RTL.Image1FlashAddr, RTL.Image1SegSize);
                    }

                    if (((ImagesToWrite & (UInt16)AmebaA_ParticleImageBits.Image2) > 0) &RTL.Image2IsSignatureOK)
                    {
                        RTL.FlashWrite(RTL.Image2FlashAddr, RTL.Image2SegSize);
                    }

                    if (((ImagesToWrite & (UInt16)AmebaA_ParticleImageBits.Image3) > 0) & RTL.Image3IsSignatureOK)
                    {
                        RTL.FlashWrite(RTL.Image3FlashAddr, RTL.Image3SegSize);
                    }

                    if (((ImagesToWrite & (UInt16)AmebaA_ParticleImageBits.OTA) > 0) & RTL.ImageOTAIsSignatureOK)
                    {
                        RTL.FlashWrite(RTL.ImageOTAFlashAddr, RTL.ImageOTASegSize);
                    }
                }
            }, (p) => (RTL.IsConnected));

            Сommand_LoadFullFlash = new RelayCommand((p) =>
            {
                if (RTL.LoadFromFile())
                {
                    RTL.CheckImages();
                    UpdateImagesPreview();
                }
            }, (p) => (true));

            Сommand_ReloadFullFlash = new RelayCommand((p) =>
            {
                if (RTL.ReloadFromFile())
                {
                    RTL.CheckImages();
                    UpdateImagesPreview();
                }
            }, (p) => (!String.IsNullOrEmpty(RTL.LastFilePath)));

            Сommand_SaveFullFlash = new RelayCommand((p) =>
            {
                RTL.SaveToFile();
            }, (p) => (true));

            Сommand_ShowConfigurator = new RelayCommand((p) =>
            {
                if (RTL.InitTargert())
                {
                    ConfiguratorWindow cw = new ConfiguratorWindow();

                    cw.Owner = Application.Current.MainWindow;
                    cw.DataContext = this;
                    cw.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Error at Initializing SoC Flasher, check swd or jtag cable, speed settings, SoC reset...", "Error!");
                }
            }, (p) => (RTL.IsConnected));
        }

        private void RTL_CallbackLog(string data)
        {
            logger.AddText(data);
        }

        private void RTL_CallbackError(string data)
        {
            logger.AddText(data);
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
			logger = Find.Child<LogViewer>(Application.Current.MainWindow, "Logger");

            // TODO: Переделать на реальную ширину окна при загрузке...
            IMG.TotalWidth = 820;
            // System
            IMG.UpdateImage(4, AmebaA_Addresses.SystemDataAddr, AmebaA_Addresses.SystemDataSize);
            // Calibration
            IMG.UpdateImage(5, AmebaA_Addresses.CalibrationDataAddr, AmebaA_Addresses.CalibrationDataSize);
        }

        void UpdateImagesPreview()
        {
            IMG.TotalMemSize = RTL.FlashSize;

            if (RTL.Image1IsSignatureOK)
                IMG.UpdateImage(0, RTL.Image1FlashAddr, RTL.Image1SegSize);
            else
                IMG.HideImage(0);

            if (RTL.Image2IsSignatureOK)
                IMG.UpdateImage(1, RTL.Image2FlashAddr, RTL.Image2SegSize);
            else
                IMG.HideImage(1);

            if (RTL.Image3IsSignatureOK)
                IMG.UpdateImage(2, RTL.Image3FlashAddr, RTL.Image3SegSize);
            else
                IMG.HideImage(2);

            if (RTL.ImageOTAIsSignatureOK)
                IMG.UpdateImage(3, RTL.ImageOTAFlashAddr, RTL.ImageOTASegSize);
            else
                IMG.HideImage(3);
        }
    }
}
