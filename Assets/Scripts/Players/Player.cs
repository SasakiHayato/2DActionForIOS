using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayersSpace;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : CharaBase, IDamageble
{
    [SerializeField] float _moveDis;
    [SerializeField] GameObject _attackCol;

    Rigidbody2D m_rb;

    Control _control = new Control();
    PlayerAI _ai = new PlayerAI();
    AttackSystem _attack = new AttackSystem();

    public GameObject AttackCol { get => _attackCol; }

    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        _attackCol.SetActive(false);
       
        _control.SetUp(this);
        _ai.SetUp(gameObject);
    }
    
    void Update()
    {
        _control.Pressing();
        _ai.UpDate();
        SetDir();

        // ���̋���
        //float h = Input.GetAxisRaw("Horizontal");
        //m_rb.velocity = new Vector2(h * 6, m_rb.velocity.y);
    }
 
    public int AddDamage()
    {
        return 1;
    }
    
    public void GetDamage(float damage)
    {
        Debug.Log("�_���[�W");
    }

    void SetDir()
    {
        if (_ai.NearEnemy == null) return;

        float enemyPosX = _ai.NearEnemy.GetObj().transform.position.x;
        Quaternion dir = Quaternion.identity;

        if (enemyPosX < transform.position.x) dir = Quaternion.Euler(0, 180, 0);
        else dir = Quaternion.Euler(0, 0, 0);
        
        transform.localRotation = dir;
    }

    public  void Move(Vector2 dir)
    {
        
    }

    public override void Attack(State state)
    {
        Debug.Log("aaa");
        AttackSystem.Attack.Set(gameObject);
        //_attack.Set();
        //_attackCol.SetActive(false);
    }
}

namespace PlayersSpace
{
    class Control
    {
        public Vector2 MoveDir { get; private set; } = Vector2.zero;

        Vector2 m_attackDir;
        Vector2 m_savePos = Vector2.zero;
        
        Player _player;
        public void SetUp(Player player) => _player = player;

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
                    _player.Attack(State.IsFloating);
                    //_player.AttackCol.SetActive(true);
                    
                }
                else if (diff > 0.3f && diff <= 2f)
                {
                    SetMoveDir(SetAngle(currentPos));
                    _player.Move(MoveDir);
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
