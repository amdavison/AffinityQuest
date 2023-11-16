using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Manages creation of maze instance.
/// </summary>
public class Maze : MonoBehaviour
{
	public IntVector2 size;
	public MazeCell cellPrefab;
	public float generationStepDelay = 0f;
	public MazePassage passagePrefab;
	public MazeDoor doorPrefab;
	[Range(0f, 1f)] public float doorProbability;
	public MazeWall[] wallPrefabs;
	public MazeRoomSettings[] roomSettings;

	private MazeCell[,] cells;
	private readonly List<MazeRoom> rooms = new();

    public IntVector2 RandomCoordinates
    {
		get {
			return new IntVector2(Random.Range(0, size.x), Random.Range(0, size.z));
		}
	}

    /// <summary>
    /// Verifies if given coordinate is within game space.
    /// </summary>
    /// <param name="coordinate">IntVector2 to verify</param>
    /// <returns>bool true if within game space, otherwise false</returns>
    public bool ContainsCoordinates(IntVector2 coordinate)
    {
		return coordinate.x >= 0 && coordinate.x < size.x && coordinate.z >= 0 && coordinate.z < size.z;
	}

    /// <summary>
    /// Retrives MazeCell at given coordinates.
    /// </summary>
    /// <param name="coordinates">IntVector2 coordinates of cell to retrieve</param>
    /// <returns>MazeCell at given coordinates</returns>
    public MazeCell GetCell(IntVector2 coordinates)
    {
		return cells[coordinates.x, coordinates.z];
	}

    /// <summary>
    /// Generates new maze of cells.
    /// </summary>
    /// <returns>IEnumerator wait delay</returns>
    public IEnumerator Generate()
    {
		WaitForSeconds delay = new(generationStepDelay);
		cells = new MazeCell[size.x, size.z];
		List<MazeCell> activeCells = new();
		DoFirstGenerationStep(activeCells);
		while (activeCells.Count > 0) {
			yield return delay;
			DoNextGenerationStep(activeCells);
		}
        for (int i = 0; i < rooms.Count; i++)
        {
            rooms[i].Hide();
        }
    }

    /// <summary>
    /// First generation step to create and add new cell to activeCells.
    /// </summary>
    /// <param name="activeCells">List<MazeCell> list of active cells</param>
    private void DoFirstGenerationStep(List<MazeCell> activeCells)
    {
		MazeCell newCell = CreateCell(RandomCoordinates);
		newCell.Initialize(CreateRoom(-1));
		activeCells.Add(newCell);
	}

    /// <summary>
    /// Secondary generation step to create a passage, wall, or room.
    /// </summary>
    /// <param name="activeCells">List<MazeCell> list of active cells</param>
    private void DoNextGenerationStep(List<MazeCell> activeCells)
    {
		int currentIndex = activeCells.Count - 1;
		MazeCell currentCell = activeCells[currentIndex];
		if (currentCell.IsFullyInitialized) {
			activeCells.RemoveAt(currentIndex);
			return;
		}
		MazeDirection direction = currentCell.RandomUninitializedDirection;
		IntVector2 coordinates = currentCell.coordinates + direction.ToIntVector2();
		if (ContainsCoordinates(coordinates)) {
			MazeCell neighbor = GetCell(coordinates);
			if (neighbor == null) {
				neighbor = CreateCell(coordinates);
				CreatePassage(currentCell, neighbor, direction);
				activeCells.Add(neighbor);
			}
			else if (currentCell.room.settingsIndex == neighbor.room.settingsIndex) {
				CreatePassageInSameRoom(currentCell, neighbor, direction);
			}
			else {
				CreateWall(currentCell, neighbor, direction);
			}
		}
		else {
			CreateWall(currentCell, null, direction);
		}
	}

