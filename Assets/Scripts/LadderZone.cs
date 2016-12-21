using UnityEngine;
using System.Collections;

namespace UnityStandardAssets._2D
{
    public class LadderZone : MonoBehaviour
    {

        private PlatformerCharacter2D m_Character;
        // Use this for initialization
        void Start()
        {
            m_Character = GetComponent<PlatformerCharacter2D>();
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log("enter");
            if (collision.tag == "Player" || collision.tag == "Player1")
            {
                m_Character = collision.GetComponent<PlatformerCharacter2D>();
                m_Character.onLadder = true;
            }
        }

        void OnTriggerExit2D(Collider2D collision)
        {
            Debug.Log("exit");
            if (collision.tag == "Player" || collision.tag == "Player1")
            {
                m_Character = collision.GetComponent<PlatformerCharacter2D>();
                m_Character.onLadder = false;
            }
        }
    }
}