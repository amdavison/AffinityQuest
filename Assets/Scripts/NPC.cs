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
        Debug.Log(dialogs.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
