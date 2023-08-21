using System.Text.RegularExpressions;

namespace PJ5_FatalFrame5_DocumentTextCentralize
{
    internal class Program
    {
        /* This software considers the following metrics:
           - uppercaseMaxCount = 24,
           - lowercaseMaxCount = 33,
           - specialCharactersMaxCount = 33,
           - whitespaceMaxCount = 94
         */

        private static readonly int uppercaseSize = 4;
        private static readonly int lowercaseSize = 2;
        private static readonly int specialCharacterSize = 2;
        private static readonly int whitespaceSize = 1;
        private static readonly int whitespaceMaxCount = 94;

        public static void Main()
        {
            int tryCount = 0;

            while (true)
            {
                Console.Clear();
                Console.Write("Put any text to centralize: ");
                string originalText = Console.ReadLine();

                if (string.IsNullOrEmpty(originalText)) 
                {
                    if (tryCount < 2)
                    {
                        Console.WriteLine($"Invalid text. Press `Enter` and try again with a valid text!");
                        tryCount++;
                        Console.ReadKey();
                        continue;
                    }
                    else
                    {
                        Console.WriteLine($"Nice try :D");
                        return;
                    }   
                }

                int uppercaseActualCount = GetActualCount(originalText, @"[A-ZÁÀÂÃÉÈÍÏÓÔÕÖÚÇÑ]\d*");
                Console.WriteLine($"UpperCase Characters: {uppercaseActualCount}");

                int lowercaseActualCount = GetActualCount(originalText, @"[a-záàâãéèêíïóôõöúçñ]\d*");
                Console.WriteLine($"LowerCase Characters: {lowercaseActualCount}");

                int specialCharacterActualCount = GetActualCount(originalText, @"[$&+,:;=?@#|'<>.^*()%!-]\d*");
                Console.WriteLine($"Special Characters: {specialCharacterActualCount}");

                int whitespaceActualCount = GetActualCount(originalText, @"[ ]\d*");
                Console.WriteLine($"Whitespaces: {whitespaceActualCount}");

                int upperCaseSum = uppercaseActualCount * uppercaseSize;
                int lowerCaseSum = lowercaseActualCount * lowercaseSize;
                int specialCharacterSum = specialCharacterActualCount * specialCharacterSize;
                int whitespaceSum = whitespaceActualCount * whitespaceSize;
                int spaceUsed = upperCaseSum + lowerCaseSum + specialCharacterSum + whitespaceSum;
                int whitespaceNeeded = whitespaceMaxCount - spaceUsed;
                int spaceBetweenPhrase = whitespaceNeeded / 2;

                // centralize original text
                var adjustedText = originalText.PadLeft(originalText.Length + spaceBetweenPhrase);
                Console.WriteLine($"\nNewText:\n{adjustedText}*\n\n");

                Console.WriteLine($"Press `Enter` to send another text.");
                Console.ReadKey();
            }
        }

        private static int GetActualCount(string originalText, string pattern)
        {
            return Regex.Count(originalText, pattern);
        }
    }
}