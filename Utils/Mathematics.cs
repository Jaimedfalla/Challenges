namespace Utils
{
    public class Mathematics
    {
        private const int BASE_10 = 10;

        /// <summary>
        /// Extracts digits of a number
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static int SplitNumberInDigits(long number) {
            double k = (int)Math.Log10(number) + 1;
            int result = 0;
            for (int i = 0; i < k; i++) {
                result += (int)(((number % Math.Pow(BASE_10, i + 1)) - (number % Math.Pow(BASE_10, i))) / Math.Pow(BASE_10, i));
            }

            return result;
        }
    }
}