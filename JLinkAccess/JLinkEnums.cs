using System;

namespace JLinkAccess
{
    public enum JLink_Reset_Type : UInt32
    {
        Normal = 0, // Resets core + peripherals. Reset pin is avoided where possible and reset via SFR access is preferred.
        Core = 100, // Resets core only - в v6.88 считается уже как не поддерживаемый или это у меня баг...
        Reset_Pin = 101, // Toggles reset pin in order to issue a reset
    }

    public enum JLink_TIF_Type : UInt32
    {
        JTAG = 0,
        SWD,
        FINE = 3,
        ICSP,
        SPI,
        C2,
        CJTAG,
        SWIM,
        PDI,
        MC2WJTAG_TDI,
        SPI_IDLE_CLK_LOW,
        Num_TIFs
    }
    public enum JLink_Device_Family : UInt32
    {
        Auto = 0,
        Cortex_M1,
        ColdFire,
        Cortex_M3,
        Simulator,
        XScale,
        Cortex_M0,
        ARM7,
        Cortex_A8_A9,
        ARM9,
        ARM10,
        ARM11,
        Cortex_R4,
        Renesas_RX,
        Cortex_M4,
        Cortex_A5,
        PowerPC,
        MIPS,
        EFM8,
        RISC_V,
        Cortex_AR_ARMV8,
        BT5511,
        ANY = 255
    }

    public enum JLink_Core_Type : UInt32
    {
        None = 0x00000000,
        Any = 0xFFFFFFFF,
        Cortex_M1 = 0x010000FF,
        Coldfire = 0x02FFFFFF,
        Cortex_M3 = 0x030000FF,
        Cortex_M3_R1P0 = 0x03000010,
        Cortex_M3_R1P1 = 0x03000011,
        Cortex_M3_R2P0 = 0x03000020,
        Cortex_M3_R2P1 = 0x03000021,
        SIM = 0x04FFFFFF,
        XScale = 0x05FFFFFF,
        Cortex_M0 = 0x060000FF,
        Cortex_M23 = 0x060100FF,
        ARM7 = 0x07FFFFFF,
        ARM7TDMI = 0x070000FF,
        ARM7TDMI_R3 = 0x0700003F,
        ARM7TDMI_R4 = 0x0700004F,
        ARM7TDMI_S = 0x070001FF,
        ARM7TDMI_S_R3 = 0x0700013F,
        ARM7TDMI_S_R4 = 0x0700014F,
        Cortex_A8 = 0x080000FF,
        Cortex_A7 = 0x080800FF,
        Cortex_A9 = 0x080900FF,
        Cortex_A12 = 0x080A00FF,
        Cortex_A15 = 0x080B00FF,
        Cortex_A17 = 0x080C00FF,
        ARM9 = 0x09FFFFFF,
        ARM9TDMI_S = 0x090001FF,
        ARM920T = 0x092000FF,
        ARM922T = 0x092200FF,
        ARM926EJ_S = 0x092601FF,
        ARM946E_S = 0x094601FF,
        ARM966E_S = 0x096601FF,
        ARM968E_S = 0x096801FF,
        ARM11 = 0x0BFFFFFF,
        ARM1136 = 0x0B36FFFF,
        ARM1136J = 0x0B3602FF,
        ARM1136J_S = 0x0B3603FF,
        ARM1136JF = 0x0B3606FF,
        ARM1136JF_S = 0x0B3607FF,
        ARM1156 = 0x0B56FFFF,
        ARM1176 = 0x0B76FFFF,
        ARM1176J = 0x0B7602FF,
        ARM1176J_S = 0x0B7603FF,
        ARM1176JF = 0x0B7606FF,
        ARM1176JF_S = 0x0B7607FF,
        Cortex_R4 = 0x0C0000FF,
        Cortex_R5 = 0x0C0100FF,
        Cortex_R8 = 0x0C0200FF,
        RX = 0x0DFFFFFF,
        RX610 = 0x0D00FFFF,
        RX62N = 0x0D01FFFF,
        RX62T = 0x0D02FFFF,
        RX63N = 0x0D03FFFF,
        RX630 = 0x0D04FFFF,
        RX63T = 0x0D05FFFF,
        RX621 = 0x0D06FFFF,
        RX62G = 0x0D07FFFF,
        RX631 = 0x0D08FFFF,
        RX65N = 0x0D09FFFF,
        RX66T = 0x0D0AFFFF,
        RX72T = 0x0D0BFFFF,
        RX66N = 0x0D0CFFFF,
        RX72M = 0x0D0DFFFF,
        RX72N = 0x0D0EFFFF,
        RX210 = 0x0D10FFFF,
        RX21A = 0x0D11FFFF,
        RX220 = 0x0D12FFFF,
        RX230 =  0x0D13FFFF,
        RX231 = 0x0D14FFFF,
        RX23T = 0x0D15FFFF,
        RX24T = 0x0D16FFFF,
        RX111 = 0x0D20FFFF,
        RX110 = 0x0D21FFFF,
        RX113 = 0x0D22FFFF,
        RX130 = 0x0D23FFFF,
        RX64M = 0x0D30FFFF,
        RX71M = 0x0D31FFFF,
        Cortex_M4 = 0x0E0000FF,
        Cortex_M7 = 0x0E0100FF,
        Cortex_M33 = 0x0E0200FF,
        Cortex_A5 = 0x0F0000FF,
        POWER_PC = 0x10FFFFFF,
        POWER_PC_N1 = 0x10FF00FF,
        POWER_PC_N2 = 0x10FF01FF,
        MIPS = 0x11FFFFFF,
        MIPS_M4K = 0x1100FFFF,
        MIPS_MICROAPTIV = 0x1101FFFF,
        MIPS_M14K = 0x1102FFFF,
        EFM8_UNSPEC = 0x12FFFFFF,
        CIP51 = 0x1200FFFF,
        RV32 = 0x13FFFFFF,
        RV64 = 0x1300FFFF,
        Cortex_A53 = 0x1400FFFF,
        Cortex_A57 = 0x1401FFFF,
        Cortex_A72 = 0x1402FFFF,
        Cortex_A35 = 0x1403FFFF,
        Num_Cores
    }

