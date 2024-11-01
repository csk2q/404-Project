namespace GraphAlgorithms.Logic;

public class FisherShuffle
{
    public static void ShuffleArray<T>(ref T[] array, Random random)
    {
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

            Console.Write($"{i},");
        }

        Console.WriteLine();
        Console.WriteLine("End shuffling array");
    }
}