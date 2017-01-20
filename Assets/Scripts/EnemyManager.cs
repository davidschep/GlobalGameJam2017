using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlobalGamejam
{
    public class EnemyManager : MonoBehaviour
    {
        public List<Vector2> m_EnemyPositions = new List<Vector2>(); // This is just a list for positions, as soon as a enemy is in the screen we start creating the enemy. This saves performance

        void Start()
        {

        }

        void Update()
        {

        }
    }
}