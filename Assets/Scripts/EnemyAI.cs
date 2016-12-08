using UnityEngine;
using Pathfinding;
using System.Collections;

[RequireComponent (typeof (Rigidbody2D))]
[RequireComponent (typeof (Seeker))]
public class EnemyAI : MonoBehaviour {
    //What to chase
    public Transform target;

    //Path update rate per second
    public float updateRate = 2f;

    private Seeker seeker;
    private Rigidbody2D rb;

    //The calculated path
    public Path path;

    //Ai speed
    public float speed = 300f;
    public ForceMode2D fMode;

    [HideInInspector]
    public bool pathIsEnded = false;

    //max distance from AI to waypoint untill next waypoint trigger
    public float nextWaypointDistance = 3;

    //The waypoint we are currently moving towards 
    private int currentWayPoint = 0;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        if(target == null)
        {
            Debug.LogError("NO player found");
            return;
        }
        StartCoroutine(UpdatePath());
    }

    IEnumerator UpdatePath()
    {
        if(target == null)
        {
            //TODO: Insert a playersearch
            return false;
        }
        seeker.StartPath(transform.position, target.position, OnPathComplete);
        yield return new WaitForSeconds(1f / updateRate);
        StartCoroutine(UpdatePath());
    }

    public void OnPathComplete(Path p)
    {
        Debug.Log("We got a path, did it have an error?"+ p.error);
        if (!p.error)
        {
            path = p;
            currentWayPoint = 0;
        }
    }

    void FixedUpdate()
    {
        if(target == null)
        {
            return;
        }
         //TODO: Always look at player
         if(path == null)
        {
            return;
        }

         if(currentWayPoint >= path.vectorPath.Count)
        {
            if (pathIsEnded)
                return;
            
            Debug.Log("End of path reached.");
            pathIsEnded = true;
            return;
        }

        pathIsEnded = false;

        //Find direction to next waypoint
        Vector3 dir = (path.vectorPath[currentWayPoint] - transform.position).normalized;
        dir *= speed * Time.fixedDeltaTime;

        //Move the AI
        rb.AddForce(dir, fMode);
        float dist = Vector3.Distance(transform.position, path.vectorPath[currentWayPoint]);
        if (dist < nextWaypointDistance)
        {
            currentWayPoint++;
            return;
        }

        
    }
}

