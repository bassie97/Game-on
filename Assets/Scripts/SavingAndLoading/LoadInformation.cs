using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadInformation : MonoBehaviour {
    public Text playerName;
    string naam;
    public int PlayerID;

    void Awake()
    {
        naam = PlayerPrefs.GetString("PlayerName" + LoadPlayers.Instance.counter);
        playerName = GetComponent<Text>();
        playerName.text = naam;
        PlayerID = LoadPlayers.Instance.counter;
    }
    
    public void LoadUser(int playerID)
    {
        PlayerID = playerID;
        //GameController.Instance.NickName = PlayerPrefs.GetString("PlayerName" + 1);
        //PlayerProgressHolder.Instance.playerName = GameController.Instance.NickName;
    }
    public void ChangeToScene(int sceneToChangeTo)
    {
        SceneManager.LoadScene(sceneToChangeTo);
    }
}
