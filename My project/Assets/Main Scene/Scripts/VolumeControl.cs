using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    public AudioSource BGM;
    public Slider audioSlider;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        BGM.volume = audioSlider.value;
    }
}
