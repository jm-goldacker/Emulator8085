namespace Emulator8085
{
    /// <summary>
    /// Instruction set of the 8085 CPU
    /// </summary>
    enum InstructionSet
    {
        /// <summary>
        /// Stops the program
        /// </summary>
        HALT = 0x76,

        MOV = 0b01000000,

        MOV_r_M = 0b01000110,

        MOV_M_r = 0b01110000,

        MVI = 0b00000110,

        LXI = 0b00000001,

        LDA = 0b00111010,

        STA = 0b00110010
    }
}
