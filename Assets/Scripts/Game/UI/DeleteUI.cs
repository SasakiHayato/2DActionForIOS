using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System;

public class DeleteUI : MonoBehaviour
{
    SpriteRenderer _sprite;
    Image _img;

    Action _action;

    void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
        _img = GetComponent<Image>();
    }

    void Update()
    {
        if (_sprite != null)
            if (_sprite.color.a <= 0) _action?.Invoke();

        if (_img != null)
            if (_img.color.a <= 0) _action?.Invoke();
    }

    public void SetAction(Action set) => _action = set;
}
