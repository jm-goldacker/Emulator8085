using Emulator8085.Masks;
using System;
using System.Collections.Generic;

namespace Emulator8085
{
    /// <summary>
    /// The 8085 CPU
    /// </summary>
    class Cpu
    {
        // Registers
        private Dictionary<byte, byte> registers = new Dictionary<byte, byte>
        {
            { 0b111, 0 }, // A
            { 0b000, 0 }, // B 
            { 0b001, 0 }, // C
            { 0b010, 0 }, // D
            { 0b011, 0 }, // E
            { 0b100, 0 }, // H
            { 0b101, 0 }, // L
        };

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
                bus.WriteToMemory(i, 0);
            }
        }

        /// <summary>
        /// Executes the program that is loaded in the RAM.
        /// </summary>
        public void Execute()
        {
            byte? currentInstruction = bus.ReadFromMemory(IC);

            while (currentInstruction != (byte)InstructionSet.HALT)
            {
                PrintRegisters();
                
                if (currentInstruction == null || currentInstruction == 0)
                    break;

                ExecuteInstruction((byte)currentInstruction);

                IC += 0x0001;
                currentInstruction = bus.ReadFromMemory(IC);
            }

            PrintRegisters();
        }

        private void PrintRegisters()
        {
            Console.WriteLine("-------------------");
            Console.WriteLine($"A: 0x{registers[(byte)RegisterAddresses.A]:X2}");
            Console.WriteLine($"B: 0x{registers[(byte)RegisterAddresses.B]:X2}");
            Console.WriteLine($"C: 0x{registers[(byte)RegisterAddresses.C]:X2}");
            Console.WriteLine($"D: 0x{registers[(byte)RegisterAddresses.D]:X2}");
            Console.WriteLine($"E: 0x{registers[(byte)RegisterAddresses.E]:X2}");
            Console.WriteLine($"H: 0x{registers[(byte)RegisterAddresses.H]:X2}");
            Console.WriteLine($"L: 0x{registers[(byte)RegisterAddresses.L]:X2}");
            Console.WriteLine($"IC: 0x{IC:X4}");
            Console.WriteLine($"SP: 0x{SP:X4}");
            Console.WriteLine("-------------------\n");
        }

        /// <summary>
        /// Executes the given instruction.
        /// </summary>
        /// <param name="instruction">the instruction to execute</param>
        /// <returns>true, if execution was successfull; otherwise false</returns>
        private void ExecuteInstruction(byte instruction)
        {
            if ((instruction & (byte)Opcodes.MOV) == (byte)InstructionSet.MOV)
            {
                var source = (byte)(instruction & 0b00000111);
                var destination = (byte)((instruction >> 3) & 0b00000111);
                MoveFromTo(destination, source);
                return;
            }

            if ((instruction & (byte)Opcodes.MVI) == (byte)InstructionSet.MVI)
            {
                var destination = (byte)((instruction >> 3));

                if (destination == 0b110)
                {
                    MoveNextByteInMemoryToMemory();
                    return;
                }

                MoveNextByteInMemoryToRegister(destination);
                return;
            }

            if ((instruction & (byte)Opcodes.LXI) == (byte)InstructionSet.LXI)
            {
                byte destinationAddress = (byte)(instruction >> 4);
                LoadRegisterPairImmediate(destinationAddress);
                return;
            }

            switch (instruction)
            {
                case (byte)InstructionSet.LDA:
                    LoadAImmediate();
                    break;
                case (byte)InstructionSet.STA:
                    StoreAImmediate();
                    break;
                default:
                    throw new ArgumentException("Unkown instruction. Exiting program!");
            }
        }

        private void MoveFromTo(byte destinationAdress, byte sourceAdress)
        {
            if (destinationAdress == 0b110)
                MoveFromRegisterToMemory(sourceAdress);

            else if (sourceAdress == 0b110)
                MoveFromMemoryToRegister(destinationAdress);

            else if (!registers.ContainsKey(destinationAdress) || !registers.ContainsKey(sourceAdress))
                throw new ArgumentException($"Register address {destinationAdress:X2} not known");
            
            registers[destinationAdress] = registers[sourceAdress];
        }

        private void MoveFromRegisterToMemory(byte sourceRegisterAdress)
        {
            var memoryAdress = GetMemoryAdressFromRegistersHandL();

            bus.WriteToMemory(memoryAdress, registers[sourceRegisterAdress]);
        }

        private void MoveFromMemoryToRegister(byte destinationRegisterAdress)
        {
            var memoryAdress = GetMemoryAdressFromRegistersHandL();

            var data = bus.ReadFromMemory(memoryAdress);

            if (!registers.ContainsKey(destinationRegisterAdress))
                throw new ArgumentException($"Register address {destinationRegisterAdress:X2} not known");

            registers[destinationRegisterAdress] = (byte)data;
        }

        /// <summary>
        /// Writes the next byte in the RAM to the given register
        /// </summary>
        /// <param name="register">register to which the next byte should be written to</param>
        /// <returns>true, if reading was successfull; otherwise false</returns>
        private void MoveNextByteInMemoryToRegister(byte destinationAdress)
        {
            var data = ReadNextByte();
            
            if (!registers.ContainsKey(destinationAdress))
                throw new ArgumentException($"Register address {destinationAdress:X2} not known");

            registers[destinationAdress] = (byte)data;
        }

        private void MoveNextByteInMemoryToMemory()
        {
            var memoryAdress = GetMemoryAdressFromRegistersHandL();

            var data = ReadNextByte();
            bus.WriteToMemory(memoryAdress, data);
        }

        private void LoadRegisterPairImmediate(byte registerPairAddress)
        {
            var lowData = ReadNextByte();
            var highData = ReadNextByte();

            switch (registerPairAddress)
            {
                case (byte)RegisterPairAddresses.B:
                    registers[(byte)RegisterAddresses.B] = highData;
                    registers[(byte)RegisterAddresses.C] = lowData;
                    break;

                case (byte)RegisterPairAddresses.D:
                    registers[(byte)RegisterAddresses.D] = highData;
                    registers[(byte)RegisterAddresses.E] = lowData;
                    break;

                case (byte)RegisterPairAddresses.H:
                    registers[(byte)RegisterAddresses.H] = highData;
                    registers[(byte)RegisterAddresses.L] = lowData;
                    break;

                case (byte)RegisterPairAddresses.SP:
                    SP = CombineBytes(lowData, highData);
                    break;

                default:
                    throw new ArgumentException($"Register pair address not known: {registerPairAddress:X2}");
            }
        }

        private void LoadAImmediate()
        {
            var lowData = ReadNextByte();
            var highData = ReadNextByte();
            var memoryAddress = CombineBytes(lowData, highData);
            var data = bus.ReadFromMemory(memoryAddress);
            registers[(byte)RegisterAddresses.A] = data;
        }

        private void StoreAImmediate()
        {
            var lowData = ReadNextByte();
            var highData = ReadNextByte();
            var memoryAddress = CombineBytes(lowData, highData);
            bus.WriteToMemory(memoryAddress, registers[(byte)RegisterAddresses.A]);
        }

        private ushort GetMemoryAdressFromRegistersHandL()
        {
            ushort memoryAdress = (ushort)(registers[(byte)RegisterAddresses.H] << 8);
            memoryAdress = (ushort)(memoryAdress | registers[(byte)RegisterAddresses.L]);

            return memoryAdress;
        }

        private byte ReadNextByte()
        {
            IC += 0x0001;
            byte data = bus.ReadFromMemory(IC);

            return data;
        }

        private ushort CombineBytes(byte lowData, byte highData)
        {
            ushort data = highData;
            data = (ushort)(data << 8);
            data = (ushort)(data | lowData);

            return data;
        }
    }
}
