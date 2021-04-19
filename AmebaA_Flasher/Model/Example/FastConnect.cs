using Prism.Mvvm;
using System;
using System.Text;

namespace AmebaA_Flasher.Model
{
    public static class WiFiSecurity
    {
        public const UInt32 WEP_Enable = 0x0001;
        public const UInt32 TKIP_Enable = 0x0002;
        public const UInt32 AES_Enable = 0x0004;
        public const UInt32 WSEC_SWFlag = 0x0008;
        public const UInt32 AES_CMAC_Enable = 0x0010;

        public const UInt32 Shared_Enable = 0x00008000;
        public const UInt32 WPA_Security = 0x00200000;
        public const UInt32 WPA2_Security = 0x00400000;
        public const UInt32 WPA3_Security = 0x00800000;
        public const UInt32 WPS_Enable = 0x10000000;
    }

    public enum WiFiSecurityType : UInt32
    {
        Open = 0,
        WEP_PSK = WiFiSecurity.WEP_Enable,
        WEP_SHARED = (WiFiSecurity.WEP_Enable | WiFiSecurity.Shared_Enable),
        WPA_TKIP_PSK = (WiFiSecurity.WPA_Security | WiFiSecurity.TKIP_Enable),
        WPA_AES_PSK = (WiFiSecurity.WPA_Security | WiFiSecurity.AES_Enable),
        WPA2_AES_PSK = (WiFiSecurity.WPA2_Security | WiFiSecurity.AES_Enable),
        WPA2_TKIP_PSK = (WiFiSecurity.WPA2_Security | WiFiSecurity.TKIP_Enable),
        WPA2_MIXED_PSK = (WiFiSecurity.WPA2_Security | WiFiSecurity.AES_Enable | WiFiSecurity.TKIP_Enable),
        WPA_WPA2_MIXED = (WiFiSecurity.WPA_Security | WiFiSecurity.WPA2_Security),
        WPA2_AES_CMAC = (WiFiSecurity.WPA2_Security | WiFiSecurity.AES_CMAC_Enable),
        WPS_OPEN = WiFiSecurity.WPS_Enable,
        WPS_SECURE = (WiFiSecurity.WPS_Enable | WiFiSecurity.AES_Enable),
        WPA3_AES_PSK = (WiFiSecurity.WPA3_Security | WiFiSecurity.AES_Enable),
        FORCE_32_BIT = 0x7fffffff
    }

    public class FastConnect_Data : BindableBase
    {
        #region ESSID
        string _ESSID;
        public string ESSID
        {
            get { return _ESSID; }
            set { SetProperty<string>(ref _ESSID, value); }
        }
        #endregion

        #region Password
        string _Password;
        public string Password
        {
            get { return _Password; }
            set { SetProperty<string>(ref _Password, value); }
        }
        #endregion

        #region GlobalPSK
        string _GlobalPSK;
        public string GlobalPSK
        {
            get { return _GlobalPSK; }
            set { SetProperty<string>(ref _GlobalPSK, value); }
        }
        #endregion

        #region Channel
        UInt32 _Channel;
        public UInt32 Channel
        {
            get { return _Channel; }
            set { SetProperty<UInt32>(ref _Channel, value); }
        }
        #endregion

        #region SecurityType
        WiFiSecurityType _SecurityType;
        public WiFiSecurityType SecurityType
        {
            get { return _SecurityType; }
            set { SetProperty<WiFiSecurityType>(ref _SecurityType, value); }
        }
        #endregion

        #region Enable
        UInt32 _Enable;
        public UInt32 Enable
        {
            get { return _Enable; }
            set { SetProperty<UInt32>(ref _Enable, value); }
        }
        #endregion
    }

    public class FastConnect
    {
        static readonly Int32 ESSID_Offset = 0;
        static readonly Int32 Password_Offset = ESSID_Offset + 32 + 4;
        static readonly Int32 GlobalPSK_Offset = Password_Offset + 64 + 1;
        static readonly Int32 Channel_Offset = GlobalPSK_Offset + 20 * 2;
        static readonly Int32 SecurityType_Offset = Channel_Offset + 4;
        static readonly Int32 Enable_Offset = SecurityType_Offset + 4;

        static readonly Int32 DataArraySize = 256;// Enable_Offset + 4;

        public UInt32 FlashAddr { get; set; } = (0x80000 - 0x1000);
        public UInt32 FlashSize { get; set; } = 0x1000;
        public FastConnect_Data Data { get; private set; } = new FastConnect_Data();

        public void FromByteArray(byte[] data)
        {
            UInt32 ToUInt32(byte[] in_data, Int32 in_offset)
            {
                UInt32 ret = 0x0;
                for (byte i = 0; i < 4; i++)
                {
                    ret |= (UInt32)(in_data[i + in_offset] << (8 * i));
                }
                return ret;
            }

            if(data.Length >= DataArraySize)
            {
                Data.ESSID = Encoding.ASCII.GetString(data, ESSID_Offset, 32 + 4);
                Data.Password = Encoding.ASCII.GetString(data, Password_Offset, 64 + 1);
                Data.GlobalPSK = Encoding.ASCII.GetString(data, GlobalPSK_Offset, 20 * 2);
                Data.Channel = ToUInt32(data, Channel_Offset);
                Data.SecurityType = (WiFiSecurityType)ToUInt32(data, SecurityType_Offset);
                Data.Enable = ToUInt32(data, Enable_Offset);
            }
        }

        public byte[] GetByteArray()
        {
            Int32 offset = 0;
            byte[] data = new byte[DataArraySize];

            void ToByteArray(UInt32 source, ref byte[] in_dest, Int32 in_offset)
            {
                for (byte i = 0; i < 4; i++)
                {
                    in_dest[in_offset + i] = (byte)((source >> (8 * i)) & 0xFF);
                }
            }

            void StringToByteArray(string in_source, byte in_maxlen, ref byte[] in_dest, Int32 in_offset)
            {
                if(in_source != null)
                {
                    byte[] bytes = Encoding.ASCII.GetBytes(in_source);
                    Int32 len_left = in_maxlen;

                    if (bytes.Length >= in_maxlen)
                    {
                        Array.Copy(bytes, 0, in_dest, in_offset, in_maxlen);
                    }
                    else
                    {
                        Array.Copy(bytes, 0, in_dest, in_offset, bytes.Length);
                        len_left -= bytes.Length;

                        for (Int32 i = bytes.Length; i < len_left; i++)
                            in_dest[i + in_offset] = 0x00;
                    }
                }
                else
                {
                    for (Int32 i = 0; i < in_maxlen; i++)
                        in_dest[i + in_offset] = 0x00;
                }
            }

            StringToByteArray(Data.ESSID, 32 + 4, ref data, offset);
            offset += 32 + 4;
            StringToByteArray(Data.Password, 64 + 1, ref data, offset);
            offset += 64 + 1;
            StringToByteArray(Data.GlobalPSK, 20 * 2, ref data, offset);
            offset += 20 * 2;

            ToByteArray(Data.Channel, ref data, offset);
            offset += 4;
            ToByteArray((UInt32)Data.SecurityType, ref data, offset);
            offset += 4;
            ToByteArray(Data.Enable, ref data, offset);

            return data;
        }
    }
}
