using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadInformationLevel : MonoBehaviour
{
    public Text levelProgress;
    PlayerProgressHolder progress;
    string level;
    void Start()
    {
        level = PlayerPrefs.GetString("LevelProgress");
        levelProgress = GetComponent<Text>();
        levelProgress.text = level;

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void sendData()
    {
        progress.LoadName(level);
    }
}
