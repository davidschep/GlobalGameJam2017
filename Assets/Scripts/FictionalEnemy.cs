using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlobalGamejam
{
    public class FictionalEnemy : MonoBehaviour
    {
        public int m_Difficulty = 1; // 1 to 10
        public Sprite m_Sprite;

        [HideInInspector]
        public float m_BeepTimer = 2f;
        [HideInInspector]
        public int m_RadioFrequency = 1; // 1 to 10
        [SerializeField]
        private float m_MoveSpeed = 10f;

        private Vector2 m_Direction;

        void Update()
        {
            transform.position += new Vector3(0, m_MoveSpeed * Time.deltaTime);
        }
    }
}