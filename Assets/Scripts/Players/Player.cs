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
    Animator _anim;

    Controller _ctrl = new Controller();
    PlayerAI _ai = new PlayerAI();
    
    AtkCtrlToPlayer _atkCtrl;

    float _flickMove;

    delegate void SetAnimEvent();
    SetAnimEvent _animEvent;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _atkCtrl = transform.GetChild(0).GetComponent<AtkCtrlToPlayer>();
        
        SetUpAtkCol();
        SetUpCtrl();

        _animEvent += SetAttackCol;
        _animEvent += SetHitStop;
        _animEvent += StateCheck;
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
        float setX = _flickMove + _ai.Move;

        if (!_ctrl.IsMove && setX == 0) _anim.Play("TestPlayer_Idle");
        else if (setX != 0 && !_ctrl.IsMove) _anim.Play("TestPlayer_Run");

        SetDir();

        _ai.SupportMove(transform, _ctrl.IsPress, _ctrl.IsMove);
        _ai.SetNearEnemy(transform);

        _ctrl.SetUp();
        
        _rb.velocity = new Vector2(setX, _rb.velocity.y);
    }

    void SetDir()
    {
        if (_ai.NearEnemy == null) return;
        float dir = transform.position.x - _ai.NearEnemy.GetObj().transform.position.x;

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
        CheckDir(dirX);
        
        yield return new WaitForSeconds(_moveTime);
        _flickMove = 0;
        _ctrl.IsMove = false;
    }

    void CheckDir(float dir)
    {
        // Front
        if (transform.localScale.x == 1 && dir == 1 || transform.localScale.x == -1 && dir == 1)
            _anim.Play("TestPlayer_Run");
        // Back
        else if (transform.localScale.x == 1 && dir == -1 || transform.localScale.x == -1 && dir == -1)
            _anim.Play("TestPlayer_Run");
    }

    public void Attack(Vector2 dir)
    {
        if (_ai.NearEnemy == null || _ctrl.IsMove) return;
        _animEvent += SetHitStop;

        if (dir == Vector2.up)
        {
            _anim.Play("TestPlayer_Attack1");
            _animEvent += StateCheck;
        }
        else _anim.Play("TestPlayer_Attack2");
        _ctrl.IsMove = true;
        
        StartCoroutine(EndAnim());
    }

    void SetAttackCol()
    {
        if (!_atkCol.enabled) _atkCol.enabled = true;
        else _atkCol.enabled = false;
    }

    void StateCheck()
    {
        IState other = _ai.NearEnemy.GetObj().GetComponent<IState>();
        StateManager.SetState(gameObject, other);
        
        _animEvent -= StateCheck;
    }

    void SetHitStop()
    {
        if (_atkCtrl.IsHit) FieldManagement.ReqestShakeCm();

        _atkCtrl.IsHit = false;
        _animEvent -= SetHitStop;
    }

    public int AddDamage() => 1;
    public void GetDamage(int damage)
    {

    }

    IEnumerator EndAnim()
    {
        bool check = false;
        while (!check)
        {
            AnimatorStateInfo info = _anim.GetCurrentAnimatorStateInfo(0);
            if (info.normalizedTime > 1) check = true;
            yield return null;
        }

        _ctrl.IsMove = false;
    }

    // AnimetionEvent‚Å‚ÌŒÄ‚Ño‚µ
    public void RequestAnimEvent() => _animEvent.Invoke();
}

namespace Players
{
    public class Controller
    {
        bool _isPress = false;

        Vector2 _setUpPos = Vector2.zero;
        Vector2 _currentPos = Vector2.zero;

        float _time;

        public float FlickTime { private get; set; } = 0;
        public float FlickLimit { private get; set; } = 0;
        public bool IsMove { get; set; } = false;
        public bool IsPress { get => _isPress; }

        public Player Player { private get; set; } = null;

        public void SetUp()
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
                    if (diffX < FlickLimit * -1) Player.Move(Vector2.right);
                    else if (diffX > FlickLimit) Player.Move(Vector2.right * -1);
                    else if (diffY * -1 > FlickLimit) Player.Attack(Vector2.up);
                    else Player.Attack(Vector2.zero);
                }

                _isPress = false;
                _time = 0;
            }
        }
    }
}
