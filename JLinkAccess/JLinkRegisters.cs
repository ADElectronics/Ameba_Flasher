using System;

namespace JLinkAccess
{
    public enum JLink_ARM_Register : UInt32
    {
        R0 = 0,
        R1,
        R2,
        R3,
        R4,
        R5,
        R6,
        R7,
        CPSR,
        R15,
        R8_USR,
        R9_USR,
        R10_USR,
        R11_USR,
        R12_USR,
        R13_USR,
        R14_USR,
        SPSR_FIQ,
        R8_FIQ,
        R9_FIQ,
        R10_FIQ,
        R11_FIQ,
        R12_FIQ,
        R13_FIQ,
        R14_FIQ,
        SPSR_SVC,
        R13_SVC,
        R14_SVC,
        SPSR_ABT,
        R13_ABT,
        R14_ABT,
        SPSR_IRQ,
        R13_IRQ,
        R14_IRQ,
        SPSR_UND,
        R13_UND,
        R14_UND,
        FPSID,
        FPSCR,
        FPEXC,
        FPS0,
        FPS1,
        FPS2,
        FPS3,
        FPS4,
        FPS5,
        FPS6,
        FPS7,
        FPS8,
        FPS9,
        FPS10,
        FPS11,
        FPS12,
        FPS13,
        FPS14,
        FPS15,
        FPS16,
        FPS17,
        FPS18,
        FPS19,
        FPS20,
        FPS21,
        FPS22,
        FPS23,
        FPS24,
        FPS25,
        FPS26,
        FPS27,
        FPS28,
        FPS29,
        FPS30,
        FPS31,
        R8,
        R9,
        R10,
        R11,
        R12,
        R13,
        R14,
        SPSR,
        NUM_REGS
    }

    public enum JLink_ARM_CM3_Register : UInt32
    {
        R0,
        R1,
        R2,
        R3,
        R4,
        R5,
        R6,
        R7,
        R8,
        R9,
        R10,
        R11,
        R12,
        R13,
        R14,
        R15,
        XPSR,
        MSP,
        PSP,
        RAZ,
        CFBP,
        APSR,
        EPSR,
        IPSR,
        PRIMASK,
        BASEPRI,
        FAULTMASK,
        CONTROL,
        BASEPRI_MAX,
        IAPSR,
        EAPSR,
        IEPSR,
        DWT_CYCCNT = 65,
        MSP_NS,
        PSP_NS,
        MSP_S,
        PSP_S,
        MSPLIM_S,
        PSPLIM_S,
        MSPLIM_NS,
        PSPLIM_NS,
        CFBP_S,
        CFBP_NS,
        PRIMASK_NS,
        BASEPRI_NS,
        FAULTMASK_NS,
        CONTROL_NS,
        BASEPRI_MAX_NS,
        PRIMASK_S,
        BASEPRI_S,
        FAULTMASK_S,
        CONTROL_S,
        BASEPRI_MAX_S,
        MSPLIM,
        PSPLIM,
        BASEPRI_BASE0, // (CFBP[15:8]  >>  8) & 0xFF
        FAULTMASK_BASE0, // (CFBP[23:16] >> 16) & 0xFF
        CONTROL_BASE0, // (CFBP[31:24] >> 24) & 0xFF
        BASEPRI_MAX_BASE0, // (CFBP[15:8]  >>  8) & 0xFF
        NUM_REGS
    }

    public enum JLink_ARM_CM4_Register : UInt32
    {
        R0,
        R1,
        R2,
        R3,
        R4,
        R5,
        R6,
        R7,
        R8,
        R9,
        R10,
        R11,
        R12,
        R13,
        R14,
        R15,
        XPSR,
        MSP,
        PSP,
        RAZ,
        CFBP, // CONTROL/FAULTMASK/BASEPRI/PRIMASK (packed into 4 bytes of word. CONTROL = CFBP[31:24], FAULTMASK = CFBP[16:23], BASEPRI = CFBP[15:8], PRIMASK = CFBP[7:0]

