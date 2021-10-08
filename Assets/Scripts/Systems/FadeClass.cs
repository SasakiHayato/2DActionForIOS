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
    bool m_endFade = false;
    object m_type = null;

    float m_r, m_g, m_b, m_a;
    float m_fadeSpeed = 0.002f;

    GameObject m_target;
    SpriteRenderer m_render;
    Image m_image;

    void Update()
    {
        if (!m_isFade) return;

        if (m_render != null)
            m_render.color = new Color(m_r, m_g, m_b, m_a);

        else if (m_image != null)
            m_image.color = new Color(m_r, m_g, m_b, m_a);

        if (m_fade == FadeType.In)
        {
            m_a += m_fadeSpeed;
            if (m_a >= 1)
                m_endFade = true;
        }
        else if (m_fade == FadeType.Out)
        {
            m_a -= m_fadeSpeed;
            if (m_a <= 0)
                m_endFade = true;
        }

        if (m_endFade) Destroy(m_target);
    }

    public void SetFadeTarget<T>(T type, GameObject target) where T : Object
    {
        m_target = target;
        m_type = type;

        if (m_type == null) return;
        GetColor(target);
        GetAlfa();

        m_isFade = true;
    }

    void GetColor(GameObject target)
    {
        if (m_type.GetType() == typeof(SpriteRenderer))
        {
            m_render = target.GetComponent<SpriteRenderer>();

            m_r = m_render.color.r;
            m_g = m_render.color.g;
            m_b = m_render.color.b;
            m_a = m_render.color.a;
        }
        else if (m_type.GetType() == typeof(Image))
        {
            m_image = target.GetComponent<Image>();

            m_r = m_image.color.r;
            m_g = m_image.color.g;
            m_b = m_image.color.b;
            m_a = m_image.color.a;
        }
    }

    void GetAlfa()
    {
        if (m_a == 1)
            m_fade = FadeType.Out;
        else if (m_a == 0)
            m_fade = FadeType.In;
        else
            Destroy(m_target);
    }
}
