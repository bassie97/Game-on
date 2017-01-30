using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinScreen : MonoBehaviour {

    [SerializeField]
    private Text score;

	// Use this for initialization
	void Start () {
        score.text = GameController.Instance.Score.ToString();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChangeToScene(int sceneToChangeTo)
    {
        SceneManager.LoadScene(sceneToChangeTo);
    }

}
