using System.Runtime.InteropServices;
using Entish.Attributes;

namespace Entish.Tests.Models;

public enum TestEnum : ushort
{
    A,
    B,
    C
}

[Swappable]
[StructLayout(LayoutKind.Sequential, Pack = 4)]
public partial struct AutoSwappableStruct
{
    [NeverSwap]
    public uint Magic_Skipped;
    
    public int Int32;
    
    public short Int16_ExpectPadding;
    
    public NestedAutoGenStruct Nested;
    
    public PrimitiveStruct Primitive;
    
    public byte Byte;
    
    public TestEnum TestEnum;
    
    public unsafe fixed int Fixed[4];
}

[StructLayout(LayoutKind.Sequential, Pack = 2)]
public struct NestedAutoGenStruct
{
    public int Int32;
    
    public short Int16_NoPadding;
}

[StructLayout(LayoutKind.Sequential, Pack = 2)]
public struct PrimitiveStruct
{
    public int Int32;
    
    public long Int64;
    
    public ushort Int16;
    
    public long Int64_2;
    
    public PrimitiveUnion SubStruct;
}

[StructLayout(LayoutKind.Explicit, Pack = 2)]
public struct PrimitiveUnion
{
    [FieldOffset(0)]
    public ushort UInt16;
    
    [FieldOffset(0)]
    public short Int16;
}