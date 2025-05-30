using UnityEngine;

public class FinishLine : MonoBehaviour
{
    // A tag to identify the player
    [SerializeField] string playerTag = "Player";


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals(playerTag))
        {
            CheckpointManager.Instance.SetCheckpoint(Vector3.zero); // Set the checkpoint to the starting point rather than setting the checkpoint to the finish line.
            Debug.Log("Player has reached the finish line!");
            other.transform.position = Vector3.zero; //Reset the player back to the very beginning
        }
    }
}