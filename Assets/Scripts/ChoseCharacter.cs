using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class ChoseCharacter : MonoBehaviour {

    public Object prefab1;
    public Object prefab2;

	// Use this for initialization
	void Start () {
        GameObject canvas = GameObject.FindGameObjectWithTag("ChooseCharacter");

	    if (GameController.Instance.AmountOfPlayers == 1)
        {
            GameObject prefab = (GameObject) Instantiate(prefab1, new Vector3(51 ,-5 ,0), Quaternion.identity);
            prefab.transform.SetParent(canvas.transform, false);
        }


        if (GameController.Instance.AmountOfPlayers == 2)
        {
            GameObject prefab = (GameObject) Instantiate(prefab2, new Vector3(51, -5, 0), Quaternion.identity);
            prefab.transform.SetParent(canvas.transform, false);
        }
    }

    public void changeToScene( int index)
    {
        SceneManager.LoadScene(index);
    }

    public void NickName(string name)
    {
        Debug.Log("name is set to:" + name);
        GameController.Instance.NickName = name;
    }

    public void NewPlayer()
    {
        int singleplayer;
        if (GameController.Instance.AmountOfPlayers == 1)
        {
            singleplayer = 1;
        }
        else { singleplayer = 0; }
        if (GameController.Instance.NickName != null)
        {
            PlayerProgressHolder.Instance.save.newPlayer(GameController.Instance.NickName, singleplayer);
        }
        else { Debug.Log("Please fill in nickname"); changeToScene(3); }
    }

	
	// Update is called once per frame
	void Update () {
	
	}
}