    public enum JLink_IFunc : UInt32
    {
        SET_HOOK_DIALOG_UNLOCK_IDCODE = 0,
        SPI_TRANSFER_MULTIPLE,
        PIN_OVERRIDE,                       // Internal use
        PIN_OVERRIDE_GET_PIN_CAPS,          // Internal use
        MRU_GETLIST,
        Reserved3,                          // Dummie
        Reserved4,                          // Dummie
        Reserved5,                          // Dummie
        GET_SESSION_ID,
        CORESIGHT_TRIGGER_READ_APDP_REG,
        CAN_ACC_MEM_WHILE_RUNNING,
        UPDATE_BTL,
        GET_CURRENT_ENDIANESS,
        ALGODB_GET_PALGO_INFO,              // Internal use (J-Flash)
        ALGODB_GET_PALGO_INFO_CFI,          // Internal use (J-Flash)
        ALGODB_GET_ALGO_NO,                 // Internal use (J-Flash)
        PCODE_SET_ENTRY_FUNC,               // Internal use (PCode compiler)
        PCODE_DOWNLOAD,                     // Internal use (script files)
        PCODE_EXEC_EX,                      // Internal use
        START_MERGE_COMMANDS,               // Internal use
        END_MERGE_COMMANDS,                 // Internal use
        RAWTRACE_BIST_STARTSTOP,            // Internal use (J-Link Commander)
        RAWTRACE_BIST_READ_ERR_STATS,       // Internal use (J-Link Commander)
        GET_PF_GET_INST_INFO,
        CORESIGHT_ACC_APDP_REG_MUL,
        Num_Funcs
    }

    public enum JLink_EMU_Product_Type : UInt32
    {
        Unknown = 0x00,
        JLink = 0x01,
        JLink_CF = 0x02,
        JLink_CE = 0x03,
        JLink_KS = 0x04,
        Digi_Link = 0x05,
        MIDAS = 0x06,
        SAMICE = 0x07,
        JTrace = 0x08,
        JTrace_CS = 0x09,
        Flasher_ARM = 0x0A,
        JLink_Pro = 0x0B,
        JLink_EDU = 0x0C,
        JLink_Ultra = 0x0D,
        Flasher_PPC = 0x0E,
        Flasher_RX = 0x0F,
        JLink_OB_RX200 = 0x10,
        JLink_Lite = 0x11,
        JLink_OB_SAM3U128 = 0x12,
        JLink_Lite_CortexM = 0x13,
        JLink_Lite_LPC = 0x14,
        JLink_Lite_STM32 = 0x15,
        JLink_Lite_FSL = 0x16,
        JLink_Lite_ADI = 0x17,
        Energy_Micro_EFM32 = 0x18,
        JLink_Lite_XMC4000 = 0x19,
        JLink_Lite_XMC4200 = 0x20,
        JLink_LPC_LINK2 = 0x21,
        Flasher_Pro = 0x22,
        JTrace_Pro = 0xFE
    }
}
