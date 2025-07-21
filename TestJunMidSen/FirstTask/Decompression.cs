namespace TestJunMidSen.FirstTask
{
    public class Decompression : IStringProcessing
    {
        public string Process(string inputStr)
        {
            string outputStr = string.Empty;
            if (inputStr.Length < 1) return outputStr;

            char tempLetter = inputStr[0];
            string lettersCountStr = string.Empty;

            for (int i = 0; i < inputStr.Length; i++)
            {
                if (!char.IsDigit(inputStr[i]))
                {
                    tempLetter = inputStr[i];
                    outputStr += tempLetter;
                }

                if (char.IsDigit(inputStr[i]))
                {
                    lettersCountStr += inputStr[i];
                }

                if ((i + 1 < inputStr.Length && !char.IsDigit(inputStr[i + 1])) || (i == inputStr.Length - 1))
                {
                    bool isDigit = int.TryParse(lettersCountStr.ToString(), out int lettersCount);
                    if (isDigit)
                    {
                        for (int l = 0; l < lettersCount - 1; l++)
                        {
                            outputStr += tempLetter;
                        }
                        lettersCountStr = string.Empty;
                    }
                }
            }

            return outputStr;
        }
    }
}
