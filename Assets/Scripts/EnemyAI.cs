using UnityEngine;
using Pathfinding;
using System.Collections;
using System;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Seeker))]
[RequireComponent(typeof(Animator))]
public class EnemyAI : MonoBehaviour
{
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

    public bool minnion = false;
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
        Physics.IgnoreLayerCollision(9, 10);
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        m_Anim = GetComponent<Animator>();
        m_Anim.SetBool("Ground", true);
        m_Anim.SetFloat("vSpeed", rb.velocity.y);
        m_Anim.SetFloat("Speed", rb.velocity.y);
        rb.AddForce(-move * 50);
        
    }

    IEnumerator UpdatePath()
    {
        try
        {
            target = GameObject.FindWithTag("Player").transform;
            seeker.StartPath(transform.position, target.position, OnPathComplete);
        }
        catch (NullReferenceException ex)
        {
        }
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
            if (health <= 0)
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
        if ((target.transform.position.x > lBorder && (Mathf.Abs(target.transform.position.x) < rBorder && rBorder > lBorder)) || (minnion))
        {
            StartCoroutine(UpdatePath());
            if (rb.velocity.x < 0f && !m_FacingRight || rb.velocity.x > 0f && m_FacingRight)
            {
                Flip();
            }
            if (currentWayPoint >= path.vectorPath.Count)
            {
                if (pathIsEnded)
                    return;
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

