using UnityEngine;

public class AddForce
{
    float m_power = 50;

    public void Set(Rigidbody2D targetRb, Transform thisPos, Transform otherPos)
    {
        float angle = 0;
        float dir = thisPos.position.x - otherPos.position.x;

        if (dir > 0)
            angle = Random.Range(115, 140);

        else if (dir < 0)
            angle = Random.Range(25, 50);

        float rad = angle * Mathf.Deg2Rad;
        Vector2 force = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));

        targetRb.velocity = force * m_power;
    }
}
