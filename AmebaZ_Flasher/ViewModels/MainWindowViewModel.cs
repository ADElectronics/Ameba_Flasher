using Prism.Mvvm;
using Ameba.Common.Controls;
using Ameba.Common.Helpers;
using Ameba.Common.Model;
using AmebaZ_Flasher.Views;
using System;
using System.Windows;
using System.Windows.Input;
using AmebaZ_Flasher.Models;

namespace AmebaZ_Flasher.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        LogViewer logger;



        public AmebaZ RTL { get; private set; } = new AmebaZ();
        public FastConnect RTL_FC { get; private set; } = new FastConnect();

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
            if (!InDesignMode.Check())
            {
                RTL.CallbackLog += RTL_CallbackLog;
                RTL.CallbackError += RTL_CallbackError;

                Application.Current.MainWindow.Loaded += MainWindow_Loaded;
            }

            Сommand_WriteConfig = new RelayCommand((p) =>
            {
                if (RTL.InitTargert())
                {
                    if (p != null)
                    {
                        switch ((string)p)
                        {
                            case "0":
                                byte[] data = RTL_FC.GetByteArray();
                                RTL.FlashWrite(RTL_FC.FlashAddr, (UInt32)data.Length, data);
                                break;

                            default:
                                break;
                        }
                    }
                }                 
            }, (p) => (RTL.IsConnected));

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
            }, (p) => (RTL.IsConnected));

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
                RTL.ReadFlashID();
            }, (p) => (RTL.IsInited));

            Сommand_ReadFullFlash = new RelayCommand((p) =>
            {
                if (RTL.FlashRead())
                {
                    RTL.CheckImages();
                    UpdateImagesPreview();
                } 
            }, (p) => (RTL.IsConnected));

            Сommand_WriteFullFlash = new RelayCommand((p) =>
            {
                if (RTL.InitTargert())
                {
                    RTL.FlashWrite();

                    /*
                    // Flasher Test
                    Random r = new Random();
                    Int32 len = r.Next(12, 80);
                    len = len - len % 4;
                    
                    byte[] test = new byte[len];
                    r.NextBytes(test);
                    //byte[] test = new byte[] { 0x23, 0x24, 0x25, 0x26, 0x27, 0x3, 0xff, 0xaa, 0x43, 0x3, 0xff, 0xaa };

                    RTL.FlashWrite(0x0000, (UInt32)len, test);

                    byte[] test_read = new byte[len];
                    RTL.FlashRead(AmebaZ_Addresses.FlashAddr + 0x0000, (UInt32)len, ref test_read);
                    */
                }
            }, (p) => (RTL.IsConnected));

            Сommand_WriteParticleFlash = new RelayCommand((p) =>
            {
                if (RTL.InitTargert())
                {

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
                ConfiguratorWindow cw = new ConfiguratorWindow();

                cw.Owner = Application.Current.MainWindow;
                cw.DataContext = this;
                cw.ShowDialog();
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
        }

        void UpdateImagesPreview()
        {

        }
    }
}
