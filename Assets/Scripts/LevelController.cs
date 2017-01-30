using UnityEngine;
using System.Collections;
/**
 * This script should be implemented by all levels of the game.
 * the script will respont to game event updates from the GameController script
 * This script will also make sure each level will be instantiated in the correct way.
 * 
*/
public class LevelController : MonoBehaviour {

    public GameObject OnePlayerHUD;
    public GameObject TwoPlayerHUD;

    [SerializeField]
    private GameObject Player0Boy;

    [SerializeField]
    private GameObject Player0Girl;

    [SerializeField]
    private GameObject Player1Boy;

    [SerializeField]
    private GameObject Player1Girl;

    private GameObject prefab;

    public float delay = 0;
    public float repeatTime;
    public int scoreIncrease;

    [SerializeField]
    private Camera cam;

    private void Awake()
    {
        GameController.Instance.subscribeScriptToGameEventUpdates(this);

       
    }

    // Use this for initialization
    void Start () {
        //notice gamecontroller that the level has started.
        GameController.Instance.playerPassedEvent(1);
        Debug.Log("two plaeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeyers");
        InvokeRepeating("IncreaseScore", delay, repeatTime);
    }

    void IncreaseScore()
    {
        GameController.Instance.IncreaseScore(scoreIncrease);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    //this method will be automatically called whenever the player passes an important event in the game;
    void gameEventUpdated()
    {

        Debug.Log("respawn players");
        
        // If ID = 1 level has started
        if (GameController.Instance.gameEventID == 1)
        {
            //Spawn player or players

            //Initialize HUD
            GameObject parent = GameObject.FindGameObjectWithTag("ChooseCharacter");
            if (GameController.Instance.AmountOfPlayers == 1)
            {
                Debug.Log("one player");

                if(GameController.Instance.Char1Model1 == 0)
                {
                    Debug.Log("one player girl instantiated");
                    prefab = (GameObject)Instantiate(Player0Girl, new Vector3(-19, -2, 0), Quaternion.identity);
                }
                if(GameController.Instance.Char1Model1 == 1)
                {
                    Debug.Log("one player boy instantiated");
                    prefab = (GameObject)Instantiate(Player0Boy, new Vector3(-19, -2, 0), Quaternion.identity);
                }
                
                cam.GetComponent<Camera2DFollow>().target = prefab.transform;
                prefab = (GameObject)Instantiate(OnePlayerHUD, new Vector3(0, 0, 0), Quaternion.identity);
                
                
            }

            if(GameController.Instance.AmountOfPlayers == 2)
            {
                Debug.Log("two players");

                if (GameController.Instance.Char1Model1 == 0)
                {
                    Debug.Log("two players girl0 instantiated");
                    prefab = (GameObject)Instantiate(Player0Girl, new Vector3(-19, -2, 0), Quaternion.identity);
                }
                if (GameController.Instance.Char1Model1 == 1)
                {
                    Debug.Log("two players boy0 instantiated");
                    prefab = (GameObject)Instantiate(Player0Boy, new Vector3(-19, -2, 0), Quaternion.identity);
                }
                cam.GetComponent<Camera2DFollow>().target = prefab.transform;

                if (GameController.Instance.Char2Model1 == 0)
                {
                    Debug.Log("two players girl1 instantiated");
                    prefab = (GameObject)Instantiate(Player1Girl, new Vector3(-22, -2, 0), Quaternion.identity);
                }
                if (GameController.Instance.Char2Model1 == 1)
                {
                    Debug.Log("two players boy1 instantiated");
                    prefab = (GameObject)Instantiate(Player1Boy, new Vector3(-22, -2, 0), Quaternion.identity);
                }
                
                cam.GetComponent<Camera2DFollow>().target1 = prefab.transform;

                prefab = (GameObject)Instantiate(TwoPlayerHUD, new Vector3(0, 0, 0), Quaternion.identity);
            }
        }

    }

    private void OnDestroy()
    {
        if (GameController.Instance != null)
        {
            GameController.Instance.deSubscribeScriptToGameEventUpdates(this);
        }
    }
}
