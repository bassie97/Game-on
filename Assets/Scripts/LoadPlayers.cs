using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadPlayers : MonoBehaviour {

    // Use this for initialization
    public Object prefab1;
    public Object prefab2;
    private int counter = 0;
    void Start () {
        GameObject canvas = GameObject.FindGameObjectWithTag("LoadUser");

        if (PlayerPrefs.HasKey("PlayerID"+counter))
        {
            GameObject prefab = (GameObject)Instantiate(prefab1, new Vector3(-200, 50, 0), Quaternion.identity);
            prefab.transform.SetParent(canvas.transform, false);
            GameController.Instance.NickName = PlayerPrefs.GetString("Player"+counter);
        }
        counter++;
        if (PlayerPrefs.HasKey("PlayerID"+counter))
        {
            GameObject prefab = (GameObject)Instantiate(prefab2, new Vector3(-200, 0, 0), Quaternion.identity);
            prefab.transform.SetParent(canvas.transform, false);
        }        
    }
	

    //used to change to a certain scene depending on the index of the scene 
    public void ChangeToScene(int sceneToChangeTo)
    {
        SceneManager.LoadScene(sceneToChangeTo);
    }
}
