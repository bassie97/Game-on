using System;
using UnityEngine;

namespace UnityStandardAssets._2D
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlatformerCharacter2D : MonoBehaviour
    {
        [SerializeField] private float m_MaxSpeed = 5f;                    // The fastest the player can travel in the x axis.
        [SerializeField] private float m_JumpForce = 400f;                  // Amount of force added when the player jumps.
        [SerializeField] private bool m_AirControl = false;                 // Whether or not a player can steer while jumping;
        [SerializeField] private LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character
		[SerializeField] private int score = 0;
		[SerializeField] private GameObject ammoPrefab;

        public int ammoCount = 10;
        private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
        const float k_GroundedRadius = .1f; // Radius of the overlap circle to determine if grounded
        private bool m_Grounded;            // Whether or not the player is grounded.
        public Animator m_Anim;            // Reference to the player's animator component.
        public Rigidbody2D m_Rigidbody2D;
        public bool m_FacingRight = true;  // For determining which way the player is currently facing.
		public Transform firePoint;
        private int fireRate = 10;
        private float timeToFire = 0;


        public bool onLadder;

        private float gravityStore;

        public int curHealth;
        public int maxhealth = 5;

        private void Awake()
        {
            
        }

        private void Start()
        {
            GameController.Instance.subscribeScriptToGameEventUpdates(this);

            // Setting up references.
            firePoint = transform.Find("FirePoint");
            m_GroundCheck = transform.Find("GroundCheck");
            m_Anim = GetComponent<Animator>();
            m_Rigidbody2D = GetComponent<Rigidbody2D>();
            m_Rigidbody2D.gravityScale = 0.5f;
            gravityStore = m_Rigidbody2D.gravityScale;
            curHealth = maxhealth;
        }

        void OnDestroy()
        {
            Debug.Log(this);
            GameController.Instance.deSubscribeScriptToGameEventUpdates(this);
        }

        //this method will be automatically called whenever the player passes an important event in the game;
        void gameEventUpdated()
        {
            Debug.Log("Our method is called");
            //if player finishes event 5, let something happennn
            if (GameController.Instance.gameEventID == 2)
            {
                //do something
                Debug.Log("Do a little dance");
            }

        }

        private void OnTriggerEnter2D(Collider2D other){
			if(other.CompareTag("PickUp")){
				score = score + 5;
				Destroy (other.gameObject);
			}
		}

		private void Update(){
            if(m_Grounded == false && onLadder == false)
            {
                m_Rigidbody2D.gravityScale = 3f;
            }else if(m_GroundCheck == true)
            {
                m_Rigidbody2D.gravityScale = gravityStore;
            }
            /*
            if (this.transform.position.y <= -4)
            {
                Die();
            }
            */
        }

        private void FixedUpdate()
        {
            m_Grounded = false;

            // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
            // This can be done using layers instead but Sample Assets will not overwrite your project settings.
            Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                    m_Grounded = true;
            }
            m_Anim.SetBool("Ground", m_Grounded);

            // Set the vertical animation
            m_Anim.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);

            if (curHealth > maxhealth)
            {
                curHealth = maxhealth;
            }
            if (curHealth <= 0)
            {
                Die();
            }
        }


        public void Move(float move , bool jump)
        {
           
            //only control the player if grounded or airControl is turned on
            if (m_Grounded || m_AirControl)
            {
                // The Speed animator parameter is set to the absolute value of the horizontal input.
                m_Anim.SetFloat("Speed", Mathf.Abs(move));

                // Move the character
                m_Rigidbody2D.velocity = new Vector2(move*m_MaxSpeed, m_Rigidbody2D.velocity.y);

                // If the input is moving the player right and the player is facing left...
                if (move > 0 && !m_FacingRight)
                {
                    // ... flip the player.
                    Flip();
                }
                    // Otherwise if the input is moving the player left and the player is facing right...
                else if (move < 0 && m_FacingRight)
                {
                    // ... flip the player.
                    Flip();
                }
            }

            // If the player should jump...
            if (m_Grounded && jump && m_Anim.GetBool("Ground"))
            {
                
                // Add a vertical force to the player.
                m_Grounded = false;
                m_Anim.SetBool("Ground", false);
                m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
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

        /*
        void throwAmmo()
        {
            ammoCount--;
            if (m_FacingRight)
            {
                GameObject tmp = (GameObject)Instantiate(ammoPrefab, firePoint.position, Quaternion.Euler(-firePoint.position.x, -firePoint.position.y, -60));
                tmp.GetComponent<Ammo>().Initialize(firePoint.right);
            }
            else
            {
                GameObject tmp = (GameObject)Instantiate(ammoPrefab, firePoint.position, Quaternion.Euler(firePoint.position.x, firePoint.position.y, 60));
                tmp.GetComponent<Ammo>().Initialize(-firePoint.right);
            }
        }
        */
    void Die()
        {
            Application.LoadLevel(Application.loadedLevel);
        }
    }
}