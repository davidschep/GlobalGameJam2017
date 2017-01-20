using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlobalGamejam
{
    public class EnemyProperties
    {
        public Vector2 m_EnemyPosition = new Vector2(50, 50);

        public int m_Difficulty = 1;
        public Sprite m_Sprite;

        public float m_BeepTimer = 2f;
    }
}