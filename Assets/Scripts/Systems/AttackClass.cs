using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GetEnumToGame;
using AudioType;

public class AttackClass
{
    bool m_attackBool = false;
    public bool GetAttack { get => m_attackBool; set { m_attackBool = value; } }

    public void Set(Vector2 setVec, GameObject parent, Parent type)
    {
        LayerMask setLayer;

        if (type == Parent.Player)
            setLayer = LayerMask.GetMask("Enemy");
        else
            setLayer = LayerMask.GetMask("Player");
        
        ShotRay(setVec, parent, setLayer);
    }

    void ShotRay(Vector2 setVec, GameObject parent, LayerMask layer)
    {
        Vector2 thisVec = parent.transform.position;
        RaycastHit2D hit = Physics2D.Raycast(thisVec, setVec, setVec.magnitude, layer);
        
        if (hit.collider)
        {
            IDamageble otherI = hit.collider.GetComponent<IDamageble>();
            IDamageble thisI = parent.GetComponent<IDamageble>();

            Attack(thisI, otherI);
            SetPostion(parent.transform, hit.collider.transform);
        }
    }

    void Attack(IDamageble thisI, IDamageble otherI)
    {
        GameManager.Instance.PlayAudio(ClipType.Slashing, BGMType.None);

        float add = thisI.AddDamage();
        otherI.GetDamage(add);

        m_attackBool = true;
    }

    void SetPostion(Transform parent, Transform other)
    {
        SetPosClass pos = new SetPosClass();
        pos.Set(parent.position.x, other.position.x);
        parent.position = new Vector2(pos.SetPosX, parent.position.y);
    }
}

class SetPosClass
{
    public float SetPosX { get; private set; }

    public void Set(float parentX, float otherX)
    {
        if (parentX > otherX)
            SetPosX = otherX - 1;
        else
            SetPosX = otherX + 1;
    }
}
