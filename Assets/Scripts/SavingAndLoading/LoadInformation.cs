using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadInformation : MonoBehaviour {
    public Text playerName;
    string naam;
            

    void Awake()
    {
        naam = PlayerPrefs.GetString("PlayerName" + LoadPlayers.Instance.counter);
        playerName = GetComponent<Text>();
        playerName.text = naam;
    }

    public void ChangeToScene(int sceneToChangeTo)
    {
        SceneManager.LoadScene(sceneToChangeTo);
    }
}
