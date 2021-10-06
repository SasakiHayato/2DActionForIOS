using UnityEngine;
using System.Threading.Tasks;

public class AddForce
{
    float m_power = 50;

    public void Set(Rigidbody2D targetRb, Transform thisPos, Transform otherPos)
    {
        float angle = 0;
        float dir = thisPos.position.x - otherPos.position.x;

        if (dir > 0)
            angle = 45;

        else if (dir < 0)
            angle = 135;

        float rad = angle * Mathf.Deg2Rad;
        Vector2 force = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));

        targetRb.velocity = force * m_power;
        Task.Run(() => ReverseForce(force, targetRb));
    }

    void ReverseForce(Vector2 dir, Rigidbody2D rb)
    {
        Vector2 updateDir = Vector2.zero;
        for (float i = dir.x; i > 0; i -= 0.05f)
        {
            Debug.Log(i);
            updateDir = new Vector2(i * -1, dir.y);
            Debug.Log(updateDir * m_power);
            //rb.velocity = updateDir * 50;
        }
    }
}
