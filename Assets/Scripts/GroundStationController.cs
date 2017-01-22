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
        private GameObject m_heightPointer;
        private const float m_minHeightRot = 138;
        private const float m_maxHeightRot = -138;

        // the warning sprite
        private int m_enabledWarningType;
        private GameObject[] m_warningSprite;

        // the possible spaceship sprites
        private Sprite[] m_spaceshipSprites;
        private GameObject m_warningWindow;

        private GameObject m_ship;
        public GameObject Ship { get { return m_ship; } }

        public float CurrentFrequency { get { return m_currentFrequency; } }

        void Start()
        {
            m_Dish = transform.FindChild("Dish");
            m_Button = GameObject.Find("UI_Canvas").transform.FindChild("AntennaSwitchBG").FindChild("AntennaSwitch").gameObject;
            m_Collider2D = transform.FindChild("Collider").GetComponent<BoxCollider2D>();
            m_manager = GameObject.Find("GameManager").GetComponent<GameManager>();
            m_SoundManager = m_manager.gameObject.GetComponent<SoundManager>();
            m_spaceshipSprites = Resources.LoadAll<Sprite>(m_spaceshipSpritePath);
            m_GroundStationPosition = GroundStationPosition.up;
            m_heightPointer = GameObject.Find("UI_Canvas").transform.FindChild("HeightIndicator").FindChild("Pointer").gameObject;

            // the warning spaceship sprites
            m_warningWindow = GameObject.Instantiate(Resources.Load<GameObject>("Art/Spaceships/WarningFrame"));
            m_warningWindow.transform.localScale = Vector3.one * 5;
            m_warningWindow.SetActive(false);
            m_warningWindow.transform.position = new Vector3(5.42f, 2.03f);
            m_warningSprite = new GameObject[3];
            for (int i = 0; i < 3; i++)
            {
                m_warningSprite[i] = GameObject.Instantiate(Resources.Load<GameObject>("Art/Spaceships/" + i));
                m_warningSprite[i].transform.SetParent(m_warningWindow.transform);
                m_warningSprite[i].transform.localPosition = new Vector2(0,-.045f);
                m_warningSprite[i].transform.localScale = Vector3.one * .15f;
                m_warningSprite[i].SetActive(false);
            }
        }

        public void UpdateHeight(float height, float maxHeight)
        {
            if(m_heightPointer != null)
            {
                float heightPercentage = height / maxHeight * 100;
                float angle = (m_maxHeightRot * 2 / 100) * heightPercentage + m_minHeightRot;
                m_heightPointer.transform.localEulerAngles = new Vector3(0, 0, angle);
            }
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
                m_SoundManager.PlaySpaceShipApproachingSound(difficulty, maxFreqDiff, freqDiff);
        }

        public void ShipLured(int difficulty)
        {
            SpawnVisualShip(difficulty);
            m_manager.MinigameManager.StartMinigame(m_manager, difficulty);
            GameObject anim = GameObject.Instantiate(Resources.Load("BlinkingLight") as GameObject, new Vector3(3.05f, -2.956f), transform.rotation);
            anim.transform.localScale = Vector2.one * 5;
        }

        [SerializeField]
        private Sprite m_Ufo1;
        [SerializeField]
        private Sprite m_Ufo2;
        [SerializeField]
        private Sprite m_Ufo3;

        private void SpawnVisualShip(int difficulty)
        {
            m_ship = new GameObject();
            if (difficulty < 4)
            {
                m_ship.AddComponent<SpriteRenderer>().sprite = m_Ufo1;
                EnableWarningSign(0);
            }
            else if (difficulty < 7)
            {
                m_ship.AddComponent<SpriteRenderer>().sprite = m_Ufo2;
                EnableWarningSign(1);
            }
            else
            {
                m_ship.AddComponent<SpriteRenderer>().sprite = m_Ufo3;
                EnableWarningSign(2);
            }
            m_ship.AddComponent<SpaceshipController>().StartFlying();
        }

        private void EnableWarningSign(int type)
        {
            m_warningWindow.SetActive(true);
            m_warningSprite[type].SetActive(true);
            m_enabledWarningType = type;
        }

        public void DisableWarningSign()
        {
            m_warningWindow.SetActive(false);
            m_warningSprite[m_enabledWarningType].SetActive(false);
        }
    }
}