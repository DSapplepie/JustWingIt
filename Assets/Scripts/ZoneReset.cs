using UnityEngine;

public class ZoneReset : MonoBehaviour
{
    // A tag to identify the player
    [SerializeField] string playerTag = "Player";
    //[SerializeField] private Transform spawnPoint;
    // If the collision is caused by a other.gameObject that has the player tag then reset (transform) the player's position to the spawnPoint.
    private void OnTriggerEnter(Collider other){
        if(other.CompareTag(playerTag)){
            //get the last saved checkpoint position from checkpoint manager
            Vector3 checkpointPosition = CheckpointManager.Instance.GetLastCheckpointPosition();

            //move the player to the checkpoint
            other.transform.position = checkpointPosition;
            
            //reset momentum
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = Vector3.zero; // Reset velocity
                rb.angularVelocity = Vector3.zero; // Reset angular velocity
            }
        }
    }
}