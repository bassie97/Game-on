using UnityEngine;
using System.Collections;

public class SaveInformation : MonoBehaviour
{
    public void saveAllInformation(int playerID, int levelProgress)
    {        
        PlayerPrefs.SetInt("LevelProgress"+playerID, levelProgress);
    }

    public void newPlayer(string playerName, int singleplayer)
    {
        int id = NewPlayerID();
        PlayerPrefs.SetInt("PlayerID" + id, id);
        PlayerPrefs.SetInt("LevelProgress" + id, 0);
        PlayerPrefs.SetString("PlayerName" + id, playerName);
        PlayerPrefs.SetInt("Singleplayer" + id, singleplayer);
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    static int NewPlayerID()
    {
        int i =0;
        if (PlayerPrefs.HasKey("PlayerID" + i))
        {
            i++;
            NewPlayerID();
        }
        else {  }
        return i;
    }
}
