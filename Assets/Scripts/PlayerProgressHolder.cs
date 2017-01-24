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
                    Debug.LogError("There is more than one PlayerProgressHolder in the scene");
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
            save.saveAllInformation(playerID,2);
        }else if (scene.name == "SceneTutorial")
        {
            save.saveAllInformation(playerID, 10);
        }
        else if (scene.name == "LevelOne")
        {
            save.saveAllInformation(playerID, 1);
        }
        else if (scene.name == "LevelThree")
        {
            save.saveAllInformation(playerID, 3);
        }
        else if (scene.name == "LevelFour")
        {
            save.saveAllInformation(playerID, 4);
        }

        if (playerName != "")
        {
            ProgressLoaded = true;
        }        
    }
    void Awake()
    {
        if (FindObjectsOfType<PlayerProgressHolder>().Length > 1)
        {
            Debug.LogError("There is more than one game controller in the scene");
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
    }


    //used to change to a certain scene depending on the index of the scene 
    public void ChangeToScene(int sceneToChangeTo)
    {
        SceneManager.LoadScene(sceneToChangeTo);
    }
}
