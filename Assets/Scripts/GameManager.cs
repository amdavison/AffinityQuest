using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

/// <summary>
/// Manages game flow.
/// </summary>
public class GameManager : MonoBehaviour
{
	public static GameManager instance;

    public static int interactionCount = 0;

    public static int correctCount = 0;

    public static NPC ActiveNPC { get; set; }

    public static bool isInactive = false;

	private List<NPCData> npcs = new();

    private float timeRemaining = 10f;

    private void Awake()
    {
        if (instance == null)
        {
			instance = this;
			DontDestroyOnLoad(gameObject);
        }
        else
        {
			Destroy(gameObject);
        }

        LoadData();
    }

    private void Update()
    {
        if (isInactive == true && timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }

        if (timeRemaining <= 0)
        {
            ActiveNPC.gameObject.SetActive(true);
            ActiveNPC = null;
            timeRemaining = 10f;
            isInactive = false;
        }
    }

    /// <summary>
    /// Loads csv file data.
    /// </summary>
    private void LoadData()
    {
		TextAsset questdata = Resources.Load<TextAsset>("questdata");

		string[] data = questdata.text.Split('\n');

		for (int i = 1; i < data.Length; i++)
        {
			string[] row = data[i].Split(',');

			NPCData npc = new();

            npc.level = (Level)Enum.Parse(typeof(Level), row[0]);
			npc.npcType = (NPCType)Enum.Parse(typeof(NPCType), row[1]);
			npc.greeting = row[2].Replace("{c}", ",");
			npc.opt1 = row[3];
			npc.opt2 = row[4];
			npc.opt3 = row[5];
			npc.dialog = row[6].Replace("{c}", ",");
			npc.correctDialog = row[7].Replace("{c}", ",");
			npc.incorrectDialog = row[8].Replace("{c}", ",");

			npcs.Add(npc);
		}
    }

	/// <summary>
    /// Shuffles a given list of type T.
    /// </summary>
    /// <typeparam name="T">Specifies the element type of list</typeparam>
    /// <param name="inputList">List<T> list of items of type T to bre shuffled</param>
	public void Shuffle<T>(List<T> inputList)
    {
		for (int i = 0; i < inputList.Count - 1; i++)
        {
			T temp = inputList[i];
			int randomIndex = UnityEngine.Random.Range(i, inputList.Count);
			inputList[i] = inputList[randomIndex];
			inputList[randomIndex] = temp;
        }
    }

	/// <summary>
    /// Creates and returns a Linked List of all NPC data objects of the same type.
    /// </summary>
    /// <param name="npcType">NPCType type of NPC</param>
    /// <returns>LinkedList<NPCData> linked list of NPC data</NPCData></returns>
	public LinkedList<NPCData> NPCs(NPCType npcType)
    {
		List<NPCData> npcsOfType = npcs.Where(npc => npc.npcType == npcType).ToList();
        Shuffle(npcsOfType);
        return new LinkedList<NPCData>(npcsOfType);
    }
}