// Author; Elena Hazard
// Date: 10/23/2025

// Class that of methods, the 1st checks if a key is pushed, the 2nd will give an error message, and the 3rd checks if you have won the game.
static class Checks
{
    public static ConsoleKey GetKey()
    {
        ConsoleKey getKey = Console.ReadKey(true).Key;
        return getKey;
    }
    public static void ErrorMessage()
    {
        Console.WriteLine("\nInput is invalid. Please use one of the listed keys.\n");
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

// Class that holds const varibles of ConsoleKey to make choices with.
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