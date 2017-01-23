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
        PlayerProgressHolder.Instance.playerID = PlayerID;
        PlayerProgressHolder.Instance.playerName = PlayerPrefs.GetString("PlayerName" + PlayerID); 
        PlayerProgressHolder.Instance.levelProgress = PlayerPrefs.GetInt("LevelProgress" + PlayerID);
    }

    public void Clicked()
    {

    }
}
