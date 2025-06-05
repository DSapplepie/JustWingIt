using UnityEditor.Search;
using UnityEngine;

public class FinishLine2 : MonoBehaviour
{
    // A tag to identify the player
    [SerializeField] string playerTag = "Player";
    [SerializeField] GameObject gameEndScreen;
    public bool finished = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals(playerTag))
        {
            CheckpointManager.Instance.SetCheckpoint(Vector3.zero); // Set the checkpoint to the starting point rather than setting the checkpoint to the finish line.
            Debug.Log("Player has reached the finish line!");
            finished = true;
            other.transform.position = Vector3.zero; //Reset the player back to the very beginning
            if (gameEndScreen != null)
            {
                Time.timeScale = 0;
                gameEndScreen.SetActive(true);
            }
            else
            {
                Debug.LogWarning("NotWorkingFriend!");
            }
        }
    }
}