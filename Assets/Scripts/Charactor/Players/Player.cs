using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Players;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : CharaBase, IDamageble
{
    [SerializeField] float _flickTime = 0.2f;
    [SerializeField] float _flickLimit = 3;
    [SerializeField] float _moveDistance = 5;
    [SerializeField] float _moveTime = 0.2f;

    CircleCollider2D _atkCol;

    Rigidbody2D _rb;
    public Animator Anim { get; private set; }

    Controller _ctrl = new Controller();
    
    AttackSetting _atkSetting;
    GameObject _rockOnEnemy;

    float _flickMove;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
        _atkSetting = GetComponent<AttackSetting>();
        
        SetUpAtkCol();
        SetUpCtrl();
    }

    void SetUpCtrl()
    {
        _ctrl.FlickTime = _flickTime;
        _ctrl.FlickLimit = _flickLimit;
        _ctrl.Player = this;
    }
    void SetUpAtkCol()
    {
        _atkCol = transform.GetChild(0).GetComponent<CircleCollider2D>();
        _atkCol.enabled = false;
    }

    void Update()
    {
        SetDir();
        _ctrl.SetNearEnemy(transform);
        _ctrl.Update();
        
        _rb.velocity = new Vector2(_flickMove, _rb.velocity.y);
    }

    void SetDir()
    {
        if (_ctrl.NearEnemy == null) return;
        
        float dir = transform.position.x - _ctrl.NearEnemy.GetObj().transform.position.x;

        if (dir < 0) transform.localScale = Vector2.one;
        else transform.localScale = new Vector2(-1, 1);
    }

    public void Move(Vector2 dir)
    {
        _ctrl.IsMove = true;
        float speed = _moveDistance / _moveTime;
        
        StartCoroutine(GoMove(dir.x, speed));
    }

    IEnumerator GoMove(float dirX, float speed)
    {
        _flickMove = dirX * speed;

        yield return new WaitForSeconds(_moveTime);
        _flickMove = 0;
        _ctrl.IsMove = false;
    }

    public override void AttackMove(int combo)
    {
        switch (combo)
        {
            case 1:
                ChangeState(State.IsFloating);
                GameObject enemy = _ctrl.NearEnemy.GetObj();
                enemy.GetComponent<IState>().ChangeState(State.IsFloating);
                enemy.GetComponent<EnemyBase>().Force(new Vector2(0, 2.5f), 5);
                _rockOnEnemy = enemy;
                break;
            case 2:
                _rb.gravityScale = 0;
                Vector2 setVec = _rockOnEnemy.transform.position;
                transform.position = new Vector2(setVec.x, setVec.y + 1);
                _rb.velocity = Vector2.zero;
                break;
            case 3:
                _rb.gravityScale = 1;
                Vector2 set = _rockOnEnemy.transform.position;
                transform.position = new Vector2(set.x, set.y + 1);
                _rockOnEnemy.GetComponent<EnemyBase>().Force(_ctrl.ForceVec, 30);
                _rockOnEnemy.GetComponent<IState>().ChangeState(State.Impact);
                _rockOnEnemy = null;
                _ctrl.ForceVec = Vector2.zero;
                ChangeState(State.IsGround);
                break;
        }
    }

    public void Attack(Vector2 dir)
    {
        if (_ctrl.NearEnemy == null || _ctrl.IsMove) return;
        if (dir != Vector2.zero || Current == State.IsFloating) _atkSetting.RequestToFloating();
        else _atkSetting.RequestToGround();
        
        _ctrl.IsMove = true;
        
        StartCoroutine(EndAnim());
    }

    public int AddDamage() => Power;
    public void GetDamage(int damage)
    {

    }

    IEnumerator EndAnim()
    {
        bool check = false;
        while (!check)
        {
            AnimatorStateInfo info = Anim.GetCurrentAnimatorStateInfo(0);
            if (info.normalizedTime > 1) check = true;
            yield return null;
        }

        _ctrl.IsMove = false;
    }

    public IEnemys RequestIEnemy() => _ctrl.NearEnemy;
    public void SetIEnemy() => _ctrl.NearEnemy = null;
    // AnimEvent‚ÅŒÄ‚Ñ‚¾‚µ
    public void SetAttackCol()
    {
        if (!_atkCol.enabled) _atkCol.enabled = true;
        else _atkCol.enabled = false;
    }
}

namespace Players
{
    public class Controller
    {
        public IEnemys NearEnemy { get; set; } = null;

        bool _isPress = false;

        Vector2 _setUpPos = Vector2.zero;
        Vector2 _currentPos = Vector2.zero;

        float _time;

        public float FlickTime { private get; set; } = 0;
        public float FlickLimit { private get; set; } = 0;
        public bool IsMove { get; set; } = false;
        public bool IsPress { get => _isPress; }

        public Vector2 ForceVec { get; set; } = Vector2.zero;

        public Player Player { private get; set; } = null;

        public void Update()
        {
            Pressed();
            Pressing();
            Released();
        }

        void Pressed()
        {
            if (IsMove) return;
            if (Input.GetMouseButtonDown(0))
            {
                _isPress = true;
                _setUpPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        }

        void Pressing()
        {
            if (!_isPress) return;

            _currentPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _time += Time.deltaTime;
        }

        void Released()
        {
            if (Input.GetMouseButtonUp(0))
            {
                float diffX = _setUpPos.x - _currentPos.x;
                float diffY = _setUpPos.y - _currentPos.y;

                if (_time < FlickTime)
                {
                    if (Mathf.Abs(diffX) > FlickLimit && Mathf.Abs(diffY) > FlickLimit)
                    {
                        Vector2 diffVec = _currentPos - _setUpPos;
                        float rad = Mathf.Atan2(diffVec.y, diffVec.x);
                        ForceVec = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
                    }
                    else
                        ForceVec = Vector2.zero;

                    Player.Attack(ForceVec);
                }
                else
                {
                    
                    if (diffX < FlickLimit * -1) Player.Move(Vector2.right);
                    else if (diffX > FlickLimit) Player.Move(Vector2.right * -1);
                }

                _isPress = false;
                _time = 0;
            }
        }

        public void SetNearEnemy(Transform player)
        {
            
            if (FieldManagement.EnemysList.Count <= 0)
            {
                NearEnemy = null;
                return;
            }

            float check = float.MaxValue;
            FieldManagement.EnemysList.ForEach(e =>
            {
                float distance = Vector2.Distance(player.position, e.GetObj().transform.position);
                if (check > distance)
                {
                    check = distance;
                    NearEnemy = e;
                }
            });
        }
    }
}
