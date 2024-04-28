using System.Numerics;
using System.Runtime.CompilerServices;

namespace Lab6;

public class ZeroBasedMatrixNode<T> : 
    IComparable<ZeroBasedMatrixNode<T>> 
    where T : INumber<T>
{
    public readonly int Y;
    public int X;
    public T Data { get; private set; }

    public ZeroBasedMatrixNode(int y, int x, T data)
        => (Y, X, Data) = (y, x, data);

    public override bool Equals(object? obj)
    {
        if (obj is ZeroBasedMatrixNode<T> node)
        {
            return Y == node.Y && X == node.X;
        }
        if (obj is T data)
        {
            return Data.Equals(data);
        }
        
        return false;
    }

    public override int GetHashCode()
    {
        return Data.GetHashCode();
    }

    public override string ToString() => $"( {Data, -4} [{Y},{X}] )";

    public int CompareTo(ZeroBasedMatrixNode<T> other)
    {
        return Data.CompareTo(other.Data);
    }
    
    public static T operator +(ZeroBasedMatrixNode<T> a, ZeroBasedMatrixNode<T> b)
    {
        return a.Data + b.Data;
    }
    
    public static T operator *(ZeroBasedMatrixNode<T> a, ZeroBasedMatrixNode<T> b)
    {
        if (a is null || b is null)
        {
            return T.Zero;
        }
        
        return a.Data * b.Data;
    }
     
    public static T operator >>(ZeroBasedMatrixNode<T> a, ZeroBasedMatrixNode<T> b)
    {
        return a.Data = b.Data;
    }
}