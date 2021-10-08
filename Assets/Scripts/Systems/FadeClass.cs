using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeClass : MonoBehaviour
{
    private enum FadeType
    {
        In,
        Out,

        None,
    }
    FadeType m_fade = FadeType.None;

    bool m_isFade = false;
    object m_type = null;

    float m_r, m_g, m_b, m_a;

    void Update()
    {
        if (!m_isFade) return;

        if (m_fade == FadeType.In)
        {

        }
        else if (m_fade == FadeType.Out)
        {

        }
    }

    public void SetFadeTarget<T>(T type, GameObject target) where T : Object
    {
        if (m_type == null) return;

        m_type = type;
        GetColor(target);
        GetAlfa();

        m_isFade = true;
    }

    void GetColor(GameObject target)
    {
        if (m_type.GetType() == typeof(SpriteRenderer))
        {
            SpriteRenderer render = target.GetComponent<SpriteRenderer>();

            m_r = render.color.r;
            m_g = render.color.g;
            m_b = render.color.b;
            m_a = render.color.a;

            return;
        }
        else if (m_type.GetType() == typeof(Image))
        {
            Image image = target.GetComponent<Image>();

            m_r = image.color.r;
            m_g = image.color.g;
            m_b = image.color.b;
            m_a = image.color.a;

            return;
        }
    }

    void GetAlfa()
    {
        if (m_a == 1)
            m_fade = FadeType.In;
        else if (m_a == 0)
            m_fade = FadeType.Out;
        else
            m_fade = FadeType.None;
    }
}
