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

    private int score;
    private int pickUps;
    private int[] pickUpsInLevels = new int [2];
    private int scoreTotal;

    private int Char1Model = 0;
    private int Char2Model = 0;

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

    private void Awake()
    {
        if (FindObjectsOfType<GameController>().Length > 1)
        {
            Debug.LogError("There is more than one game controller in the scene");
            Destroy(gameObject);
        }
    }

    public int Char1Model1
    {
        get
        {
            return Char1Model;
        }

        set
        {
            if (Char1Model == 0 || Char1Model == 1)
            {
                Debug.Log("Char1Model set to: " + value);
                Char1Model = value;
            }else
            {
                Debug.LogError("Model " + Char1Model + " does not exist");
            }
        }
    }

    public int Char2Model1
    {
        get
        {
            return Char2Model;
        }

        set
        {
            if (Char2Model == 0 || Char2Model == 1)
            {
                Debug.Log("Char2Model set to: " + value);
                Char2Model = value;
            }
            else
            {
                Debug.LogError("Model " + Char2Model + " does not exist");
            }
        }
    }

    public int Score
    {
        get
        {
            return score;
        }

        set
        {
            score = value;
        }
    }

    public void IncreaseScore(int addition)
    {
        this.score += addition;
    }

    public int PickUps
    {
        get
        {
            return pickUps;
        }
    }

    public void IncreasePickUps()
    {
        pickUps++;
    }

    public void ResetPickUps()
    {
        pickUps = 0;
    }

    public int[] PickUpsInLevels
    {
        get
        {
            return pickUpsInLevels;
        }
    }

    public int ScoreTotal
    {
        get
        {
            return scoreTotal;
        }

        set
        {
            scoreTotal = value;
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
            _script.Invoke("gameEventUpdated", 0);
        }
    }

    // Use this for initialization
    void Start () {
        pickUpsInLevels[0] = 4; pickUpsInLevels[1] = 6;
        DontDestroyOnLoad( gameObject);
    }

    public void toggleMusic(bool toggle )
    {
        Debug.Log("test" + toggle);
        if (toggle) AudioListener.volume = 1;
        if (!toggle) AudioListener.volume = 0;
    }
}
