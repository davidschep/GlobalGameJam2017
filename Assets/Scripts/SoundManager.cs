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

        void Start()
        {
            m_AudioSource = GetComponent<AudioSource>();
        }

        void Update()
        {

        }

        public float PlaySpaceShipApproachingSound(int difficulty, float distance, float beepTimer)
        {
            if (beepTimer - ((distance / 10) * Time.deltaTime) < 0)
            {
                m_AudioSource.PlayOneShot(m_ApproachSounds[difficulty]);
                return -distance / 10;
            }
            else
                return (distance / 10) * Time.deltaTime;
        }
    }
}