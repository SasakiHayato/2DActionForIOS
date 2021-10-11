using UnityEngine;

public class AddForce
{
    float m_power = 50;

    public void Set<T>(Rigidbody2D targetRb, Transform thisPos, T set) where T : EnemyBase
    {
        Transform otherPos = GameObject.FindGameObjectWithTag("Player").transform;
        
        float angle = 0;
        float dir = thisPos.position.x - otherPos.position.x;

        if (dir > 0)
            angle = Random.Range(115, 140);

        else if (dir < 0)
            angle = Random.Range(40, 65);

        float rad = angle * Mathf.Deg2Rad;
        Vector2 forceAngle = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
        targetRb.velocity = forceAngle * m_power;
        set.Speed = targetRb.velocity.x;
    }
}
