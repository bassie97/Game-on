using UnityEngine;
using System.Collections;

public class SaveInformation : MonoBehaviour
{
    public void saveAllInformation(int levelProgress, string player)
    {      
        PlayerPrefs.SetInt("LevelProgressPlayer1", levelProgress);
        PlayerPrefs.SetString("Player1", player);
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }


}
