namespace MyLib
{
    public class Math
    {
        public static void SquarePrint(int n)
        {
            Console.WriteLine($"Square {n} = {Square(n)}");
        }

        public static int Square(int n)
        {
            return n * n;
        }
    }
}