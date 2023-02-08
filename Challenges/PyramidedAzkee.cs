/* "Given a positive integer n, return a string representing the pyramid associated with it."
Pyramids can get huge, so range of n is 1 - 100. Rows are delimited by a newline '\n' and all rows have same length. Make sure to include the corrrect amount of leading and trailing whitespace where necessary. Be aware there is also a recurring pattern of whitespace inside the pyramid.

Characters used in pyramid: '/', '\', ' ', '_', ':', '|', '.', '´'.
Code length limited to 1000 (C#: 2000) characters to prevent hadcoding.

'*' shows whitespace to give a better impression how to render the pyramid

 n = 3      *******./\*****
            ****.´\/__\****
            *.´\´\/__:_\***
            \*\´\/_|__|_\**
            *\´\/|__|__|_\*
            **\/__|__|__|_\ */

namespace Challenges
{
    internal class PyramidedAzkee : IAlgorithm
    {
        private readonly IDictionary<char, char> _topLeftRules = new Dictionary<char, char>() {
                {' ','\\'},
                {'.','´'},
                {'´','\\'},
                {'\\','´'},
                {'|','_'},
                {'_','_'},
                {':','_'},
                {'/','_'}
        };

        public void Execute()
        {
            int n = 3;
            string pyramid = string.Empty;
            string[] pry = getSolutionTwo(n);
            for (int i = 0; i < pry.Length; i++)
            {
                pyramid = $"{pyramid}{pry[i]}{(i == (n * 2) - 1 ? string.Empty : Environment.NewLine)}";
            }
            Console.WriteLine(pyramid);
        }

        private string[] getSolutionTwo(int n)
        {
            int spaces = n > 1 ? (2 * n) + (n - 2) : 1;
            char character = ' ';
            string[] pyramid = new string[n * 2];
            for (int y = 0; y < n * 2; y++)
            {
                char startChar = y >= n ? '\\' : '.';
                char[] row = new char[n * 5];
                for (int x = 0; x < n * 5; x++)
                {
                    character = x switch
                    {
                        _ when x == spaces => startChar,
                        _ when x == (3 * n) - (y + 1) => '/',
                        _ when x == (3 * n) + y || (y >= n && x == 0) => '\\',
                        _ when y == 2 && x == 3 * n => ':',
                        _ when (x > (3 * n) - (y + 1) && $"{row[x - 2]}{row[x - 1]}".Equals("__")) || (y % 3 == 0 && x == (3 * n) - (y - 1)) || (y > 3 && (y + 2) % 3 == 0 && x == (3 * n) - y) => '|',
                        _ when x < spaces || x > (3 * n) + y || ((y % 2 == 1) && ((y >= 3 && y < n && x == spaces + 3) || (x == spaces + 1 && y == n))) => ' ',
                        _ => _topLeftRules[character]
                    };
                    row[x] = character;
                }

                spaces += y switch
                {
                    _ when y == n - 1 => -1,
                    _ when y >= n => 1,
                    _ => -3

                };

                pyramid[y] = new string(row);
            }

            return pyramid;
        }
    }
}
