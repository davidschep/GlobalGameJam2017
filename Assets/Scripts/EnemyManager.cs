using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlobalGamejam
{
    public class EnemyManager : MonoBehaviour
    {
        public List<EnemyProperties> m_EnemyProperties = new List<EnemyProperties>(); // This is just a list for positions, as soon as a enemy is in the screen we start creating the enemy. This saves performance

        void Start()
        {
            m_EnemyProperties.Add(new EnemyProperties());
            m_EnemyProperties[0].m_EnemyPosition = new Vector2(-10, 5);
        }

        void Update()
        {
        }
    }
}