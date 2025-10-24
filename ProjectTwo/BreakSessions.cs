using System;
using System.Security.Cryptography;
using static System.Console;

class BreakSessions
{
    private Enum _Location; // dorm room is default
    public Enum GetLocation => _Location;

    public void GoToDormRoom(PlayerCharacter obj)
    {
        _Location = LocationsEnum.DormRoom;
        Write("You went to your room...");

        // you have enough energy to study
        if (obj.Energy >= 50)
        {
            // you're study level increases depending on how energized you are
            int studyIncrease = obj.Energy / 10;
            obj.UpdateGrade(studyIncrease, SchoolClass.Bible); // no one's weak to Bible haha

            WriteLine($"and studied for a bit! Your Grade increased by {studyIncrease}!");
        }

        // you're too tired and fall asleep
        else
        {
            obj.UpdateEnergy(50);
            WriteLine("and fell asleep! Your Energy increased by 50.");
        }
    }

    public void GoToCobo(PlayerCharacter obj)
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
                    obj.UpdateEnergy(50);
                    WriteLine("and decided to get homestyle. It was actually good today! Your Energy increased by 60!");
                }
                else
                {
                    obj.UpdateEnergy(15);
                    WriteLine("and decided to get homestyle. It wasn't good, and you didn't eat much of it. Your Energy increased by 15.");
                }

                break;

            case CoboMealsEnum.TacoBar: // they always give you way too much food haha
                obj.UpdateEnergy(75);
                WriteLine("and decided to go through the taco bar. You were given so much food, and it was good! Your Energy increased by 75!");
                break;

            case CoboMealsEnum.Dessert: // somehow this increases your energy MORE than bad homestyle
                obj.UpdateEnergy(25);
                WriteLine("and decided to get...just dessert? Man does it taste good, but your Energy only increases by 25.");
                break;

            case CoboMealsEnum.SaladBar: // I'll take Elena's word on this xD
                obj.UpdateEnergy(-15);
                WriteLine("and you decide to look over the salad bar. As usual, there's not anything edible. Your Energy decreases by 15...");
                break;

            default: // pizza's typically solid
                obj.UpdateEnergy(50);
                WriteLine("and decided to get pizza. It's typically solid. Your Energy increased by 50.");
                break;
        }
    }

    public void GoToMerchStore(PlayerCharacter obj, List<StoreItem> items, MerchStuff wallet)
    {
        _Location = LocationsEnum.MerchStore;
        WriteLine("You went to the merch store. Here's what you can buy:");

        // create a reference to _BusterBucks
        double w = MerchStuff._BusterBucks;

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
        WriteLine($"You have {w} Buster-Bucks. Will you buy anything?");

        // determine choice
        int choice = (int)(Checks.GetKey() - 48) - 1;

        // if the item you want exists, try to buy it
        if (choice < items.Count && items[choice] != null)

            // if you can afford the item, buy it
            if (items[choice]._Cost <= w)
            {
                wallet.AddBucks(-items[choice]._Cost);

                // don't add the textbook, just update a boolean
                if (items[choice]._Name == "All-Class Textbook")
                {
                    wallet.UpdateBoughtTextbook(true);
                }

                // buy a typical item
                else
                {
                    wallet.AddItemToInventory(items[choice]._Name);
                }
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