using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveChannel : MonoBehaviour
{
    public float m_DistortedValue = 1f;
    public float m_DistortionSpeed = 1f; // in seconds
    private RectTransform[] m_Particles;

    private float m_Timer = 0;

    [SerializeField]
    private WaveChannelSetting m_WaveChannelSetting = WaveChannelSetting.OddParticlesRandom;

    void Start()
    {
        m_Particles = transform.GetComponentsInChildren<RectTransform>();
    }

    void Update()
    {
        m_Timer -= Time.deltaTime;
        if (m_Timer < 0)
        {
            m_Timer = m_DistortionSpeed;
            // foreach (RectTransform t in m_Particles)
            switch (m_WaveChannelSetting)
            {
                case WaveChannelSetting.FullyRandom:
                    for (int i = 0; i < m_Particles.Length; i++)
                        if (m_Particles[i].gameObject.name != "WaveChannel")
                                m_Particles[i].anchoredPosition = new Vector3(m_Particles[i].anchoredPosition.x, -27 + Random.Range(-m_DistortedValue, m_DistortedValue));
                    break;
                case WaveChannelSetting.OddParticlesRandom:
                    for (int i = 0; i < m_Particles.Length; i++)
                        if (m_Particles[i].gameObject.name != "WaveChannel")
                            if (IsOdd(i))
                                m_Particles[i].anchoredPosition = new Vector3(m_Particles[i].anchoredPosition.x, -27 + Random.Range(-m_DistortedValue, m_DistortedValue));

                    for (int i = 0; i < m_Particles.Length; i++)
                        if (m_Particles[i].gameObject.name != "WaveChannel")
                            if (!IsOdd(i))
                                try
                                {
                                    m_Particles[i].anchoredPosition = new Vector3(m_Particles[i].anchoredPosition.x, (m_Particles[i - 1].anchoredPosition.y + m_Particles[i + 1].anchoredPosition.y) / 2);
                                }
                                catch { }
                    break;
            }
        }
    }

    private bool IsOdd(int value)
    {
        return value % 2 != 0;
    }
}

public enum WaveChannelSetting
{
    FullyRandom, OddParticlesRandom
};