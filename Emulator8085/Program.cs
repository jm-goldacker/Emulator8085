using System;

namespace Emulator8085
{
    /// <summary>
    /// entry point for the emulator
    /// </summary>
    class Program
    {
        /// <summary>
        /// entry point for the emulator
        /// A simple program is executed that writes 0x42 to register A.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            var bus = new Bus();

            bus.WriteToMemory(0x2000, InstructionSet.MVI_H);
            bus.WriteToMemory(0x2001, 0x27);
            bus.WriteToMemory(0x2002, InstructionSet.MVI_L);
            bus.WriteToMemory(0x2003, 0xFF);
            bus.WriteToMemory(0x2004, InstructionSet.MVI_A);
            bus.WriteToMemory(0x2005, 0x42);
            bus.WriteToMemory(0x2006, InstructionSet.MOV_M_A);
            bus.WriteToMemory(0x2007, InstructionSet.MOV_B_M);
            bus.WriteToMemory(0x2008, InstructionSet.HALT);

            bus.ConnectComponents();

            bus.ExecuteProgram();

            Console.WriteLine(bus.ReadFromMemory(0x27FF));
        }
    }
}
