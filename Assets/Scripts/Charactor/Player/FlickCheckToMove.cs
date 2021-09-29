using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickCheckToMove
{
    GameObject m_parent;

    public void IsFrick(GameObject get, float dir)
    {
        m_parent = get;

        PlayerController player = get.GetComponent<PlayerController>();
        float set = player.Speed;
        Vector2 setVec = new Vector2(dir, 0);
        
        SetRay(setVec, get.transform, set);
        SetTrans(get, dir);
    }

    void SetRay(Vector2 dir, Transform parent,float distance)
    {
        LayerMask mask = LayerMask.GetMask("Wall");
        
        RaycastHit2D hit = Physics2D.Raycast(parent.position, dir, dir.magnitude * distance, mask);
        Vector3 setVec = dir * distance;
        if (!hit.collider)
            m_parent.transform.position += setVec;
    }

    void SetTrans(GameObject parent, float dir)
    {
        if (dir < 0)
            parent.transform.localRotation = Quaternion.Euler(0, 0, 0);
        else if (dir > 0)
            parent.transform.localRotation = Quaternion.Euler(0, 180, 0);
    }
}
