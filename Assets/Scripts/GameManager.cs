using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour {

    private int AmountOfPlayers;

    public int AmountOfPlayers1
    {
        get
        {
            return AmountOfPlayers;
        }

        set
        {
            Debug.Log("Player amount is set to: " + value);
            AmountOfPlayers = value;
            SceneManager.LoadScene(2);
        }
    }

    //used to change to a certain scene depending on the index of the scene 
    public void ChangeToScene(int sceneToChangeTo)
    {
        SceneManager.LoadScene(sceneToChangeTo);
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
