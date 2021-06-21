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
                bus.TryWriteToMemory(i, 0);
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
                
                if (currentInstruction == null 
                    || currentInstruction == 0
                    || !TryExecuteInstruction((byte)currentInstruction))
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
                    return TryMoveNextByteInMemoryTo(ref A);

                case InstructionSet.MVI_B:
                    return TryMoveNextByteInMemoryTo(ref B);

                case InstructionSet.MVI_C:
                    return TryMoveNextByteInMemoryTo(ref C);

                case InstructionSet.MVI_D:
                    return TryMoveNextByteInMemoryTo(ref D);

                case InstructionSet.MVI_E:
                    return TryMoveNextByteInMemoryTo(ref E);

                case InstructionSet.MVI_H:
                    return TryMoveNextByteInMemoryTo(ref H);

                case InstructionSet.MVI_L:
                    return TryMoveNextByteInMemoryTo(ref L);

                case InstructionSet.MOV_A_A:
                    MoveToRegisterFromRegister(ref A, A);
                    return true;

                case InstructionSet.MOV_A_B:
                    MoveToRegisterFromRegister(ref A, B);
                    return true; 

                case InstructionSet.MOV_A_C:
                    MoveToRegisterFromRegister(ref A, C);
                    return true;

                case InstructionSet.MOV_A_D:
                    MoveToRegisterFromRegister(ref A, D);
                    return true;

                case InstructionSet.MOV_A_E:
                    MoveToRegisterFromRegister(ref A, E);
                    return true;

                case InstructionSet.MOV_A_H:
                    MoveToRegisterFromRegister(ref A, H);
                    return true;

                case InstructionSet.MOV_A_L:
                    MoveToRegisterFromRegister(ref A, L);
                    return true;

                case InstructionSet.MOV_B_A:
                    MoveToRegisterFromRegister(ref B, A);
                    return true;

                case InstructionSet.MOV_B_B:
                    MoveToRegisterFromRegister(ref B, B);
                    return true;

                case InstructionSet.MOV_B_C:
                    MoveToRegisterFromRegister(ref B, C);
                    return true;

                case InstructionSet.MOV_B_D:
                    MoveToRegisterFromRegister(ref B, D);
                    return true;

                case InstructionSet.MOV_B_E:
                    MoveToRegisterFromRegister(ref B, E);
                    return true;

                case InstructionSet.MOV_B_H:
                    MoveToRegisterFromRegister(ref B, H);
                    return true;

                case InstructionSet.MOV_B_L:
                    MoveToRegisterFromRegister(ref B, L);
                    return true;

                case InstructionSet.MOV_C_A:
                    MoveToRegisterFromRegister(ref C, A);
                    return true;

                case InstructionSet.MOV_C_B:
                    MoveToRegisterFromRegister(ref C, B);
                    return true;

                case InstructionSet.MOV_C_C:
                    MoveToRegisterFromRegister(ref C, C);
                    return true;

                case InstructionSet.MOV_C_D:
                    MoveToRegisterFromRegister(ref C, D);
                    return true;

                case InstructionSet.MOV_C_E:
                    MoveToRegisterFromRegister(ref C, E);
                    return true;

                case InstructionSet.MOV_C_H:
                    MoveToRegisterFromRegister(ref C, H);
                    return true;

                case InstructionSet.MOV_C_L:
                    MoveToRegisterFromRegister(ref C, L);
                    return true;

                case InstructionSet.MOV_D_A:
                    MoveToRegisterFromRegister(ref D, A);
                    return true;

                case InstructionSet.MOV_D_B:
                    MoveToRegisterFromRegister(ref D, B);
                    return true;

                case InstructionSet.MOV_D_C:
                    MoveToRegisterFromRegister(ref D, C);
                    return true;

                case InstructionSet.MOV_D_D:
                    MoveToRegisterFromRegister(ref D, D);
                    return true;

                case InstructionSet.MOV_D_E:
                    MoveToRegisterFromRegister(ref D, E);
                    return true;

                case InstructionSet.MOV_D_H:
                    MoveToRegisterFromRegister(ref D, H);
                    return true;

                case InstructionSet.MOV_D_L:
                    MoveToRegisterFromRegister(ref D, L);
                    return true;

                case InstructionSet.MOV_E_A:
                    MoveToRegisterFromRegister(ref E, A);
                    return true;

                case InstructionSet.MOV_E_B:
                    MoveToRegisterFromRegister(ref E, B);
                    return true;

                case InstructionSet.MOV_E_C:
                    MoveToRegisterFromRegister(ref E, C);
                    return true;

                case InstructionSet.MOV_E_D:
                    MoveToRegisterFromRegister(ref E, D);
                    return true;

                case InstructionSet.MOV_E_E:
                    MoveToRegisterFromRegister(ref E, E);
                    return true;

                case InstructionSet.MOV_E_H:
                    MoveToRegisterFromRegister(ref E, H);
                    return true;

                case InstructionSet.MOV_E_L:
                    MoveToRegisterFromRegister(ref E, L);
                    return true;

                case InstructionSet.MOV_H_A:
                    MoveToRegisterFromRegister(ref H, A);
                    return true;

                case InstructionSet.MOV_H_B:
                    MoveToRegisterFromRegister(ref H, B);
                    return true;

                case InstructionSet.MOV_H_C:
                    MoveToRegisterFromRegister(ref H, C);
                    return true;

                case InstructionSet.MOV_H_D:
                    MoveToRegisterFromRegister(ref H, D);
                    return true;

                case InstructionSet.MOV_H_E:
                    MoveToRegisterFromRegister(ref H, E);
                    return true;

                case InstructionSet.MOV_H_H:
                    MoveToRegisterFromRegister(ref H, H);
                    return true;

                case InstructionSet.MOV_H_L:
                    MoveToRegisterFromRegister(ref H, L);
                    return true;

                case InstructionSet.MOV_L_A:
                    MoveToRegisterFromRegister(ref L, A);
                    return true;

                case InstructionSet.MOV_L_B:
                    MoveToRegisterFromRegister(ref L, B);
                    return true;

                case InstructionSet.MOV_L_C:
                    MoveToRegisterFromRegister(ref L, C);
                    return true;

                case InstructionSet.MOV_L_D:
                    MoveToRegisterFromRegister(ref L, D);
                    return true;

                case InstructionSet.MOV_L_E:
                    MoveToRegisterFromRegister(ref L, E);
                    return true;

                case InstructionSet.MOV_L_H:
                    MoveToRegisterFromRegister(ref L, H);
                    return true;

                case InstructionSet.MOV_L_L:
                    MoveToRegisterFromRegister(ref L, L);
                    return true;

                case InstructionSet.MOV_M_A:
                    return TryMoveFromRegisterToMemory(A);

                case InstructionSet.MOV_M_B:
                    return TryMoveFromRegisterToMemory(B);

                case InstructionSet.MOV_M_C:
                    return TryMoveFromRegisterToMemory(C);

                case InstructionSet.MOV_M_D:
                    return TryMoveFromRegisterToMemory(D);

                case InstructionSet.MOV_M_E:
                    return TryMoveFromRegisterToMemory(E);

                case InstructionSet.MOV_M_H:
                    return TryMoveFromRegisterToMemory(H);

                case InstructionSet.MOV_M_L:
                    return TryMoveFromRegisterToMemory(L);

                case InstructionSet.MOV_A_M:
                    return TryMoveFromMemoryToRegister(ref A);

                case InstructionSet.MOV_B_M:
                    return TryMoveFromMemoryToRegister(ref B);

                case InstructionSet.MOV_C_M:
                    return TryMoveFromMemoryToRegister(ref C);

                case InstructionSet.MOV_D_M:
                    return TryMoveFromMemoryToRegister(ref D);

                case InstructionSet.MOV_E_M:
                    return TryMoveFromMemoryToRegister(ref E);

                case InstructionSet.MOV_H_M:
                    return TryMoveFromMemoryToRegister(ref H);

                case InstructionSet.MOV_L_M:
                    return TryMoveFromMemoryToRegister(ref L);

                default:
                    Console.WriteLine("Unknown instruction. Stopping programm!");
                    return false;
            }
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

        private void MoveToRegisterFromRegister(ref byte destinationRegister, byte sourceRegister)
        {
            destinationRegister = sourceRegister;
        }

        private bool TryMoveFromRegisterToMemory(byte sourceRegister)
        {
            ushort memoryAdress = (ushort)(H << 8);
            memoryAdress = (ushort)(memoryAdress | L);

            return bus.TryWriteToMemory(memoryAdress, sourceRegister);
        }

        private bool TryMoveFromMemoryToRegister(ref byte destinationRegister)
        {
            ushort memoryAdress = (ushort)(H << 8);
            memoryAdress = (ushort)(memoryAdress | L);

            var data = bus.ReadFromMemory(memoryAdress);

            if (data == null)
                return false;

            destinationRegister = (byte)data;
            return true;
        }
    }
}
