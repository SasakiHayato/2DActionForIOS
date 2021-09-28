using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    Slider m_slider;

    void Start()
    {
        m_slider = transform.GetChild(0).GetComponent<Slider>();
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
}
