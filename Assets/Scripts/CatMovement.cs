using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatMovement : MonoBehaviour {

    
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

    public int timesTouched;

    private bool moveRand = false;

    public Animator catAnimator;

    private GameObject objectToFollow;
    



    void Start()
    {
        objectToFollow = GameObject.FindGameObjectWithTag("MainCamera");
        journeyLength = 0;
        //catAnimator.GetComponent<Animator>();
    }

    // Follows the target position like with a spring
    void Update()
    {
        if (objectToFollow != null)
        {
            if (Vector3.Distance(transform.position, objectToFollow.transform.position) > stopDistance)
            {
                catAnimator.SetBool("IsRunning", true);

                Vector3 followFloor = objectToFollow.transform.position;
                followFloor.y = transform.position.y;

                transform.position = Vector3.MoveTowards(transform.position, followFloor, speed * Time.deltaTime);
            }
            else
            {
                catAnimator.SetBool("IsRunning", false);
            }
            



            // Always look at the player 
            var lookPos = objectToFollow.transform.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);

            


        }

        
        //if (journeyLength > 0)
        //{
        //    // Distance moved = time * speed.
        //    float distCovered = (Time.time - startTime) * speed;

        //    // Fraction of journey completed = current distance divided by total distance.
        //    float fracJourney = distCovered / journeyLength;

        //    // Set our position as a fraction of the distance between the markers.
        //    transform.position = Vector3.Lerp(startMarker.position, endMarker, fracJourney);

        //    if (fracJourney < 0.1)  //just at the beginning of the journey
        //    {
        //        var lookPos = endMarker - transform.position;
        //        lookPos.y = 0;
        //        var rotation = Quaternion.LookRotation(lookPos);
        //        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
        //    }
        //    try
        //    {
        //        if (Vector3.Distance(startMarker.position, endMarker) < 0.1)
        //        {
        //            catAnimator.SetBool("IsRunning", false);
        //        }
        //    }
        //    catch (System.Exception e)
        //    {
        //        Debug.Log("Cannt kep runniign");
        //    }

        //}

        if (moveRand)
        {
            moveRand = false;

            // Set our position as a fraction of the distance between the markers.
            transform.position = Vector3.Lerp(startMarker.position, endMarker, 1);

        }
    }
    //public void StartMove(Transform startPosition, Vector3 endPosition)
    //{
    //    try
    //    {
    //        catAnimator.SetBool("IsRunning", true);
    //    }
    //    catch(System.Exception e)
    //    {
    //        Debug.Log("Ughh stupid");
    //    }
        
    //    startMarker = startPosition;

    //    endMarker = endPosition;
    //    // Keep a note of the time the movement started.
    //    startTime = Time.time;

    //    // Calculate the journey length.
    //    journeyLength = Vector3.Distance(startMarker.position, endMarker);
    //}


    public void GoTo(Transform startPosition, Vector3 endPosition)
    {
        startMarker = startPosition;

        endMarker = endPosition;
        moveRand = true;
    }


}
