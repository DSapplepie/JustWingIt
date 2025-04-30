using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Credit https://www.youtube.com/watch?v=aJ4bVrrF2xY
// Comments and my understanding is mainly based on what the commentator was saying
// plus chatgpt for portions that I didn't understand after watching the video

public class MovingPlatform : MonoBehaviour
{
    //pointA & pointB will be the 2 cube objects that will act as the points in which the platform will travel between.
    [SerializeField] GameObject pointA;
    [SerializeField] GameObject pointB;
    //default speed for how fast the platforms move & default delay for how long the platforms stop as they hit the points before moving again
    [SerializeField] float speed = 10f;
    [SerializeField] float delay = 1f;
    // Just the platform gameobject
    [SerializeField] GameObject platform;
    // the target position the platform is currently moving to
    private Vector3 targetPosition;
    
    // sets initial position of the platform (pointA), initial targetposition (pointB), and calls the coroutine/function MovePlatform
    void Start(){
        platform.transform.position = pointA.transform.position;
        targetPosition = pointB.transform.position;
        StartCoroutine(MovePlatform());
    }

    // Function to move platform between A and B given some delay
    IEnumerator MovePlatform(){
        while (true){
            // Moves platform towards target position
            while ((targetPosition - platform.transform.position).sqrMagnitude > 0.01f){
                platform.transform.position = Vector3.MoveTowards(platform.transform.position, targetPosition, speed * Time.deltaTime);
                // Wait for the next frame
                yield return null;
            }
            // Switches the target position when the platform reaches the target
            targetPosition = targetPosition == pointA.transform.position ? pointB.transform.position : pointA.transform.position;
            // Wait for the default of 1 second (or whatever delay is modified to)
            yield return new WaitForSeconds(delay);
        }
    }
}
