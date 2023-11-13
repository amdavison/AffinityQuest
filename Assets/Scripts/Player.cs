using UnityEngine;

/// <summary>
/// Player controller.
/// </summary>
public class Player : MonoBehaviour
{
    public bool canMove = true;

    public static Player instance;

    private MazeManager mazeManager;
    private MazeCell currentCell;
	private MazeDirection currentDirection;
    private DialogText dialog;
    private NPC activeNPC = null;

    private void Start()
    {
        instance = this;
        dialog = FindFirstObjectByType<DialogText>();
        mazeManager = FindFirstObjectByType<MazeManager>();
    }

    /// <summary>
	/// Sets local position of player to new cell position.
	/// </summary>
	/// <param name="cell">MazeCell cell to move to</param>
	public void SetLocation(MazeCell cell) {
		if (currentCell != null) {
			currentCell.OnPlayerExited();
		}
		currentCell = cell;
		transform.localPosition = cell.transform.localPosition;
        currentCell.OnPlayerEntered();
	}

    /// <summary>
    /// Moves player in given direction.
    /// </summary>
    /// <param name="direction">MazeDirection direction to move player</param>
    private void Move(MazeDirection direction)
    {
        MazeCellEdge edge = currentCell.GetEdge(direction);
        if (edge is MazePassage)
        {
            SetLocation(edge.otherCell);
        }
    }

    /// <summary>
    /// Sets locale rotation of player to turn to specified direction.
    /// </summary>
    /// <param name="direction">MazeDirection direction for player to face</param>
    private void Look(MazeDirection direction) {
		transform.localRotation = direction.ToRotation();
        currentDirection = direction;
	}

    private void Update()
    {
        if (canMove == true)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Move(currentDirection);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                Move(currentDirection.GetOpposite());
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                Look(currentDirection.GetNextCounterclockwise());
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                Look(currentDirection.GetNextClockwise());
            }
        }
        if (activeNPC != null && Input.GetKeyDown(KeyCode.Space))
        {
            canMove = false;
            AudioManager.instance.PlaySFX(AudioManager.instance.npc);
            dialog.StartInteraction(activeNPC.GetNPCData());
        }
    }

    /// <summary>
    /// Event handler when trigger is entered.
    /// </summary>
    /// <param name="other">Collider other game object collider</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Door"))
        {
            AudioManager.instance.PlaySFX(AudioManager.instance.openDoor);
        }
        else if (other.CompareTag("NPC"))
        {
            GameManager.ActiveNPC = activeNPC = other.GetComponent<NPC>();
            dialog.ShowHint();
        }
        else if (other.CompareTag("Portal"))
        {
            mazeManager.LevelComplete();
        }
    }

    /// <summary>
    /// Event handler when trigger is exited.
    /// </summary>
    /// <param name="other">Collider other game object collider</param>
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Door"))
        {
            AudioManager.instance.PlaySFX(AudioManager.instance.closeDoor);
        }
        else if (other.CompareTag("NPC"))
        {
            activeNPC = null;
            dialog.hintText.gameObject.SetActive(false);
        }
    }
}