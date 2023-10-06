using UnityEngine;

/// <summary>
/// Abstract cell edge object.
/// </summary>
public abstract class MazeCellEdge : MonoBehaviour {

	public MazeCell cell, otherCell;

	public MazeDirection direction;

	/// <summary>
    /// Initializes and sets MazeCellEdge.
    /// </summary>
    /// <param name="cell">MazeCell current cell</param>
    /// <param name="otherCell">MazeCell neighbor</param>
    /// <param name="direction">MazeDirection direction of edge</param>
	public virtual void Initialize (MazeCell cell, MazeCell otherCell, MazeDirection direction) {
		this.cell = cell;
		this.otherCell = otherCell;
		this.direction = direction;
		cell.SetEdge(direction, this);
		transform.parent = cell.transform;
		transform.localPosition = Vector3.zero;
		transform.localRotation = direction.ToRotation();
	}

	public virtual void OnPlayerEntered () {}

	public virtual void OnPlayerExited () {}
}