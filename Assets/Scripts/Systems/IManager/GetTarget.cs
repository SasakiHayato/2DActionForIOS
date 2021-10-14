using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetTarget : IManager
{
    [SerializeField] Sprite m_sprite;
    [SerializeField] Color m_defColor;
    
    GameObject m_target;
    Player m_player;

    public void Execution()
    {
        Call();

        if (m_player.NearEnemy[0] == Vector2.zero)
            m_target.SetActive(false);
        else
            m_target.SetActive(true);

        m_target.transform.position = m_player.NearEnemy[0];
    }

    void Call()
    {
        if (m_target == null)
        {
            m_target = new GameObject();
            m_target.name = "TargetObject";
            Set();
        }

        if (m_player == null)
        {
            m_player = GameObject.FindObjectOfType<Player>();
        }
    }

    void Set()
    {
        Canvas();
        Image();
    }

    void Canvas()
    {
        Canvas setCanvas = m_target.AddComponent<Canvas>();
        RectTransform rect = setCanvas.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(3, 3);
    }

    void Image()
    {
        GameObject image = new GameObject();
        image.name = "TargetImage";

        image.transform.parent = m_target.transform;
        Image setImage = image.AddComponent<Image>();
        RectTransform rect = setImage.GetComponent<RectTransform>();

        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;

        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.zero;

        setImage.sprite = m_sprite;
        setImage.color = m_defColor;
    }
}

