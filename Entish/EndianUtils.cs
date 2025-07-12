using System.Buffers.Binary;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Entish;

public enum Endianness : ushort
{
    Big = 0xFEFF,
    Little = 0xFFFE,
}

public static unsafe class EndianUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ushort Swap(ushort u16) => BinaryPrimitives.ReverseEndianness(u16);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Swap(ref ushort u16) => u16 = BinaryPrimitives.ReverseEndianness(u16);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Swap(ushort* u16) => *u16 = BinaryPrimitives.ReverseEndianness(*u16);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static short Swap(short s16) => BinaryPrimitives.ReverseEndianness(s16);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Swap(ref short s16) => s16 = BinaryPrimitives.ReverseEndianness(s16);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Swap(short* s16) => Swap((ushort*)s16);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static char Swap(char @char) => (char)BinaryPrimitives.ReverseEndianness(@char);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Swap(ref char @char) => @char = (char)BinaryPrimitives.ReverseEndianness(@char);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Swap(char* @char) => Swap((ushort*)@char);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint Swap(uint u32) => BinaryPrimitives.ReverseEndianness(u32);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Swap(ref uint u32) => u32 = BinaryPrimitives.ReverseEndianness(u32);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Swap(uint* u32) => *u32 = BinaryPrimitives.ReverseEndianness(*u32);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Swap(int s32) => BinaryPrimitives.ReverseEndianness(s32);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Swap(ref int s32) => s32 = BinaryPrimitives.ReverseEndianness(s32);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Swap(int* s32) => Swap((uint*)s32);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Swap(float f32) => Unsafe.BitCast<float, _32bitUnion>(f32).Swap();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Swap(ref float f32) => f32 = Swap(f32);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Swap(float* f32) => Swap((uint*)f32);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong Swap(ulong u64) => BinaryPrimitives.ReverseEndianness(u64);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Swap(ref ulong u64) => u64 = BinaryPrimitives.ReverseEndianness(u64);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Swap(ulong* u64) => *u64 = BinaryPrimitives.ReverseEndianness(*u64);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long Swap(long s64) => BinaryPrimitives.ReverseEndianness(s64);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Swap(ref long s64) => s64 = BinaryPrimitives.ReverseEndianness(s64);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Swap(long* s64) => Swap((ulong*)s64);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Swap(double f64) => Unsafe.BitCast<double, _64bitUnion>(f64).SwapF64();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Swap(ref double f64) => f64 = Swap(f64);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Swap(double* f64) => Swap((ulong*)f64);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DateTime Swap(DateTime dateTime) => Unsafe.BitCast<DateTime, _64bitUnion>(dateTime).SwapDateTime();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Swap(ref DateTime dateTime) => dateTime = Swap(dateTime);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Swap(DateTime* dateTime) => Swap((ulong*)dateTime);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static UIntPtr Swap(UIntPtr ptr) => BinaryPrimitives.ReverseEndianness(ptr);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Swap(ref UIntPtr ptr) => ptr = BinaryPrimitives.ReverseEndianness(ptr);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Swap(UIntPtr* ptr) => *ptr = BinaryPrimitives.ReverseEndianness(*ptr);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool ShouldSwap(Endianness endianness) => BitConverter.IsLittleEndian != endianness is Endianness.Little;
}

[StructLayout(LayoutKind.Explicit)]
file struct _32bitUnion
{
    [FieldOffset(0)]
    public uint U32;
    
    [FieldOffset(0)]
    public float F32;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public unsafe float Swap()
    {
        fixed (uint* ptr = &U32) {
            EndianUtils.Swap(ptr);
        }

        return F32;
    }
}

[StructLayout(LayoutKind.Explicit)]
file struct _64bitUnion
{
    [FieldOffset(0)]
    public ulong U64;
    
    [FieldOffset(0)]
    public double F64;
    
    [FieldOffset(0)]
    public DateTime DateTime;
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public unsafe double SwapF64()
    {
        fixed (ulong* ptr = &U64) {
            EndianUtils.Swap(ptr);
        }

        return F64;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public unsafe DateTime SwapDateTime()
    {
        fixed (ulong* ptr = &U64) {
            EndianUtils.Swap(ptr);
        }

        return DateTime;
    }
}