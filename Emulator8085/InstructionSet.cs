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

        /// <summary>
        /// Moves byte in register A to register A
        /// </summary>
        public const byte MOV_A_A = 0x7F;

        /// <summary>
        /// Moves byte in register B to register A
        /// </summary>
        public const byte MOV_A_B = 0x78;

        /// <summary>
        /// Moves byte in register C to register A
        /// </summary>
        public const byte MOV_A_C = 0x79;

        /// <summary>
        /// Moves byte in register D to register A
        /// </summary>
        public const byte MOV_A_D = 0x7A;

        /// <summary>
        /// Moves byte in register E to register A
        /// </summary>
        public const byte MOV_A_E = 0x7B;

        /// <summary>
        /// Moves byte in register H to register A
        /// </summary>
        public const byte MOV_A_H = 0x7C;

        /// <summary>
        /// Moves byte in register L to register A
        /// </summary>
        public const byte MOV_A_L = 0x7D;

        /// <summary>
        /// Moves byte in register A to register B
        /// </summary>
        public const byte MOV_B_A = 0x47;

        /// <summary>
        /// Moves byte in register B to register B
        /// </summary>
        public const byte MOV_B_B = 0x40;

        /// <summary>
        /// Moves byte in register C to register B
        /// </summary>
        public const byte MOV_B_C = 0x41;

        /// <summary>
        /// Moves byte in register D to register B
        /// </summary>
        public const byte MOV_B_D = 0x42;

        /// <summary>
        /// Moves byte in register E to register B
        /// </summary>
        public const byte MOV_B_E = 0x43;

        /// <summary>
        /// Moves byte in register H to register B
        /// </summary>
        public const byte MOV_B_H = 0x44;

        /// <summary>
        /// Moves byte in register L to register B
        /// </summary>
        public const byte MOV_B_L = 0x45;

        /// <summary>
        /// Moves byte in register A to register C
        /// </summary>
        public const byte MOV_C_A = 0x4F;

        /// <summary>
        /// Moves byte in register B to register C
        /// </summary>
        public const byte MOV_C_B = 0x48;

        /// <summary>
        /// Moves byte in register C to register C
        /// </summary>
        public const byte MOV_C_C = 0x49;

        /// <summary>
        /// Moves byte in register D to register C
        /// </summary>
        public const byte MOV_C_D = 0x4A;

        /// <summary>
        /// Moves byte in register E to register C
        /// </summary>
        public const byte MOV_C_E = 0x4B;

        /// <summary>
        /// Moves byte in register H to register C
        /// </summary>
        public const byte MOV_C_H = 0x4C;

        /// <summary>
        /// Moves byte in register L to register C
        /// </summary>
        public const byte MOV_C_L = 0x4D;

        /// <summary>
        /// Moves byte in register A to register D
        /// </summary>
        public const byte MOV_D_A = 0x57;

        /// <summary>
        /// Moves byte in register B to register D
        /// </summary>
        public const byte MOV_D_B = 0x50;

        /// <summary>
        /// Moves byte in register C to register D
        /// </summary>
        public const byte MOV_D_C = 0x51;

        /// <summary>
        /// Moves byte in register D to register D
        /// </summary>
        public const byte MOV_D_D = 0x52;

        /// <summary>
        /// Moves byte in register E to register D
        /// </summary>
        public const byte MOV_D_E = 0x53;

        /// <summary>
        /// Moves byte in register H to register D
        /// </summary>
        public const byte MOV_D_H = 0x54;

        /// <summary>
        /// Moves byte in register L to register D
        /// </summary>
        public const byte MOV_D_L = 0x55;

        /// <summary>
        /// Moves byte in register A to register E
        /// </summary>
        public const byte MOV_E_A = 0x5F;

        /// <summary>
        /// Moves byte in register B to register E
        /// </summary>
        public const byte MOV_E_B = 0x58;

        /// <summary>
        /// Moves byte in register C to register E
        /// </summary>
        public const byte MOV_E_C = 0x59;

        /// <summary>
        /// Moves byte in register D to register E
        /// </summary>
        public const byte MOV_E_D = 0x5A;

        /// <summary>
        /// Moves byte in register E to register E
        /// </summary>
        public const byte MOV_E_E = 0x5B;

        /// <summary>
        /// Moves byte in register H to register E
        /// </summary>
        public const byte MOV_E_H = 0x5C;

        /// <summary>
        /// Moves byte in register L to register E
        /// </summary>
        public const byte MOV_E_L = 0x5D;

        /// <summary>
        /// Moves byte in register A to register H
        /// </summary>
        public const byte MOV_H_A = 0x67;

        /// <summary>
        /// Moves byte in register B to register H
        /// </summary>
        public const byte MOV_H_B = 0x60;

        /// <summary>
        /// Moves byte in register C to register H
        /// </summary>
        public const byte MOV_H_C = 0x61;

        /// <summary>
        /// Moves byte in register D to register H
        /// </summary>
        public const byte MOV_H_D = 0x62;

        /// <summary>
        /// Moves byte in register E to register H
        /// </summary>
        public const byte MOV_H_E = 0x63;

        /// <summary>
        /// Moves byte in register H to register H
        /// </summary>
        public const byte MOV_H_H = 0x64;

        /// <summary>
        /// Moves byte in register L to register H
        /// </summary>
        public const byte MOV_H_L = 0x65;

        /// <summary>
        /// Moves byte in register A to register L
        /// </summary>
        public const byte MOV_L_A = 0x6F;

        /// <summary>
        /// Moves byte in register B to register L
        /// </summary>
        public const byte MOV_L_B = 0x68;

        /// <summary>
        /// Moves byte in register C to register L
        /// </summary>
        public const byte MOV_L_C = 0x69;

        /// <summary>
        /// Moves byte in register D to register L
        /// </summary>
        public const byte MOV_L_D = 0x6A;

        /// <summary>
        /// Moves byte in register E to register L
        /// </summary>
        public const byte MOV_L_E = 0x6B;

        /// <summary>
        /// Moves byte in register H to register L
        /// </summary>
        public const byte MOV_L_H = 0x6C;

        /// <summary>
        /// Moves byte in register L to register L
        /// </summary>
        public const byte MOV_L_L = 0x6D;

        /// <summary>
        /// Writes content of register A to RAM adress specified in H and L
        /// </summary>
        public const byte MOV_M_A = 0x77;

        /// <summary>
        /// Writes content of register B to RAM adress specified in H and L
        /// </summary>
        public const byte MOV_M_B = 0x70;

        /// <summary>
        /// Writes content of register C to RAM adress specified in H and L
        /// </summary>
        public const byte MOV_M_C = 0x71;

        /// <summary>
        /// Writes content of register D to RAM adress specified in H and L
        /// </summary>
        public const byte MOV_M_D = 0x72;

        /// <summary>
        /// Writes content of register E to RAM adress specified in H and L
        /// </summary>
        public const byte MOV_M_E = 0x73;

        /// <summary>
        /// Writes content of register H to RAM adress specified in H and L
        /// </summary>
        public const byte MOV_M_H = 0x74;

        /// <summary>
        /// Writes content of register L to RAM adress specified in H and L
        /// </summary>
        public const byte MOV_M_L = 0x75;


    }
}
