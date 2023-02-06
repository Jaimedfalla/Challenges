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
        private IDictionary<char, char> _topLeftRules = new Dictionary<char, char>() {
                {' ','.'},
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
            int size = 8;
            string pyramid = string.Empty;
            string[] left = getTopLefPyramid(size);
            string[] right = getTopRigthPyramid(size);
            for (int i = 0; i < size*2; i++) {
                string wleft = left[i];
                string wright = right[i];
                pyramid = $"{pyramid}{wleft}{wright}{Environment.NewLine}";
            }
            Console.WriteLine(pyramid);
        }

        private string[] getTopLefPyramid(int n) {
            string[] pyramid = new string[n*2];
            int increment = 0;
            int spaces = 1;

            for (int x=n-1; x >= 0;x--) {
                char[] rowChars = new char[n * 5];
                char startChar = ' ';
                int width = (n * 2) + increment;

                for (int y = 0; y < width; y++) {

                    startChar = y >= spaces? _topLeftRules[startChar]: startChar;
                    rowChars[y] = startChar;
                }
 
                spaces += 3;

                increment++;
                pyramid[x] = new string(rowChars);
            }

            pyramid = getBottomLeftPyramid(n, pyramid);

            return pyramid;
        }

        private string[] getTopRigthPyramid(int n)
        {
            string[] pyramid = new string[n*2];
            int increment = 0;

            for(int y = 0; y < n*2; y++)
            {
                char[] rowChars = new char[n * 5];
                char startChar = '/';

                int width = 2 + increment;
                for (int x = 0; x < width; x++)
                {
                    startChar = x switch
                    {
                        _ when x == 0 => startChar,
                        _ when x == width - 1 => '\\',
                        _ when y== (3*(y-2))-2 && x==1 => '|',
                        _ when y == 2 && x == 3 => ':',
                        _ when y % 3==0 && x==2 => '|',
                        _ when x > 1 && $"{rowChars[x - 2]}{rowChars[x-1]}".Equals("__") => '|',
                        _ => _topLeftRules[startChar]
                    };
                    
                    rowChars[x] = startChar;
                }

                increment += 2;
                pyramid[y] = new string(rowChars);
            }

            return pyramid;
        }

        private string[] getBottomLeftPyramid(int n, string[] pyramid)
        {
            int decrement = 1;
            int size = (n * 2) - decrement;
            int spaces = 0;
            for (int y = 0; y < n; y++) {
                char startChar = '\\';
                char[] rowChars = new char[size];
                int width = (n * 2) - decrement;
                for (int x = 0; x < width; x++) {
                    startChar = x switch
                    {
                        _ when spaces > 0 && x <=spaces => ' ',
                        _ when x == spaces + 1 || x == width - 1 => '\\',
                        _ => _topLeftRules[startChar]
                    };

                    rowChars[x] = startChar;
                }

                decrement++;
                pyramid[n+y] = new string(rowChars);
            }

            return pyramid;
        }
    }
}
