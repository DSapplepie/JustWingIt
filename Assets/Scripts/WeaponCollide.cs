using UnityEngine;

public class WeaponCollide : MonoBehaviour
{
    // A tag to identify the player
    [SerializeField] string playerTag = "Player";
    // Platform the player should be able to stand on

    // If the collision is caused by a other.gameObject that has the player tag then set the player's parent to the platform so they move with it.
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