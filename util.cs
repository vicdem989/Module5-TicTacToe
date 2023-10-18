using System.Reflection.Metadata.Ecma335;

namespace Utils
{
    public static class ANSI_COLORS
    {
        public static string RED = "\u001b[31m";
        public static string GREEN = "\u001b[32m";
        public static string YELLOW = "\u001b[33m";
        public static string BLUE = "\u001b[34m";
        public static string MAGENTA = "\u001b[35m";
        public static string CYAN = "\u001b[36m";
        public static string WHITE = "\u001b[37m";
        public static string RESET = "\u001b[0m";
    }
    public class Output
    {
        private int indentLevel = 0;

        public Output() { }

        public Output WriteLine(string text)
        {
            System.Console.WriteLine(text);
            return this;
        }

        public Output Write(string text)
        {
            System.Console.Write(text);
            return this;
        }

        public Output Color(string color)
        {
            System.Console.Write(color);
            return this;
        }

        public Output Reset()
        {
            System.Console.Write(ANSI_COLORS.RESET);
            return this;
        }

        public Output AddColor(string text, string modifier, bool reset = true, bool oneLine = true)
        {
            if (oneLine)
            {
                WriteLine(modifier + $"{text}");
            }
            else
            {
                Write(text);
            }
            if (reset)
                Reset();
            return this;
        }

    }

}