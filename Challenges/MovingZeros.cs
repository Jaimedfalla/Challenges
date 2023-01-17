namespace Challenges
{
    internal class MovingZeros: IAlgorithm
    {
        /* CHALLENGE:
         * Write an algorithm that takes an array and moves all of the zeros to the end, preserving the order of the other elements.*/
        public void Execute() {
            int[] xs = new int[] { 1, 2, 0, 1, 0, 1, 0, 3, 0, 1 };
            var ans = new int[xs.Length];
            var i = 0;
            foreach (var x in xs)
            {
                if (x == 0) continue;
                ans[i++] = x;
            }
            Console.WriteLine(string.Join(",", ans));
        }
    }
}
