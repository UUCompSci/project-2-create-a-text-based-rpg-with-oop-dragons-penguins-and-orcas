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

    // static void WinConditions()
    // {
    //     if (_grade >= 70)
    //     {
    //         Console.WriteLine("You have gotten at least a C in all of your classes! You do not have to retake any classes next semester! Congratulations!");
    //     }
    //     else
    //     {
    //         Console.WriteLine("You have not gotten a C in your classes and you will have to retake them next semester!");
    //     }
    // }
}