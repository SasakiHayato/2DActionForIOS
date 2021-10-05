using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GetParent;

public class AttackClass
{
    Transform m_hitPos;
    public Transform HitPos { get => m_hitPos; private set { m_hitPos = value; } }

    public void Set(GameObject parent, Vector2 dir, float range, Parent type)
    {
        LayerMask layer;
        if (type == Parent.Player)
            layer = LayerMask.GetMask("Enemy");
        else
            layer = LayerMask.GetMask("Player");

        RaycastHit2D hit = Physics2D.Raycast(parent.transform.position, dir, dir.magnitude * range, layer);
        if (hit.collider)
        {
            HitPos = hit.collider.gameObject.transform;
            float add = parent.GetComponent<IDamageble>().AddDamage();
            IDamageble get = hit.collider.GetComponent<IDamageble>();
            get.GetDamage(add);
        }
    }
}
