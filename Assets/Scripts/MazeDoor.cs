using UnityEngine;

/// <summary>
/// Concrete maze door passage object.
/// </summary>
public class MazeDoor : MazePassage {

	private static Quaternion
		normalRotation = Quaternion.Euler(0f, -90f, 0f),
		mirroredRotation = Quaternion.Euler(0f, 90f, 0f);

	public Transform hinge;

	public AudioClip openDoor;

	public AudioClip closeDoor;

	private bool isMirrored;

	private MazeDoor OtherSideOfDoor {
		get {
			return otherCell.GetEdge(direction.GetOpposite()) as MazeDoor;
		}
	}
	
	/// <summary>
    /// Initializes MazeDoor passage.
    /// </summary>
    /// <param name="primary">MazeCell primary cell</param>
    /// <param name="other">MazeCell secondary neighbor</param>
    /// <param name="direction">MazeDirection direction of door instance for primary cell</param>
	public override void Initialize (MazeCell primary, MazeCell other, MazeDirection direction) {
		base.Initialize(primary, other, direction);
		if (OtherSideOfDoor != null) {
			isMirrored = true;
			hinge.localScale = new Vector3(-1f, 1f, 1f);
			Vector3 p = hinge.localPosition;
			p.x = -p.x;
			hinge.localPosition = p;
			Destroy(gameObject.GetComponent<Collider>());
		}
		for (int i = 0; i < transform.childCount; i++) {
			Transform child = transform.GetChild(i);
			if (child != hinge) {
				child.GetComponent<Renderer>().material = cell.room.settings.wallMaterial;
			}
		}
	}

	/// <summary>
    /// Opens door and shows neighboring room.
    /// </summary>
	public override void OnPlayerEntered () {
		OtherSideOfDoor.hinge.localRotation = hinge.localRotation = isMirrored ? mirroredRotation : normalRotation;
		OtherSideOfDoor.cell.room.Show();
		
	}
	
	/// <summary>
    /// Closes door.
    /// </summary>
	public override void OnPlayerExited () {
		OtherSideOfDoor.hinge.localRotation = hinge.localRotation = Quaternion.identity;
		//OtherSideOfDoor.cell.room.Hide();
	}

	/// <summary>
    /// Handles playing of open door SFX when player collider enters trigger.
    /// </summary>
    /// <param name="other">Collider collider component of other game object</param>
	private void OnTriggerEnter(Collider other)
    {
		if (other.gameObject.CompareTag("Player"))
        {
			AudioManager.instance.PlaySFX(openDoor);
		}
	}

	/// <summary>
    /// Handles playing of close door SFX when player collider exits trigger.
    /// </summary>
    /// <param name="other">Collider collider component of other game object</param>
	private void OnTriggerExit(Collider other)
    {
		if (other.gameObject.CompareTag("Player"))
		{
			AudioManager.instance.PlaySFX(closeDoor);
		}
    }
}