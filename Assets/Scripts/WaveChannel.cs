using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveChannel : MonoBehaviour
{
    public float m_DistortedValue = 1f;
    public float m_DistortionSpeed = 1f; // in seconds
    private RectTransform m_Particles;

    private float m_Timer = 0;

    void Start()
    {
        m_Particles = transform.GetComponentInChildren<RectTransform>();
    }

    void Update()
    {
        m_Timer -= Time.deltaTime;
        if (m_Timer < 0)
        {
            m_Timer = m_DistortionSpeed;
            foreach (RectTransform t in m_Particles)
                t.anchoredPosition = new Vector3(t.anchoredPosition.x, -27 + Random.Range(-m_DistortedValue, m_DistortedValue));
        }
    }
}
