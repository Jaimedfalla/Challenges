namespace Challenges
{
    /* CHALLENGE:
     * Given n, take the sum of the digits of n. If that value has more than one digit,
     * continue reducing in this way until a single-digit number is produced. The input will be a non-negative integer.*/
    internal class SumOfDigits: IAlgorithm
    {
        public void Execute()
        {
            long number = 493193;
            int result = DigitalRoot(number);

            Console.WriteLine($"number: {result}");

            int DigitalRoot(long n)
            {
                return (n > 9) ? DigitalRoot(n.ToString().Sum(x => x - '0')) : (int)n;
            }
        }
    }
}
