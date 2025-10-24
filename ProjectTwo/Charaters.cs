// Author: Elena Hazard
// Date: 10/20/2025

// Elena's stats.
class Character 
{
    public CharacterStats _Elena = new() { Name = "Elena", Strength = "ComputerScience", Strength_Value = 5, Weakness = "Biology", Weakness_Value = 5 };

// Kate's stats.

    public CharacterStats _Kate = new() { Name = "Kate", Strength = "French", Strength_Value = 5, Weakness = "ArtHistory", Weakness_Value = 5 };

// Jonathan's stats.
    public CharacterStats _Jonathan = new() { Name = "Jonathan", Strength = "ComputerScience", Strength_Value = 5, Weakness = "WorldCiv", Weakness_Value = 5 };
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