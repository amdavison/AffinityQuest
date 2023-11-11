using UnityEngine;

/// <summary>
/// Concrete maze wall edge.
/// </summary>
public class MazeWall : MazeCellEdge
{
	public Transform wall;

	/// <summary>
    /// Initializes wall instance between current cell and neighbor.
    /// </summary>
    /// <param name="cell">MazeCell current cell</param>
    /// <param name="otherCell">MazeCell neighbor</param>
    /// <param name="direction">MazeDirection direction of wall between cells</param>
	public override void Initialize(MazeCell cell, MazeCell otherCell, MazeDirection direction) {
		base.Initialize (cell, otherCell, direction);
		wall.GetComponent<Renderer>().material = cell.room.settings.wallMaterial;
	}
}