using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

    public List<MonoBehaviour> eventsSubscribedScripts = new List<MonoBehaviour>();
    public int gameEventID = 0;

    private static GameController instance;

    public int AmountOfPlayers;
    public string NickName;

    public static GameController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameController>();
#if UNITY_EDITOR
                if (FindObjectsOfType<GameController>().Length > 1)
                {
                    Debug.LogError("There is more than one game controller in the scene");
                }
#endif
            }
            return instance;
        }
    }

    public void subscribeScriptToGameEventUpdates(MonoBehaviour pScript)
    {
        Debug.Log("Added: " + pScript);
        eventsSubscribedScripts.Add(pScript);
    }

    public void deSubscribeScriptToGameEventUpdates(MonoBehaviour pScript)
    {
        while (eventsSubscribedScripts.Contains(pScript))
        {
            Debug.Log("Removed from list: " + pScript);
            eventsSubscribedScripts.Remove(pScript);
        }
    }

    public void playerPassedEvent(int _ID)
    {
        gameEventID = _ID;

        //GAME ID's
        // ID = 1 means level started.
        foreach (MonoBehaviour _script in eventsSubscribedScripts)
        {
            _script.Invoke("gameEventUpdated", gameEventID);
        }
    }

    // Use this for initialization
    void Start () {
        DontDestroyOnLoad( gameObject);
    }
}
