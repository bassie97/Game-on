using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadPlayers : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //used to change to a certain scene depending on the index of the scene 
    public void ChangeToScene(int sceneToChangeTo)
    {
        SceneManager.LoadScene(sceneToChangeTo);
    }
}
