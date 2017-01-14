using UnityEngine;
using System.Collections;

public class SaveInformation : MonoBehaviour
{
    public void saveAllInformation(string levelProgress, string player)
    {      
        PlayerPrefs.SetString("LevelProgress", levelProgress);
        PlayerPrefs.SetString("Player1", player);
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }


}
