using System;
using System.Collections.Generic;

/*summary is that you may take 3 actions during class that will affect grade/energy levels. grade and energy are 0 to 100. Class automatically ends and grade falls if energy reaches zero.
there is also a 50 percent chance that a special event. Special events are two choices for how you react to them and may also affect energy/grade. Special events do not take up one of the 3 action slots.
Special events also don't tell the player how they will affect energy/grade, so players will have to guess which one is best.
inform player of energy and grade level after every action
*/

//enums for class
public enum SchoolClass
{
    Bible,
    French,
    WorldCiv,
    ComputerScience,
    ArtHistory,
    Biology
}

//immutable data for profs
public record Professor(string Name, SchoolClass ClassType);

//info on players
class PlayerCharacter
{
    //player classes go here
    private int _grade;
    private int _energy;
    //grade and energy are both 0 to 100. each action taken will either decrease or increase grade and energy

    public int Grade
    {
        get => _grade; //get current grade and return
        private set => _grade = Math.Clamp(value, 0, 100);
    }
    public int Energy
    {
        get => _energy;
        private set => _energy = Math.Clamp(value, 0, 100);
    }
//.clamp to keep energy and grade between 0 and 100

    public void UpdateEnergy(int change)
    {
        Energy += change; //updates energy level
    }

    public void UpdateGrade(int change, SchoolClass classType)//update grade with classType parameter

    {
        Grade += change;
    }
}

class ClassSession
{
    public SchoolClass ClassType { get; }
    public Professor Professor { get; }
    private static Random rnd = new();//randomize order of school classes
    private const int ActionsAllowed = 3;//players can do a total of 3 actions in one class period that will affect their grade and energy (some execptions, like energy reaching 0 automatically ends class)

    // Static list for special events available (shared across all sessions)
    public static List<string> SpecialEventsAvailable = new List<string> //special event has 50 percent chance to appear during a class period, code for that farther down
    {
        "PowerOutage",
        "FrogInThroat",
        "BadJoke",
        "ForgotHomework",
        "QuestionAsked",
        "StomachGrowl"
    };

    public ClassSession(SchoolClass classType, Professor professor)
    {
        ClassType = classType;
        Professor = professor;
    }

