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

                case InstructionSet.MOV_A_B:
                    MoveFromTo(B, ref A);
                    break;

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

        private void MoveFromTo(byte sourceRegister, ref byte destinationRegister)
        {
            destinationRegister = sourceRegister;
        }
    }
}
