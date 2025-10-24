static class Checks
{
    public static ConsoleKey GetKey()
    {
        ConsoleKey getKey = Console.ReadKey(true).Key;
        return getKey;
    }
    public static void ErrorMessage()
    {
        Console.WriteLine("Input is invalid. Please use one of the listed keys.");
    }

    public static void WinConditions(int grade)
    {
        Console.WriteLine("\nIt is the end of the semester at last and you have just received your grades back, the results are...");
        if (grade >= 70)
        {
            Console.WriteLine("You have gotten at least a C in all of your classes! You do not have to retake any classes next semester! Congratulations!");
        }
        else
        {
            Console.WriteLine("You have not gotten a C in your classes and you will have to retake them next semester!");
        }
    }
}

public static class CheckKey
    {
        public const ConsoleKey _1 = ConsoleKey.D1;
        public const ConsoleKey _2 = ConsoleKey.D2;
        public const ConsoleKey _3 = ConsoleKey.D3;
        public const ConsoleKey _4 = ConsoleKey.D4;
        public const ConsoleKey _5 = ConsoleKey.D5;
        public const ConsoleKey _6 = ConsoleKey.D6;
        public const ConsoleKey _7 = ConsoleKey.D7;
        public const ConsoleKey _8 = ConsoleKey.D8;
        public const ConsoleKey _9 = ConsoleKey.D9;
    }