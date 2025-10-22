// Author: Elena Hazard
// Date: 10/20/2025

class Elena
{
    private CharacterName _Elena_Name = new() { Name = "Elena" };
    private CharacterAbilities _Elena_Abilities = new() { Strength1 = "Game Development", Strength1_Value = 2, Stength2 = "Computer Science", Strength2_Value = 1, Weakness = "Biology", Weakness_Value = -2 };
    private StatusEffects _Elena_Current_Status;

    public StatusEffects Status
    {
        get { return _Elena_Current_Status; }
        set
        {
            switch (value)
            {
                case StatusEffects.Exhaustion:
                    _Elena_Current_Status = value;
                    break;
                case StatusEffects.Tired:
                    _Elena_Current_Status = value;
                    break;
                case StatusEffects.Alert:
                    _Elena_Current_Status = value;
                    break;
                case StatusEffects.Energized:
                    _Elena_Current_Status = value;
                    break;
                default:
                    _Elena_Current_Status = value;
                    break;
            }
        }
    }
}

class Kate
{
    private CharacterName _Kate_Name = new() { Name = "Kate" };
    private CharacterAbilities _Kate_Abilities = new() { Strength1 = "Spanish", Strength1_Value = 2, Stength2 = "French", Strength2_Value = 1, Weakness = "Art History", Weakness_Value = -2 };
    private StatusEffects _Kate_Current_Status;

    public StatusEffects Status
    {
        get { return _Kate_Current_Status; }
        set
        {
            switch (value)
            {
                case StatusEffects.Exhaustion:
                    _Kate_Current_Status = value;
                    break;
                case StatusEffects.Tired:
                    _Kate_Current_Status = value;
                    break;
                case StatusEffects.Alert:
                    _Kate_Current_Status = value;
                    break;
                case StatusEffects.Energized:
                    _Kate_Current_Status = value;
                    break;
                default:
                    _Kate_Current_Status = value;
                    break;
            }
        }
    }
}

class Jonathan
{
    private CharacterName _Jonathan_Name = new() { Name = "Jonathan" };
    private CharacterAbilities _Jonathan_Abilities = new() { Strength1 = "Cybersecurity", Strength1_Value = 2, Stength2 = "Computer Science", Strength2_Value = 1, Weakness = "World Civilizations", Weakness_Value = -2 };
    private StatusEffects _Jonathan_Current_Status;

    public StatusEffects Status
    {
        get { return _Jonathan_Current_Status; }
        set
        {
            switch (value)
            {
                case StatusEffects.Exhaustion:
                    _Jonathan_Current_Status = value;
                    break;
                case StatusEffects.Tired:
                    _Jonathan_Current_Status = value;
                    break;
                case StatusEffects.Alert:
                    _Jonathan_Current_Status = value;
                    break;
                case StatusEffects.Energized:
                    _Jonathan_Current_Status = value;
                    break;
                default:
                    _Jonathan_Current_Status = value;
                    break;
            }
        }
    }
}

enum StatusEffects
{
    None = 0,
    Exhaustion = 1,
    Tired = 2,
    Alert = 4,
    Energized = 8
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