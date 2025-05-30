using UnityEngine;

// CheckpointManager - other scripts can call this to set and get the last checkpoint position
public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager Instance;
    private Vector3 lastCheckpointPosition;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            transform.SetParent(null);
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Set a new checkpoint position
    // This method can be called by other scripts to update the checkpoint
    public void SetCheckpoint(Vector3 newCheckpointposition)
    {
        lastCheckpointPosition = newCheckpointposition;
        Debug.Log("Checkpoint set at: " + lastCheckpointPosition);
    }

    // Get the last checkpoint position
    // This method can be called by other scripts to retrieve the checkpoint position
    public Vector3 GetLastCheckpointPosition()
    {
        Debug.Log($"Last checkpoint position: {lastCheckpointPosition}");
        return lastCheckpointPosition;
    }
}
