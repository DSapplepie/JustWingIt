using UnityEngine;

public class FinishLine : MonoBehaviour
{
    // A tag to identify the player
    [SerializeField] string playerTag = "Player";
    [SerializeField] private Transform spawnPoint;
    // If the collision is caused by a other.gameObject that has the player tag then reset (transform) the player's position to the spawnPoint.
    private void OnTriggerEnter(Collider other){
        if(other.gameObject.tag.Equals(playerTag)){
            other.transform.position = spawnPoint.position;
        }
    }
}