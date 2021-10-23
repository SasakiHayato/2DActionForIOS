using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    [SerializeField] GameObject _parent;
    
    IDamageble _damageble;
    IState _state;

    void Start()
    {
        _damageble = _parent.GetComponent<IDamageble>();
        _state = _parent.GetComponent<IState>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageble damageble = collision.gameObject.GetComponent<IDamageble>();
        IState state = collision.gameObject.GetComponent<IState>();

        if (damageble == null || state == null) return;

        float add = _damageble.AddDamage();
        damageble.GetDamage(add);
    }
}
