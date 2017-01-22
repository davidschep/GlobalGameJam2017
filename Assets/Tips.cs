using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tips : MonoBehaviour
{
    [SerializeField]
    private string[] m_Tips = new string[3];
    [SerializeField]
    private float m_WaitTime = 2f;
    private int m_Counter = 0;

    private Text m_Text;

    void Start()
    {
        m_Text = GetComponent<Text>();
        m_Text.text = m_Tips[m_Counter];
    }

    public void StartTutorial()
    {
        m_Counter++;
        StartCoroutine(TipsTimer());
        transform.parent.GetComponent<Button>().enabled = false;
    }

    private IEnumerator TipsTimer()
    {
        m_Text.text = m_Tips[m_Counter];
        yield return new WaitForSeconds(m_WaitTime);
        if (m_Counter < m_Tips.Length - 1)
        {
            m_Counter++;
            StartCoroutine(TipsTimer());
        }
    }
}