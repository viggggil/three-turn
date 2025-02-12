using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource BGMAudio;
    [SerializeField] AudioSource SFXAudio;
    public AudioClip bgm;
    void Start()
    {
        BGMAudio.clip = bgm;
        BGMAudio.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