    public void Run(PlayerCharacter player)
    {
        Console.WriteLine($"\n--- Now attending {ClassType} with Professor {Professor.Name} ---");//new line!
        Console.WriteLine($"You have {ActionsAllowed} actions during this class.");

        int actionsLeft = ActionsAllowed;
        bool specialEventHappened = false;

        if (SpecialEventsAvailable.Count > 0 && rnd.NextDouble() < 0.5)//50 percent chance to occur
        {
            specialEventHappened = true;
            string specialEvent = StuffWeNeed.PickRandom(SpecialEventsAvailable);
            SpecialEventsAvailable.Remove(specialEvent);//once a special event happens, that same special event will not occur again during the game, so it is removed from pool of avaliable events
            HandleSpecialEvent(player, specialEvent);
        }

        while (actionsLeft > 0)
        {
            //accidentaly nap if energy goes to 0 :(
            if (player.Energy == 0)
            {
                Console.WriteLine("You accidentally took a nap due to exhaustion!");
                player.UpdateEnergy(30); //+30 energy
                player.UpdateGrade(-20, ClassType); //-20 grade 
                actionsLeft--;
                StuffWeNeed.PrintStatus(player);
                continue;
            }

            Console.WriteLine($"Actions left: {actionsLeft}");
            Console.WriteLine("Choose an action:");
            Console.WriteLine("1 - Ask Questions (-10 Energy, +10 Grade)");
            Console.WriteLine("2 - Participate in Discussions (-10 Energy, +10 Grade)");
            Console.WriteLine("3 - Try to Decode Professor's Handwriting (-5 Energy, +5 Grade)");
            Console.WriteLine("4 - Quote Some Random Philosopher (-5 Energy, +5 Grade)");
            Console.WriteLine("5 - Go to the Bathroom (+5 Energy, -5 Grade)");
            Console.WriteLine("6 - Text Your BFF (+5 Energy, -5 Grade)");
            Console.WriteLine("7 - Daydream About Wing Wednesday (+10 Energy, -10 Grade)");
            Console.WriteLine("8 - Skip Class (+50 Energy, -40 Grade, ends class immediately)");

            var input = Console.ReadLine();

            if (!int.TryParse(input, out int choice) || choice < 1 || choice > 8)
            {
                Console.WriteLine("Not an option. Try again!");
                continue;
            }

            switch (choice)
            {
                case 1: // Ask questions
                    player.UpdateEnergy(-10);
                    player.UpdateGrade(10, ClassType);
                    StuffWeNeed.PrintActionResult("good job asking quetions!", player);
                    actionsLeft--;
                    break;

                case 2: //participate in discussions
                    player.UpdateEnergy(-10);
                    player.UpdateGrade(10, ClassType);
                    StuffWeNeed.PrintActionResult("You participated in discussions. Energy decreased, grade increased.", player);
                    actionsLeft--;
                    break;

                case 3: //decode professor's terrible handwriting
                    player.UpdateEnergy(-5);
                    player.UpdateGrade(5, ClassType);
                    StuffWeNeed.PrintActionResult("You tried have miraculously succeeded in decoding the professor's handwriting. Slight energy spent, slight grade boost.", player);
                    actionsLeft--;
                    break;

                case 4: //quote philosopher to sound smart
                    player.UpdateEnergy(-5);
                    player.UpdateGrade(5, ClassType);
                    StuffWeNeed.PrintActionResult("You quoted some random philosopher and now everyone things you're smarter than you actually are. Energy down, grade up.", player);
                    actionsLeft--;
                    break;

                case 5: //go to bathroom
                    player.UpdateEnergy(5);
                    player.UpdateGrade(-5, ClassType);
                    StuffWeNeed.PrintActionResult("You went to the bathroom even though you didn't really need to. Energy up, grade down.", player);
                    actionsLeft--;
                    break;

                case 6: //text your BFF ♥︎
                    player.UpdateEnergy(5);
                    player.UpdateGrade(-5, ClassType);
                    StuffWeNeed.PrintActionResult("You texted your BFF silly memes. Energy up, grade down.", player);
                    actionsLeft--;
                    break;

                case 7: //daydream about wing wednesday!
                    player.UpdateEnergy(10);
                    player.UpdateGrade(-10, ClassType);
                    StuffWeNeed.PrintActionResult("You daydreamed about wing wednesday. Energy up, grade down more.", player);
                    actionsLeft--;
                    break;

                case 8: //kkip class
                    player.UpdateEnergy(50);
                    player.UpdateGrade(-40, ClassType);
                    StuffWeNeed.PrintActionResult("You skipped class! Bige energy boost, grade goes down by a lot. Class ends immediately.", player);
                    actionsLeft = 0; //automatically end class
                    break;
            }
        }

        Console.WriteLine($"Class {ClassType} ended. Current status:");
        StuffWeNeed.PrintStatus(player);//tell play energy/grade level
    }

