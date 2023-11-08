using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public NPCType npcType;

    private LinkedList<NPCData> dialogs;

    void Start()
    {
        dialogs = GameManager.instance.NPCs(npcType);
        Debug.Log("Dialogs size should be 3: " + dialogs.Count);
        foreach (NPCData npc in dialogs)
        {
            Debug.Log("NPCType: " + npc.npcType + ", Greeting: " + npc.greeting + ", Options: " + npc.opt1 + ", " + npc.opt2 + ", " + npc.opt3);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
