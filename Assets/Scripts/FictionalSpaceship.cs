using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlobalGamejam
{
    public class FictionalSpaceship
    {
        public int m_Difficulty = 1; // 1 to 10

        // set the frequency limits
        private const float m_minFrequency = 40;
        private const float m_maxFrequency = 160;
        // set the height values
        private const float m_maxHeight = 100;
        private const float m_minStartHeight = 40;
        private const float m_maxStartHeight = 70;
        // the current frequency
        private float m_currentFrequency;
        // the increment value
        private float m_frequencyIncrementValue;
        // the max increment value
        private float m_maxIncrementValue;
        // the max frequency respond difference
        private float m_maxFreqDifference;
        // the ground station
        GroundStationController m_groundStation;
        // the gamemanager
        GameManager m_manager;
        // the current y pos
        float y;
        GroundStationPosition m_pos;


        [HideInInspector]
        public float m_BeepTimer = 2f;
        private float m_MoveSpeed = 10f;

        public void Initialize(GameManager manager)
        {
            m_groundStation = GameObject.Find("GroundStation").GetComponent<GroundStationController>();
            m_manager = manager;
        }

        public void Activate()
        {
            SetValues();
            m_manager.StartCoroutine(UpdatePosition());
        }

        private IEnumerator UpdatePosition()
        {
            // if the freq diff is too high to respond
            if (Mathf.Abs(m_frequencyIncrementValue - m_groundStation.CurrentFrequency) > m_maxFreqDifference || m_pos != m_groundStation.m_GroundStationPosition)
            {
                y += m_MoveSpeed * Time.deltaTime;
                if(y > m_maxHeight)
                {
                    yield return new WaitForSeconds(Random.Range(1f, 5f));
                    Activate();
                    yield break;
                }
            }
            else
            {
                y -= m_MoveSpeed * Time.deltaTime / (m_Difficulty * .5f);
                if (y <= 0) yield break;
            }
            UpdateFrequency();
        }

        private void SetValues()
        {
            m_pos = (GroundStationPosition)Random.Range(0, 3);
            m_currentFrequency = Random.Range(m_minFrequency, m_maxFrequency);
            float rand = Random.Range(0, 100);
            if (rand < 75) m_Difficulty = Random.Range(0, 5);
            else m_Difficulty = Random.Range(5, 10);
            y = Random.Range(m_minStartHeight, m_maxStartHeight);
        }

        private void UpdateFrequency()
        {
            if (m_frequencyIncrementValue > 0)
            {
                m_frequencyIncrementValue += Random.Range(-10, 110) / 100 * Time.deltaTime;
                if (m_frequencyIncrementValue + m_currentFrequency >= m_maxFrequency) m_frequencyIncrementValue *= -1;
                m_currentFrequency += m_frequencyIncrementValue;
            }
            else if (m_frequencyIncrementValue < 0)
            {
                m_frequencyIncrementValue -= Random.Range(-10, 110) / 100 * Time.deltaTime;
                if (m_frequencyIncrementValue <= m_maxFrequency) m_frequencyIncrementValue *= -1;
                m_currentFrequency += m_frequencyIncrementValue;
            }
            else m_frequencyIncrementValue += 1 / 100 * Random.Range(-100, 100);
        }
    }
}