﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadInformationLevel : MonoBehaviour
{
    public Text levelProgress;
    int level;
    void Start()
    {
        try
        {
            level = PlayerPrefs.GetInt("LevelProgress" + PlayerProgressHolder.Instance.playerID);
            if (level == 0)
            {
                levelProgress = GetComponent<Text>();
                levelProgress.text = "Has to start his journey!";
            }
            if (level == 1)
            {
                levelProgress = GetComponent<Text>();
                levelProgress.text = "Level 1";
            }
            if (level == 2)
            {
                levelProgress = GetComponent<Text>();
                levelProgress.text = "Level 2";
            }
            if (level == 10)
            {
                levelProgress = GetComponent<Text>();
                levelProgress.text = "Scene tutorial";
            }
        }
        catch { Debug.Log("Error loadding information level"); }


    }

    // Update is called once per frame
    void Update()
    {

    }
    public void sendData()
    {
        PlayerProgressHolder.Instance.LoadLevel(level);
    }
}
