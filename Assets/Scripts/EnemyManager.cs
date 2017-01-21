using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlobalGamejam
{
    public class EnemyManager : MonoBehaviour
    {
        public List<GameObject> m_FictionalEnemies = new List<GameObject>(); // This is just a list for positions, as soon as a enemy is in the screen we start creating the enemy. This saves performance

        void Start()
        {
            CreateFictionalEnemy();
        }

        void Update()
        {
        }

        private void CreateFictionalEnemy()
        {
            GameObject f = new GameObject();
            f.AddComponent<FictionalEnemy>();
            m_FictionalEnemies.Add(f);
        }
    }
}