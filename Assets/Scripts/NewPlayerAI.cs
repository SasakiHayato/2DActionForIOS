using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerAI
{
    IEnemys _near;
    
    public void SetNiarEnemy(Transform player)
    {
        if (FieldManagement.EnmysList.Count <= 0) return;

        float check = float.MaxValue;
        FieldManagement.EnmysList.ForEach(e => 
        {
            Debug.Log(e.GetObj().transform.position);
            float distance = Vector2.Distance(player.position, e.GetObj().transform.position);
            if (check > distance)
            {
                check = distance;
                _near = e;
            }
        });
    }
}
