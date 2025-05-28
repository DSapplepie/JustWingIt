using UnityEngine;

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

    public void SetCheckpoint(Vector3 newCheckpointposition)
    {
        lastCheckpointPosition = newCheckpointposition;
        Debug.Log("Checkpoint set at: " + lastCheckpointPosition);
    }
    
    public Vector3 GetLastCheckpointPosition()
    {
        Debug.Log($"Last checkpoint position: {lastCheckpointPosition}");
        return lastCheckpointPosition;
    }
}
