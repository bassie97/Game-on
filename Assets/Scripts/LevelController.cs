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
    private GameObject Player0;

    [SerializeField]
    private GameObject Player1;

    [SerializeField]
    private Camera cam;

    private void Awake()
    {
        GameController.Instance.subscribeScriptToGameEventUpdates(this);

        //notice gamecontroller that the level has started.
        GameController.Instance.playerPassedEvent(1);
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //this method will be automatically called whenever the player passes an important event in the game;
    void gameEventUpdated()
    {
        
        // If ID = 1 level has started
        if (GameController.Instance.gameEventID == 1)
        {
            //Spawn player or players

            //Initialize HUD
            GameObject parent = GameObject.FindGameObjectWithTag("ChooseCharacter");
            if (GameController.Instance.AmountOfPlayers == 1)
            {
                GameObject prefab = (GameObject)Instantiate(OnePlayerHUD, new Vector3(0, 0, 0), Quaternion.identity);
                prefab = (GameObject)Instantiate(Player0, new Vector3(0, 0, 0), Quaternion.identity);
                cam.GetComponent<Camera2DFollow>().target = prefab.transform;
            }
            if(GameController.Instance.AmountOfPlayers == 2)
            {
                GameObject prefab = (GameObject)Instantiate(TwoPlayerHUD, new Vector3(0, 0, 0), Quaternion.identity);
                prefab = (GameObject)Instantiate(Player0, new Vector3(-6, 0, 0), Quaternion.identity);
                cam.GetComponent<Camera2DFollow>().target = prefab.transform;
                prefab = (GameObject)Instantiate(Player1, new Vector3(-8, 0, 0), Quaternion.identity);
                cam.GetComponent<Camera2DFollow>().target1 = prefab.transform;
            }
        }

    }
}
