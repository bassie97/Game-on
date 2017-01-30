using UnityEngine;
using System.Collections;
using Pathfinding;
using System;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Seeker))]
[RequireComponent(typeof(Animator))]
public class BossAi : MonoBehaviour
{
    //What to chase
    public Transform target;

    [SerializeField]
    GameObject enemyPrefab;

    //Path update rate per second
    private bool shouldSpawn = false; 
    public float updateRate = 2f;
    private bool m_FacingRight = true;
    private Seeker seeker;
    private Rigidbody2D rb;
    private Animator m_Anim;
    private Vector2 move = new Vector2(5f, 0f);

    //The calculated path
    public Path path;

    //Ai speed
    public float speed = 150f;
    public ForceMode2D fMode;


    //Obstacles info
    private float rBorder;
    private float lBorder;

    //Health
    public int health = 100;
    int orgHp = 0;
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
        StartCoroutine(UpdatePath());
    }

    IEnumerator UpdatePath()
    {
        try
        {
            target = GameObject.FindWithTag("Player").transform;
            seeker.StartPath(transform.position, target.position, OnPathComplete);
            Debug.Log("Enemy location:" + transform.position + "target location:" + target.position);
        }
        catch (NullReferenceException ex)
        {
            Debug.Log(ex);
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
                ChangeToScene(9);
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
        Debug.Log("We got a path, did it have an error?" + p.error);
        if (!p.error)
        {
            path = p;
            currentWayPoint = 0;
        }
    }

    public void ChangeToScene(int sceneToChangeTo)
    {
        SceneManager.LoadScene(sceneToChangeTo);
    }

    void Update()
    {
        m_Anim.SetFloat("vSpeed", rb.velocity.y);
        m_Anim.SetFloat("Speed", Mathf.Abs(transform.InverseTransformDirection(rb.velocity).x));
       
    }
    void FixedUpdate()
    {
        //TODO: Always look at player
        if (path == null)
        {
            return;
        }
        Spawn();
        Debug.Log("Enemy velocity: " + rb.velocity);
        // && (Mathf.Abs(target.transform.position.x) < rBorder && rBorder > lBorder)))
        GameObject temp = GameObject.FindWithTag("Door");
        if (((target.transform.position.x + 4.42/*(Compensation for Door object offset */) > temp.transform.position.x))
        {
            Debug.Log("Start the chase?");
            if (rb.velocity.x < 0f && !m_FacingRight)
            {
                Flip();
            }
            else if (rb.velocity.x > 0f && m_FacingRight)
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
            }

            pathIsEnded = false;

            //Find direction to next waypoint
            Vector3 dir = (path.vectorPath[currentWayPoint] - transform.position).normalized;
            Debug.Log("wtf is dir?:" + dir);
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
    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    private void Spawn()
    { 
        if(orgHp == 0)
        {
            orgHp = health;
        }
        if (health <= orgHp/2 && !shouldSpawn)
        {
            Vector3 temp = transform.position;
            temp -= new Vector3(-2f, 0, 0);
            for (int i = 0; i < 2; i++)
            {
            GameObject something = (GameObject) Instantiate(enemyPrefab, temp, Quaternion.identity);
                something.GetComponent<EnemyAI>().minnion = true;
                something.transform.localScale -= new Vector3(1f, 1f, 0);
            }
            shouldSpawn = true;
        }
    }
}
