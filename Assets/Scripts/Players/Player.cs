using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Players;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [SerializeField] float _flickTime;
    [SerializeField] float _flickLimit;
    [SerializeField] float _moveDistance;
    [SerializeField] float _moveTime;

    Rigidbody2D _rb;
    Controller _crtl = new Controller();
    NewPlayerAI _ai = new NewPlayerAI();
    Animator _anim;

    float _flickMove;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();

        _crtl.FlickTime = _flickTime;
        _crtl.FlickLimit = _flickLimit;
        _crtl.Player = this;
    }

    void Update()
    {
        if (!_crtl.IsMove) _anim.Play("Player_idle");

        SetDir();

        _ai.SetNiarEnemy(transform);
        _crtl.SetUp();

        _rb.velocity = new Vector2(0 + _flickMove, _rb.velocity.y);
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
        _crtl.IsMove = true;
        float speed = _moveDistance / _moveTime;
        
        StartCoroutine(GoMove(dir.x, speed));
    }

    IEnumerator GoMove(float dirX, float speed)
    {
        _flickMove = dirX * speed;
        CheckDir(dirX);
        
        yield return new WaitForSeconds(_moveTime);
        _flickMove = 0;
        _crtl.IsMove = false;
    }

    void CheckDir(float dir)
    {
        if (transform.localScale.x == 1 && dir == 1 || transform.localScale.x == -1 && dir == 1) 
            _anim.Play("Player_MoveFront");
        else if (transform.localScale.x == 1 && dir == -1 || transform.localScale.x == -1 && dir == -1) 
            _anim.Play("Player_MoveBack");
    }

    public void Attack()
    {
        Debug.Log("UŒ‚");
    }
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
                float diff = _setUpPos.x - _currentPos.x;

                if (_time < FlickTime)
                {
                    if (diff < FlickLimit * -1) Player.Move(Vector2.right);
                    else if (diff > FlickLimit) Player.Move(Vector2.right * -1);
                    else Player.Attack();
                }

                _isPress = false;
                _time = 0;
            }
        }
    }
}
