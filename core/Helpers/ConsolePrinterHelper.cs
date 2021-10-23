namespace net6.core.Helpers
{
    public class ConsolePrinterHelper
    {
        public static void PrintWithColor(string message, ConsoleColor color = ConsoleColor.DarkGreen)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
