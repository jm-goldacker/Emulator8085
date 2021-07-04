namespace Emulator8085.Masks
{
    enum Opcodes
    {
        MOV = 0b11000000,
        MVI = 0b11000111,
        MOV_r_M = 0b11000111,
        MOV_M_r = 0b11111000,
        LXI = 0b11001111
    }
}
