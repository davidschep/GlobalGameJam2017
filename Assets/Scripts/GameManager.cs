using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlobalGamejam
{
    public class GameManager : MonoBehaviour
    {
        [HideInInspector]
        public int m_ResearchPoints = 0;
        private MinigameManager m_minigameManager;

        void Start()
        {
            m_minigameManager = new MinigameManager();
            m_minigameManager.Initialize();
            m_minigameManager.StartMinigame(this,10);
        }

        void Update()
        {

        }
    }
}