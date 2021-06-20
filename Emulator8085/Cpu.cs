using System;

namespace Emulator8085
{
    /// <summary>
    /// The 8085 CPU
    /// </summary>
    class Cpu
    {
        // Registers
        private byte A;
        private byte B;
        private byte C;
        private byte D;
        private byte E;
        private byte H;
        private byte L;

        // Program Counter
        private ushort IC;
        // Stack Pointer
        private ushort SP;

        // Bus which connects other components with the CPU
        private Bus bus;

        /// <summary>
        /// Creates a new Cpu and sets the program counter to the first adress of the RAM.
        /// </summary>
        public Cpu()
        {
            IC = 0x2000;
        }

        /// <summary>
        /// Connects the CPU to the bus.
        /// </summary>
        /// <param name="bus">the bus</param>
        public void ConnectToBus(Bus bus)
        {
            this.bus = bus;
        }

        /// <summary>
        /// Resets the CPU and clears the RAM
        /// </summary>
        public void Reset()
        {
            IC = 0x2000;

            for (ushort i = 0x2000; i < 0x27FF; i += 0x0001)
            {
                bus.TryWriteToMemory(i, 0x0000);
            }
        }

        /// <summary>
        /// Executes the program that is loaded in the RAM.
        /// </summary>
        public void Execute()
        {
            byte? currentInstruction = bus.ReadFromMemory(IC);

            while (currentInstruction != InstructionSet.HALT)
            {
                PrintRegisters();
                
                if (currentInstruction == null || !TryExecuteInstruction((byte)currentInstruction))
                    break;

                IC += 0x0001;
                currentInstruction = bus.ReadFromMemory(IC);
            }

            PrintRegisters();
        }

        private void PrintRegisters()
        {
            Console.WriteLine("-------------------");
            Console.WriteLine($"A: 0x{A:X2}");
            Console.WriteLine($"B: 0x{B:X2}");
            Console.WriteLine($"C: 0x{C:X2}");
            Console.WriteLine($"D: 0x{D:X2}");
            Console.WriteLine($"E: 0x{E:X2}");
            Console.WriteLine($"H: 0x{H:X2}");
            Console.WriteLine($"L: 0x{L:X2}");
            Console.WriteLine($"IC: 0x{IC:X4}");
            Console.WriteLine($"SP: 0x{SP:X4}");
            Console.WriteLine("-------------------\n");
        }

