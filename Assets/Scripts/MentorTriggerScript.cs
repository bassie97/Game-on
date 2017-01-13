using UnityEngine;
using System.Collections;
using UnityStandardAssets._2D;

public class MentorTriggerScript : MonoBehaviour
{

    [SerializeField]
    public int id;

    private bool isEntering = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && collision.GetComponent<PlatformerCharacter2D>().m_FacingRight)
        {
            if (isEntering)
                return;
            isEntering = true;
            GameObject.FindObjectOfType<MentorControllerScript>().Act(transform.position, id);
        }       
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isEntering = false;
        }
    }

}
