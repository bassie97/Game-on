﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGameScript : MonoBehaviour {

    [SerializeField]
    private Text score;

    [SerializeField]
    private Text pickUps;

    // Use this for initialization
    void Start()
    {
        score.text = GameController.Instance.Score.ToString();
        pickUps.text = GameController.Instance.ScoreTotal.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeToScene(int sceneToChangeTo)
    {
        SceneManager.LoadScene(sceneToChangeTo);
    }

    public void EndGmae()
    {
        Application.Quit();
    }
}
