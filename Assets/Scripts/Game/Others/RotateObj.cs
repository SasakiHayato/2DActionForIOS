using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObj : MonoBehaviour
{
    [SerializeField] float _rate;
    void Update()
    {
        gameObject.transform.Rotate(0, 0, 1 / _rate);
    }
}
