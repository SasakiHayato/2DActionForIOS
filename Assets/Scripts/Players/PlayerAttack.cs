using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] float _speed;

    public void GroundAttack(float distance)
    {
        StartCoroutine(Go());
    }

    IEnumerator Go()
    {
        yield return new WaitForSeconds(1f);
    }

    public void FloatingAttack()
    {

    }
}
