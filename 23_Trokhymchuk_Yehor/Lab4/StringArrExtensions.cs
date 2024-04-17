namespace Lab4;

public static class StringArrExtensions
{
    public static string ConcatBlocks(this string[] blocks, ref int pos)
    {
        string reslutString;
            
        if (blocks[pos][0] == '\"' && blocks[pos][blocks[pos].Length - 1] != '\"')
        {
            reslutString = blocks[pos];
                
            while (blocks[pos][blocks[pos].Length - 1] != '\"')
            {
                pos++;
                reslutString += " " + blocks[pos];
            }
        }
        else
        {
            reslutString = blocks[pos];
        }

        pos++;
            
        return reslutString;
    }
}