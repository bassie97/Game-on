using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadPlayers : MonoBehaviour {

    // Use this for initialization
    public GameObject prefab1;
    public GameObject prefab2;
    public GameObject prefab3;
    public GameObject prefab4;
    public GameObject prefab5;
    public GameObject prefab6;
    public GameObject prefab7;
    public GameObject prefab8;
    public GameObject prefab9;
    public int counter = 0;

    private static LoadPlayers instance;

    public static LoadPlayers Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<LoadPlayers>();
#if UNITY_EDITOR
                if (FindObjectsOfType<LoadPlayers>().Length > 1)
                {
                    Debug.LogError("There is more than one PlayerProgressHolder in the scene");
                }
#endif
            }
            return instance;
        }
    }

    void Start () {
        GameObject canvas = GameObject.FindGameObjectWithTag("LoadUser");

        if (PlayerPrefs.HasKey("PlayerID"+counter))
        {
            GameObject prefab = (GameObject)Instantiate(prefab1, new Vector3(-200, 250, 0), Quaternion.identity);
            prefab.transform.SetParent(canvas.transform, false);
        }
        counter++;
        if (PlayerPrefs.HasKey("PlayerID"+counter))
        {
            GameObject prefab = (GameObject)Instantiate(prefab2, new Vector3(-200, 150, 0), Quaternion.identity);
            prefab.transform.SetParent(canvas.transform, false);
        }
        counter++;
        if (PlayerPrefs.HasKey("PlayerID" + counter))
        { 
            GameObject prefab = (GameObject)Instantiate(prefab3, new Vector3(-200, 50, 0), Quaternion.identity);
            prefab.transform.SetParent(canvas.transform, false);
        }
        counter++;
        if (PlayerPrefs.HasKey("PlayerID" + counter))
        {
            GameObject prefab = (GameObject)Instantiate(prefab4, new Vector3(-200, -50, 0), Quaternion.identity);
            prefab.transform.SetParent(canvas.transform, false);
        }
        counter++;
        if (PlayerPrefs.HasKey("PlayerID" + counter))
        {
            GameObject prefab = (GameObject)Instantiate(prefab5, new Vector3(-200, -150, 0), Quaternion.identity);
            prefab.transform.SetParent(canvas.transform, false);
        }
    }

    //used to change to a certain scene depending on the index of the scene 
    public void ChangeToScene(int sceneToChangeTo)
    {
        SceneManager.LoadScene(sceneToChangeTo);
    }

    public void LoadUser()
    {
        //int i = prefab1.GetComponent<LoadInformation>().PlayerID;
        PlayerProgressHolder.Instance.playerID = 1;
        PlayerProgressHolder.Instance.playerName = "Gerbrand";
        PlayerProgressHolder.Instance.levelProgress = 1;
    }
}
