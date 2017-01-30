using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour {
    public int PlayerID;

    // Use this for initialization
    void Start () {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(OnCLickButton);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void Awake()
    {
        PlayerID = LoadPlayers.Instance.counter;
    }

    private void OnCLickButton()
    {
        int singleplayer;
        if (PlayerPrefs.GetInt("Singleplayer" + PlayerID) == 1)
        {
            singleplayer = 1;
            GameController.Instance.AmountOfPlayers = 1;
        }
        else{
                singleplayer = 0;
                GameController.Instance.AmountOfPlayers = 2;
            }
        PlayerProgressHolder.Instance.playerID = PlayerID;
        PlayerProgressHolder.Instance.playerName = PlayerPrefs.GetString("PlayerName" + PlayerID); 
        PlayerProgressHolder.Instance.levelProgress = PlayerPrefs.GetInt("LevelProgress" + PlayerID);
        PlayerProgressHolder.Instance.singleplayer = singleplayer;
        PlayerProgressHolder.Instance.gender1 = PlayerPrefs.GetInt("GenderOnePlayer" + PlayerID);
        PlayerProgressHolder.Instance.gender2 = PlayerPrefs.GetInt("GenderTwoPlayer" + PlayerID);
        GameController.Instance.NickName = PlayerProgressHolder.Instance.playerName;
    }

    public void Clicked()
    {

    }
}
