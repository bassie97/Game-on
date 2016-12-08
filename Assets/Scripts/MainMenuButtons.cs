using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenuButtons : MonoBehaviour {

    //refrence for the pause menu panel in the hierarchy
    public GameObject settingsPanel;
    //animator reference
    private Animator anim;
    //variable for checking if the game is paused 
    private bool settingsIsShowed = false;

    // Use this for initialization
    void Start()
    {
        Time.timeScale = 1;
        //get the animator component
        anim = settingsPanel.GetComponent<Animator>();
        //disable it on start to stop it from playing the default animation
        anim.enabled = false;
    }

    //used to change to a certain scene depending on the index of the scene 
    public void ChangeToScene (int sceneToChangeTo)
    {
        SceneManager.LoadScene(sceneToChangeTo);
    }

    //function to show settings
    public void ShowSettings()
    {
        //enable the animator component
        anim.enabled = true;
        //play the Slidein animation
        anim.Play("SettingsMenuSlideIn");
        //set the isPaused flag to true to indicate that the game is paused
        settingsIsShowed = true;
        //freeze the timescale
        Time.timeScale = 0;
    }

    //function to hide settings
    public void HideSettings()
    {
        //set the isPaused flag to false to indicate that the game is not paused
        settingsIsShowed = false;
        //play the SlideOut animation
        anim.Play("SettingsMenuSlideOut");
        //set back the time scale to normal time scale
        Time.timeScale = 1;
    }
}
