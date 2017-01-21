using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlobalGamejam
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField]
        private AudioClip[] m_ApproachSounds = new AudioClip[10];
        private AudioSource m_AudioSource;
        private static float m_delay = 2;
        private float m_remainingDelay;

        void Start()
        {
            m_remainingDelay = m_delay;
            m_AudioSource = GetComponent<AudioSource>();
        }

        public void PlaySpaceShipApproachingSound(int difficulty, float maxFreqDiff, float freqDiff)
        {
            m_remainingDelay -= Time.deltaTime;
            if (m_remainingDelay <= 0)
            {
                m_AudioSource.PlayOneShot(m_ApproachSounds[difficulty]);
                m_remainingDelay = m_delay / 100 * (freqDiff / maxFreqDiff * 100) + .5f;
            }
        }
    }
}