        // Data for CFBP pseudo regs is expected & returned
        // in same byte lane as when reading/writing the CFBP.
        // This means when reading
        //  PRIMASK,     the data of PRIMASK is expected/returned at     bits [7:0]
        //  BASEPRI,     the data of BASEPRI is expected/returned at     bits [15:8]
        //  BASEPRI_MAX, the data of BASEPRI_MAX is expected/returned at bits [15:8]
        //  FAULTMASK,   the data of FAULTMASK is expected/returned at   bits [23:16]
        //  CONTROL,     the data of CONTROL is expected/returned at     bits [31:24]
        //
        // Sample CFBP = 0xx11223344
        // Read PRIMASK   will return 0x00000044
        // Read BASEPRI   will return 0x00003300
        // Read FAULTMASK will return 0x00220000
        // Read CONTROL   will return 0x11000000
        // When writing, data is expected at the same position

        APSR,
        EPSR,
        IPSR,
        PRIMASK,
        BASEPRI,
        FAULTMASK,
        CONTROL,
        BASEPRI_MAX,
        IAPSR,
        EAPSR,
        IEPSR,
        FPSCR,
        FPS0,
        FPS1,
        FPS2,
        FPS3,
        FPS4,
        FPS5,
        FPS6,
        FPS7,
        FPS8,
        FPS9,
        FPS10,
        FPS11,
        FPS12,
        FPS13,
        FPS14,
        FPS15,
        FPS16,
        FPS17,
        FPS18,
        FPS19,
        FPS20,
        FPS21,
        FPS22,
        FPS23,
        FPS24,
        FPS25,
        FPS26,
        FPS27,
        FPS28,
        FPS29,
        FPS30,
        FPS31,
        DWT_CYCCNT,
        // New regs introduced with ARMv8M architecture
        MSP_NS,
        PSP_NS,
        MSP_S,
        PSP_S,
        MSPLIM_S,
        PSPLIM_S,
        MSPLIM_NS,
        PSPLIM_NS,
        CFBP_S,
        CFBP_NS,

        // Data for CFBP pseudo regs is expected & returned
        // in same byte lane as when reading/writing the CFBP.
        // This means when reading
        //  PRIMASK,     the data of PRIMASK is expected/returned at     bits [7:0]
        //  BASEPRI,     the data of BASEPRI is expected/returned at     bits [15:8]
        //  BASEPRI_MAX, the data of BASEPRI_MAX is expected/returned at bits [15:8]
        //  FAULTMASK,   the data of FAULTMASK is expected/returned at   bits [23:16]
        //  CONTROL,     the data of CONTROL is expected/returned at     bits [31:24]
        //
        // Sample CFBP = 0xx11223344
        // Read PRIMASK   will return 0x00000044
        // Read BASEPRI   will return 0x00003300
        // Read FAULTMASK will return 0x00220000
        // Read CONTROL   will return 0x11000000
        // When writing, data is expected at the same position

        PRIMASK_NS,
        BASEPRI_NS,
        FAULTMASK_NS,
        CONTROL_NS,
        BASEPRI_MAX_NS,
        PRIMASK_S,
        BASEPRI_S,
        FAULTMASK_S,
        CONTROL_S,
        BASEPRI_MAX_S,
        MSPLIM,
        PSPLIM,
        NUM_REGS
    }

