// Author: Elena Hazard
// Date: 10/20/2025

class ChooseCharacter
{
    private string player;

    public void Character(int choice)
    {
        switch (choice)
        {
            case 1:
                player = "Elena";
                break;
            case 2:
                player = "Kate";
                break;
            case 3:
                player = "Jonathan";
                break;
            default:
                Checks.ErrorMessage();
                break;
        }
    }
}
class Elena
{
    private CharacterName _Elena_Name = new() { Name = "Elena" };
    private CharacterAbilities _Elena_Abilities = new() { Strength1 = "Game Development", Strength1_Value = 2, Stength2 = "Computer Science", Strength2_Value = 1, Weakness = "Biology", Weakness_Value = -2 };

}

class Kate
{
    private CharacterName _Kate_Name = new() { Name = "Kate" };
    private CharacterAbilities _Kate_Abilities = new() { Strength1 = "Spanish", Strength1_Value = 2, Stength2 = "French", Strength2_Value = 1, Weakness = "Art History", Weakness_Value = -2 };

}

class Jonathan
{
    private CharacterName _Jonathan_Name = new() { Name = "Jonathan" };
    private CharacterAbilities _Jonathan_Abilities = new() { Strength1 = "Cybersecurity", Strength1_Value = 2, Stength2 = "Computer Science", Strength2_Value = 1, Weakness = "World Civilizations", Weakness_Value = -2 };
   
}

record CharacterName
{
    public string Name { get; init; }
}

record CharacterAbilities
{
    public string Strength1 { get; init; }
    public int Strength1_Value { get; init; }
    public string Stength2 { get; init; }
    public int Strength2_Value { get; init; }
    public string Weakness { get; init; }
    public int Weakness_Value { get; init; }
}