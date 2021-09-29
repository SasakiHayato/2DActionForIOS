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
    }

    void SetRay(Vector2 dir, Transform parent,float distance)
    {
        LayerMask mask = LayerMask.GetMask("Wall");
        
        RaycastHit2D hit = Physics2D.Raycast(parent.position, dir, dir.magnitude * distance, mask);
        Vector2 setVec = dir * distance;
        if (hit.collider)
        {
            Debug.Log("AA");
        }
        else
            Move(setVec);
    }

    void Move(Vector3 distance)
    {
        m_parent.transform.position += distance;
    }
}
