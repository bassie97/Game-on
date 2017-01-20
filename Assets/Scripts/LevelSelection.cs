using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour {

    public string sceneToLoad = "MainMenu";
    public Object prefab1;
    public Object prefab2;
    public Object prefab3;
    public Object prefab4;
    public Object prefab5;
    private GameObject instObj;

    void Update()
    {
        
    }

    void Start()
    {
        if (PlayerProgressHolder.Instance.ProgressLoaded == true)
        {
            //SceneManager.LoadScene(sceneToLoad);
            GameObject canvas = GameObject.FindGameObjectWithTag("ChooseCharacter");
            int level = PlayerPrefs.GetInt("LevelProgress"+PlayerProgressHolder.Instance.playerID);
            if (level >= 1)
            {
                GameObject prefab = (GameObject)Instantiate(prefab1, new Vector3(-200, 0, 0), Quaternion.identity);
                prefab.transform.SetParent(canvas.transform, false);    
            }
            if (level >= 2)
            {
                GameObject prefab = (GameObject)Instantiate(prefab2, new Vector3(0, 0, 0), Quaternion.identity);
                prefab.transform.SetParent(canvas.transform, false);
            }
            if (level >= 3)
            {
                GameObject prefab = (GameObject)Instantiate(prefab3, new Vector3(200, 0, 0), Quaternion.identity);
                prefab.transform.SetParent(canvas.transform, false);
            }
            if (level >= 4)
            {
                GameObject prefab = (GameObject)Instantiate(prefab4, new Vector3(400, 0, 0), Quaternion.identity);
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
