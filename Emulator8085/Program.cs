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

            bus.TryWriteToMemory(0x2000, InstructionSet.MVI_B);
            bus.TryWriteToMemory(0x2001, 0x42);
            bus.TryWriteToMemory(0x2002, InstructionSet.MOV_A_B);
            bus.TryWriteToMemory(0x2003, InstructionSet.HALT);

            bus.ConnectComponents();

            bus.ExecuteProgram();
        }
    }
}
