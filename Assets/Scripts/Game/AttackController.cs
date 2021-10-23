using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    [SerializeField] GameObject _parent;
    
    IDamageble _damageble;
    CharaBase _chara;

    void Start()
    {
        _damageble = _parent.GetComponent<IDamageble>();
        _chara = _parent.GetComponent<CharaBase>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageble damageble = collision.gameObject.GetComponent<IDamageble>();
        IState state = collision.gameObject.GetComponent<IState>();

        if (damageble == null || state == null) return;

        _chara.Attack(state.Current);

        float add = _damageble.AddDamage();
        damageble.GetDamage(add);

        gameObject.SetActive(false);
    }
}
