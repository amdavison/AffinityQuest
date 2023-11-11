/// <summary>
/// Enum that represents game level.
/// </summary>
public enum Level
{
    Dark,
    Light
}

/// <summary>
/// Enum that represents NPC type.
/// </summary>
public enum NPCType
{
    Sad,
    Angry,
    Scared,
    Happy,
    Strong,
    Calm
}

/// <summary>
/// Data class for NPCs.
/// </summary>
public class NPCData
{
    public Level level;
    public NPCType npcType;
    public string greeting;
    public string opt1;
    public string opt2;
    public string opt3;
    public string dialog;
    public string correctDialog;
    public string incorrectDialog;
}
