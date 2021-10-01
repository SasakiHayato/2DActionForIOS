using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideCheckToAttack
{
    AttackClass m_attack = new AttackClass();

    public void IsSlide(GameObject get, float dir)
    {
        PlayerController player = get.GetComponent<PlayerController>();
        Vector2 setVec = new Vector2(player.AttackRange * dir, 0);

        m_attack.Set(setVec, get, GetEnumToGame.Parent.Player);
    }
}
