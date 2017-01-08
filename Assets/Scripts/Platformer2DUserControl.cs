using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets._2D
{
    [RequireComponent(typeof (PlatformerCharacter2D))]
    public class Platformer2DUserControl : MonoBehaviour
    {
        private bool p0_jump;
        private bool p1_jump;

        //Variables for the ammo firing
        [SerializeField] private GameObject ammoPrefab;
        // private Transform firePoint;
        private int fireRate = 0;
        private float timeToFire = 0;

        //The CharacterControllers
        PlatformerCharacter2D playerOne;
        PlatformerCharacter2D playerTwo;


        void Start()
        {
           // firePoint = transform.Find("FirePoint");
            if (GameObject.FindGameObjectWithTag("Player") != null)
            {
                playerOne = GameObject.FindGameObjectWithTag("Player").GetComponent<PlatformerCharacter2D>();
            }

            if (GameObject.FindGameObjectWithTag("Player1") != null)
            {
                playerTwo = GameObject.FindGameObjectWithTag("Player1").GetComponent<PlatformerCharacter2D>();
            }
        }


        private void Update()
        {

            if (playerOne != null)
            {
                if (!p0_jump && !playerOne.onLadder)
                {
                    // Read the jump input in Update so button presses aren't missed.
                    if (CrossPlatformInputManager.GetButtonDown("P0_Jump"))
                    {
                        Debug.Log("P0_jump");
                        p0_jump = CrossPlatformInputManager.GetButtonDown("P0_Jump");
                    }
                }
            }

            if (playerTwo != null)
            {
                if (!p1_jump && !playerTwo.onLadder)
                {
                    if (CrossPlatformInputManager.GetButtonDown("P1_Jump"))
                    {
                        Debug.Log("p1 jump");
                        p1_jump = CrossPlatformInputManager.GetButtonDown("P1_Jump");
                    }
                }
            }
            if (fireRate == 0)
            {
                if (Input.GetKeyDown(KeyCode.RightControl) && this.CompareTag("Player") && playerOne.ammoCount > 0)
                {
                    Debug.Log("Are you triggered?");
                    throwAmmo(playerOne);
                    playerOne.ammoCount--;
                }
                if (Input.GetKeyDown(KeyCode.E) && this.CompareTag("Player1") && playerTwo.ammoCount > 0)
                {
                    throwAmmo(playerTwo);
                    playerTwo.ammoCount--;
                }
            }
            else
            {
                /*   if (Input.GetKeyDown(KeyCode.E) && Time.time > timeToFire)
                   {
                       timeToFire = Time.time + 1 / fireRate;
                       throwAmmo();
                   }
               }*/
            }
        }
        void throwAmmo(PlatformerCharacter2D player)
        {
            if (player.m_FacingRight)
            {
                GameObject tmp = (GameObject)Instantiate(ammoPrefab, player.firePoint.position, Quaternion.Euler(-player.firePoint.position.x, -player.firePoint.position.y, -60));
                tmp.GetComponent<Ammo>().Initialize(player.firePoint.right);
            }
            else
            {
                GameObject tmp = (GameObject)Instantiate(ammoPrefab, player.firePoint.position, Quaternion.Euler(player.firePoint.position.x, player.firePoint.position.y, 60));
                tmp.GetComponent<Ammo>().Initialize(-player.firePoint.right);
            }
        }

        private void FixedUpdate()
        {
            float h = 0;

            // Read the inputs and pass input to character controller.
            if (playerOne != null)
            {
                h = CrossPlatformInputManager.GetAxis("P0_Horizontal");
                playerOne.Move(h, p0_jump);
                
            }

            if (playerTwo != null)
            {
                h = CrossPlatformInputManager.GetAxis("P1_Horizontal");
                playerTwo.Move(h, p1_jump);
                
            }
            p1_jump = false;
            p0_jump = false;
        }
    }
}
