using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Manages game start and restart.
/// </summary>
public class GameManager : MonoBehaviour {

	public Maze mazePrefab;

	public Player playerPrefab;

	private Maze mazeInstance;

	private Player playerInstance;

	List<NPC> npcs = new List<NPC>();

	private void Start () {
		LoadData();
		StartCoroutine(BeginGame());
	}
	
	private void Update () {
		if (Input.GetKeyDown(KeyCode.Space)) {
			RestartGame();
		}
	}

	/// <summary>
    /// Initializes and starts new game.
    /// </summary>
    /// <returns>IEnumerator mazeInstance generation</returns>
	private IEnumerator BeginGame () {
        Camera.main.clearFlags = CameraClearFlags.Skybox;
        Camera.main.rect = new Rect(0f, 0f, 1f, 1f);
		mazeInstance = Instantiate(mazePrefab) as Maze;
		yield return StartCoroutine(mazeInstance.Generate());
		playerInstance = Instantiate(playerPrefab) as Player;
		playerInstance.SetLocation(mazeInstance.GetCell(mazeInstance.RandomCoordinates));
		Camera.main.clearFlags = CameraClearFlags.Depth;
		Camera.main.rect = new Rect(0f, 0f, 0.5f, 0.5f);
    }

	/// <summary>
    /// Stops all coruoutines and starts new game.
    /// </summary>
	private void RestartGame () {
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

			NPC npc = new NPC();

			int.TryParse(row[0], out npc.level);
			int.TryParse(row[1], out npc.npcType);
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