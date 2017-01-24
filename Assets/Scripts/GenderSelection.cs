using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GenderSelection : MonoBehaviour {

    [SerializeField]
    private GameObject OnePlayerGenderSelectionPrefab;

    [SerializeField]
    private GameObject TwoPlayerGenderSelectionPrefab;


	// Use this for initialization
	void Start () {

        GameObject parent = GameObject.FindGameObjectWithTag("GenderSelection");
        if (GameController.Instance.AmountOfPlayers == 1)
        {
            GameObject prefab = (GameObject)Instantiate(OnePlayerGenderSelectionPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            prefab.transform.SetParent(parent.transform, false);
        }
        if (GameController.Instance.AmountOfPlayers == 2)
        {
            GameObject prefab = (GameObject)Instantiate(TwoPlayerGenderSelectionPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            prefab.transform.SetParent(parent.transform, false);
        }
    }

    public void ChangeToScene(int sceneToChangeTo)
    {
        SceneManager.LoadScene(sceneToChangeTo);
    }
}
