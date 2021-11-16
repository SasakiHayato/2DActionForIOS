using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySprite : MonoBehaviour
{
    SpriteRenderer _sprite;

    void Start() => _sprite = GetComponent<SpriteRenderer>();

    void Update()
    {
        if (_sprite.color.a <= 0) Destroy(gameObject);
    }
}
