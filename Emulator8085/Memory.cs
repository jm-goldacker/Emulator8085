namespace Emulator8085
{
    /// <summary>
    /// read only memory
    /// </summary>
    class Memory
    {
        private byte[] data = new byte[2048];

        /// <summary>
        /// Returns the data stored at the given adress.
        /// </summary>
        /// <param name="adress">adress to read from</param>
        /// <returns>data stored at the adress</returns>
        public byte Read(ushort adress)
        {
            return data[adress - 0x2000];
        }

        /// <summary>
        /// Writes data to the given adress.
        /// </summary>
        /// <param name="adress">adress to write at</param>
        /// <param name="data">data to write</param>
        public void Write(ushort adress, byte data)
        {
            this.data[adress - 0x2000] = data;
        }
    }
}
