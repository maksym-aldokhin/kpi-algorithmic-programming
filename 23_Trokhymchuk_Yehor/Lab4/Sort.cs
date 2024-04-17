namespace Lab4;

public static class Sort
{
    private static void MergeArraysVer1<T>(List<T> result, List<T> arrA, List<T> arrB)
        where T : IComparable<T>
    {
        int aInd = 0, bInd = 0, resInd = 0;

        while (aInd < arrA.Count && bInd < arrB.Count)
        {
            result[resInd++] = arrA[aInd].CompareTo(arrB[bInd]) > 0
                ? arrB[bInd++]
                : arrA[aInd++];
        }

        while (aInd < arrA.Count)
        {
            result[resInd++] = arrA[aInd++];
        }

        while (bInd < arrB.Count)
        {
            result[resInd++] = arrB[bInd++];
        }
    }
    
    public static void MergeRecAscVer1<T>(List<T> arr)
        where T : IComparable<T>
    {
        if (arr.Count <= 1)
        {
            return;
        }

        int leftPartSize = arr.Count / 2,
            rightPartSize = arr.Count - leftPartSize;

        List<T> leftArr = new (leftPartSize),
                rightArr = new (rightPartSize);
        int i;
        for (i = 0; i < leftPartSize; i++)
        {
            leftArr.Add(arr[i]);
        }
        for(i = leftPartSize; i < arr.Count; i++)
        {
            rightArr.Add(arr[i]);
        }
        
        MergeRecAscVer1(leftArr);
        MergeRecAscVer1(rightArr);

        MergeArraysVer1(arr, leftArr, rightArr);
    }
}