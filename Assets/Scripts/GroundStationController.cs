using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlobalGamejam
{
    public class GroundStationController : MonoBehaviour
    {
        private EnemyManager m_EnemyManager;

        private Transform m_Dish;
        [HideInInspector]
        public GroundStationPosition m_GroundStationPosition = GroundStationPosition.left;
        [SerializeField]
        private float m_DishMoveSpeed = 10f;

        void Start()
        {
            m_Dish = transform.FindChild("Dish");
            m_EnemyManager = GameObject.Find("GameManager").GetComponent<EnemyManager>();
        }

        void Update()
        {
            switch (m_GroundStationPosition) // the turret will correct to a position according to what position it's set to.
            {
                case GroundStationPosition.left:
                    m_Dish.eulerAngles = new Vector3(0, 0, Mathf.LerpAngle(m_Dish.eulerAngles.z, 135, m_DishMoveSpeed * Time.deltaTime));
                    foreach (Vector2 v in m_EnemyManager.m_EnemyPositions)
                    {
                     //   if (IsPointInClockwiseTriangle(v, transform.position, new Vector2(-50, 0), new Vector2(0, 50))) 
                    }
                    break;
                case GroundStationPosition.up:
                    m_Dish.eulerAngles = new Vector3(0, 0, Mathf.LerpAngle(m_Dish.eulerAngles.z, 90, m_DishMoveSpeed * Time.deltaTime));
                    foreach (Vector2 v in m_EnemyManager.m_EnemyPositions)
                    {
                       // if (IsPointInClockwiseTriangle(v, transform.position, new Vector2(-25, 50), new Vector2(25, 50))) 
                    }
                    break;
                case GroundStationPosition.right:
                    m_Dish.eulerAngles = new Vector3(0, 0, Mathf.LerpAngle(m_Dish.eulerAngles.z, 45, m_DishMoveSpeed * Time.deltaTime));
                    foreach (Vector2 v in m_EnemyManager.m_EnemyPositions)
                    {
                        //if (IsPointInClockwiseTriangle(v, transform.position, new Vector2(50, 50), new Vector2(50, 0))) 
                    }
                    break;
            }
        }

        private bool IsPointInClockwiseTriangle(Vector2 p, Vector2 p0, Vector2 p1, Vector2 p2)
        {
            var s = (p0.y * p2.x - p0.x * p2.y + (p2.y - p0.y) * p.x + (p0.x - p2.x) * p.y);
            var t = (p0.x * p1.y - p0.y * p1.x + (p0.y - p1.y) * p.x + (p1.x - p0.x) * p.y);

            if (s <= 0 || t <= 0)
                return false;

            var A = (-p1.y * p2.x + p0.y * (-p1.x + p2.x) + p0.x * (p1.y - p2.y) + p1.x * p2.y);

            return (s + t) < A;
        }
    }

    public enum GroundStationPosition
    {
        left, up, right
    };
}