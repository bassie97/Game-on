using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityStandardAssets._2D;

public class PlayerProgressHolder : MonoBehaviour {

    public SaveInformation save;
    PlayerProgress progress;
    
    public bool ProgressLoaded { get; private set; }
    Scene scene;
    public int playerID;
    public int levelProgress;
    public string playerName;
    public int singleplayer;

    private static PlayerProgressHolder instance;

    public static PlayerProgressHolder Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PlayerProgressHolder>();
#if UNITY_EDITOR
                if (FindObjectsOfType<PlayerProgressHolder>().Length > 1)
                {
                    Debug.LogError("There is more than one game controller in the scene");
                }
#endif
            }
            return instance;
        }
    }

    void Update()
    {
        scene = SceneManager.GetActiveScene();
        if (scene.name == "LevelTwo")
        {
            levelProgress = 2;
            Debug.Log(scene.name);
            save.saveAllInformation(playerID,levelProgress);
        }else if (scene.name == "SceneTutorial")
        {
            levelProgress = 10;
            Debug.Log(scene.name);
            save.saveAllInformation(playerID, levelProgress);
        }
        
    }
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        if (playerName !=null) {
            ProgressLoaded = true;
        }
        if(GameController.Instance.AmountOfPlayers == 1)
        {

        }
    }


    //used to change to a certain scene depending on the index of the scene 
    public void ChangeToScene(int sceneToChangeTo)
    {
        SceneManager.LoadScene(sceneToChangeTo);
    }

    public void LoadName(string playername)
    {
        playerName = playername;
    }
    public void LoadLevel(int levelprogress)
    {
        levelProgress = levelprogress;
    }
}
