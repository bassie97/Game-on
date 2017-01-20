using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadInformation : MonoBehaviour {
    public Text playerName;
    string naam;
    void Start () {
        naam = PlayerPrefs.GetString("PlayerName"+PlayerProgressHolder.Instance.playerID);
        playerName = GetComponent<Text>();
        playerName.text = naam;
    }
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void sendData()
    {
        PlayerProgressHolder.Instance.LoadName(naam);
    }
}
