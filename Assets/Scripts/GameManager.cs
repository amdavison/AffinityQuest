using UnityEngine;
using System.Collections.Generic;
using System;

/// <summary>
/// Manages game flow.
/// </summary>
public class GameManager : MonoBehaviour
{
	public static GameManager instance;

	private List<NPCData> npcs = new();

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
    }

    private void Start () { LoadData(); }

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
}