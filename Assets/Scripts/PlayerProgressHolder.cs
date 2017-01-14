using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityStandardAssets._2D;

public class PlayerProgressHolder : MonoBehaviour {

    public SaveInformation save;
    PlayerProgress progress;
    public string levelProgress;
    public bool ProgressLoaded { get; private set; }
    Scene scene;
    string playerName;

    void Update()
    {
        scene = SceneManager.GetActiveScene();
        if (scene.name == "LevelTwo")
        {
            levelProgress = "Level Two";
            Debug.Log(scene.name);
            save.saveAllInformation(levelProgress, "Gerbrand");
        }else if (scene.name == "SceneTutorial")
        {
            levelProgress = "Scene Tutorial";
            Debug.Log(scene.name);
            save.saveAllInformation(levelProgress, "Gerbrand");
        }
        
    }
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        ProgressLoaded = true;
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
    public void LoadLevel(string levelprogress)
    {
        levelProgress = levelprogress;
    }
}
