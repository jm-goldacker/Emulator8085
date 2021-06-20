﻿using System;

namespace Emulator8085
{
    /// <summary>
    /// Bus that connects the components.
    /// </summary>
    class Bus
    {
        private Memory memory;
        private Cpu cpu;

        /// <summary>
        /// Creates a new bus with CPU and RAM.
        /// </summary>
        public Bus()
        {
            memory = new Memory();
            cpu = new Cpu();
        }

        /// <summary>
        /// Connects the components to the bus.
        /// </summary>
        public void ConnectComponents()
        {
            cpu.ConnectToBus(this);
        }

        /// <summary>
        /// Executes the currently loaded program.
        /// </summary>
        public void ExecuteProgram()
        {
            cpu.Execute();
        }

        /// <summary>
        /// Reads the data that is stored in the RAM at the given adress.
        /// </summary>
        /// <param name="adress">adress to read from</param>
        /// <returns>the data that is stored in the RAM at the given adress</returns>
        public byte? ReadFromMemory(ushort adress)
        {
            if (adress < 0x2000 || adress > 0x27FF)
            {
                Console.WriteLine("Invalid RAM adress!");
                return null;
            }

            return memory.Read(adress);
        }

        /// <summary>
        /// Writes data to the RAM at the given adress.
        /// </summary>
        /// <param name="adress">adress to write at</param>
        /// <param name="data">data to write</param>
        /// <returns>true, if writing was successfull, otherwise false</returns>
        public bool TryWriteToMemory(ushort adress, byte data)
        {
            if (adress < 0x2000 || adress > 0x27FF)
            {
                Console.WriteLine("Invalid RAM adress!");
                return false;
            }

            memory.Write(adress, data);
            return true;
        }
    }
}
