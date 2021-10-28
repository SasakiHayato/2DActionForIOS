using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayersSpace;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : CharaBase, IDamageble
{
    [SerializeField] float _moveDis;
    [SerializeField] GameObject _attackCol;

    Rigidbody2D _rb;

    Control _control = new Control();
    PlayerAI _ai = new PlayerAI();
    PlayerAttack _attack;

    public GameObject AttackCol { get => _attackCol; }

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _attack = gameObject.AddComponent<PlayerAttack>();
        _attackCol.SetActive(false);
       
        _control.SetUp(this);
        _ai.SetUp(gameObject);
    }
    
    void Update()
    {
        _control.Pressed();
        _control.Pressing();
        _ai.UpDate();
        SetDir();
    }
 
    public int AddDamage()
    {
        return 1;
    }
    
    public void GetDamage(float damage)
    {
        Debug.Log("É_ÉÅÅ[ÉW");
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

    public void Setter()
    {
        GameObject enemy = _ai.NearEnemy.GetObj();
        IState state = enemy.GetComponent<IState>();
        float dis = Vector2.Distance(transform.position, enemy.transform.position);
        Debug.Log(dis);
        if (dis > 5) return;

        GoAttack(state.Current, enemy, dis);
    }

    void GoAttack(State othres, GameObject enemy, float distance)
    {
        IState chenge = enemy.GetComponent<IState>();
        if (Current == State.IsGround && othres == State.IsGround)
        {
            chenge.ChangeState(State.IsFloating);
            MoveFloating.SetEnemy(enemy);
            _attack.GroundAttack(distance);
        }
        else if (Current == State.IsGround && othres == State.IsFloating)
        {
            Current = State.IsFloating;
            _rb.gravityScale = 0;
            _attack.FloatingAttack(gameObject);
        }
        else if (Current == State.IsFloating && othres == State.IsFloating)
        {
            Current = State.IsGround;
            _rb.gravityScale = 1;
            chenge.ChangeState(State.IsGround);
            MoveFloating.DeleteList();
        }

        AttackCol.SetActive(true);
    }
}

namespace PlayersSpace
{
    class Control
    {
        public Vector2 MoveDir { get; private set; } = Vector2.zero;

        Vector2 m_attackDir;
        Vector2 m_savePos = Vector2.zero;

        bool _isPressed = false;
        
        Player _player;
        public void SetUp(Player player) => _player = player;

        public void Pressed()
        {
            if (Input.GetMouseButtonDown(0)) _isPressed = true;
        }

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
                    if (!_isPressed) return;
                    _isPressed = false;
                    SetAttackDir(SetAngle(currentPos));
                    _player.Setter();
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