    /// <summary>
    /// Creates new MazeCell at given coordinates.
    /// </summary>
    /// <param name="coordinates">IntVector2 coordinates of new cell</param>
    /// <returns>MazeCell that was created</returns>
    private MazeCell CreateCell(IntVector2 coordinates)
    {
		MazeCell newCell = Instantiate(cellPrefab);
		cells[coordinates.x, coordinates.z] = newCell;
		newCell.coordinates = coordinates;
		newCell.name = "Maze Cell " + coordinates.x + ", " + coordinates.z;
		newCell.transform.parent = transform;
		newCell.transform.localPosition = new Vector3(coordinates.x - size.x * 0.5f + 0.5f, 0f, coordinates.z - size.z * 0.5f + 0.5f);
		return newCell;
	}

    /// <summary>
    /// Creates MazePassage doorway instance between current cell and neighbor for separate rooms.
    /// </summary>
    /// <param name="cell">MazeCell current cell</param>
    /// <param name="otherCell">MazeCell neighbor</param>
    /// <param name="direction">MazeDirection direction of passage placement</param>
    private void CreatePassage(MazeCell cell, MazeCell otherCell, MazeDirection direction)
    {
		MazePassage prefab = Random.value < doorProbability ? doorPrefab : passagePrefab;
		MazePassage passage = Instantiate(prefab);
		passage.Initialize(cell, otherCell, direction);
		passage = Instantiate(prefab);
		if (passage is MazeDoor) {
			otherCell.Initialize(CreateRoom(cell.room.settingsIndex));
		}
		else {
			otherCell.Initialize(cell.room);
		}
		passage.Initialize(otherCell, cell, direction.GetOpposite());
	}

    /// <summary>
    /// Creates MazePassage instance between current cell and neighbor for same room.
    /// </summary>
    /// <param name="cell">MazeCell current cell</param>
    /// <param name="otherCell">MazeCell neighbor</param>
    /// <param name="direction">MazeDirection direction of passage placement</param>
    private void CreatePassageInSameRoom(MazeCell cell, MazeCell otherCell, MazeDirection direction)
    {
		MazePassage passage = Instantiate(passagePrefab);
		passage.Initialize(cell, otherCell, direction);
		passage = Instantiate(passagePrefab);
		passage.Initialize(otherCell, cell, direction.GetOpposite());
		if (cell.room != otherCell.room) {
			MazeRoom roomToAssimilate = otherCell.room;
			cell.room.Assimilate(roomToAssimilate);
			rooms.Remove(roomToAssimilate);
			Destroy(roomToAssimilate);
		}
	}

    /// <summary>
    /// Creates new MazeWall instance between current cell and neighbor.
    /// </summary>
    /// <param name="cell">MazeCell current cell</param>
    /// <param name="otherCell">MazeCell neighbor</param>
    /// <param name="direction">MazeDirection direction of wall placement</param>
    private void CreateWall(MazeCell cell, MazeCell otherCell, MazeDirection direction)
    {
		MazeWall wall = Instantiate(wallPrefabs[Random.Range(0, wallPrefabs.Length)]);
		wall.Initialize(cell, otherCell, direction);
		if (otherCell != null) {
			wall = Instantiate(wallPrefabs[Random.Range(0, wallPrefabs.Length)]);
			wall.Initialize(otherCell, cell, direction.GetOpposite());
		}
	}

    /// <summary>
    /// Creates new MazeRoom.
    /// </summary>
    /// <param name="indexToExclude">int index of current room</param>
    /// <returns>MazeRoom that was created</returns>
    private MazeRoom CreateRoom(int indexToExclude)
    {
		MazeRoom newRoom = ScriptableObject.CreateInstance<MazeRoom>();
		newRoom.settingsIndex = Random.Range(0, roomSettings.Length);
		if (newRoom.settingsIndex == indexToExclude) {
			newRoom.settingsIndex = (newRoom.settingsIndex + 1) % roomSettings.Length;
		}
		newRoom.settings = roomSettings[newRoom.settingsIndex];
		rooms.Add(newRoom);
		return newRoom;
	}
}