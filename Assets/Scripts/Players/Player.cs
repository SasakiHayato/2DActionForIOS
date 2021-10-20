using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayersSpace;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [SerializeField] float m_moveDis;

    [SerializeField] Collider2D m_attackCol;
    Rigidbody2D m_rb;

    Control m_control = new Control();
    PlayerAI m_ai = new PlayerAI();

    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_attackCol.enabled = false;

        m_control.SetUp(this);
        m_ai.SetUp(gameObject);
    }
    
    void Update()
    {
        m_ai.UpDate();
        SetDir();

        float h = Input.GetAxisRaw("Horizontal");
        m_rb.velocity = new Vector2(h * 6, m_rb.velocity.y);
    }

    void SetDir()
    {
        if (m_ai.NearEnemy == null) return;

        float enemyPosX = m_ai.NearEnemy.GetObj().transform.position.x;
        Quaternion dir = Quaternion.identity;

        if (enemyPosX < transform.position.x) dir = Quaternion.Euler(0, 180, 0);
        else dir = Quaternion.Euler(0, 0, 0);
        
        transform.localRotation = dir;
    }

    public void Move(Vector2 dir)
    {
        //float speed = m_moveDis / 0.2f;
        //StartCoroutine(SetUpMove(speed, dir));
    }

    IEnumerator SetUpMove(float speed, Vector2 setVce)
    {
        m_rb.velocity = setVce * speed;
        yield return new WaitForSeconds(0.2f);
        m_rb.velocity = Vector2.zero;
    }

    public void Attack()
    {
        //IAttacks[0].SetUpToAttack();
        //StartCoroutine(SetUpAttack(m_control.AttackDir));
    }

    IEnumerator SetUpAttack(Vector2 setVec)
    {
        m_attackCol.enabled = true;
        m_attackCol.offset = setVec * 2;
        yield return new WaitForSeconds(0.3f);
        m_attackCol.enabled = false;
        
    }
}

namespace PlayersSpace
{
    class Control
    {
        public Vector2 MoveDir { get; private set; } = Vector2.zero;

        Vector2 m_attackDir;
        Vector2 m_savePos = Vector2.zero;
        
        Player m_player;  
        public void SetUp(Player player) => m_player = player;

        public void Pressing()
        {
            if (!Input.GetMouseButton(0))
            {
                m_savePos = Vector2.zero;
                return;
            }

            Vector2 currentPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            float diff = Vector2.Distance(currentPos, m_savePos);
            if (m_savePos == Vector2.zero) diff = 0;
            
            if (diff != 0)
            {
                if (diff > 2.5f)
                {
                    SetAttackDir(SetAngle(currentPos));
                    m_player.Attack();
                }
                else if (diff > 0.3f && diff <= 2f)
                {
                    SetMoveDir(SetAngle(currentPos));
                    m_player.Move(MoveDir);
                }
            }
            
            m_savePos = currentPos;
        }

        float SetAngle(Vector2 mouse)
        {
            Vector2 distance = mouse - m_savePos;
            float angle = Mathf.Atan2(distance.x, distance.y) * Mathf.Rad2Deg;
            return angle - 90;
        }

        void SetAttackDir(float angle)
        {
            if (-45 <= angle && angle < 45)
                m_attackDir = Vector2.right;
            else if (45 <= angle || angle < -225)
                m_attackDir = Vector2.up;
            else if (-225 <= angle && angle < -135)
                m_attackDir = Vector2.left;
            else if (-135 <= angle && angle < -45)
                m_attackDir = Vector2.down;

            m_attackDir.y *= -1;
        }

        void SetMoveDir(float angle)
        {
            if (-90 <= angle && angle < 90)
                MoveDir = Vector2.right;

            else if (90 <= angle || angle < -90)
                MoveDir = Vector2.left;
        }
    }
}
