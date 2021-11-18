using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboUISetting : MonoBehaviour
{
    float _moveRate = 64;
    float _time;

    float _setX = 5;
    float _setY = 2;

    bool _setUp = false;
    GameObject _parent;

    void Update()
    {
        if (!_setUp) return;

        transform.Translate(transform.right.x / _moveRate, 0, 0);
        _time += Time.unscaledDeltaTime;
        if (_time > 0.5f) Destroy(_parent.gameObject);
    }

    public void GetData(int count)
    {
        Text txt = GetComponentInChildren<Text>();
        _parent = transform.root.gameObject;
        txt.text = $"{count} Combo";

        SetPos(count);
        SetRotate(count);
        _setUp = true;
    }

    void SetPos(int count)
    {
        Transform pTf = GameObject.FindGameObjectWithTag("Player").transform;
        Vector2 setV = Vector2.zero;

        if (count % 2 != 0) setV = new Vector2(pTf.position.x + _setX, pTf.position.y + _setY);
        else
        {
            setV = new Vector2(pTf.position.x - _setX, pTf.position.y + _setY);
            _moveRate *= -1;
        }
        _parent.transform.position = setV;
    }

    void SetRotate(int count)
    {
        Quaternion q = Quaternion.identity;
        if (count % 2 != 0) q = Quaternion.Euler(0, 0, 30);
        else
        {
            q = Quaternion.Euler(0, 0, 150);
            RectTransform rect = transform.GetChild(0).GetComponent<RectTransform>();
            rect.localScale *= -1;
        }
        _parent.transform.localRotation = q;
    }
}
