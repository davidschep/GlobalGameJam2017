//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//namespace GlobalGamejam
//{
//    public class EnemyManager : MonoBehaviour
//    {
//        public GameObject m_FictionalEnemy; // This is just a list for positions, as soon as a enemy is in the screen we start creating the enemy. This saves performance

//        void Start()
//        {
//            CreateFictionalEnemy();
//        }

//        public void CreateFictionalEnemy()
//        {
//            if (m_FictionalEnemy != null)
//                Destroy(m_FictionalEnemy);
//            GameObject f = new GameObject();
//            m_FictionalEnemy = f;
//            f.transform.position = FindRandomPoint();
//        }

//        private Vector2 FindRandomPoint()
//        {
//            return new Vector2(Random.Range(-15, 15), Random.Range(20, 50));
//        }
//    }
//}