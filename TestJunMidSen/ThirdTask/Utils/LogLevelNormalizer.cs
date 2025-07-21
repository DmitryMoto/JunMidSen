namespace TestJunMidSen.ThirdTask.Utils
{
    public static class LogLevelNormalizer
    {
        public static string Normalize(string level) => level.ToUpper() switch
        {
            "INFORMATION" => "INFO",
            "WARNING" => "WARN",
            "INFO" => "INFO",
            "WARN" => "WARN",
            "ERROR" => "ERROR",
            "DEBUG" => "DEBUG",
            _ => "UNKNOWN"
        };
    }
}
