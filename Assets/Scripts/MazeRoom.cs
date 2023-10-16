using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Maze room scriptable object.
/// </summary>
public class MazeRoom : ScriptableObject {

	public int settingsIndex;

	public MazeRoomSettings settings;
	
	private List<MazeCell> cells = new List<MazeCell>();

	/// <summary>
    /// Adds room cells to list of cells for room.
    /// </summary>
    /// <param name="cell">MazeCell cell to add to room</param>
	public void Add (MazeCell cell) {
		cell.room = this;
		cells.Add(cell);
	}

	/// <summary>
    /// Assimilates room instance with current room.
    /// </summary>
    /// <param name="room">MazeRoom room to assimilate</param>
	public void Assimilate (MazeRoom room) {
		for (int i = 0; i < room.cells.Count; i++) {
			Add(room.cells[i]);
		}
	}

	/// <summary>
    /// Hides all cells in room.
    /// </summary>
	public void Hide () {
		for (int i = 0; i < cells.Count; i++) {
			cells[i].Hide();
		}
	}
	
	/// <summary>
    /// Shows all cells in room.
    /// </summary>
	public void Show () {
		for (int i = 0; i < cells.Count; i++) {
			cells[i].Show();
		}
	}
}