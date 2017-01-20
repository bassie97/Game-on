using UnityEngine;
using System.Collections;

namespace UnityStandardAssets._2D {
    public class DoorScript : MonoBehaviour
    {

        [SerializeField]
        Sprite doorSpriteOpen;
        [SerializeField]
        Sprite doorSpriteClosed;
        [SerializeField]
        private int minAmmo = 5;

        // Variables
        private PlatformerCharacter2D m_Character;
        private BoxCollider2D door;
        private bool locked = false;
        private bool[] playerInside;

        // Use this for initialization
        void Start()
        {
            door = GameObject.FindGameObjectWithTag("Door").GetComponent<BoxCollider2D>();
            playerInside = new bool[GameController.Instance.AmountOfPlayers];
        }

        // Update is called once per frame
        void Update()
        {
           
        }

        /* 
        * Called when the player nters the door trigger.
        * Checks if the player has enough ammo to enter
        */
        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Player" || collision.tag == "Player1")
            {
                
                m_Character = collision.GetComponent<PlatformerCharacter2D>();
                if (m_Character.ammoCount >= minAmmo)
                {
                    GetComponentsInChildren<SpriteRenderer>()[1].sprite = doorSpriteOpen;
                } else
                {
                    door.isTrigger = false;
                    Debug.Log("Not enough ammo");
                }
            }
        }

       /* 
        * Called when the player exits the door trigger.
        * The following occurs in this method:
        * 1. Change the sprite of the door
        * 2. Set the trigger to true
        * 3. Check if both player are in the room, if so, lock the room
        */
        void OnTriggerExit2D(Collider2D collision)
        {

            if (collision.tag == "Player" || collision.tag == "Player1")
            {
                GetComponentsInChildren<SpriteRenderer>()[1].sprite = doorSpriteClosed;
                door.isTrigger = true;
                m_Character = collision.GetComponent<PlatformerCharacter2D>();
                if (AreCharactersInside(m_Character))
                {
                    door.isTrigger = false;
                    locked = true;
                }
            }
        }

        /* 
         * Check if the door is locked, if not set it's trigger to true.
         */
        void OnCollisionEnter2D(Collision2D collision)
        {
            if (!locked)
            {
                door.isTrigger = true;
            }
        }

       /* 
        * Check if both players are inside the boss room
        */
        private bool AreCharactersInside(PlatformerCharacter2D character)
        {
            switch (character.tag)
            {
                case "Player":
                    playerInside[0] = character.m_FacingRight;
                    break;
                case "Player1":
                    playerInside[1] = character.m_FacingRight;
                    break;
            }
            
            foreach(bool inside in playerInside)
            {
                if (!inside)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
