using UnityEngine;

//Credit : https://www.youtube.com/watch?v=s6chmaGuDFY
// Comments and my understanding is mainly based on what the commentator was saying
// plus chatgpt for portions that I didn't understand after watching the video


public class PlatformCollision : MonoBehaviour
{
    // A tag to identify the player
    [SerializeField] string playerTag = "Player";
    // Platform the player should be able to stand on
    [SerializeField] Transform platform;

    // If the collision is caused by a other.gameObject that has the player tag then set the player's parent to the platform so they move with it.
    private void OnTriggerEnter(Collider other){
        if(other.gameObject.tag.Equals(playerTag)){
            other.gameObject.transform.parent = platform;
        }
    }

    // If the object that caused the collision exits the collider & is the player then set the player's parents to null to detach it.
    private void OnTriggerExit(Collider other){
        if(other.gameObject.tag.Equals(playerTag)){
            other.gameObject.transform.parent = null;
        }
    }
}
