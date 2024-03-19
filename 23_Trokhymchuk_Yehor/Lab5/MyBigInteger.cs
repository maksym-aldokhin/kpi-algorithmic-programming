using System.Collections.Generic;
using System.Linq;
using DoubleLinkedList;


namespace Lab5;

public sealed class MyBigInteger // wrapping on dlinklist<>
{
    private DoubleLinkedList<char> _value;

    private IEnumerable<char> ReadNumberReversed()
        => _value.ReadFromHead();

    public IEnumerable<char> ReadNumber()
        => _value.ReadFromTail();

    public MyBigInteger()
    {
        _value = new();
    }

    public MyBigInteger(DoubleLinkedList<char> value)
    {
        _value = value;
    }

    public static implicit operator DoubleLinkedList<char>(MyBigInteger integer)
        => integer._value;

    public static implicit operator MyBigInteger(DoubleLinkedList<char> list) =>
        new(list);

    public static bool TryParse(string s, out MyBigInteger myInteger)
    {
        var list = new DoubleLinkedList<char>();

        bool isCorrect = true;
        int startInd = 0;

        if (s[0] == '-')
        {
            list.AddToBeg('-');
            startInd = 1;
        }

        for (int i = startInd; i < s.Length; i++)
        {
            var value = s[i] - 48;
            if (value >= 0 && value <= 9)
            {
                list.AddToBeg(s[i]);
            }
            else
            {
                isCorrect = false;
            }
        }

        myInteger = isCorrect ? new(list) : null!;
        return isCorrect;
    }

    public static bool TryParseLog(string s, out MyBigInteger myInteger, TimeLogger.TimeLogger logger)
    {
        var list = new DoubleLinkedList<char>();

        bool isCorrect = true;
        int startInd = 0;

        logger.SetMessage().Start();
        if (s[0] == '-')
        {
            list.AddToBeg('-');
            startInd = 1;
        }

        for (int i = startInd; i < s.Length; i++)
        {
            var value = s[i] - 48;
            if (value >= 0 && value <= 9)
            {
                list.AddToBeg(s[i]);
            }
            else
            {
                isCorrect = false;
            }
        }

        logger.Stop();

        myInteger = isCorrect ? new(list) : null!;
        return isCorrect;
    }

    private static void TrimZeroes(MyBigInteger integer)
    {
        var val = integer._value.PeekTail();
        if (val == '0' && integer._value.Capacity > 1)
        {
            do
            {
                integer._value.RemoveFirstFromTail('0');
            } while (integer._value.PeekTail() == '0' && integer._value.Capacity > 1);
        }
        else if (val == '-' && integer._value.PeekTailPrev() == '0' && integer._value.Capacity > 2) // e.g -01
        {
            do
            {
                integer._value.RemoveFirstFromTail('0');
            } while (integer._value.PeekTailPrev() == '0' && integer._value.Capacity > 2);
        }
    }

    public static MyBigInteger MultiplyOptimized(MyBigInteger left, MyBigInteger right)
    {
        TrimZeroes(left);
        TrimZeroes(right);

        bool leftSigned = left._value.PeekTail() == '-';
        bool rightSigned = right._value.PeekTail() == '-'; //-00123

        if (leftSigned)
        {
            left._value.PopTail();
        }

        if (rightSigned)
        {
            right._value.PopTail();
        }

        MyBigInteger result = null!;
        if (left._value.Capacity < right._value.Capacity)
        {
            result = MultiplyHelperOptimized(right, left);
        }
        else
        {
            result = MultiplyHelperOptimized(left, right);
        }

        if (leftSigned ^ rightSigned)
        {
            result._value.AddToEnd('-');
        }
        if (leftSigned)
        {
            left._value.AddToEnd('-');
        }
        if (rightSigned)
        {
            right._value.AddToEnd('-');
        }

        return result;
    }

    private static MyBigInteger MultiplyHelperOptimized(MyBigInteger left, MyBigInteger right)
    {
        MyBigInteger previousResult = null!;
        MyBigInteger result = null!;
        DoubleLinkedList<char> resultList = null!;
        DoubleLinkedList<char> values = null!;
        
        byte value,
            leftValue,
            remember = 0,
            tempRemember;

        int i = 0;

        foreach (var rightChar in right.ReadNumberReversed())
        {
            if (rightChar == '-')
            {
                continue;
            }

            value = (byte)(rightChar - 48);
            i++;
            remember = 0;
            values = new DoubleLinkedList<char>();
            resultList = values;

            AddZeros(values, i - 1);
            foreach (var leftChar in left.ReadNumberReversed())
            {
                /*if (leftChar == '-')
                {
                    continue;
                }*/

                leftValue = (byte)(leftChar - 48);
                remember += (byte)(leftValue * value);

                if (remember > 9)
                {
                    tempRemember = (byte)(remember % 10);
                    remember = (byte)((remember - tempRemember) / 10);
                    values.AddToEnd((char)(tempRemember + 48));
                }
                else
                {
                    values.AddToEnd((char)(remember + 48));
                    remember = 0;
                }
            }

            if (remember != 0)
            {
                values.AddToEnd((char)(remember + 48));
            }

            if (previousResult is null)
            {
                previousResult = values;
            }
            else
            {
                result = AddTwoLists(resultList, previousResult);
                previousResult = result;
            }
        }

        return result ?? previousResult;
    }

