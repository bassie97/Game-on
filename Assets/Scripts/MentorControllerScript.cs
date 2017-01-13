using UnityEngine;
using System.Collections;
using System.Xml;
using System.Collections.Generic;
using System;

public class MentorControllerScript : MonoBehaviour {

    [SerializeField]
    private Sprite[] bubbles;

    // Variables
    private List<List<string>> speeches = new List<List<string>>();
    private List<string> currectSpeech;
    private Rigidbody2D mentorRigidbody;
    private SpriteRenderer bubbleSpeech;

    // Use this for initialization
    void Start () {
        Load();
        LoadCurrentLevelSpeech();
        mentorRigidbody = GetComponent<Rigidbody2D>();
        bubbleSpeech = GetComponentsInChildren<SpriteRenderer>()[1];
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    /// <summary>
    /// Load every speech for every level
    /// </summary>
    private void Load()
    {
        // Load mentor tutorial speech
        var levelTutorial = new List<string>();
        levelTutorial.Add("If you are layer one" + Environment.NewLine + " press W to jump. " + Environment.NewLine + "Player two " + Environment.NewLine + "can use the upper arrow to jump");
        levelTutorial.Add("You can jump on" + Environment.NewLine + " each other to" + Environment.NewLine + " reach higher obstacles");
        levelTutorial.Add("Press the jump button while" + Environment.NewLine + " standing in front of the ladder" + Environment.NewLine + " to use the ladder");
        levelTutorial.Add("Player one " + Environment.NewLine + "can use the E button the shoot bubbles. " + Environment.NewLine + "Player two can do this with " + Environment.NewLine + "the right control button");
        levelTutorial.Add("Well done, " + Environment.NewLine + "you've finished the tutorial");
        speeches.Add(levelTutorial);
        // Load mentor level one speech
        var levelOne = new List<string>();
        levelOne.Add("Welcome to you first boss fight! Defeat the boss by throwing bubbles at it");
        speeches.Add(levelOne);
        // Load mentor level two speech
        var levelTwo = new List<string>();
        levelTwo.Add("Welcome to you second boss fight! Defeat the boss by throwing bubbles at it");
        speeches.Add(levelTwo);
        // Load mentor level three speech
        var levelThree = new List<string>();
        levelThree.Add("Welcome to you third boss fight! Defeat the boss by throwing bubbles at it");
        speeches.Add(levelThree);
        // Load mentor level four speech
        var levelFour = new List<string>();
        levelFour.Add("Welcome to you fourth boss fight! Defeat the boss by throwing bubbles at it");
        speeches.Add(levelFour);
        // Load mentor other speeches
        var others = new List<string>();
        others.Add("Not enough ammo, go back to collect more.");
        speeches.Add(others);
    }


    /// <summary>
    /// Select the current speech list depending on the level
    /// </summary>
    private void LoadCurrentLevelSpeech()
    {
        int level = 0;
        currectSpeech = speeches[level];
    }

    public void Act(Vector2 position, int index)
    {
        GoTo(position);
        Speak(index);        
    }

    private void GoTo(Vector2 position)
    {
        mentorRigidbody.position = Vector2.MoveTowards(mentorRigidbody.position, position, 20);
        
    }

    private void Speak(int index)
    {
        bubbleSpeech.sprite = bubbles[index];
        Debug.Log("Index: " + index);
    }
}
