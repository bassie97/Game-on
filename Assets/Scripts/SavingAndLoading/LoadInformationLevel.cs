using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadInformationLevel : MonoBehaviour
{
    public Text levelProgress;
    string level;
    void Start()
    {
        level = PlayerPrefs.GetInt("LevelProgressPlayer1").ToString();
        if (PlayerPrefs.GetInt("LevelProgressPlayer1") == 0)
        {
            levelProgress = GetComponent<Text>();
            levelProgress.text = "Has to start his journey!";
        }
        if (PlayerPrefs.GetInt("LevelProgressPlayer1") == 1)
        {
            levelProgress = GetComponent<Text>();
            levelProgress.text = "Level 1";
        }
        if (PlayerPrefs.GetInt("LevelProgressPlayer1") == 2)
        {
            levelProgress = GetComponent<Text>();
            levelProgress.text = "Level 2";
        }
        if (PlayerPrefs.GetInt("LevelProgressPlayer1") == 10)
        {
            levelProgress = GetComponent<Text>();
            levelProgress.text = "Scene tutorial";
        }


    }

    // Update is called once per frame
    void Update()
    {

    }
    public void sendData()
    {
        PlayerProgressHolder.Instance.LoadName(level);
    }
}
