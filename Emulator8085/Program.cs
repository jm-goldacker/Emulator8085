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

            bus.WriteToMemory(0x2000, 0x01);
            bus.WriteToMemory(0x2001, 1);
            bus.WriteToMemory(0x2002, 2);
            bus.WriteToMemory(0x2003, 0x11);
            bus.WriteToMemory(0x2004, 3);
            bus.WriteToMemory(0x2005, 4);
            bus.WriteToMemory(0x2006, 0x21);
            bus.WriteToMemory(0x2007, 5);
            bus.WriteToMemory(0x2008, 6);
            bus.WriteToMemory(0x2009, 0x31);
            bus.WriteToMemory(0x200A, 0x10);
            bus.WriteToMemory(0x200B, 0x11);

            bus.ConnectComponents();

            bus.ExecuteProgram();

            Console.WriteLine(bus.ReadFromMemory(0x27FF));
        }
    }
}
