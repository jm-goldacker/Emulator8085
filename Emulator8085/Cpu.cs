using System;

namespace Emulator8085
{
    /// <summary>
    /// The 8085 CPU
    /// </summary>
    unsafe class Cpu
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
        private ushort PC;
        // Stack Pointer
        private ushort SP;

        // Bus which connects other components with the CPU
        private Bus bus;

        // the instruction set
        private InstructionSet instructionSet;

        /// <summary>
        /// Creates a new Cpu and sets the program counter to the first adress of the RAM.
        /// </summary>
        public Cpu()
        {
            instructionSet = new();

            PC = 0x2000;
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
            PC = 0x2000;

            for (ushort i = 0x2000; i < 0x27FF; i += 0x0001)
            {
                bus.WriteToMemory(i, 0x0000);
            }
        }

        /// <summary>
        /// Executes the program that is loaded in the RAM.
        /// </summary>
        public void Execute()
        {
            byte currentInstruction = bus.ReadFromMemory(PC);

            while (currentInstruction != InstructionSet.HALT)
            {
                ExecuteInstruction(currentInstruction);

                PC += 0x0001;
                currentInstruction = bus.ReadFromMemory(PC);
            }

            // Debug
            Console.WriteLine($"A: {A}");
            Console.WriteLine($"B: {B}");
            Console.WriteLine($"C: {C}");
            Console.WriteLine($"D: {D}");
            Console.WriteLine($"E: {E}");
            Console.WriteLine($"H: {H}");
            Console.WriteLine($"L: {L}");
        }

        /// <summary>
        /// Executes the given instruction.
        /// </summary>
        /// <param name="instruction">the instruction to execute</param>
        private void ExecuteInstruction(byte instruction)
        {
            switch (instruction)
            {
                case InstructionSet.MVI_A:
                    fixed (byte* p = &A)
                        MoveNextByteInMemoryTo(p);
                    break;

                case InstructionSet.MVI_B:
                    fixed (byte* p = &B)
                        MoveNextByteInMemoryTo(p);
                    break;

                case InstructionSet.MVI_C:
                    fixed (byte* p = &C)
                        MoveNextByteInMemoryTo(p);
                    break;

                case InstructionSet.MVI_D:
                    fixed (byte* p = &D)
                        MoveNextByteInMemoryTo(p);
                    break;

                case InstructionSet.MVI_E:
                    fixed (byte* p = &E)
                        MoveNextByteInMemoryTo(p);
                    break;

                case InstructionSet.MVI_H:
                    fixed (byte* p = &H)
                        MoveNextByteInMemoryTo(p);
                    break;

                case InstructionSet.MVI_L:
                    fixed (byte* p = &L)
                        MoveNextByteInMemoryTo(p);
                    break;
            }
        }

        /// <summary>
        /// Writes the next byte in the RAM to the given register
        /// </summary>
        /// <param name="register">register to which the next byte should be written to</param>
        private void MoveNextByteInMemoryTo(byte* register)
        {
            PC += 0x0001;
            byte data = bus.ReadFromMemory(PC);
            *register = data;
        }
    }
}
