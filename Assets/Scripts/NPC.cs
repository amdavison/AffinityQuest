using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public NPCType npcType;

    private LinkedList<NPCData> dialogs;
    private int startCount;
    private bool isInactive = false;
    private bool timerNeeded = false;
    private GameObject orb;
    private GameObject marker;
    private BoxCollider boxCollider;
    private float timeRemaining = 5f;

    public NPCData Data { get { return dialogs.Count > 0 ? dialogs.First.Value : null; } }

    public bool StartTimer { set { timerNeeded = value; } }

    void Start()
    {
        dialogs = GameManager.instance.NPCs(npcType);
        startCount = dialogs.Count;
        orb = transform.GetChild(1).gameObject;
        marker = transform.GetChild(2).gameObject;
        boxCollider = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        if (timerNeeded)
        {
            if (isInactive && timeRemaining > 0 && Data != null)
                    {
                        timeRemaining -= Time.deltaTime;
                    }

            if (timeRemaining <= 0)
            {
                Activate();
                timeRemaining = 5f;
                timerNeeded = false;
            }
        }
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
            dialogs.RemoveFirst();
            if (dialogs.Count == startCount - 1) { GameManager.interactionCount++; }
        }
        return data;
    }

    /// <summary>
    /// Deactivates NPC object.
    /// </summary>
    public void Deactivate()
    {
        orb.SetActive(false);
        marker.SetActive(false);
        boxCollider.enabled = false;
        isInactive = true;
    }

    /// <summary>
    /// Activates NPC object.
    /// </summary>
    public void Activate()
    {
        orb.SetActive(true);
        marker.SetActive(true);
        boxCollider.enabled = true;
        isInactive = false;
    }
}
