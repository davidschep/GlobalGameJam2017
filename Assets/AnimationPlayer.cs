using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPlayer : MonoBehaviour
{
    [SerializeField]
    private Sprite[] m_Sprites = new Sprite[7];

    private float m_Timer = 0.0f;

    void Update()
    {
        m_Timer += Time.deltaTime;
        if (m_Timer > 0.7)
            Destroy(gameObject);
        GetComponent<SpriteRenderer>().sprite = m_Sprites[Mathf.RoundToInt((m_Timer * 10))];
    }
}
