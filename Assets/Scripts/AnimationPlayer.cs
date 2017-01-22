using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPlayer : MonoBehaviour
{
    [SerializeField]
    private Sprite[] m_Sprites = new Sprite[7];
    [SerializeField]
    private bool m_Loop = false;
    [SerializeField]
    private float m_TimeBetweenFrames = 1f;

    private float m_Timer = 0.0f;

    void Update()
    {
        m_Timer += Time.deltaTime * m_TimeBetweenFrames;
        if (gameObject.name.Contains("Explosion"))
        {
            if(m_Timer >0.3f)
                GetComponent<SpriteRenderer>().sortingOrder = -4;
            if (m_Timer > 0.8f)
            {
                GameObject.Find("GroundStation").transform.position = new Vector3(0, 0);
                GameObject.Find("SpriteBackground").transform.position = new Vector3(0, 0);
            }
            else if (m_Timer > 0.3f)
            {
                GameObject.Find("GroundStation").transform.position = new Vector3(Random.Range(-1, 1) * Time.deltaTime * 5, Random.Range(-1, 1) * Time.deltaTime * 5);
                GameObject.Find("SpriteBackground").transform.position = new Vector3(Random.Range(-1, 1) * Time.deltaTime * 3, Random.Range(-1, 1) * Time.deltaTime * 3);
            }
        }
        if (m_Timer > (float)(m_Sprites.Length - 1) / 10)
                if (m_Loop)
                    m_Timer = 0;
                else
                    Destroy(gameObject);
        GetComponent<SpriteRenderer>().sprite = m_Sprites[Mathf.RoundToInt((m_Timer * 10))];
    }
}
