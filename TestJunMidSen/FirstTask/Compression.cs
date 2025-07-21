namespace TestJunMidSen.FirstTask
{
    public class Compression : IStringProcessing
    {
        public string Process(string inputStr)
        {
            string outputStr = string.Empty;
            if (inputStr.Length < 1) return outputStr;

            char tempLetter = inputStr[0];
            int count = 0;
            int letterIndex = 0;

            foreach (char letter in inputStr)
            {
                if (tempLetter == letter)
                {
                    count++;
                }
                else
                {
                    outputStr += $"{tempLetter}{(count > 1 ? count : "")}";
                    count = 1;
                    tempLetter = letter;
                }

                letterIndex++;
                if (letterIndex == inputStr.Length)
                {
                    outputStr += $"{tempLetter}{(count > 1 ? count : "")}";
                }
            }
            return outputStr;
        }
    }
}
