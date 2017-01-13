using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class AmountOfPlayers : MonoBehaviour {

    private int amount;

    public int amount1
    {
        get
        {
            return amount;
        }

        set
        {
            Debug.Log("Player amount is set to: " + value);
            amount = value;
            GameController.Instance.AmountOfPlayers = amount;
            //go to next screen
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