    public enum JLink_RISCV_Register : UInt32
    {
        FFLAGS = 0x001,          // Bits [4:0] of FCSR
        FRM,                     // Bits [7:5] of FCSR
        FCSR,                    // 32-bit
        USTATUS = 0x000,         // 32/64-bit
        UIE = 0x004,             // 32/64-bit
        UTVEC = 0x005,
        USCRATCH = 0x040,
        UEPC,                    // 32/64-bit
        UCAUSE,
        UTVAL,
        UIP,                     // 32/64-bit
        // Gap
        SSTATUS = 0x100,         // 32/64-bit
        // Gap
        SEDELEG = 0x102,         // 32/64-bit
        SIDELEG,                 // 32/64-bit
        SIE,                     // 32/64-bit
        STVEC,                   // 32/64-bit
        SCOUNTEREN,              // 32-bit
        // Gap
        SSCRATCH = 0x140,        // 32/64-bit
        SEPC,                    // 32/64-bit
        SCAUSE,                  // 32/64-bit
        STVAL,                   // 32/64-bit Also called "SBADADDR"
        SIP,                     // 32/64-bit
        // Gap
        SATP = 0x0180,           // 32/64-bit Also called "SPTBR"
        // Gap
        MSTATUS = 0x300,         // 32/64-bit
        MISA,                    // 32/64-bit
        MEDELEG,                 // 32/64-bit
        MIDELEG,                 // 32/64-bit
        MIE,                     // 32/64-bit
        MTVEC,                   // 32/64-bit
        MCOUNTEREN,              // 32-bit
        // Gap
        MHPMEVENT3 = 0x323,      // 32/64-bit
        MHPMEVENT4,              // 32/64-bit
        MHPMEVENT5,              // 32/64-bit
        MHPMEVENT6,              // 32/64-bit
        MHPMEVENT7,              // 32/64-bit
        MHPMEVENT8,              // 32/64-bit
        MHPMEVENT9,              // 32/64-bit
        MHPMEVENT10,             // 32/64-bit
        MHPMEVENT11,             // 32/64-bit
        MHPMEVENT12,             // 32/64-bit
        MHPMEVENT13,             // 32/64-bit
        MHPMEVENT14,             // 32/64-bit
        MHPMEVENT15,             // 32/64-bit
        MHPMEVENT16,             // 32/64-bit
        MHPMEVENT17,             // 32/64-bit
        MHPMEVENT18,             // 32/64-bit
        MHPMEVENT19,             // 32/64-bit
        MHPMEVENT20,             // 32/64-bit
        MHPMEVENT21,             // 32/64-bit
        MHPMEVENT22,             // 32/64-bit
        MHPMEVENT23,             // 32/64-bit
        MHPMEVENT24,             // 32/64-bit
        MHPMEVENT25,             // 32/64-bit
        MHPMEVENT26,             // 32/64-bit
        MHPMEVENT27,             // 32/64-bit
        MHPMEVENT28,             // 32/64-bit
        MHPMEVENT29,             // 32/64-bit
        MHPMEVENT30,             // 32/64-bit
        MHPMEVENT31,             // 32/64-bit
        // Gap
        MSCRATCH = 0x340,        // 32/64-bit
        MEPC,                    // 32/64-bit
        MCAUSE,                  // 32/64-bit
        MTVAL,                   // Also called "MBADADDR"
        MIP,                     // 32/64-bit
        // Gap
        PMPCFG0 = 0x3A0,         // 32-bit
        PMPCFG1,                 // 32-bit
        PMPCFG2,                 // 32-bit
        PMPCFG3,                 // 32-bit
        // Gap
        PMPADDR0 = 0x3B0,        // 32/64-bit
        PMPADDR1,                // 32/64-bit
        PMPADDR2,                // 32/64-bit
        PMPADDR3,                // 32/64-bit
        PMPADDR4,                // 32/64-bit
        PMPADDR5,                // 32/64-bit
        PMPADDR6,                // 32/64-bit
        PMPADDR7,                // 32/64-bit
        PMPADDR8,                // 32/64-bit
        PMPADDR9,                // 32/64-bit
        PMPADDR10,               // 32/64-bit
        PMPADDR11,               // 32/64-bit
        PMPADDR12,               // 32/64-bit
        PMPADDR13,               // 32/64-bit
        PMPADDR14,               // 32/64-bit
        PMPADDR15,               // 32/64-bit
        // Gap
        TSELECT = 0x7A0,         // 32/64-bit
        TDATA1,                  // 32/64-bit
        TDATA2,                  // 32/64-bit
        TDATA3,                  // 32/64-bit
        // Gap
        DCSR = 0x7B0,            // 32-bit
        DPC,                     // 32/64-bit
        DSCRATCH,
        // Gap
        MCYCLE = 0xB00,
        // Gap
        MINSTRET = 0xB02,
        MHPMCOUNTER3, 
        MHPMCOUNTER4, 
        MHPMCOUNTER5, 
        MHPMCOUNTER6, 
        MHPMCOUNTER7, 
        MHPMCOUNTER8, 
        MHPMCOUNTER9, 
        MHPMCOUNTER10,
        MHPMCOUNTER11,
        MHPMCOUNTER12,
        MHPMCOUNTER13,
        MHPMCOUNTER14,
        MHPMCOUNTER15,
        MHPMCOUNTER16,
        MHPMCOUNTER17,
        MHPMCOUNTER18,
        MHPMCOUNTER19,
        MHPMCOUNTER20,
        MHPMCOUNTER21,
        MHPMCOUNTER22,
        MHPMCOUNTER23,
        MHPMCOUNTER24,
        MHPMCOUNTER25,
        MHPMCOUNTER26,
        MHPMCOUNTER27,
        MHPMCOUNTER28,
        MHPMCOUNTER29,
        MHPMCOUNTER30,
        MHPMCOUNTER31,
        // Gap
        MCYCLEH = 0xB80,   
        // Gap
        MINSTRETH = 0xB82,
        MHPMCOUNTER3H, 
        MHPMCOUNTER4H, 
        MHPMCOUNTER5H, 
        MHPMCOUNTER6H, 
        MHPMCOUNTER7H, 
        MHPMCOUNTER8H, 
        MHPMCOUNTER9H, 
        MHPMCOUNTER10H,
        MHPMCOUNTER11H,
        MHPMCOUNTER12H,
        MHPMCOUNTER13H,
        MHPMCOUNTER14H,
        MHPMCOUNTER15H,
        MHPMCOUNTER16H,
        MHPMCOUNTER17H,
        MHPMCOUNTER18H,
        MHPMCOUNTER19H,
        MHPMCOUNTER20H,
        MHPMCOUNTER21H,
        MHPMCOUNTER22H,
        MHPMCOUNTER23H,
        MHPMCOUNTER24H,
        MHPMCOUNTER25H,
        MHPMCOUNTER26H,
        MHPMCOUNTER27H,
        MHPMCOUNTER28H,
        MHPMCOUNTER29H,
        MHPMCOUNTER30H,
        MHPMCOUNTER31H,
        // Gap
        CYCLE = 0xC00,           // 64-bit
        TIME,                    // 64-bit
        INSTRET,                 // 64-bit
        HPMCOUNTER3, 
        HPMCOUNTER4, 
        HPMCOUNTER5, 
        HPMCOUNTER6, 
        HPMCOUNTER7, 
        HPMCOUNTER8, 
        HPMCOUNTER9, 
        HPMCOUNTER10,
        HPMCOUNTER11,
        HPMCOUNTER12,
        HPMCOUNTER13,
        HPMCOUNTER14,
        HPMCOUNTER15,
        HPMCOUNTER16,
        HPMCOUNTER17,
        HPMCOUNTER18,
        HPMCOUNTER19,
        HPMCOUNTER20,
        HPMCOUNTER21,
        HPMCOUNTER22,
        HPMCOUNTER23,
        HPMCOUNTER24,
        HPMCOUNTER25,
        HPMCOUNTER26,
        HPMCOUNTER27,
        HPMCOUNTER28,
        HPMCOUNTER29,
        HPMCOUNTER30,
        HPMCOUNTER31,
        // Gap
        CYCLEH = 0xC80,
        TIMEH,
        INSTRETH,
        HPMCOUNTER3H,
        HPMCOUNTER4H,
        HPMCOUNTER5H,
        HPMCOUNTER6H,
        HPMCOUNTER7H,
        HPMCOUNTER8H,
        HPMCOUNTER9H,
        HPMCOUNTER10H,
        HPMCOUNTER11H,
        HPMCOUNTER12H,
        HPMCOUNTER13H,
        HPMCOUNTER14H,
        HPMCOUNTER15H,
        HPMCOUNTER16H,
        HPMCOUNTER17H,
        HPMCOUNTER18H,
        HPMCOUNTER19H,
        HPMCOUNTER20H,
        HPMCOUNTER21H,
        HPMCOUNTER22H,
        HPMCOUNTER23H,
        HPMCOUNTER24H,
        HPMCOUNTER25H,
        HPMCOUNTER26H,
        HPMCOUNTER27H,
        HPMCOUNTER28H,
        HPMCOUNTER29H,
        HPMCOUNTER30H,
        HPMCOUNTER31H,
        // Gap
        MVENDORID = 0xF11,       // 32/64-bit
        MARCHID,                 // 32/64-bit
        MIMPID,                  // 32/64-bit
        MHARTID,                 // 32/64-bit
        // Gap
        X0 = 0x1000,             // 32/64-bit
        X1,                      // 32/64-bit
        X2,                      // 32/64-bit
        X3,                      // 32/64-bit
        X4,                      // 32/64-bit
        X5,                      // 32/64-bit
        X6,                      // 32/64-bit
        X7,                      // 32/64-bit
        X8,                      // 32/64-bit
        X9,                      // 32/64-bit
        X10,                     // 32/64-bit
        X11,                     // 32/64-bit
        X12,                     // 32/64-bit
        X13,                     // 32/64-bit
        X14,                     // 32/64-bit
        X15,                     // 32/64-bit
        X16,                     // 32/64-bit
        X17,                     // 32/64-bit
        X18,                     // 32/64-bit
        X19,                     // 32/64-bit
        X20,                     // 32/64-bit
        X21,                     // 32/64-bit
        X22,                     // 32/64-bit
        X23,                     // 32/64-bit
        X24,                     // 32/64-bit
        X25,                     // 32/64-bit
        X26,                     // 32/64-bit
        X27,                     // 32/64-bit
        X28,                     // 32/64-bit
        X29,                     // 32/64-bit
        X30,                     // 32/64-bit
        X31,                     // 32/64-bit
        // Gap
        F0 = 0x1020,
        F1, 
        F2, 
        F3, 
        F4, 
        F5, 
        F6, 
        F7, 
        F8, 
        F9, 
        F10,
        F11,
        F12,
        F13,
        F14,
        F15,
        F16,
        F17,
        F18,
        F19,
        F20,
        F21,
        F22,
        F23,
        F24,
        F25,
        F26,
        F27,
        F28,
        F29,
        F30,
        F31,
        // Gap
        V0 = 0x1040,
        V1, 
        V2, 
        V3, 
        V4, 
        V5, 
        V6, 
        V7, 
        V8, 
        V9, 
        V10,
        V11,
        V12,
        V13,
        V14,
        V15,
        V16,
        V17,
        V18,
        V19,
        V20,
        V21,
        V22,
        V23,
        V24,
        V25,
        V26,
        V27,
        V28,
        V29,
        V30,
        V31,
        VP0,
        VP1,
        VP2,
        VP3,
        VP4,
        VP5,
        VP6,
        VP7,
        // Gap
        PC = 0x1080,             // 32/64-bit
        JLINK_RISCV_NUM_REGS
        // 0x10000 - 0x1FFFF are reserved for DLL internal use
    }
}
