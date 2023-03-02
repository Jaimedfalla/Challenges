/* You are at position [0, 0] in maze NxN and you can only move in one of the four
cardinal directions (i.e. North, East, South, West). Return true if you can reach position [N-1, N-1] or false otherwise.

Empty positions are marked ..
Walls are marked W.
Start and exit positions are empty in all test cases. */

namespace Challenges
{
    internal class PathFinder : IAlgorithm
    {
        public void Execute()
        {
            bool result = false;
            int position = 0;
            Stack<int> alternativePositions = new();

            string maze = ".W...W.W..\n" +
                          ".......W..\n" +
                          "W..W......\n" +
                          "...W.WW...\n" +
                          "WW..W..WW.\n" +
                          ".....W....\n" +
                          "W.W.W....W\n" +
                          "...WW...W.\n" +
                          ".W........\n" +
                          ".W........\n";

            maze = maze.Replace("\n", "");
            int size = (int)Math.Sqrt(maze.Length);
            bool[] visited = new bool[maze.Length];
            alternativePositions.Push(position);

            while (alternativePositions.Any() && !result)
            {
                position = alternativePositions.Pop();

                if (position == maze.Length - 1) result = true;
                else
                {
                    visited[position] = true;
                    IEnumerable<int> steps = NextPosition(position, maze, size, visited);
                    foreach (int step in steps)
                    {
                        alternativePositions.Push(step);
                    }
                }
            }

            Console.WriteLine(result);
        }

        private IEnumerable<int> NextPosition(int actual, string matrix, int size, bool[] visited)
        {
            IEnumerable<int> positions = Enumerable.Empty<int>();

            int up = actual - size;
            int down = actual + size;
            int left = actual - 1;
            int rigth = actual + 1;
            int fila = actual / size;

            if (IsValidStep(up, matrix, visited)) positions = positions.Append(up);
            if (IsValidStep(left, matrix, visited) && (left / size) == fila) positions = positions.Append(left);
            if (IsValidStep(down, matrix, visited)) positions = positions.Append(down);
            if (IsValidStep(rigth, matrix, visited) && (rigth / size) == fila) positions = positions.Append(rigth);

            return positions;
        }

        private bool IsValidStep(int step, string maze, bool[] visited) => step >= 0 && step < maze.Length && !visited[step] && maze[step] == '.';
    }
}