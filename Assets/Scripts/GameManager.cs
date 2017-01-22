using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GlobalGamejam
{
    public class GameManager : MonoBehaviour
    {
        [HideInInspector]
        public int m_ResearchPoints = 0;
        private MinigameManager m_minigameManager;
        private FictionalSpaceship m_spaceShip;
        private GroundStationController m_groundStation;
        private Image m_researchImage;

        public MinigameManager MinigameManager { get { return m_minigameManager; } }

        void Start()
        {
            m_groundStation = GameObject.Find("GroundStation").GetComponent<GroundStationController>();
            m_researchImage = GameObject.Find("Wave (1)").GetComponent<Image>();
            m_spaceShip = new FictionalSpaceship();
            m_spaceShip.Initialize(this);
            m_minigameManager = new MinigameManager();
            m_minigameManager.Initialize();
            m_spaceShip.Activate();
            m_minigameManager.OnMinigameSuccess += OnSuccess;
            m_minigameManager.OnMinigameFailed += OnFailed;
        }

        private void OnSuccess(int difficulty)
        {
            m_ResearchPoints += difficulty;
            m_researchImage.fillAmount = m_ResearchPoints / 100;
            m_groundStation.Ship.GetComponent<SpaceshipController>().Die();
            m_spaceShip.Activate();
        }

        public void OnFailed()
        {
            m_ResearchPoints -= Random.Range(1, 5);
            m_researchImage.fillAmount = m_ResearchPoints / 100;
            Destroy(m_groundStation.Ship);
            m_spaceShip.Activate();
        }
    }
}