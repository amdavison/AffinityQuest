using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public NPCType npcType;

    private LinkedList<NPCData> dialogs;

    private int startCount;

    void Start()
    {
        dialogs = GameManager.instance.NPCs(npcType);
        startCount = dialogs.Count;
        Debug.Log("Dialogs size should be 3: " + dialogs.Count);
        foreach (NPCData npc in dialogs)
        {
            Debug.Log("NPCType: " + npc.npcType + ", Greeting: " + npc.greeting + ", Options: " + npc.opt1 + ", " + npc.opt2 + ", " + npc.opt3);
        }
    }

    /// <summary>
    /// Retrieves NPCData from Linked List.
    /// </summary>
    /// <returns>NPCData data at head of Linked List</returns>
    public NPCData GetNPCData()
    {
        NPCData data = dialogs.First.Value;
        dialogs.RemoveFirst();
        if (dialogs.Count == startCount - 1) { GameManager.interactionCount++; }
        return data;
    }
}
