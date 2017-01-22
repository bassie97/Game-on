using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
namespace UnityStandardAssets._2D
{
    [RequireComponent(typeof(PlatformerCharacter2D))]
    public class PlatformerControl2 : MonoBehaviour
    {
        public PlatformerCharacter2D playerTwo;
        private bool p1_jump;
        private float climbVelocity;
        public float climbSpeed = 5;
        [SerializeField]
        private GameObject ammoPrefab;
        // private Transform firePoint;
        private int fireRate = 0;
        private float timeToFire = 0;

        void Start()
        {
            if (GameObject.FindGameObjectWithTag("Player1") != null)
            {
                playerTwo = GameObject.FindGameObjectWithTag("Player1").GetComponent<PlatformerCharacter2D>();
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (playerTwo != null)
            {

                if (playerTwo.onLadder)
                {
                    climbVelocity = climbSpeed * Input.GetAxisRaw("P1_Vertical");
                    playerTwo.m_Anim.SetBool("onLadder", playerTwo.onLadder);
                   

                    playerTwo.m_Rigidbody2D.velocity = new Vector2(playerTwo.m_Rigidbody2D.velocity.x, climbVelocity);
                }

                if (!playerTwo.onLadder)
                {
                    playerTwo.m_Anim.SetBool("onLadder", false);
                }

                if (!p1_jump && !playerTwo.onLadder)
                {
                    if (CrossPlatformInputManager.GetButtonDown("P1_Jump"))
                    {
                        p1_jump = CrossPlatformInputManager.GetButtonDown("P1_Jump");
                    }
                }
            }
                if (fireRate == 0)
                {
                if (Input.GetKeyDown(KeyCode.E) && this.CompareTag("Player1") && playerTwo.ammoCount > 0)
                {
                    throwAmmo(playerTwo);
                    playerTwo.ammoCount--;
                }
            }

        }
        void throwAmmo(PlatformerCharacter2D player)
        {
            if (player.m_FacingRight)
            {
                GameObject tmp = (GameObject)Instantiate(ammoPrefab, player.firePoint.position, Quaternion.Euler(-player.firePoint.position.x, -player.firePoint.position.y, -60));
                tmp.tag = "AmmoObject";
                tmp.GetComponent<Ammo>().Initialize(player.firePoint.right);
            }
            else
            {
                GameObject tmp = (GameObject)Instantiate(ammoPrefab, player.firePoint.position, Quaternion.Euler(player.firePoint.position.x, player.firePoint.position.y, 60));
                tmp.tag = "AmmoObject";
                tmp.GetComponent<Ammo>().Initialize(-player.firePoint.right);
            }
        }

        private void FixedUpdate()
        {
            float h = 0;
            if (playerTwo != null)
            {
                h = CrossPlatformInputManager.GetAxis("P1_Horizontal");
                playerTwo.Move(h, p1_jump);

            }
            p1_jump = false;
        }
   }
}