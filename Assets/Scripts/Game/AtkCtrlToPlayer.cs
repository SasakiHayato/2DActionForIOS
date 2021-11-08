using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkCtrlToPlayer : MonoBehaviour
{
    IDamageble _damageble;
    public bool IsHit { get; set; } = false;
    
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        _damageble = player.GetComponent<IDamageble>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageble damageble = collision.gameObject.GetComponent<IDamageble>();
        if (damageble == null) return;
        IsHit = true;
        int add = _damageble.AddDamage();
        damageble.GetDamage(add);
    }
}
