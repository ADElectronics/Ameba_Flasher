using JLinkAccess;
using Prism.Mvvm;
using System;

namespace Ameba.Common.Model
{
    public abstract class FlasherBase : BindableBase
    {
		#region Публичные свойства
		public abstract bool IsOpen { get; set; }
		public abstract bool IsConnected { get; set; }
		public abstract bool IsInited { get; set; }
		#endregion

		#region Публичные события

		public abstract event CallbackLogDelegate CallbackLog;
		public abstract event CallbackLogDelegate CallbackError;

		#endregion

		#region Публичные методы
		public abstract bool OpenDebugger();
		public abstract bool OpenDebugger(JLink_TIF_Type TIF, UInt32 Speed);
		public abstract bool Connect();
		public abstract void Disconnect();
		public abstract bool InitTargert();
		public abstract void Restart();
		public abstract bool WriteU8(UInt32 Addr, UInt32 Count, byte[] data);
		public abstract bool WriteU8(UInt32 Addr, byte data);
		public abstract bool ReadU8(UInt32 Addr, UInt32 Count, ref byte[] data);
		public abstract bool ReadU8(UInt32 Addr, ref byte data);
		public abstract bool WriteU32(UInt32 Addr, UInt32 Count, UInt32[] data);
		public abstract bool WriteU32(UInt32 Addr, UInt32 data);
		public abstract bool ReadU32(UInt32 Addr, UInt32 Count, ref UInt32[] data);
		public abstract bool ReadU32(UInt32 Addr, ref UInt32 data);
		public abstract bool VerifyU32(UInt32 Addr, UInt32 Count, UInt32[] data);
		public abstract bool VerifyU32(UInt32 Addr, UInt32 data);
		//public abstract bool EraseSector(UInt32 Sector);
		//public abstract bool EraseAll();
		#endregion
	}
}
