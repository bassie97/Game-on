using UnityEngine;
using System.Collections;

public class SaveInformation : MonoBehaviour
{
    private int i = 0;
    public void saveAllInformation(int playerID, int levelProgress)
    {
        if (PlayerPrefs.GetInt("LevelProgress" + playerID) > levelProgress == false) {
            PlayerPrefs.SetInt("LevelProgress" + playerID, levelProgress);
            PlayerProgressHolder.Instance.levelProgress = levelProgress;
        }
    }

    public void newPlayer(string playerName, int singleplayer)
    {
        int id = NewPlayerID(i);
        Debug.Log("id: "+id);
        if (PlayerPrefs.HasKey("PlayerID" + id) == false)
        {
            //playerPrefs instellen
            PlayerPrefs.SetInt("PlayerID" + id, id);
            PlayerPrefs.SetInt("LevelProgress" + id, 0);
            PlayerPrefs.SetString("PlayerName" + id, playerName);
            PlayerPrefs.SetInt("Singleplayer" + id, singleplayer);
            //PlayerPrefs.SetInt("HighscoreLevel1PlayerID" + id, highscoreLevel1PlayerID);
            //PlayerProgressHolder inladen
            PlayerProgressHolder.Instance.playerID = id;
            PlayerProgressHolder.Instance.playerName = PlayerPrefs.GetString("PlayerName" + id);
            PlayerProgressHolder.Instance.levelProgress = PlayerPrefs.GetInt("LevelProgress" + id);
            GameController.Instance.NickName = PlayerProgressHolder.Instance.playerName;

            if (PlayerPrefs.GetInt("Singleplayer" + id) == 1)
            {
                GameController.Instance.AmountOfPlayers = 1;
            }
            else
            {
                GameController.Instance.AmountOfPlayers = 2;
            }
        }
        else { Debug.Log("Er zijn al 9 profielen."); }
    }

    void Awake()
    {
        if (FindObjectsOfType<SaveInformation>().Length > 1)
        {
            Debug.LogError("There is more than one game controller in the scene");
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    private static int NewPlayerID(int i)
    {
        int j;
        for (j=0; PlayerPrefs.HasKey("PlayerID" + j) == true; j++)
        {
            //nothing
        }
        Debug.Log(j);
        if (j <= 8)
        {
            return j;
        }
        return 0;
    }
}

