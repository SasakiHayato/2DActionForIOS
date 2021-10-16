using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayersSpace;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [SerializeField] float m_moveDis;

    Rigidbody2D m_rb;
    Control m_control = new Control();

    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_control.SetUp(this);
    }

    void Update()
    {
        //m_control.Pressing();
        m_control.NewPressing();
    }

    public void Move()
    {
        float speed = m_moveDis / 0.2f;
        StartCoroutine(SetUpMove(speed));
    }

    IEnumerator SetUpMove(float speed)
    {
        m_rb.velocity = m_control.AttackDir * speed;
        yield return new WaitForSeconds(0.2f);
        m_rb.velocity = Vector2.zero;
    }

    public void Attack()
    {

    }
}

namespace PlayersSpace
{
    class Control
    {
        bool m_check = false;
        float m_pushTime;

        Vector2 m_origin = Vector2.zero;

        float m_distance;
        public Vector2 AttackDir { get; private set; } = Vector2.zero;
        public Vector2 MoveDir { get; private set; } = Vector2.zero;

        Vector2 m_savePos = Vector2.zero;
        
        Player m_player;
        public void SetUp(Player player) => m_player = player;

        public void NewPressing()
        {
            if (!Input.GetMouseButton(0)) return;

            Vector2 currentPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            float diff = Vector2.Distance(currentPos, m_savePos);
            if (m_savePos == Vector2.zero) diff = 0;
            m_savePos = currentPos;

            Debug.Log(diff);
        }











        public void Pressing()
        {
            bool isPush = Input.GetMouseButton(0);
            if (isPush)
            {
                m_pushTime += Time.deltaTime;
                if (!m_check) m_origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                m_check = true;
            }
            else
            {
                Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                SetMoveDir(SetAngle(mouse));
                Check(mouse);
                m_pushTime = 0;
            }
            
        }

        void Check(Vector2 endPos)
        {
            if (!m_check) return;
            m_check = false;

            SetAttackDir(SetAngle(endPos));
            
            if (m_distance > 1)
            {
                if (m_pushTime < 0.12f)
                    m_player.Attack();
                else
                    m_player.Move();
            }

            m_origin = Vector2.zero;
        }

        float SetAngle(Vector2 end)
        {
            m_distance = Vector2.Distance(m_origin, end);
            Vector2 distance = m_origin - end;
            distance.x *= -1;

            float angle = Mathf.Atan2(distance.x, distance.y) * Mathf.Rad2Deg;
            return angle - 90;
        }

        void SetAttackDir(float angle)
        {
            if (-45 <= angle && angle < 45)
                AttackDir = Vector2.right;
            else if (45 <= angle || angle < -225)
                AttackDir = Vector2.up;
            else if (-225 <= angle && angle < -135)
                AttackDir = Vector2.left;
            else if (-135 <= angle && angle < -45)
                AttackDir = Vector2.down;
        }

        void SetMoveDir(float angle)
        {
            if (-90 <= angle && angle < 90)
            {
                Debug.Log("‰E");
            }
            else if (90 <= angle || angle < -90)
            {
                Debug.Log("¶");
            }
        }
    }
}