        /// <summary>
        /// Executes the given instruction.
        /// </summary>
        /// <param name="instruction">the instruction to execute</param>
        /// <returns>true, if execution was successfull; otherwise false</returns>
        private bool TryExecuteInstruction(byte instruction)
        {
            switch (instruction)
            {
                case InstructionSet.MVI_A:
                    TryMoveNextByteInMemoryTo(ref A);
                    break;

                case InstructionSet.MVI_B:
                    TryMoveNextByteInMemoryTo(ref B);
                    break;

                case InstructionSet.MVI_C:
                    TryMoveNextByteInMemoryTo(ref C);
                    break;

                case InstructionSet.MVI_D:
                    TryMoveNextByteInMemoryTo(ref D);
                    break;

                case InstructionSet.MVI_E:
                    TryMoveNextByteInMemoryTo(ref E);
                    break;

                case InstructionSet.MVI_H:
                    TryMoveNextByteInMemoryTo(ref H);
                    break;

                case InstructionSet.MVI_L:
                    TryMoveNextByteInMemoryTo(ref L);
                    break;

                case InstructionSet.MOV_A_A:
                    MoveToFrom(ref A, A);
                    break;

                case InstructionSet.MOV_A_B:
                    MoveToFrom(ref A, B);
                    break;

                case InstructionSet.MOV_A_C:
                    MoveToFrom(ref A, C);
                    break;

                case InstructionSet.MOV_A_D:
                    MoveToFrom(ref A, D);
                    break;

                case InstructionSet.MOV_A_E:
                    MoveToFrom(ref A, E);
                    break;

                case InstructionSet.MOV_A_H:
                    MoveToFrom(ref A, H);
                    break;

                case InstructionSet.MOV_A_L:
                    MoveToFrom(ref A, L);
                    break;

                case InstructionSet.MOV_B_A:
                    MoveToFrom(ref B, A);
                    break;

                case InstructionSet.MOV_B_B:
                    MoveToFrom(ref B, B);
                    break;

                case InstructionSet.MOV_B_C:
                    MoveToFrom(ref B, C);
                    break;

                case InstructionSet.MOV_B_D:
                    MoveToFrom(ref B, D);
                    break;

                case InstructionSet.MOV_B_E:
                    MoveToFrom(ref B, E);
                    break;

                case InstructionSet.MOV_B_H:
                    MoveToFrom(ref B, H);
                    break;

                case InstructionSet.MOV_B_L:
                    MoveToFrom(ref B, L);
                    break;

                case InstructionSet.MOV_C_A:
                    MoveToFrom(ref C, A);
                    break;

                case InstructionSet.MOV_C_B:
                    MoveToFrom(ref C, B);
                    break;

                case InstructionSet.MOV_C_C:
                    MoveToFrom(ref C, C);
                    break;

                case InstructionSet.MOV_C_D:
                    MoveToFrom(ref C, D);
                    break;

                case InstructionSet.MOV_C_E:
                    MoveToFrom(ref C, E);
                    break;

                case InstructionSet.MOV_C_H:
                    MoveToFrom(ref C, H);
                    break;

                case InstructionSet.MOV_C_L:
                    MoveToFrom(ref C, L);
                    break;

                case InstructionSet.MOV_D_A:
                    MoveToFrom(ref D, A);
                    break;

                case InstructionSet.MOV_D_B:
                    MoveToFrom(ref D, B);
                    break;

                case InstructionSet.MOV_D_C:
                    MoveToFrom(ref D, C);
                    break;

                case InstructionSet.MOV_D_D:
                    MoveToFrom(ref D, D);
                    break;

                case InstructionSet.MOV_D_E:
                    MoveToFrom(ref D, E);
                    break;

                case InstructionSet.MOV_D_H:
                    MoveToFrom(ref D, H);
                    break;

                case InstructionSet.MOV_D_L:
                    MoveToFrom(ref D, L);
                    break;

                case InstructionSet.MOV_E_A:
                    MoveToFrom(ref E, A);
                    break;

                case InstructionSet.MOV_E_B:
                    MoveToFrom(ref E, B);
                    break;

                case InstructionSet.MOV_E_C:
                    MoveToFrom(ref E, C);
                    break;

                case InstructionSet.MOV_E_D:
                    MoveToFrom(ref E, D);
                    break;

                case InstructionSet.MOV_E_E:
                    MoveToFrom(ref E, E);
                    break;

                case InstructionSet.MOV_E_H:
                    MoveToFrom(ref E, H);
                    break;

                case InstructionSet.MOV_E_L:
                    MoveToFrom(ref E, L);
                    break;

                case InstructionSet.MOV_H_A:
                    MoveToFrom(ref H, A);
                    break;

                case InstructionSet.MOV_H_B:
                    MoveToFrom(ref H, B);
                    break;

                case InstructionSet.MOV_H_C:
                    MoveToFrom(ref H, C);
                    break;

                case InstructionSet.MOV_H_D:
                    MoveToFrom(ref H, D);
                    break;

                case InstructionSet.MOV_H_E:
                    MoveToFrom(ref H, E);
                    break;

                case InstructionSet.MOV_H_H:
                    MoveToFrom(ref H, H);
                    break;

                case InstructionSet.MOV_H_L:
                    MoveToFrom(ref H, L);
                    break;

                case InstructionSet.MOV_L_A:
                    MoveToFrom(ref L, A);
                    break;

                case InstructionSet.MOV_L_B:
                    MoveToFrom(ref L, B);
                    break;

                case InstructionSet.MOV_L_C:
                    MoveToFrom(ref L, C);
                    break;

                case InstructionSet.MOV_L_D:
                    MoveToFrom(ref L, D);
                    break;

                case InstructionSet.MOV_L_E:
                    MoveToFrom(ref L, E);
                    break;

                case InstructionSet.MOV_L_H:
                    MoveToFrom(ref L, H);
                    break;

                case InstructionSet.MOV_L_L:
                    MoveToFrom(ref L, L);
                    break;

                    /*
                case InstructionSet.MOV_M_A:
                    break;

                case InstructionSet.MOV_M_B:
                    break;

                case InstructionSet.MOV_M_C:
                    break;

                case InstructionSet.MOV_M_D:
                    break;

                case InstructionSet.MOV_M_E:
                    break;

                case InstructionSet.MOV_M_H:
                    break;

                case InstructionSet.MOV_M_L:
                    break;
                    */
                default:
                    Console.WriteLine("Unknown instruction. Stopping programm!");
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Writes the next byte in the RAM to the given register
        /// </summary>
        /// <param name="register">register to which the next byte should be written to</param>
        /// <returns>true, if reading was successfull; otherwise false</returns>
        private bool TryMoveNextByteInMemoryTo(ref byte register)
        {
            IC += 0x0001;
            byte? data = bus.ReadFromMemory(IC);
            
            if (data == null)
                return false;

            register = (byte)data;
            return true;
        }

        private void MoveToFrom(ref byte destinationRegister, byte sourceRegister)
        {
            destinationRegister = sourceRegister;
        }
    }
}
