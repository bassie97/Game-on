using UnityEngine;
using Pathfinding;
using System.Collections;

[RequireComponent (typeof (Rigidbody2D))]
[RequireComponent (typeof (Seeker))]
[RequireComponent(typeof(Animator))]
public class EnemyAI : MonoBehaviour {
    //What to chase
    public Transform target;

    //Path update rate per second
    public float updateRate = 2f;
    private bool m_FacingRight = true;
    private Seeker seeker;
    private Rigidbody2D rb;
	private Animator m_Anim;
    private Vector2 move = new Vector2(5f, 0f);

    //The calculated path
    public Path path;

    //Ai speed
    public float speed = 300f;
    public ForceMode2D fMode;

    //Obstacles info
    private float rBorder;
    private float lBorder;

    //Health
    public int health = 100;
    [HideInInspector]
    public bool pathIsEnded = false;
    public bool hit = false;
    //max distance from AI to waypoint untill next waypoint trigger
    public float nextWaypointDistance = 3;

    //The waypoint we are currently moving towards 
    private int currentWayPoint = 0;
    void Start()
    {
        
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
		m_Anim = GetComponent<Animator>();
		m_Anim.SetBool("Ground", true);
        m_Anim.SetFloat("vSpeed", rb.velocity.y);
        m_Anim.SetFloat("Speed", rb.velocity.y);
        if (target == null)
        {
            //Debug.LogError("NO player found");
            return;
        }
        rb.AddForce(-move * 50);
        StartCoroutine(UpdatePath());
    }

    IEnumerator UpdatePath()
    {
        if(target == null)
        {
            //TODO: Insert a playersearch
            //Debug.LogError("Target(player) not found.");
            yield break;
        }
        seeker.StartPath(transform.position, target.position, OnPathComplete);
        yield return new WaitForSeconds(1f / updateRate);
        StartCoroutine(UpdatePath());
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("AmmoObject"))
        {
            int temp = other.GetComponent<Ammo>().damage;
            Destroy(other.gameObject);
            health -= temp;
            if(health <= 0)
            {
                Destroy(this.gameObject);
            }
        }
        if (other.CompareTag("Obstacle"))
        {
            rb.velocity = Vector2.zero;
            Flip();
            if (m_FacingRight && rBorder == 0)
            {
                rBorder = other.transform.position.x;
            }
            if (!m_FacingRight && lBorder == 0)
            {
                lBorder = other.transform.position.x;
            }
        }
    }
    public void OnPathComplete(Path p)
    {
        //Debug.Log("We got a path, did it have an error?"+ p.error);
        if (!p.error)
        {
            path = p;
            currentWayPoint = 0;
        }
    }
    void Update()
    {
        m_Anim.SetFloat("vSpeed", rb.velocity.y);
        m_Anim.SetFloat("Speed", Mathf.Abs(transform.InverseTransformDirection(rb.velocity).x));

    }
    void FixedUpdate()
    {
        Debug.Log("Left border coor: " + lBorder + "Right corner coor: " + rBorder + "Player coor: " + target.position.x);
        if (target == null)
        {
            return;
        }
        //TODO: Always look at player
        if (path == null)
        {
            return;
        }
        Debug.Log("Enemy velocity: " + rb.velocity);
        if ((target.transform.position.x > lBorder && (Mathf.Abs(target.transform.position.x) < rBorder && rBorder > lBorder)))
        {
            if(rb.velocity.x < 0 && !m_FacingRight)
            {
                Flip();
            }
            else if (rb.velocity.x > 0 && m_FacingRight)
            {
                Flip();
            }
            if (currentWayPoint >= path.vectorPath.Count)
            {
                if (pathIsEnded)
                    return;

                Debug.Log("End of path reached.");
                pathIsEnded = true;
                return;
<<<<<<< HEAD
<<<<<<< HEAD
            
           // Debug.Log("End of path reached.");
            pathIsEnded = true;
            return;
        }
=======
            }
>>>>>>> feature/enemyInteraction
=======
            }
>>>>>>> feature/enemyInteraction

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
        else
        {
            if (m_FacingRight && Mathf.Abs(transform.InverseTransformDirection(rb.velocity).x) < 5f)
            {
                Debug.Log(!hit && m_FacingRight);
                rb.AddForce(-move * 1f);
            }
            if (!m_FacingRight && Mathf.Abs(transform.InverseTransformDirection(rb.velocity).x) < 5f)
            {
                rb.AddForce(move * 1f);
            }
        }
    }
    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}

