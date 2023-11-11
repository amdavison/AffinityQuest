using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages creation and teardown of maze.
/// </summary>
public class MazeManager : MonoBehaviour
{
	public Maze mazePrefab;

	public Player playerPrefab;

	public GameObject portalPrefab;

	private Maze mazeInstance;

	private Player playerInstance;

	private GameObject portalInstance;

	public GameObject Portal { get; }

	private FadeInOut fade;

	private void Start()
	{
		fade = FindFirstObjectByType<FadeInOut>();
		StartCoroutine(BeginGame());

	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			LevelComplete();
		}
	}

	public void LevelComplete()
    {
		if (GameManager.level == Level.Dark)
        {
			RestartGame();
			GameManager.level = Level.Light;
        }
        else
        {
			SceneChanger.instance.LoadScene("Finale");
        }
    }

	/// <summary>
	/// Initializes and starts new game.
	/// </summary>
	/// <returns>IEnumerator mazeInstance generation</returns>
	private IEnumerator BeginGame()
	{
		mazeInstance = Instantiate(mazePrefab) as Maze;
		yield return StartCoroutine(mazeInstance.Generate());
		playerInstance = Instantiate(playerPrefab) as Player;
		//portalInstance = Instantiate(portalPrefab);
		MazeCell initialCell = mazeInstance.GetCell(mazeInstance.RandomCoordinates);
		//portalInstance.transform.localPosition = initialCell.transform.localPosition;
		//portalInstance.SetActive(false);
		playerInstance.SetLocation(initialCell);
		SpawnNPCs();
		Destroy(Camera.main.GetComponent<AudioListener>());
		fade.FadeOut();
		AudioManager.instance.PlayBackground(AudioManager.instance.gamePlay);
	}

	/// <summary>
	/// Stops all coruoutines and starts new game.
	/// </summary>
	private void RestartGame()
	{
		fade.FadeIn();
		AudioManager.instance.PlayBackground(AudioManager.instance.transition);
		Camera.main.gameObject.AddComponent<AudioListener>();
		StopAllCoroutines();
		Destroy(mazeInstance.gameObject);
		if (playerInstance != null)
		{
			Destroy(playerInstance.gameObject);
		}
		StartCoroutine(BeginGame());
	}

	/// <summary>
    /// Spawns NPC objects, based on level, at random maze coordinates.
    /// </summary>
	public void SpawnNPCs()
    {
		List<GameObject> npcPrefabs = GameManager.instance.NPCs();
		foreach (GameObject npc in npcPrefabs)
        {
			MazeCell spawnLocation = mazeInstance.GetCell(mazeInstance.RandomCoordinates);
			GameObject npcInstance = Instantiate(npc);
			npcInstance.transform.localPosition = spawnLocation.transform.localPosition;
        }
    }
}
