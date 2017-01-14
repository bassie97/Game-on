using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour {

    public string sceneToLoad = "MainMenu";
    public Object prefab1;
    public Object prefab2;
    public Object prefab3;
    public Object prefab4;
    public Object prefab5;
    void Update()
    {
        
    }

    void Start()
    {
        if (PlayerProgressHolder.Instance.ProgressLoaded == true)
        {
            Debug.Log("hallo1");
            //SceneManager.LoadScene(sceneToLoad);
            GameObject canvas = GameObject.FindGameObjectWithTag("ChooseCharacter");
            Debug.Log(PlayerPrefs.GetInt("LevelProgressPlayer1"));
            if (PlayerPrefs.HasKey("LevelProgressPlayer1"))
            {
                GameObject prefab = (GameObject)Instantiate(prefab1, new Vector3(51, -5, 0), Quaternion.identity);
                prefab.transform.SetParent(canvas.transform, false);
            }
            if (PlayerPrefs.HasKey("Player2"))
            {
                GameObject prefab = (GameObject)Instantiate(prefab2, new Vector3(51, -5, 0), Quaternion.identity);
                prefab.transform.SetParent(canvas.transform, false);
            }
        }
    }
    	
    //used to change to a certain scene depending on the index of the scene 
    public void ChangeToScene(int sceneToChangeTo)
    {
        SceneManager.LoadScene(sceneToChangeTo);
    }
}
