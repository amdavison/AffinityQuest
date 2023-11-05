using System.Collections;
using UnityEngine;

/// <summary>
/// Manages creation and teardown of maze.
/// </summary>
public class MazeManager : MonoBehaviour
{
	public Maze mazePrefab;

	public Player playerPrefab;

	public AudioClip gamePlay;

	public AudioClip transition;

	private Maze mazeInstance;

	private Player playerInstance;

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
			RestartGame();
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
		playerInstance.SetLocation(mazeInstance.GetCell(mazeInstance.RandomCoordinates));
		Destroy(Camera.main.GetComponent<AudioListener>());
		fade.FadeOut();
		AudioManager.instance.PlayBackground(gamePlay);
	}

	/// <summary>
	/// Stops all coruoutines and starts new game.
	/// </summary>
	private void RestartGame()
	{
		fade.FadeIn();
		AudioManager.instance.PlayBackground(transition);
		Camera.main.gameObject.AddComponent<AudioListener>();
		StopAllCoroutines();
		Destroy(mazeInstance.gameObject);
		if (playerInstance != null)
		{
			Destroy(playerInstance.gameObject);
		}
		StartCoroutine(BeginGame());
	}
}
