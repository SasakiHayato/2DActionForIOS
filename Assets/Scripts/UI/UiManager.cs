using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] Text m_scoreText;
    Slider m_slider;
    
    float m_totalScore;
    string m_text;

    void Start()
    {
        m_slider = transform.GetChild(0).GetComponent<Slider>();
        m_text = m_scoreText.text;
    }

    public void SetSliderParam(bool get)
    {
        if (get)
            m_slider.value -= 0.5f * Time.deltaTime;
        else
            m_slider.value = m_slider.maxValue;
        
        if (m_slider.value <= 0)
            GameManager.Instance.DeidPlayer();
    }

    public void AddScore(float score)
    {
        m_totalScore += score;
        m_scoreText.text = m_totalScore.ToString(m_text);
    }
}
