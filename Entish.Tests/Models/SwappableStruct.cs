// ReSharper disable InconsistentNaming

using System.Runtime.InteropServices;
using Entish.Attributes;

namespace Entish.Tests.Models;

[StructLayout(LayoutKind.Sequential, Pack = 4)]
public struct SwappableStruct : ISwappable<SwappableStruct>
{
    [NeverSwap]
    public uint Magic_Skipped;
    
    public int Int32;
    
    public short Int16_ExpectPadding;
    
    public byte Byte;
    
    public unsafe fixed int Fixed[4];
    
    public static unsafe void Swap(SwappableStruct* value)
    {
        EndianUtils.Swap(&value->Int32);
        EndianUtils.Swap(&value->Int16_ExpectPadding);
        EndianUtils.Swap(&value->Fixed[0]);
        EndianUtils.Swap(&value->Fixed[1]);
        EndianUtils.Swap(&value->Fixed[2]);
        EndianUtils.Swap(&value->Fixed[3]);
    }
}