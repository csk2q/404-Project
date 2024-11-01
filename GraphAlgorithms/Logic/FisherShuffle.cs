namespace GraphAlgorithms.Logic;

public class FisherShuffle
{
    private const bool DEBUG_PRINT = false;

    
    public static void ShuffleArray<T>(ref T[] array, Random random)
    {
        if(DEBUG_PRINT)
        Console.WriteLine("Begin shuffling array");

        for (int i = array.Length - 1; i > 1; i--)
        {
            int x;
            int y;

            do
            {
                x = random.Next(0, i);
                y = random.Next(0, i);
            } while (x == y);

            (array[x], array[y]) = (array[y], array[x]);

            if(DEBUG_PRINT)
            Console.Write($"{i},");
        }

        if (DEBUG_PRINT)
        {
            Console.WriteLine();
            Console.WriteLine("End shuffling array");
        }
    }
}