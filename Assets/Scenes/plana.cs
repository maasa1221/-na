using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plana : MonoBehaviour
{   
    AudioSource m_MyAudioSource;
    float m_MySliderValue;
    void Start()
    {
        //Initiate the Slider value to half way
        m_MySliderValue = 0.1f;
        //Fetch the AudioSource from the GameObject
        m_MyAudioSource = GetComponent<AudioSource>();
        //Play the AudioClip attached to the AudioSource on startup
        m_MyAudioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        m_MyAudioSource.volume = m_MySliderValue;
    }
}


   