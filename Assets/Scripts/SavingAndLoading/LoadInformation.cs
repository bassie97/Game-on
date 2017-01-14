using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadInformation : MonoBehaviour {
    public Text playerName;
    string name;
    void Start () {
        name = PlayerPrefs.GetString("Player1");
        playerName = GetComponent<Text>();
        playerName.text = name;
    }
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void sendData()
    {
        PlayerProgressHolder.Instance.LoadName(name);
    }
}
