using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IEnemys
{
    public bool IsRockOn { get; set; }
    public GameObject GetObj() => gameObject;
}
