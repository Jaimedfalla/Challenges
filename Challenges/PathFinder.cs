/* You are at position [0, 0] in maze NxN and you can only move in one of the four
cardinal directions (i.e. North, East, South, West). Return true if you can reach position [N-1, N-1] or false otherwise.

Empty positions are marked ..
Walls are marked W.
Start and exit positions are empty in all test cases. */

namespace Challenges{

    internal class PathFinder : IAlgorithm
    {
        public void Execute()
        {
            bool result = true;
            (int vertical,int horizontal) position = (0,0);

            string d = "......\n" +
                   "......\n" +
                   "......\n" +
                   "......\n" +
                   ".....W\n" +
                   "....W.";

            string[] matrix = d.Split("\n");

            while(position.horizontal < matrix[0].Length && position.vertical <  matrix.Length){
                position = NextPosition(position,matrix);

                if(position.horizontal == default && position.vertical==default)
                {
                    result = false;
                    break;
                }

                if(position.horizontal == matrix[0].Length-1 && position.vertical==matrix.Length - 1)
                {
                    result = true;
                    break;
                }

                result = true;
            }

            Console.WriteLine(result);
        }

        private (int,int) NextPosition((int,int)actual, string[] matrix){

            if(actual.Item2 + 1 < matrix[0].Length &&
                matrix[actual.Item1][actual.Item2 + 1] == '.') return (actual.Item1,actual.Item2 + 1);

            if(actual.Item1 + 1 < matrix.Length &&
                matrix[actual.Item1 + 1][actual.Item2] == '.') return (actual.Item1 +1 ,actual.Item2);

            if(actual.Item1 == matrix.Length - 1 &&
                actual.Item2==matrix[0].Length - 1) return actual;

            return (default,default);
        }
    }
}