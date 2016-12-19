using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets._2D
{
    [RequireComponent(typeof (PlatformerCharacter2D))]
    public class Platformer2DUserControl : MonoBehaviour
    {
        private PlatformerCharacter2D m_Character;
        private bool p0_jump;
        private bool p1_jump;

        //The CharacterControllers
        PlatformerCharacter2D playerOne;
        PlatformerCharacter2D playerTwo;


        void Start()
        {
            if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlatformerCharacter2D>() != null)
            {
                playerOne = GameObject.FindGameObjectWithTag("Player").GetComponent<PlatformerCharacter2D>();
            }

            if (GameObject.FindGameObjectWithTag("Player1").GetComponent<PlatformerCharacter2D>() != null)
            {
                playerTwo = GameObject.FindGameObjectWithTag("Player1").GetComponent<PlatformerCharacter2D>();
            }
        }


        private void Update()
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

            if (!p1_jump && !playerTwo.onLadder)
            {
                if (CrossPlatformInputManager.GetButtonDown("P1_Jump"))
                {
                    Debug.Log("p1 jump");
                    p1_jump = CrossPlatformInputManager.GetButtonDown("P1_Jump");
                }
            }
        }


        private void FixedUpdate()
        {
            float h = 0;

            // Read the inputs and pass input to character controller.
            if (playerOne != null)
            {
                Debug.Log("P0: " + p0_jump);
                h = CrossPlatformInputManager.GetAxis("P0_Horizontal");
                playerOne.Move(h, p0_jump);
                
            }

            if (playerTwo != null)
            {
                Debug.Log("P1: " + p1_jump);
                h = CrossPlatformInputManager.GetAxis("P1_Horizontal");
                playerTwo.Move(h, p1_jump);
                
            }
            p1_jump = false;
            p0_jump = false;
        }
    }
}
