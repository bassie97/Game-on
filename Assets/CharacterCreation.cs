using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class CharacterCreation : MonoBehaviour {

    private List<GameObject> models;

    private int SelectionIndex = 0;
    //Default index of the model;

	// Use this for initialization
	void Start () {
        models = new List<GameObject>();
        foreach(Transform t in transform)
        {
            models.Add(t.gameObject);
            t.gameObject.SetActive(false);
        }

        models[SelectionIndex].SetActive(true);
	}
	
	public void Select(int index)
    {
        if(index == SelectionIndex)
            return;


        //make sure that whe don't get a nullpointer
        if (index < 0 || index >= models.Count)
            return;

        models[SelectionIndex].SetActive(false);
        SelectionIndex = index;
        models[SelectionIndex].SetActive(true);

        //update the gamecontroller
        if (this.name == "Player1")
            GameController.Instance.Char1Model1 = index;

        if (this.name == "Player2")
            GameController.Instance.Char2Model1 = index;

    }
}
