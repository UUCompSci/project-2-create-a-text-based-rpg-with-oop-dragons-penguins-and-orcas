// Author: Elena Hazard
// Date: 10/20/2025

// Elena's stats.
class Character 
{
    private CharacterStats _Elena = new() { Name = "Elena", Strength = "Computer Science", Strength_Value = 10, Weakness = "Biology", Weakness_Value = -10 };

// Kate's stats.

    private CharacterStats _Kate = new() { Name = "Kate", Strength = "French", Strength_Value = 10, Weakness = "Art History", Weakness_Value = -10 };

// Jonathan's stats.
    private CharacterStats _Jonathan = new() { Name = "Jonathan", Strength = "ComputerScience", Strength_Value = 10, Weakness = "WorldCiv", Weakness_Value = -10 };
}

// Makes a record of the character's name, strengths, and weaknesses.
record CharacterStats
{
    public string Name { get; init; }
    public string Strength { get; init; }
    public int Strength_Value { get; init; }
    public string Weakness { get; init; }
    public int Weakness_Value { get; init; }
}