    private void HandleSpecialEvent(PlayerCharacter player, string eventName)
    {
        Console.WriteLine($"\n*** Special Event: {eventName.Replace('_', ' ')} ***");//replace underscores with spaces. ***s are just for decoration

        switch (eventName)
        {
            case "PowerOutage":
                Console.WriteLine("A power outage occurs!");
                if (ClassType == SchoolClass.ComputerScience)
                {
                    player.UpdateGrade(-5, ClassType);
                    Console.WriteLine("Since this is computer science, your grade drops by 5."); //automatically drop grade if you're currently in computer science
                }
                Console.WriteLine("How will you respond?:");
                Console.WriteLine("1 - Decide class is not worth going to anymore");
                Console.WriteLine("2 - Try to keep learning in the dark");
                var input = Console.ReadLine();
                if (input == "1")
                {
                    player.UpdateGrade(-10, ClassType);
                    player.UpdateEnergy(10);
                    Console.WriteLine("You gave up on class. -10 grade, +10 energy.");
                }
                else
                {
                    player.UpdateGrade(10, ClassType);
                    player.UpdateEnergy(-10);
                    Console.WriteLine("You keep learning despite being faced with total darkness. +10 grade, -10 energy.");
                }
                break;

            case "FrogInThroat":
                Console.WriteLine("You have a frog in your throat!");
                Console.WriteLine("how will you respond?:");
                Console.WriteLine("1 - Try to hold in your cough");
                Console.WriteLine("2 - Have a coughing fit in front of everyone");
                input = Console.ReadLine();
                if (input == "1")
                {
                    player.UpdateEnergy(-5);
                    player.UpdateGrade(5, ClassType);
                    Console.WriteLine("You held in the cough no matter how difficult it was. -5 energy, +5 grade.");
                }
                else
                {
                    Console.WriteLine("You coughed loudly and everyone stared at you the entire time. Grade and energy unchanged.");
                }
                break;

            case "BadJoke":
                Console.WriteLine($"{Professor.Name} tells a bad joke!");
                Console.WriteLine("whatever shall you do?:");
                Console.WriteLine("1 - Laugh obnoxiously loud");
                Console.WriteLine("2 - Do not look amused");
                input = Console.ReadLine();
                if (input == "1")
                {
                    player.UpdateEnergy(-5);
                    player.UpdateGrade(5, ClassType);
                    Console.WriteLine("You laughed loudly and probably annoyed your classmates, but the professor appreciated it. -5 energy, +5 grade.");
                }
                else
                {
                    Console.WriteLine("You stayed very serious. No changes to grade or energy.");
                }
                break;

            case "ForgotHomework":
                Console.WriteLine("You realized you forgot the homework that was due today!");
                Console.WriteLine("Either way you're cooked but try to deal with it anyway:");
                Console.WriteLine("1 - Blame your dog!");
                Console.WriteLine("2 - Confess the true truth");
                input = Console.ReadLine();
                if (input == "1")
                {
                    player.UpdateEnergy(-5);
                    player.UpdateGrade(-10, ClassType);
                    Console.WriteLine("You blamed your ugly dog. -5 energy, -10 grade.");
                }
                else
                {
                    player.UpdateEnergy(-10);
                    player.UpdateGrade(-5, ClassType);
                    Console.WriteLine("You confessed. -10 energy, -5 grade.");
                }
                break;

            case "QuestionAsked":
                Console.WriteLine($"{Professor.Name} asks a question you don't know the answer to and makes eye contact with you!");
                Console.WriteLine("panic mode... what will you do??:");
                Console.WriteLine("1 - Try to answer correctly");
                Console.WriteLine("2 - Look down and pretend to take notes");
                input = Console.ReadLine();
                if (input == "1")
                {
                    player.UpdateGrade(10, ClassType);
                    player.UpdateEnergy(-10);
                    Console.WriteLine("You answered correctly!. +10 grade, -10 energy.");
                }
                else
                {
                    player.UpdateEnergy(5);
                    player.UpdateGrade(-5, ClassType);
                    Console.WriteLine("You pretended to take awesome notes and the professor picked some other unfortunate soul. +5 energy, -5 grade.");
                }
                break;

            case "StomachGrowl":
                Console.WriteLine("Your stomach growls loudly and everyone stares at you!");
                Console.WriteLine("Choose your response");
                Console.WriteLine("1 - Blame the person next to you");
                Console.WriteLine("2 - Pretend it was your silly ringtone");
                input = Console.ReadLine();
                if (input == "1")
                {
                    player.UpdateEnergy(5);
                    player.UpdateGrade(-5, ClassType);
                    Console.WriteLine("You blamed your poor neighbor. +5 energy, -5 grade.");
                }
                else
                {
                    Console.WriteLine("You pretended it was your ringtone. No change to grade or energy.");
                }
                break;
        }

        StuffWeNeed.PrintStatus(player);
    }
}

// Static utility class
static class StuffWeNeed
{
    private static Random rnd = new();

    public static void PrintActionResult(string message, PlayerCharacter player)
    {
        Console.WriteLine(message);
        PrintStatus(player);
    }

    public static void PrintStatus(PlayerCharacter player)
    {
        Console.WriteLine($"Current Energy: {player.Energy}, Current Grade: {player.Grade}");
    }

    public static string PickRandom(List<string> list)
    {
        int index = rnd.Next(list.Count);
        return list[index];
    }
}
public partial class Program
{
    static void Main()
    {
        var profs = new List<Professor>
        {
            new Professor("Frank Anderson", SchoolClass.Bible),
            new Professor("Victoria Malone", SchoolClass.French),
            new Professor("Jason Strandquist", SchoolClass.WorldCiv),
            new Professor("Mason Ruby", SchoolClass.ComputerScience),
            new Professor("Christopher Nadaskay", SchoolClass.ArtHistory),
            new Professor("Mark Bolyard", SchoolClass.Biology)
        };

        var classOrder = new List<SchoolClass>
        {
            SchoolClass.Bible,
            SchoolClass.French,
            SchoolClass.WorldCiv,
            SchoolClass.ComputerScience,
            SchoolClass.ArtHistory,
            SchoolClass.Biology
        };

        var rnd = new Random();
        //used the fisher yates method to shuffle class order
        for (int i = classOrder.Count - 1; i > 0; i--)
        {
            int j = rnd.Next(i + 1);
            (classOrder[i], classOrder[j]) = (classOrder[j], classOrder[i]);
        }


        //run classes in shuffled order
        foreach (var schoolClass in classOrder)
        {
            var professor = profs.Find(p => p.ClassType == schoolClass);
            var session = new ClassSession(schoolClass, professor);
            session.Run(player);//this part doesn't work right now but this line but "player" here will be updated once the rest of code is combined
        }
    }
}