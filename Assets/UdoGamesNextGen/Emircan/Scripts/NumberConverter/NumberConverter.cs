namespace MelonMath
{
    /// <summary>
    /// A utility class for number-related conversions.
    /// </summary>
    public static class NumberConverter
    {
        # region CONVERT TO KILO

        /// <summary>
        /// Converts a number to kilos.
        /// </summary>
        /// <param name="number">The number to convert.</param>
        /// <returns>The equivalent number in kilos.</returns>
        public static float ConvertToKilo(int number)
        {
            return number / 1000f;
        }

        /// <summary>
        /// Converts a number to kilos and separates the kilo part and remainder.
        /// </summary>
        /// <param name="number">The number to convert.</param>
        /// <param name="kiloPart">Output: The equivalent kilo part.</param>
        /// <param name="remainder">Output: The remainder after converting to kilos.</param>
        public static void ConvertToKilo(int number, out int kiloPart, out int remainder)
        {
            kiloPart = number / 1000;
            remainder = number % 1000;
        }

        # endregion

        # region CONVERT TO MILLION

        /// <summary>
        /// Converts a number to millions.
        /// </summary>
        /// <param name="number">The number to convert.</param>
        /// <returns>The equivalent number in millions.</returns>
        public static float ConvertToMillion(int number)
        {
            return number / 1000000f;
        }

        /// <summary>
        /// Converts a number to millions and separates the million part and remainder.
        /// </summary>
        /// <param name="number">The number to convert.</param>
        /// <param name="millionPart">Output: The equivalent million part.</param>
        /// <param name="remainder">Output: The remainder after converting to millions.</param>
        public static void ConvertToMillion(int number, out int millionPart, out int remainder)
        {
            millionPart = number / 1000000;
            remainder = number % 1000000;
        }

        /// <summary>
        /// Converts a number to millions and separates the million, kilo, and remainder parts.
        /// </summary>
        /// <param name="number">The number to convert.</param>
        /// <param name="millionPart">Output: The equivalent million part.</param>
        /// <param name="remainderKiloPart">Output: The remainder in kilos after converting to millions.</param>
        /// <param name="remainder">Output: The remainder after converting to millions.</param>
        public static void ConvertToMillion(int number, out int millionPart, out int remainderKiloPart, out int remainder)
        {
            millionPart = number / 1000000;
            int kiloPart = number % 1000000;
            remainderKiloPart = kiloPart / 1000;
            remainder = kiloPart % 1000;
        }

        # endregion

        # region CONVERT TO BILLION

        /// <summary>
        /// Converts a number to billions.
        /// </summary>
        /// <param name="number">The number to convert.</param>
        /// <returns>The equivalent number in billions.</returns>
        public static float ConvertToBillion(int number)
        {
            return number / 1000000000f;
        }

        /// <summary>
        /// Converts a number to billions and separates the billion part and remainder.
        /// </summary>
        /// <param name="number">The number to convert.</param>
        /// <param name="billionPart">Output: The equivalent billion part.</param>
        /// <param name="remainder">Output: The remainder after converting to billions.</param>
        public static void ConvertToBillion(int number, out int billionPart, out int remainder)
        {
            billionPart = number / 1000000000;
            remainder = number % 1000000000;
        }

        # endregion

        # region CONVERT TO TRILLION

        /// <summary>
        /// Converts a number to trillions.
        /// </summary>
        /// <param name="number">The number to convert.</param>
        /// <returns>The equivalent number in trillions.</returns>
        public static float ConvertToTrillion(long number)
        {
            return number / 1000000000000f;
        }

        /// <summary>
        /// Converts a number to trillions and separates the trillion part and remainder.
        /// </summary>
        /// <param name="number">The number to convert.</param>
        /// <param name="trillionPart">Output: The equivalent trillion part.</param>
        /// <param name="remainder">Output: The remainder after converting to trillions.</param>
        public static void ConvertToTrillion(long number, out long trillionPart, out long remainder)
        {
            trillionPart = number / 1000000000000;
            remainder = number % 1000000000000;
        }

        # endregion


        public static string ConvertToString(long number)
        {
            if (number >= 1000000000000f)
            {
                return ConvertToTrillion(number).ToString("0.0") + "T";
            }
            else if (number >= 1000000000f)
            {
                return ConvertToBillion((int)number).ToString("0.0")+ "B";
            }
            else if (number >= 1000000f)
            {
                return ConvertToMillion((int)number).ToString("0.0")+ "M";
            }
            else if (number >= 1000f)
            {
                return ConvertToKilo((int)number).ToString("0.0")+ "K";
            }
            else
            {
                return number.ToString();
            }
        }
    }
}
