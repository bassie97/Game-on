using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class GameSound : MonoBehaviour
{

    public AudioClip sound;

    private AudioSource source { get { return GetComponent<AudioSource>(); } }

    // Use this for initialization
    void Start()
    {
        gameObject.AddComponent<AudioSource>();
        source.clip = sound;
        source.Play();

    }
}
