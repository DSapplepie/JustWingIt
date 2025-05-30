using UnityEngine;

public class ToMedium : MonoBehaviour

{
    // A tag to identify the player
    [SerializeField] string playerTag = "Player";


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals(playerTag))
        {
            other.transform.position = new Vector3(211.345f, 3.29171f, 40.9282f); // Teleports the player to the start of the medium level.
        }
    }
}