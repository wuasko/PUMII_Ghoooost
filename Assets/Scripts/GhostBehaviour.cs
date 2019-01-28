using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBehaviour : MonoBehaviour {

    // Transforms to act as start and end markers for the journey.
    public Transform startMarker;
    public Vector3 endMarker;



    // Movement speed in units/sec.
    public float speed = 0.05F;
    private float rotationSpeed = 10f;
    public float stopDistance = 1f;

    // Time when the movement started.
    private float startTime;

    // Total distance between the markers.
    private float journeyLength;

    public int timesTouched, timesHeHit;

    private bool moveRand = false;

    public Animator ghostAnimator;
    private GameObject objectToFollow;

    float counter = 0f;
    public float attackRate = 5;

    // Use this for initialization
    void Start () {
        objectToFollow = GameObject.FindGameObjectWithTag("MainCamera");
        journeyLength = 0;
        timesTouched = 0;
        timesHeHit = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (objectToFollow != null)
        {
            // Too far to attack?
            if (Vector3.Distance(transform.position, objectToFollow.transform.position) > stopDistance)
            {
                counter = 0f;
                ghostAnimator.SetBool("isRunning", true);
                ghostAnimator.SetBool("isAttacking", false);

                Vector3 followFloor = objectToFollow.transform.position;
                followFloor.y = transform.position.y;

                transform.position = Vector3.MoveTowards(transform.position, followFloor, speed * Time.deltaTime);
            }
            else
            {
                ghostAnimator.SetBool("isRunning", false);
                counter += Time.deltaTime;
                // time to hit
                if(counter > attackRate)
                {
                    ghostAnimator.SetBool("isAttacking", true);
                    timesHeHit++;
                    counter = 0;
                }
            }

            // Always look at the player 
            var lookPos = objectToFollow.transform.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
        }

        if (moveRand)
        {
            moveRand = false;

            // Set our position as a fraction of the distance between the markers.
            transform.position = Vector3.Lerp(startMarker.position, endMarker, 1);

        }



    }
    

    public void GoTo(Transform startPosition, Vector3 endPosition)
    {
        startMarker = startPosition;

        endMarker = endPosition;
        moveRand = true;
    }

}
