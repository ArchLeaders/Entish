namespace Entish;

public unsafe interface ISwappable<T> where T : unmanaged
{
    static abstract void Swap(T* value);
}