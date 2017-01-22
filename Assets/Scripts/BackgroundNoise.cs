using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundNoise : MonoBehaviour {

    [SerializeField]
    private AudioClip m_Noise;
    private AudioSource m_NoiseMaker;

    void Start()
    {
        m_NoiseMaker = GetComponent<AudioSource>();
        m_NoiseMaker.clip = m_Noise;
        m_NoiseMaker.Play();
    }

    void Update()
    {
        if (m_NoiseMaker.isPlaying == false)
            m_NoiseMaker.Play();
    }
}