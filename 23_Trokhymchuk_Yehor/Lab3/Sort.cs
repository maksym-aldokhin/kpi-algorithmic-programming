using System.Collections;
using TwoDArrOps;

namespace Lab3;

public static class Sort
{
    public static void InsertionAsc<T>(T[] arr)
        where T : IComparable, IComparable<T>
    {
        T tempElem;

        int leftBound = 0,
            rightBound = 1;

        bool isSorted;
        
        while (rightBound < arr.Length)
        {
            leftBound = rightBound - 1;
            tempElem = arr[rightBound];
            isSorted = true;

            while (leftBound >= 0 && isSorted)
            {
                if (tempElem.CompareTo(arr[leftBound]) < 0)
                {
                    arr[leftBound + 1] = arr[leftBound];
                    leftBound--;
                    arr[leftBound + 1] = tempElem;
                }
                else
                {
                    isSorted = false;
                }
            }
            rightBound++;
        }
    }

    private static void MergeArraysVer1<T>(T[] result, T[] arrA, T[] arrB)
        where T : IComparable, IComparable<T>
    {
        int aInd = 0, bInd = 0, resInd = 0;

        while (aInd < arrA.Length && bInd < arrB.Length)
        {
            result[resInd++] = arrA[aInd].CompareTo(arrB[bInd]) > 0
                ? arrB[bInd++]
                : arrA[aInd++];
        }

        while (aInd < arrA.Length)
        {
            result[resInd++] = arrA[aInd++];
        }

        while (bInd < arrB.Length)
        {
            result[resInd++] = arrB[bInd++];
        }
    }


    public static void MergeRecAscVer1<T>(T[] arr)
        where T : IComparable, IComparable<T>
    {
        if (arr.Length <= 1)
        {
            return;
        }

        int leftPartSize = arr.Length / 2,
            rightPartSize = arr.Length - leftPartSize;

        T[] leftArr = new T[leftPartSize],
            rightArr = new T[rightPartSize];

        Array.Copy(arr, 0, leftArr, 0, leftPartSize);
        Array.Copy(arr, leftPartSize, rightArr, 0, rightPartSize);

        MergeRecAscVer1(leftArr);
        MergeRecAscVer1(rightArr);

        MergeArraysVer1(arr, leftArr, rightArr);
    }

    private static void MergeArraysVer2<T>(T[] result, int leftBound, int rightBound, int middle)
        where T : IComparable, IComparable<T>
    {
        int leftPartSize = middle - leftBound + 1,
            rightPartSize = rightBound - middle,
            aInd = 0,
            bInd = 0,
            resInd = leftBound;

        T[] leftArr = new T[leftPartSize],
            rightArr = new T[rightPartSize];

        Array.Copy(result, resInd, leftArr, 0, leftPartSize);
        Array.Copy(result, middle + 1, rightArr, 0, rightPartSize);

        while (aInd < leftPartSize && bInd < rightPartSize)
        {
            result[resInd++] = leftArr[aInd].CompareTo(rightArr[bInd]) > 0
                ? rightArr[bInd++]
                : leftArr[aInd++];
        }

        while (aInd < leftArr.Length)
        {
            result[resInd++] = leftArr[aInd++];
        }

        while (bInd < rightArr.Length)
        {
            result[resInd++] = rightArr[bInd++];
        }
    }

    public static void MergeRecAscVer2<T>(T[] arr, int leftBound, int rightBound)
        where T : IComparable, IComparable<T>
    {
        if (leftBound < rightBound)
        {
            int middle = leftBound + (rightBound - leftBound) / 2;

            MergeRecAscVer2(arr, leftBound, middle);
            MergeRecAscVer2(arr, middle + 1, rightBound);
            MergeArraysVer2(arr, leftBound, rightBound, middle);
        }
    }

    public static void MergeRecAscVer2<T>(T[] arr)
        where T : IComparable, IComparable<T> => MergeRecAscVer2(arr, 0, arr.Length - 1);

    public static void MergeIterAsc<T>(T[] arr)
        where T : IComparable, IComparable<T>
    {
        int curr_size;
        int left_start;

        for (curr_size = 1;
             curr_size <= arr.Length - 1;
             curr_size = 2 * curr_size)
        {
            for (left_start = 0;
                 left_start < arr.Length - 1;
                 left_start += 2 * curr_size)
            {
                int mid = Math.Min(left_start + curr_size - 1, arr.Length - 1);

                int right_end = Math.Min(left_start
                    + 2 * curr_size - 1, arr.Length - 1);

                MergeArraysVer2(arr, left_start, right_end, mid);
            }
        }
    }
}