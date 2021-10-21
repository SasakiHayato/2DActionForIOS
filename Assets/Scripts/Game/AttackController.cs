using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    [SerializeField] GameObject _parent;
    IDamageble _damageble;

    void Start()
    {
        _damageble = _parent.GetComponent<IDamageble>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageble damageble = collision.gameObject.GetComponent<IDamageble>();

        if (damageble == null) return;

        float add = _damageble.AddDamage();
        damageble.GetDamage(add);
    }
}
