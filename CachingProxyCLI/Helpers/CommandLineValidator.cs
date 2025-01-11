using System.Text.RegularExpressions;

namespace CachingProxyCLI.Validations
{
    public static class CommandLineValidator
    {
        public static bool ValidateArguments(string[] args)
        {
            if (args.Length < 4)
            {
                Console.WriteLine("Insufficient arguments provided.");
                return false;
            }

            if(args[0] != "--port" || !IsValidPort(args[1]) || args[2] != "--origin" || !IsValidUrl(args[3]))
            {
                Console.WriteLine("Invalid command-line arguments.");
                return false;
            }

            return true;
        }

        private static bool IsValidPort(string port)
        {
            if (int.TryParse(port, out int number) && number >= 0 && number <= 65535)
            {
                return true;
            }

            Console.WriteLine("The given port number is invalid. Please try again.");
            return false;
        }

        private static bool IsValidUrl(string URL)
        {
            string pattern = "^https?:\\/\\/([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,}$";
            if(Regex.IsMatch(URL, pattern))
            {
                return true;
            }

            Console.WriteLine("The given origin URL is invalid. Please try again.");
            return false;
        }
    }
}