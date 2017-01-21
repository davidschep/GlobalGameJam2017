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
        private float m_maxFreqDifference = 20;
        // the ground station
        GroundStationController m_groundStation;
        // the gamemanager
        GameManager m_manager;
        // the current y pos
        float m_yPos;
        // if the fictional spaceship is active
        bool m_isActive;

        // the curent lane
        GroundStationPosition m_pos;


        [HideInInspector]
        public float m_BeepTimer = 2f;
        private float m_MoveSpeed = 10;

        public void Initialize(GameManager manager)
        {
                m_groundStation = GameObject.Find("GroundStation").GetComponent<GroundStationController>();
                m_manager = manager;
        }

        public void Activate()
        {
            if (!m_isActive)
            {
                m_isActive = true;
                SetValues();
                m_manager.StartCoroutine(UpdatePosition());
                m_manager.StartCoroutine(HandleBeeps());
            }
        }

        private IEnumerator UpdatePosition()
        {
            while(m_isActive)
            {
                Debug.Log("YPOS" + m_yPos + " SIDE " + m_pos + " FREQ DIFF " + Mathf.Abs(m_currentFrequency - m_groundStation.CurrentFrequency));
                // if the freq diff is too high to respond
                if (Mathf.Abs(m_currentFrequency - m_groundStation.CurrentFrequency) > m_maxFreqDifference || m_pos != m_groundStation.m_GroundStationPosition)
                {
                    m_yPos += (m_MoveSpeed / 100 * (Mathf.Abs(m_currentFrequency - m_groundStation.CurrentFrequency) / m_maxFrequency)) * Time.deltaTime * 15;
                    if (m_yPos > m_maxHeight)
                    {
                        m_manager.StopCoroutine(HandleBeeps());
                        yield return new WaitForSeconds(Random.Range(1f, 5f));
                        yield return null;
                        Activate();
                        yield break;
                    }
                }
                else
                {
                    m_yPos -= (m_MoveSpeed / 100 * (100 -(Mathf.Abs(m_currentFrequency - m_groundStation.CurrentFrequency) / m_maxFrequency))) * Time.deltaTime;
                    //Debug.Log("YPOS" + m_yPos + " SIDE " + m_pos + " FREQ DIFF " + Mathf.Abs(m_currentFrequency - m_groundStation.CurrentFrequency));
                    if (m_yPos <= 0)
                    {
                        m_groundStation.ShipLured(m_Difficulty);
                        m_isActive = false;
                        yield break;
                    }
                }
                //UpdateFrequency();
                yield return null;
            }
            m_isActive = false;
            yield break;
        }

        private IEnumerator HandleBeeps()
        {
            float startY = m_yPos;
            while(m_isActive)
            {
                if (m_pos == m_groundStation.m_GroundStationPosition)
                {
                    m_groundStation.LaneMatchUpdate(m_Difficulty, m_maxFrequency, Mathf.Abs(m_currentFrequency - m_groundStation.CurrentFrequency));
                }
                yield return null;
            }
            yield break;
        }

        private void SetValues()
        {
            m_pos = (GroundStationPosition)Random.Range(0, 3);
            m_currentFrequency = Random.Range(m_minFrequency, m_maxFrequency);
            float rand = Random.Range(0, 100);
            if (rand < 75) m_Difficulty = Random.Range(0, 5);
            else m_Difficulty = Random.Range(5, 10);
            m_yPos = Random.Range(m_minStartHeight, m_maxStartHeight);
            Debug.Log("CurrentFreq" + m_currentFrequency + " POS" + m_pos.ToString());
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
            else m_frequencyIncrementValue = 1f / 100f * Random.Range(-100f, 100f);
        }
    }
}