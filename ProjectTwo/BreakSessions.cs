using System;
using System.Security.Cryptography;
using static System.Console;


// for testing
TempVaraibles tempObject = new();
tempObject._EnergyLevel = 40;
tempObject._StudyLevel = 60;
tempObject._BusterBucks = 198.46;

StoreItem testBook = new("Testbook", 62.79, "A testbook, not a textbook.");
StoreItem textBook = new("Textbook", 71.58, "This, however, IS a textbook.");

List<StoreItem> testItems = [ testBook, textBook ];

BreakSessions testSession = new(LocationsEnum.DormRoom);
testSession.RunBreakSession(tempObject, LocationsEnum.MerchStore, testItems);

class BreakSessions
{
    private Enum _Location; // dorm room is default
    public BreakSessions(Enum location)
    {
        _Location = location;
    }
    public Enum GetLocation => _Location;

    public void GoToDormRoom(TempVaraibles obj)
    {
        _Location = LocationsEnum.DormRoom;
        Write("You went to your room...");

        // you have enough energy to study
        if (obj._EnergyLevel >= 50)
        {
            // you're study level increases depending on how energized you are
            int studyIncrease = obj._EnergyLevel / 10;
            obj._StudyLevel += studyIncrease;

            WriteLine($"and studied for a bit! Your Study-Level increased by {studyIncrease}!");
        }

        // you're too tired and fall asleep
        else
        {
            obj._EnergyLevel += 50;
            WriteLine("and fell asleep! Your Energy-Level increased by 50.");
        }
    }

    public void GoToCobo(TempVaraibles obj)
    {
        _Location = LocationsEnum.Cobo;
        Write("You went to COBO for a meal...");

        // randomly decide the meal you get
        Random randomMeal = new Random();
        Enum[] possibleMeals = {
            CoboMealsEnum.Pizza, CoboMealsEnum.Pizza, CoboMealsEnum.Pizza,
            CoboMealsEnum.Homestyle, CoboMealsEnum.Homestyle,
            CoboMealsEnum.TacoBar, CoboMealsEnum.TacoBar,
            CoboMealsEnum.Dessert,
            CoboMealsEnum.SaladBar
        };
        Enum coboMeal = possibleMeals[randomMeal.Next(0, possibleMeals.Length)];

        // handle effects based on meal
        switch (coboMeal)
        {
            case CoboMealsEnum.Homestyle: // was the homestyle good today?
                Random randomHomestyleEffect = new Random();
                int homestyleEffect = randomHomestyleEffect.Next(1, 11);

                if (homestyleEffect >= 7)
                {
                    obj._EnergyLevel += 50;
                    WriteLine("and decided to get homestyle. It was actually good today! Your Energy-Level increased by 60!");
                }
                else
                {
                    obj._EnergyLevel += 15;
                    WriteLine("and decided to get homestyle. It wasn't good, and you didn't eat much of it. Your Energy-Level increased by 15.");
                }

                break;

            case CoboMealsEnum.TacoBar: // they always give you way too much food haha
                obj._EnergyLevel += 75;
                WriteLine("and decided to go through the taco bar. You were given so much food, and it was good! Your Energy-Level increased by 75!");
                break;

            case CoboMealsEnum.Dessert: // somehow this increases your energy MORE than bad homestyle
                obj._EnergyLevel += 25;
                WriteLine("and decided to get...just dessert? Man does it taste good, but your Energy-Level only increases by 25.");
                break;

            case CoboMealsEnum.SaladBar: // I'll take Elena's word on this xD
                obj._EnergyLevel -= 15;
                WriteLine("and you decide to look over the salad bar. As usual, there's not anything edible. Your Energy-Level decreases by 15...");
                break;

            default: // pizza's typically solid
                obj._EnergyLevel += 50;
                WriteLine("and decided to get pizza. It's typically solid. Your Energy-Level increased by 50.");
                break;
        }
    }

    public void GoToMerchStore(TempVaraibles obj, List<StoreItem> items)
    {
        _Location = LocationsEnum.MerchStore;
        WriteLine("You went to the merch store. Here's what you can buy:");

        for (int i = 0; i < items.Count; i++)
        {
            StoreItem si = items[i]; // si is for store-item
            WriteLine($"{i + 1}. {si._Name} ~ {si._Description}. ({si._Cost:C})");

            // if we've reached the end of the items, display the option to leave
            if (i + 1 == items.Count)
            {
                WriteLine($"{i + 2}. Leave.");
            }
        }
        WriteLine($"You have {obj._BusterBucks} Buster-Bucks. Will you buy anything?");

        // determine choice
        int choice = int.Parse(ReadLine()) - 1;

        // if the item you want exists, try to buy it
        if (choice < items.Count && items[choice] != null)

            // if you can afford the item, buy it
            if (items[choice]._Cost <= obj._BusterBucks)
            {
                obj._BusterBucks -= items[choice]._Cost;
                obj.AddItemToInventory(items[choice]._Name);

                WriteLine("Thank you. Have a nice day.");
            }
            else // you're broke!
            {
                WriteLine("Sorry, you can't afford that. Have a nice day anyway.");
            }
        else // the item doesn't exist / you left
        {
            WriteLine("Have a nice day, then.");
        }
    }

    public void RunBreakSession(TempVaraibles obj, Enum location, List<StoreItem> storeItems)
    {
        switch (location)
        {
            case LocationsEnum.Cobo:
                GoToCobo(obj);
                break;

            case LocationsEnum.MerchStore:
                GoToMerchStore(obj, storeItems);
                break;

            default: // go to dorm room
                GoToDormRoom(obj);
                break;
        }
    }
}

class TempVaraibles // I spelled 'Variables' wrong xD
{
    public int _EnergyLevel { get; set; }
    public int _StudyLevel { get; set; }
    public double _BusterBucks { get; set; }

    private List<string> _Inventory = new();
    public void DisplayInventory() // also copied from me and William's project
    {
        foreach (string item in _Inventory)
        {
            Console.WriteLine("- " + item);
        }
    }
    public void AddItemToInventory(string item) // also also copied from me and William's project
    {
        _Inventory.Add(item);
    }
}

class StoreItem
{
    public string _Name { get; set; }
    public double _Cost { get; set; }
    public string _Description { get; set; }

    // DON'T USE ANY PERIODS FOR NAME OR DESCRIPTION!
    public StoreItem(string name, double cost, string description)
    {
        _Name = name;
        _Cost = cost;
        _Description = description;
    }
}


enum LocationsEnum
{
    DormRoom,
    Cobo,
    MerchStore
}

enum CoboMealsEnum
{
    Pizza,
    TacoBar,
    Homestyle,
    SaladBar,
    Dessert
}