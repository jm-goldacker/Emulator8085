namespace Emulator8085
{
    /// <summary>
    /// Instruction set of the 8085 CPU
    /// </summary>
    class InstructionSet
    {
        /// <summary>
        /// Stops the program
        /// </summary>
        public const byte HALT = 0x76;

        /// <summary>
        /// Moves next byte in memory to register A
        /// </summary>
        public const byte MVI_A = 0x3E;

        /// <summary>
        /// Moves next byte in memory to register B
        /// </summary>
        public const byte MVI_B = 0x06;

        /// <summary>
        /// Moves next byte in memory to register C
        /// </summary>
        public const byte MVI_C = 0x0E;

        /// <summary>
        /// Moves next byte in memory to register D
        /// </summary>
        public const byte MVI_D = 0x16;

        /// <summary>
        /// Moves next byte in memory to register E
        /// </summary>
        public const byte MVI_E = 0x1E;

        /// <summary>
        /// Moves next byte in memory to register H
        /// </summary>
        public const byte MVI_H = 0x26;

        /// <summary>
        /// Moves next byte in memory to register L
        /// </summary>
        public const byte MVI_L = 0x2E;
    }
}
