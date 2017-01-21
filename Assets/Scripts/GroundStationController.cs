﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlobalGamejam
{
    public class GroundStationController : MonoBehaviour
    {
        private EnemyManager m_EnemyManager;
        private SoundManager m_SoundManager;

        private Transform m_Dish;
        private BoxCollider2D m_Collider2D;
        [HideInInspector]
        public GroundStationPosition m_GroundStationPosition = GroundStationPosition.left;
        [SerializeField]
        private float m_DishMoveSpeed = 10f;

        private GameObject m_Button;

        void Start()
        {
            m_Dish = transform.FindChild("Dish");
            m_Button = GameObject.Find("UI_Canvas").transform.FindChild("Background Image").FindChild("AntennaSwitchBG").FindChild("AntennaSwitch").gameObject;
            m_Collider2D = transform.FindChild("Collider").GetComponent<BoxCollider2D>();
            m_EnemyManager = GameObject.Find("GameManager").GetComponent<EnemyManager>();
            m_SoundManager = m_EnemyManager.GetComponent<SoundManager>();
        }

                void Update()
        {
                if (!m_Collider2D.bounds.Contains(m_EnemyManager.m_FictionalEnemy.transform.position))
                    m_EnemyManager.CreateFictionalEnemy();
            switch (m_GroundStationPosition) // the turret will correct to a position according to what position it's set to.
            {
                case GroundStationPosition.left:
                    m_Dish.eulerAngles = new Vector3(0, 0, Mathf.LerpAngle(m_Dish.eulerAngles.z, 45, m_DishMoveSpeed * Time.deltaTime));
                    if (m_Collider2D.bounds.Contains(m_EnemyManager.m_FictionalEnemy.transform.position) && m_EnemyManager.m_FictionalEnemy.transform.position.x < -5)
                        m_EnemyManager.m_FictionalEnemy.GetComponent<FictionalEnemy>().m_BeepTimer -= m_SoundManager.PlaySpaceShipApproachingSound(m_EnemyManager.m_FictionalEnemy.GetComponent<FictionalEnemy>().m_Difficulty, Vector2.Distance(m_EnemyManager.m_FictionalEnemy.transform.position, transform.position), m_EnemyManager.m_FictionalEnemy.GetComponent<FictionalEnemy>().m_BeepTimer);
                    break;
                case GroundStationPosition.up:
                    m_Dish.eulerAngles = new Vector3(0, 0, Mathf.LerpAngle(m_Dish.eulerAngles.z, 0, m_DishMoveSpeed * Time.deltaTime));
                    if (m_Collider2D.bounds.Contains(m_EnemyManager.m_FictionalEnemy.transform.position) && m_EnemyManager.m_FictionalEnemy.transform.position.x > -5 && m_EnemyManager.m_FictionalEnemy.transform.position.x < 5)
                        m_EnemyManager.m_FictionalEnemy.GetComponent<FictionalEnemy>().m_BeepTimer -= m_SoundManager.PlaySpaceShipApproachingSound(m_EnemyManager.m_FictionalEnemy.GetComponent<FictionalEnemy>().m_Difficulty, Vector2.Distance(m_EnemyManager.m_FictionalEnemy.transform.position, transform.position), m_EnemyManager.m_FictionalEnemy.GetComponent<FictionalEnemy>().m_BeepTimer);
                    break;
                case GroundStationPosition.right:
                    if (m_Collider2D.bounds.Contains(m_EnemyManager.m_FictionalEnemy.transform.position) && m_EnemyManager.m_FictionalEnemy.transform.position.x > 5)
                        m_EnemyManager.m_FictionalEnemy.GetComponent<FictionalEnemy>().m_BeepTimer -= m_SoundManager.PlaySpaceShipApproachingSound(m_EnemyManager.m_FictionalEnemy.GetComponent<FictionalEnemy>().m_Difficulty, Vector2.Distance(m_EnemyManager.m_FictionalEnemy.transform.position, transform.position), m_EnemyManager.m_FictionalEnemy.GetComponent<FictionalEnemy>().m_BeepTimer);
                    m_Dish.eulerAngles = new Vector3(0, 0, Mathf.LerpAngle(m_Dish.eulerAngles.z, -45, m_DishMoveSpeed * Time.deltaTime));
                    break;
            }
        }

        private int m_ButtonPosition = 2;

        public void ClickButton()
        {
            switch (m_ButtonPosition)
            {
                case 1:
                    m_Button.transform.eulerAngles = new Vector3(0, 0, 0);
                    m_GroundStationPosition = GroundStationPosition.up;
                    m_ButtonPosition = 2;
                    break;
                case 2:
                    m_Button.transform.eulerAngles = new Vector3(0, 0, -55);
                    m_GroundStationPosition = GroundStationPosition.right;
                    m_ButtonPosition = 3;
                    break;
                case 3:
                    m_Button.transform.eulerAngles = new Vector3(0, 0, 55);
                    m_GroundStationPosition = GroundStationPosition.left;
                    m_ButtonPosition = 1;
                    break;
            }
        }
    }

    public enum GroundStationPosition
    {
        left, up, right
    };
}