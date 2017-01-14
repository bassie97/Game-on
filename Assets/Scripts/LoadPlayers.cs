using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadPlayers : MonoBehaviour {

    // Use this for initialization
    public Object prefab1;
    public Object prefab2;
    void Start () {
        GameObject canvas = GameObject.FindGameObjectWithTag("ChooseCharacter");

        if (PlayerPrefs.HasKey("Player1"))
        {
            GameObject prefab = (GameObject)Instantiate(prefab1, new Vector3(51, -5, 0), Quaternion.identity);
            prefab.transform.SetParent(canvas.transform, false);
            GameController.Instance.NickName = PlayerPrefs.GetString("Player1");
        }
        if (PlayerPrefs.HasKey("Player2"))
        {
            GameObject prefab = (GameObject)Instantiate(prefab2, new Vector3(51, -5, 0), Quaternion.identity);
            prefab.transform.SetParent(canvas.transform, false);
        }        
    }
	

    //used to change to a certain scene depending on the index of the scene 
    public void ChangeToScene(int sceneToChangeTo)
    {
        SceneManager.LoadScene(sceneToChangeTo);
    }
}
