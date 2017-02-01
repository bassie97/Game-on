using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinScreen : MonoBehaviour {

    [SerializeField]
    private Text score;

    [SerializeField]
    private Text pickUps;

    // Use this for initialization
    void Start () {
        score.text = GameController.Instance.Score.ToString();
        GameController.Instance.ScoreTotal += GameController.Instance.PickUps / GameController.Instance.PickUpsInLevels[GameController.Instance.gameEventID - 1] * 100;
        pickUps.text = GameController.Instance.ScoreTotal.ToString();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChangeToScene(int sceneToChangeTo)
    {
        SceneManager.LoadScene(sceneToChangeTo);
    }

}
