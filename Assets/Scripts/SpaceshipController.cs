using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipController : MonoBehaviour
{
    private Vector3 m_Point = new Vector3(-2.5f, 2.5f);
    private float m_FlySpeed = 2f;
    private float m_Up = 0.4f;
    private float m_Dir = 0.4f;
    private float m_Pos = 0f;

    private bool m_FlyingAway = false;

    public void StartFlying()
    {
        GetComponent<SpriteRenderer>().sortingOrder = -1;
        transform.position = new Vector3(-10, 5);
    }

    void Update()
    {
        m_Pos = Mathf.Lerp(m_Pos, m_Dir, m_FlySpeed * Time.deltaTime);
        if (m_Pos > m_Up - 0.1f)
            m_Dir = 0f;
        if (m_Pos < 0f + 0.1f)
            m_Dir = m_Up;
        transform.position = Vector3.Lerp(transform.position, m_Point + new Vector3(0, m_Pos), m_FlySpeed * Time.deltaTime);

        if (m_FlyingAway && Vector3.Distance(transform.position, m_Point) < 1f)
            Destroy(gameObject);
    }

    public void Die()
    {
        GameObject.Instantiate(Resources.Load("Explosion") as GameObject, new Vector3(-1.5f, 3.5f), transform.rotation);
        Destroy(gameObject);
    }

    public void FlyAway()
    {
        m_Point = new Vector3(10, 7);
        m_FlyingAway = true;
    }
}