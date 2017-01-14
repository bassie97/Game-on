using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour {

    public string sceneToLoad = "MainMenu";
    public PlayerProgressHolder progressHolder;
    void Update()
    {
        if (progressHolder.ProgressLoaded)
        {
            //SceneManager.LoadScene(sceneToLoad);
        }
    }
    	
    //used to change to a certain scene depending on the index of the scene 
    public void ChangeToScene(int sceneToChangeTo)
    {
        SceneManager.LoadScene(sceneToChangeTo);
    }
}
