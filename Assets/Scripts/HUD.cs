using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public Sprite[] HeartSprites;
    public Image HeartUI;
    private UnityStandardAssets._2D.PlatformerCharacter2D player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<UnityStandardAssets._2D.PlatformerCharacter2D>();

    }

    void Update()
    {
        HeartUI.sprite = HeartSprites[player.curHealth];

    }

}
