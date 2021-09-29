using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] Text m_scoreText;
    [SerializeField] GameObject m_hpCanvas;

    Slider m_slider;
    
    float m_totalScore;
    string m_text;

    public float GetHp {private get; set; }

    void Start()
    {
        m_slider = transform.Find("TimeSlider").GetComponent<Slider>();
        m_text = m_scoreText.text;

        SetHp();
    }

    void SetHp()
    {
        for (int i = 0; i < (int)GetHp; i++)
        {
            GameObject setImage = new GameObject();
            setImage.name = $"Hp :{i}";
            setImage.AddComponent<Image>();
            RectTransform rect = setImage.GetComponent<RectTransform>();

            rect.localScale = new Vector2(0.275f, 0.275f);
            setImage.transform.parent = m_hpCanvas.transform;
        }
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
