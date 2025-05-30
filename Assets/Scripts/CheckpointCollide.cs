using UnityEngine;

public class FinisCheckpointCollide : MonoBehaviour

{
    // A tag to identify the player
    [SerializeField] string playerTag = "Player";


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals(playerTag))
        {
            CheckpointManager.Instance.SetCheckpoint(transform.position); // Set the checkpoint to the finish line position
            Debug.Log("Player has reached the finish line!");
        }
    }
}