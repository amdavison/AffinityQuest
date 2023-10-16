using UnityEngine;

/// <summary>
/// Manages creation of maze cell instance.
/// </summary>
public class MazeCell : MonoBehaviour {

	public IntVector2 coordinates;

	public MazeRoom room;

	private MazeCellEdge[] edges = new MazeCellEdge[MazeDirections.Count];

	private int initializedEdgeCount;

	public bool IsFullyInitialized {
		get {
			return initializedEdgeCount == MazeDirections.Count;
		}
	}

	public MazeDirection RandomUninitializedDirection {
		get {
			int skips = Random.Range(0, MazeDirections.Count - initializedEdgeCount);
			for (int i = 0; i < MazeDirections.Count; i++) {
				if (edges[i] == null) {
					if (skips == 0) {
						return (MazeDirection)i;
					}
					skips -= 1;
				}
			}
			throw new System.InvalidOperationException("MazeCell has no uninitialized directions left.");
		}
	}

	/// <summary>
    /// Initializes and sets material for MazeCell
    /// </summary>
    /// <param name="room">MazeRoom cell's room</param>
	public void Initialize (MazeRoom room) {
		room.Add(this);
		transform.GetChild(0).GetComponent<Renderer>().material = room.settings.floorMaterial;
	}

	/// <summary>
    /// Gets MazeCellEdge in given direction.
    /// </summary>
    /// <param name="direction">MazeDirection given direction</param>
    /// <returns>MazeCellEdge edge at given direction</returns>
	public MazeCellEdge GetEdge (MazeDirection direction) {
		return edges[(int)direction];
	}

	/// <summary>
    /// Sets and tracks initialized MazeCell edge count.
    /// </summary>
    /// <param name="direction"></param>
    /// <param name="edge"></param>
	public void SetEdge (MazeDirection direction, MazeCellEdge edge) {
		edges[(int)direction] = edge;
		initializedEdgeCount += 1;
	}

	/// <summary>
    /// Shows MazeCell.
    /// </summary>
	public void Show () {
		gameObject.SetActive(true);
	}

	/// <summary>
    /// Hides MazeCell.
    /// </summary>
	public void Hide () {
		gameObject.SetActive(false);
	}

	/// <summary>
    /// Shows room player has entered.
    /// </summary>
	public void OnPlayerEntered () {
		room.Show();
		for (int i = 0; i < edges.Length; i++) {
			edges[i].OnPlayerEntered();
		}
	}
	
	/// <summary>
    /// Hides room player exited.
    /// </summary>
	public void OnPlayerExited () {
		room.Hide();
		for (int i = 0; i < edges.Length; i++) {
			edges[i].OnPlayerExited();
		}
	}
}