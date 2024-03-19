using System.Collections.Generic;


namespace DoubleLinkedList;

public sealed class DoubleLinkedList<T>
{
    private DoubleLinkedListNode<T>? _tail = null;
    private DoubleLinkedListNode<T>? _head = null;
    public int Capacity { get; private set; }
    private bool InitRoot(T data)
    {
        if (_head is null)
        {
            var temp = new DoubleLinkedListNode<T>(data);
            _head = temp;
            _tail = temp;
            
            Capacity = 1;
            
            return true;
        }
        return false;
    }

    public static implicit operator DoubleLinkedList<T>(T[] values)
    {
        var result = new DoubleLinkedList<T>();

        foreach (var item in values)
        {
            result.AddToEnd(item);
        }

        return result;
    }
    
    public void AddToEndFromBegin(T data)
    {
        if (InitRoot(data))
        {
            return;
        }

        Capacity++;
        var temp = new DoubleLinkedListNode<T>(data);
        var lastNode = _head; //a pointer (copy of a reference) to _root
            
        while (lastNode.Next is not null) // in C# i can't make here (lastNode is not null) condition
        {
            lastNode = lastNode.Next;
        }

        lastNode.Next = temp; // because here if i write (lastNode = temp)
                              // it will not affect _root because i use a COPY of a refernce to _root
                              // also i can't take value of _root by reference (ref var lastNode = ref root)
                              // because it will affect _root (in while loop)!
                              // My approach is working correctly because i only change the state of pointer but
                              // not the pointer exactly. If you comapre this approach to c++ one you can go crazy.
                              
        temp.Previous = lastNode;
        _tail = temp;
    }

    public void AddToBeginFromEnd(T data)
    {
        if (InitRoot(data))
        {
            return;
        }
        
        Capacity++;
        var temp = new DoubleLinkedListNode<T>(data);
        var lastNode = _tail;
            
        while (lastNode.Previous is not null)
        {
            lastNode = lastNode.Previous;
        }

        lastNode.Previous = temp;
        _head = lastNode.Previous;
        _head.Next = lastNode;
    }

    public void AddToBeg(T data)
    {
        if (InitRoot(data))
        {
            return;
        }
        
        Capacity++;
        var node = new DoubleLinkedListNode<T>(data);
        node.Next = _head!;
        _head!.Previous = node;

        if (_tail == _head)
        {
            _head = node;
            _tail = _head.Next;
        }
        else
        {
            _head = node;
        }
    }

    public void AddToEnd(T data)
    {
        if (InitRoot(data))
        {
            return;
        }
        
        Capacity++;
        var node = new DoubleLinkedListNode<T>(data);
        node.Previous = _tail!;
        
        if (_tail == _head)
        {
            _tail = node;
            _tail.Previous = _head!;
            _head!.Next = node;
            
        }
        else
        {
            _tail = node;
            _tail!.Previous!.Next = _tail;
        }
    }
    
    public IEnumerable<T> ReadFromTail()
    {
        var temp = _tail;

        while (temp is not null)
        {
            yield return temp;
            temp = temp.Previous;
        }

    }
    
    public IEnumerable<T> ReadFromHead()
    {
        var temp = _head;

        while (temp is not null)
        {
            yield return temp;
            temp = temp.Next;
        }
    }

    public T? PeekHead() => _head;
    public T? PeekTail() => _tail;
    public T? PeekTailPrev() => _tail!.Previous;

    public T? PopHead()
    {
        if (_head is null)
        {
            return default;
        }

        Capacity--;
        T value = _head!;
        _head = _head.Next;
        _head.Previous = null!;
        return value;
    }

    public T? PopTail()
    {
        if (_tail is null)
        {
            return default;
        }

        Capacity--;
        T value = _tail!;
        _tail = _tail.Previous;
        _tail.Next = null!;
        return value;
    }

    public void RemoveFirstFromTail(T elem)
    {
        var temp = _tail;
        
        while (temp.Previous is not null && !temp.Equals(elem))
        {
            temp = temp.Previous;
        }

        if (temp.Previous is null)
        {
            PopHead();
        }
        else if (temp.Next is null)
        {
            PopTail();
        }
        else
        {
            temp.Previous.Next = temp.Next;
            temp.Next.Previous = temp.Previous;
            temp.Previous = null!;
            temp.Next = null!;
            temp = null!;
            Capacity--;
        }
    }
}