    private static MyBigInteger AddTwoLists(DoubleLinkedList<char> a, DoubleLinkedList<char> b)
        => new(Add(a, b));

    public static MyBigInteger Multiply(MyBigInteger left, MyBigInteger right)
    {
        TrimZeroes(left);
        TrimZeroes(right);

        bool leftSigned = left._value.PeekTail() == '-';
        bool rightSigned = right._value.PeekTail() == '-';

        if (leftSigned)
        {
            left._value.PopTail();
        }

        if (rightSigned)
        {
            right._value.PopTail();
        }

        MyBigInteger result = null!;
        if (left._value.Capacity < right._value.Capacity)
        {
            result = MultiplyHelper(right, left);
        }
        else
        {
            result = MultiplyHelper(left, right);
        }

        if (leftSigned ^ rightSigned)
        {
            result._value.AddToEnd('-');
        }
        if (leftSigned)
        {
            left._value.AddToEnd('-');
        }
        if (rightSigned)
        {
            right._value.AddToEnd('-');
        }

        return result;
    }

    private static MyBigInteger MultiplyHelper(MyBigInteger left, MyBigInteger right)
    {
        var val = right._value.PeekTail();
        var tempList = new DoubleLinkedList<char>();
        tempList.AddToEnd('0');
        var tempInteger = new MyBigInteger(tempList);

        if (val == '0')
        {
            return tempInteger;
        }
        else if (val == '-' && right._value.PeekTailPrev() == '0')
        {
            return tempInteger;
        }

        tempInteger = null;
        tempList = null;

        var valList = new DoubleLinkedList<DoubleLinkedList<char>>();

        byte value,
            leftValue,
            remember = 0,
            tempRemember;

        int i = 0;

        foreach (var rightChar in right.ReadNumberReversed())
        {
            /*if (rightChar == '-')
            {
                continue;
            }*/
            
            value = (byte)(rightChar - 48);
            i++;
            remember = 0;

            var values = new DoubleLinkedList<char>();
            AddZeros(values, i - 1);

            foreach (var leftChar in left.ReadNumberReversed())
            {
                /*if (leftChar == '-')
                {
                    continue;
                }*/

                leftValue = (byte)(leftChar - 48);
                remember += (byte)(leftValue * value);

                if (remember > 9)
                {
                    tempRemember = (byte)(remember % 10);
                    remember = (byte)((remember - tempRemember) / 10);
                    values.AddToEnd((char)(tempRemember + 48));
                }
                else
                {
                    values.AddToEnd((char)(remember + 48));
                    remember = 0;
                }
            }

            if (remember != 0)
            {
                values.AddToEnd((char)(remember + 48));
            }

            valList.AddToBeg(values);
        }

        var result = AddRange(valList);
        return result;
    }

    private static void AddZeros(DoubleLinkedList<char> values, int count)
    {
        for (int i = 0; i < count; i++)
        {
            values.AddToEnd('0');
        }
    }

    private static MyBigInteger AddRange(DoubleLinkedList<DoubleLinkedList<char>> list)
    {
        var resultList = list.ReadFromTail().First();
        foreach (var item in list.ReadFromTail().Skip(1))
        {
            resultList = Add(resultList, item);
        }

        return new(resultList);
    }

    public static DoubleLinkedList<char> Add(DoubleLinkedList<char> left, DoubleLinkedList<char> right)
    {
        if (left.Capacity < right.Capacity)
        {
            return AddHelper(right, left);
        }

        return AddHelper(left, right);
    }

    private static DoubleLinkedList<char> AddHelper(DoubleLinkedList<char> left, DoubleLinkedList<char> right)
    {
        var result = new DoubleLinkedList<char>();

        byte value,
            tempValue,
            remember = 0,
            tempRemember;

        int i = 0;
        var leftEnumerable = left.ReadFromHead();

        foreach (var rightChar in right.ReadFromHead())
        {
            value = (byte)(rightChar - 48);
            var leftChar = leftEnumerable.Skip(i).Take(1).First();
            i++;

            tempValue = (byte)(leftChar - 48);
            remember += (byte)(value + tempValue);
            if (remember > 9)
            {
                tempRemember = (byte)(remember % 10);
                remember = (byte)((remember - tempRemember) / 10);
                result.AddToEnd((char)(tempRemember + 48));
            }
            else
            {
                result.AddToEnd((char)(remember + 48));
                remember = 0;
            }
        }

        foreach (var leftChar in leftEnumerable.Skip(i))
        {
            value = (byte)(leftChar - 48);
            remember += (byte)(value);
            if (remember > 9)
            {
                tempRemember = (byte)(remember % 10);
                remember = (byte)((remember - tempRemember) / 10);
                result.AddToEnd((char)(tempRemember + 48));
            }
            else
            {
                result.AddToEnd((char)(remember + 48));
                remember = 0;
            }
        }

        if (remember != 0)
        {
            result.AddToEnd((char)(remember + 48));
        }

        return result;
    }

    public override string ToString()
    {
        return string.Join("", ReadNumber());
    }
}