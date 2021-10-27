using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForce : MonoBehaviour
{
    private static AddForce _instance = new AddForce();
    public static AddForce Instance => _instance;

    Rigidbody2D _rb;

    public static void Set(GameObject target, Quaternion dir)
    {
        float anlge = 0;
        if (dir.y == 0) anlge = Random.Range(120, 140);
        else if (dir.y == 1) anlge = Random.Range(45, 50);

        Instance._rb = target.GetComponent<Rigidbody2D>();

        float rad = anlge * Mathf.Deg2Rad;
        Vector2 setForce = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
        Instance._rb.velocity = setForce * 50;
    }
}

