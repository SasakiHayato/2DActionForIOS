using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] AnimationState _state;
    Rigidbody2D _rb;
    
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //AnimState.SetAnimOneShot(gameObject, "Player_attack");
        }
        
        _rb.velocity = new Vector2(h, _rb.velocity.y);
    }
}
