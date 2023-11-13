using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public NPCType npcType;

    private LinkedList<NPCData> dialogs;
    private int startCount;

    public NPCData Data { get { return dialogs.Count > 0 ? dialogs.First.Value : null; } }

    void Start()
    {
        dialogs = GameManager.instance.NPCs(npcType);
        startCount = dialogs.Count;
    }

    /// <summary>
    /// Retrieves NPCData from Linked List.
    /// </summary>
    /// <returns>NPCData data at head of Linked List</returns>
    public NPCData GetNPCData()
    {
        NPCData data = Data;
        if (data != null)
        {
            //data = dialogs.First.Value;
            dialogs.RemoveFirst();
            if (dialogs.Count == startCount - 1) { GameManager.interactionCount++; }
        }
        return data;
    }
}
