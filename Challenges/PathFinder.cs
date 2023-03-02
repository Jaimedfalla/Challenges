/* You are at position [0, 0] in maze NxN and you can only move in one of the four
cardinal directions (i.e. North, East, South, West). Return true if you can reach position [N-1, N-1] or false otherwise.

Empty positions are marked ..
Walls are marked W.
Start and exit positions are empty in all test cases. */

namespace Challenges{

    internal class PathFinder : IAlgorithm
    {
        private IEnumerable<(int,int)> alternativePositions = Enumerable.Empty<(int,int)>();

        private IEnumerable<(int,int)> path = Enumerable.Empty<(int,int)>();

        public void Execute()
        {
            bool result = true;
            (int vertical,int horizontal) position = (0,0);

            string maze = ".W...W.W..\n" +
                          ".......W..\n" +
                          "W..W......\n" +
                          "...W.WW...\n" +
                          "WW..W..WWW\n" +
                          ".....W....\n" +
                          "W.W.W....W\n" +
                          "...WW...W.\n" +
                          ".W........\n" +
                          ".W........";

            string[] matrix = maze.Split("\n");

            while((position.horizontal < matrix[0].Length && position.vertical <  matrix.Length)){
                path = path.Append(position);
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

            if(actual.Item1 == matrix.Length - 1 &&
                actual.Item2==matrix[0].Length - 1) return actual;

            (int,int) nextMove = (default,default);
            (int vertical,int horizontal) up = (actual.Item1 - 1 >=0? actual.Item1 - 1: 0,actual.Item2);
            (int vertical,int horizontal) down = (actual.Item1 + 1 < matrix.Length? actual.Item1 + 1: matrix.Length-1,actual.Item2);
            (int vertical,int horizontal) left = (actual.Item1,actual.Item2-1 >=0?actual.Item2 - 1:0);
            (int vertical,int horizontal) rigth = (actual.Item1,actual.Item2+1 < matrix.Length?actual.Item2+1:matrix.Length - 1);

            if(!path.Contains(rigth) && matrix[rigth.vertical][rigth.horizontal] == '.') nextMove = rigth;

            if(nextMove == (default,default) && matrix[down.vertical][down.horizontal] == '.') nextMove = down;
            AddToAlternative(up,matrix);

            if(nextMove == (default,default) && matrix[left.vertical][left.horizontal] == '.') nextMove = left;
            AddToAlternative(left,matrix);

            if(nextMove == (default,default) && matrix[up.vertical][up.horizontal] == '.') nextMove = up;
            AddToAlternative(up,matrix);

            return nextMove;
        }

        private void AddToAlternative((int vertical,int horizontal) position,string[] matrix)
        {
            if(!path.Contains(position) && matrix[position.vertical][position.horizontal] == '.') alternativePositions = alternativePositions.Append(position);
        }
    }
}