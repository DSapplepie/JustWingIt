using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Credit https://www.youtube.com/watch?v=aJ4bVrrF2xY
// Comments and my understanding is mainly based on what the commentator was saying
// plus chatgpt for portions that I didn't understand after watching the video

public class MovingWeapon : MonoBehaviour
{
    [SerializeField] GameObject pointA;
    [SerializeField] GameObject pointB;
    [SerializeField] float speed = 10f;
    [SerializeField] float delay = 1f;
    [SerializeField] GameObject platform;
    private Vector3 targetPosition;
    [SerializeField] float rotationSpeed = 10f;

    // The necessary funnctionality & code is the same as the MovingPlatform script but with the additional code to rotate the weapon.

    void Start()
    {
        platform.transform.position = pointA.transform.position;
        targetPosition = pointB.transform.position;
        StartCoroutine(MovePlatform());
    }

    void Update()
    {
        // Spins the object around the Y axis at 90 degrees per second
        platform.transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }

    // Function to move platform between A and B given some delay
    IEnumerator MovePlatform()
    {
        while (true)
        {
            // Moves platform towards target position
            while ((targetPosition - platform.transform.position).sqrMagnitude > 0.01f)
            {
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