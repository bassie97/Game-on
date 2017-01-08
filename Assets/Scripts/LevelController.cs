using UnityEngine;
using System.Collections;
/**
 * This script should be implemented by all levels of the game.
 * the script will respont to game event updates from the GameController script
 * This script will also make sure each level will be instantiated in the correct way.
 * 
*/
public class LevelController : MonoBehaviour {

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

        }

    }
}
