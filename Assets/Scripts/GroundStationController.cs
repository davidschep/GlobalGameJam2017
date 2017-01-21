using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlobalGamejam
{
    public class GroundStationController : MonoBehaviour
    {
        private EnemyManager m_EnemyManager;
        private SoundManager m_SoundManager;

        private Transform m_Dish;
        [HideInInspector]
        public GroundStationPosition m_GroundStationPosition = GroundStationPosition.left;
        [SerializeField]
        private float m_DishMoveSpeed = 10f;

        void Start()
        {
            m_Dish = transform.FindChild("Dish");
            m_EnemyManager = GameObject.Find("GameManager").GetComponent<EnemyManager>();
            m_SoundManager = m_EnemyManager.GetComponent<SoundManager>();
        }

        void Update()
        {
            foreach (GameObject go in m_EnemyManager.m_FictionalEnemies)
            {
                if (m_Dish.GetComponent<BoxCollider2D>().bounds.Contains(go.transform.position))
                    go.GetComponent<FictionalEnemy>().m_BeepTimer -= m_SoundManager.PlaySpaceShipApproachingSound(go.GetComponent<FictionalEnemy>().m_Difficulty, Vector2.Distance(go.transform.position, transform.position), go.GetComponent<FictionalEnemy>().m_BeepTimer);
            }
            switch (m_GroundStationPosition) // the turret will correct to a position according to what position it's set to.
            {
                case GroundStationPosition.left:
                    m_Dish.eulerAngles = new Vector3(0, 0, Mathf.LerpAngle(m_Dish.eulerAngles.z, 135, m_DishMoveSpeed * Time.deltaTime));
                    break;
                case GroundStationPosition.up:
                    m_Dish.eulerAngles = new Vector3(0, 0, Mathf.LerpAngle(m_Dish.eulerAngles.z, 90, m_DishMoveSpeed * Time.deltaTime));
                    break;
                case GroundStationPosition.right:
                    m_Dish.eulerAngles = new Vector3(0, 0, Mathf.LerpAngle(m_Dish.eulerAngles.z, 45, m_DishMoveSpeed * Time.deltaTime));
                    break;
            }
        }
    }

    public enum GroundStationPosition
    {
        left, up, right
    };
}