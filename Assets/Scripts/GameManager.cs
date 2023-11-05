using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// Manages game start and restart.
/// </summary>
public class GameManager : MonoBehaviour {

	public Maze mazePrefab;

	public Player playerPrefab;

	private Maze mazeInstance;

	private Player playerInstance;

	private List<NPCData> npcs = new();

	private FadeInOut fade;

	private void Start () {
		fade = FindFirstObjectByType<FadeInOut>();
		LoadData();
		StartCoroutine(BeginGame());
		
	}
	
	private void Update () {
		if (Input.GetKeyDown(KeyCode.Space)) {
            RestartGame();
		}
	}

	public void QuitApplication()
    {
		Application.Quit();
    }

	/// <summary>
    /// Initializes and starts new game.
    /// </summary>
    /// <returns>IEnumerator mazeInstance generation</returns>
	private IEnumerator BeginGame () {
		mazeInstance = Instantiate(mazePrefab) as Maze;
		yield return StartCoroutine(mazeInstance.Generate());
		playerInstance = Instantiate(playerPrefab) as Player;
		playerInstance.SetLocation(mazeInstance.GetCell(mazeInstance.RandomCoordinates));
		Destroy(Camera.main.GetComponent<AudioListener>());
		fade.FadeOut();
    }

	/// <summary>
    /// Stops all coruoutines and starts new game.
    /// </summary>
	private void RestartGame () {
		fade.FadeIn();
		Camera.main.gameObject.AddComponent<AudioListener>();
		StopAllCoroutines();
		Destroy(mazeInstance.gameObject);
		if (playerInstance != null) {
			Destroy(playerInstance.gameObject);
		}
		StartCoroutine(BeginGame());
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
}