using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlobalGamejam
{
    public enum GroundStationPosition
    {
        left, up, right
    };
    public class GroundStationController : MonoBehaviour
    {
        private static string m_spaceshipSpritePath = "Art/Spaceships";
        //private EnemyManager m_EnemyManager;
        private SoundManager m_SoundManager;
        private float m_currentFrequency;

        private Transform m_Dish;
        private BoxCollider2D m_Collider2D;
        [HideInInspector]
        public GroundStationPosition m_GroundStationPosition = GroundStationPosition.left;
        [SerializeField]
        private float m_DishMoveSpeed = 10f;

        private GameObject m_Button;
        private GameManager m_manager;

        // the possible spaceship sprites
        private Sprite[] m_spaceshipSprites;

        public float CurrentFrequency { get { return m_currentFrequency; } }

        void Start()
        {
            m_Dish = transform.FindChild("Dish");
            m_Button = GameObject.Find("UI_Canvas").transform.FindChild("Background Image").FindChild("AntennaSwitchBG").FindChild("AntennaSwitch").gameObject;
            m_Collider2D = transform.FindChild("Collider").GetComponent<BoxCollider2D>();
            m_manager = GameObject.Find("GameManager").GetComponent<GameManager>();
            m_SoundManager = m_manager.gameObject.GetComponent<SoundManager>();
            m_spaceshipSprites = Resources.LoadAll<Sprite>(m_spaceshipSpritePath);
        }

        public void UpdateFrequency(float freq)
        {
            m_currentFrequency = freq;
            GameObject.Find("WaveChannel").GetComponent<WaveChannel>().m_DistortedValue = freq / 25;
        }

        void Update()
        {
            switch (m_GroundStationPosition) // the turret will correct to a position according to what position it's set to.
            {
                case GroundStationPosition.left:
                    m_Dish.eulerAngles = new Vector3(0, 0, Mathf.LerpAngle(m_Dish.eulerAngles.z, 45, m_DishMoveSpeed * Time.deltaTime));
                    break;
                case GroundStationPosition.up:
                    m_Dish.eulerAngles = new Vector3(0, 0, Mathf.LerpAngle(m_Dish.eulerAngles.z, 0, m_DishMoveSpeed * Time.deltaTime));
                   
                    break;
                case GroundStationPosition.right:
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

        public void LaneMatchUpdate(int difficulty, float maxFreqDiff, float freqDiff)
        {
            if (m_SoundManager != null)
                m_SoundManager.PlaySpaceShipApproachingSound(difficulty,maxFreqDiff,freqDiff);
        }

        public void ShipLured(int difficulty)
        {
            m_GroundStationPosition = GroundStationPosition.up; SpawnVisualShip(difficulty);
            m_manager.MinigameManager.StartMinigame(m_manager, difficulty);
        }

        [SerializeField]
        private Sprite m_Ufo1;
        [SerializeField]
        private Sprite m_Ufo2;
        [SerializeField]
        private Sprite m_Ufo3;

        private void SpawnVisualShip(int difficulty)
        {
            GameObject ship = new GameObject();
            if (difficulty < 4)
                ship.AddComponent<SpriteRenderer>().sprite = m_Ufo1;
            else if(difficulty < 7)
                ship.AddComponent<SpriteRenderer>().sprite = m_Ufo2;
            else
                ship.AddComponent<SpriteRenderer>().sprite = m_Ufo3;
            ship.AddComponent<SpaceshipController>().StartFlying();
        }
    }
}