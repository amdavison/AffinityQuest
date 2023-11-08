using UnityEngine;

/// <summary>
/// Maze Direction enum.
/// </summary>
public enum MazeDirection {
	North,
	East,
	South,
	West
}

/// <summary>
/// Static maze directions object.
/// </summary>
public static class MazeDirections {

	public const int Count = 4;

	public static MazeDirection RandomValue {
		get {
			return (MazeDirection)Random.Range(0, Count);
		}
	}

	private static MazeDirection[] opposites = {
		MazeDirection.South,
		MazeDirection.West,
		MazeDirection.North,
		MazeDirection.East
	};

	/// <summary>
    /// Calculates opposite direction from given direction.
    /// </summary>
    /// <param name="direction">MazeDirection current direction</param>
    /// <returns>MazeDirection direction opposite from given direction</returns>
	public static MazeDirection GetOpposite(this MazeDirection direction) {
		return opposites[(int)direction];
	}

	/// <summary>
    /// Calculates next clockwise direction from given direction.
    /// </summary>
    /// <param name="direction">MazeDirection current direction</param>
    /// <returns>MazeDirection direction clockwise from given direction</returns>
	public static MazeDirection GetNextClockwise(this MazeDirection direction) {
		return (MazeDirection)(((int)direction + 1) % Count);
	}

	/// <summary>
    /// Calculates next counterclockwise direction from given direction.
    /// </summary>
    /// <param name="direction">MazeDirection current direction</param>
    /// <returns>MazeDirection direction counterclockwise from given direction</returns>
	public static MazeDirection GetNextCounterclockwise(this MazeDirection direction) {
		return (MazeDirection)(((int)direction + Count - 1) % Count);
	}
	
	private static IntVector2[] vectors = {
		new IntVector2(0, 1),
		new IntVector2(1, 0),
		new IntVector2(0, -1),
		new IntVector2(-1, 0)
	};
	
	/// <summary>
    /// Calculates required IntVector2 from given direction.
    /// </summary>
    /// <param name="direction">MazeDirection current direction</param>
    /// <returns>IntVector2 vector</returns>
	public static IntVector2 ToIntVector2(this MazeDirection direction) {
		return vectors[(int)direction];
	}

	private static Quaternion[] rotations = {
		Quaternion.identity,
		Quaternion.Euler(0f, 90f, 0f),
		Quaternion.Euler(0f, 180f, 0f),
		Quaternion.Euler(0f, 270f, 0f)
	};
	
	/// <summary>
    /// Calculates required rotation from given direction.
    /// </summary>
    /// <param name="direction">MazeDirection current direction</param>
    /// <returns>Quaternion rotation</returns>
	public static Quaternion ToRotation(this MazeDirection direction) {
		return rotations[(int)direction];
	}
}