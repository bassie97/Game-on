using UnityEngine;
using System.Collections;
using Pathfinding;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Seeker))]
[RequireComponent(typeof(Animator))]
public class BossAi : MonoBehaviour
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

    [SerializeField]
    public GameObject spawnPrefab;

    //Boss specific stats
    private static int maxClones = 7;
    public GameObject[] clones = new GameObject[maxClones];
    private int cloneCount = 0;
    private float hpTrig;

    //Ai speed
    public float speed = 300f;
    public ForceMode2D fMode;

    //For holding the boss room edge values
    private float rBorder;
    private float lBorder;

    //Health
    public int health = 1000;
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
        rb.gravityScale = 0;
        rb.angularDrag = 0;
        hpTrig = health - 200;
        if (target == null)
        {
            Debug.LogError("NO player found");
            return;
        }
       
        rb.AddForce(-move * 50);
        StartCoroutine(UpdatePath());
    }

    IEnumerator UpdatePath()
    {
        if (target == null)
        {
            //TODO: Insert a playersearch
            Debug.LogError("Target(player) not found.");
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
            if (health <= 0)
            {
                Destroy(this.gameObject);
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
    void Update()
    {
        m_Anim.SetFloat("vSpeed", rb.velocity.y);
        m_Anim.SetFloat("Speed", Mathf.Abs(transform.InverseTransformDirection(rb.velocity).x));

    }
    void FixedUpdate()
    {
        if(cloneCount > 0)
        {
            cCluster();
        }
        if (health < hpTrig && cloneCount < maxClones && this.CompareTag("Boss"))
        {
            Debug.Log("Lengte van array: " + clones.Length + "Inst cond status: " + (clones.Length < maxClones));

            if (isEmpty(clones))
            {
                // Vector3 temp = this.transform.position;
                //temp.x = temp.x + 1f;
                GameObject clone = Instantiate(spawnPrefab);
                clone.gameObject.tag = "clone";
                clone.transform.localScale -= new Vector3(0.2f, 0.2f, 0);
                clones[cloneCount] = clone;
                cloneCount++;
                if (hpTrig > 200){
                hpTrig -= 200;
                }   
                for (int i = 0; i < clones.Length; i++)
                {
                    clones[i].transform.localScale = clone.transform.localScale;
                }

                Debug.Log(cloneCount < maxClones);
            }
        }
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
            if (rb.velocity.x < 0 && !m_FacingRight)
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
    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private bool isEmpty(GameObject[] check)
    {
        GameObject[] temp = check;
        if (temp[maxClones - 1] == null)
        {
            return true;
        }
        else
            /*for(int i = 0; i < temp.Length; i++)
            {
                if (i == temp.Length - 1 && temp[i] == null)
                {
                    return true;
                }
            }*/
            return false;
    }
    private void cCluster()
    {
        for (int i = 0; i < clones.Length; i++)
        {
            if (clones[i] != null)
            {
                Vector3 offset = this.transform.position - clones[i].transform.position;
                Vector3 temp = -offset;
                offset.z = 0;
                float magsqr = offset.sqrMagnitude;
                if (magsqr > 0.0001f)
                    clones[i].GetComponent<Rigidbody2D>().AddForce(2f * offset.normalized / magsqr);
            }
        }
    }

}