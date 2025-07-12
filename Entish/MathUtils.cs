using System.Numerics;
using System.Runtime.CompilerServices;

namespace Entish;

public static class MathUtils
{
    /// <summary>
    /// Align <paramref name="value"/> up to <paramref name="size"/> and return the result.
    /// </summary>
    /// <typeparam name="T">The integral type to return.</typeparam>
    /// <param name="value">The value to align.</param>
    /// <param name="size">The alignment size.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T AlignUp<T>(this T value, T size) where T : ISubtractionOperators<T, T, T>, IModulusOperators<T, T, T>
    {
        return (size - value % size) % size;
    }

    /// <summary>
    /// Align <paramref name="value"/> down to <paramref name="size"/> and return the result.
    /// </summary>
    /// <typeparam name="T">The integral type to return.</typeparam>
    /// <param name="value">The value to align.</param>
    /// <param name="size">The alignment size.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T AlignDown<T>(this T value, T size) where T : IUnaryNegationOperators<T, T>, IModulusOperators<T, T, T>
    {
        return -(value % size);
    }
}