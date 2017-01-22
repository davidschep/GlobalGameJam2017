using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPlayer : MonoBehaviour
{
    [SerializeField]
    private Sprite[] m_Sprites = new Sprite[7];
    [SerializeField]
    private bool m_Loop = false;

    private float m_Timer = 0.0f;

    void Update()
    {
        m_Timer += Time.deltaTime;
        if (m_Timer > (float)(m_Sprites.Length - 1) / 10)
            if (m_Loop)
                m_Timer = 0;
            else
                Destroy(gameObject);
        GetComponent<SpriteRenderer>().sprite = m_Sprites[Mathf.RoundToInt((m_Timer * 10))];
    }
}
