using UnityEngine;
using System.Collections;

public class SaveInformation : MonoBehaviour
{
    private int i = 0;
    public void saveAllInformation(int playerID, int levelProgress)
    {        
        PlayerPrefs.SetInt("LevelProgress"+playerID, levelProgress);
    }

    public void newPlayer(string playerName, int singleplayer)
    {
        int id = NewPlayerID(i);
        Debug.Log("id: "+id);
        if (PlayerPrefs.HasKey("PlayerID" + id) == false) {

            PlayerPrefs.SetInt("PlayerID" + id, id);
            PlayerPrefs.SetInt("LevelProgress" + id, 0);
            PlayerPrefs.SetString("PlayerName" + id, playerName);
            PlayerPrefs.SetInt("Singleplayer" + id, singleplayer);
            //PlayerPrefs.SetInt("HighscoreLevel1PlayerID" + id, highscoreLevel1PlayerID);
        }
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    static int NewPlayerID(int i)
    {
        int j;
        for (j=0; PlayerPrefs.HasKey("PlayerID" + j) == true; j++)
        {
            //nothing
        }
        Debug.Log(j);
        return j;
    }
}

