using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeleteUI : MonoBehaviour
{
    SpriteRenderer _sprite;
    Image _img;

    void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
        _img = GetComponent<Image>();
    }

    void Update()
    {
        if (_sprite != null) 
            if (_sprite.color.a <= 0) Destroy(gameObject);

        if (_img != null)
            if (_img.color.a <= 0) Destroy(gameObject);
    }
}
