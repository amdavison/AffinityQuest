using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Level
{
    Dark,
    Light
}

public enum NPCType
{
    Sad,
    Angry,
    Scared,
    Happy,
    Strong,
    Calm
}

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
