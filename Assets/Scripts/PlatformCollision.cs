using UnityEngine;
//Credit : https://www.youtube.com/watch?v=s6chmaGuDFY
public class PlatformCollision : MonoBehaviour
{
    [SerializeField] string playerTag = "Player";
    [SerializeField] Transform platform;

    private void OnTriggerEnter(Collider other){
        if(other.gameObject.tag.Equals(playerTag)){
            other.gameObject.transform.parent = platform;
        }
    }
    private void OnTriggerExit(Collider other){
        if(other.gameObject.tag.Equals(playerTag)){
            other.gameObject.transform.parent = null;
        }
    }
}
