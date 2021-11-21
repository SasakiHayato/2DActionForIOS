using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Players;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : CharaBase, IDamageble
{
    [SerializeField] float _flickTime = 0.2f;
    [SerializeField] float _flickLimit = 3;

    CircleCollider2D _atkCol;

    Rigidbody2D _rb;
    public Animator Anim { get; private set; }

    Controller _ctrl = new Controller();
    CheckGround _ground;
    AttackSetting _atkSetting;
    GameObject _rockOnEnemy;

    TutorialPlayer _tutorial;
    public bool TutorialEvent { get; set; } = false;

    private void Awake()
    {
        if (GameManager.CurrentState == GameManager.State.Tutorial)
            _tutorial = gameObject.AddComponent<TutorialPlayer>();
    }

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
        _atkSetting = GetComponent<AttackSetting>();
        _ground = GetComponentInChildren<CheckGround>();
        
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
        if (GameManager.CurrentState == GameManager.State.Tutorial && !TutorialEvent) return;
        _ctrl.SetNearEnemy(transform);
        _ctrl.Update();

        //if (!_ground.IsGround && _rb.velocity.y < 0 && !_ctrl.IsMove)
        //    Anim.Play("TestPlayer_Fall");
    }

    void SetDir()
    {
        if (_ctrl.NearEnemy == null) return;
        
        float dir = transform.position.x - _ctrl.NearEnemy.GetObj().transform.position.x;

        if (dir < 0) transform.localScale = Vector2.one;
        else transform.localScale = new Vector2(-1, 1);
    }

    public void FloatAttack(int combo)
    {
        switch (combo)
        {
            case 1:
                ChangeState(State.IsFloating);
                GameObject enemy = _ctrl.NearEnemy.GetObj();
                enemy.GetComponent<IState>().ChangeState(State.IsFloating);
                enemy.GetComponent<EnemyBase>().Force(new Vector2(0, 2.5f), 7);
                _rockOnEnemy = enemy;
                break;
            case 2:
                _rb.drag = 100;
                if (_rockOnEnemy == null) ChangeState(State.IsGround);
                Vector2 setVec = _rockOnEnemy.transform.position;
                transform.position = new Vector2(setVec.x, setVec.y + 1);
                _rb.velocity = Vector2.zero;
                break;
            case 3:
                _rb.drag = 0;
                if (_rockOnEnemy == null) ChangeState(State.IsGround);
                Vector2 set = _rockOnEnemy.transform.position;
                transform.position = new Vector2(set.x, set.y + 1);
                _rockOnEnemy.GetComponent<EnemyBase>().Force(_ctrl.ForceVec, 50);
                _rockOnEnemy.GetComponent<IState>().ChangeState(State.ImpactFloat);
                _rockOnEnemy = null;
                ChangeState(State.IsGround);
                break;
        }
    }

    public void GroundAttack()
    {
        GameObject enemy = _ctrl.NearEnemy.GetObj();
        enemy.GetComponent<IState>().ChangeState(State.IsFloating);
        enemy.GetComponent<EnemyBase>().Force(_ctrl.ForceVec, 15);
        enemy.GetComponent<IState>().ChangeState(State.ImpactGround);
    }

    public void Attack()
    {
        if (_ctrl.NearEnemy == null || _ctrl.IsMove) return;
        if (_ctrl.ForceVec == Vector2.zero && Current == State.IsGround) return;
        
        if (Current == State.IsGround)
        {
            float dis = Vector2.Distance
                (transform.position, _ctrl.NearEnemy.GetObj().transform.position);
            
            if (dis > 8) return;
        }

        float angle = Mathf.Atan2(_ctrl.ForceVec.y, _ctrl.ForceVec.x) * Mathf.Rad2Deg;
        if (_tutorial != null) _tutorial.SetData(angle);
        if (GameManager.CurrentState == GameManager.State.Tutorial && !_tutorial.GetBool) return;

        FieldManagement.SetTimeRate(false);
        if (angle >= 45 && angle < 130 || Current == State.IsFloating) _atkSetting.RequestToFloating();
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
            if (Time.timeScale == 0) check = true;
            
            yield return null;
        }

        _ctrl.IsMove = false;
    }

    public IEnemys RequestIEnemy() => _ctrl.NearEnemy;
    public void DeleteIEnemy() => _ctrl.NearEnemy = null;
    // AnimEvent‚ÅŒÄ‚Ñ‚¾‚µ
    public void SetAttackCol()
    {
        if (!_atkCol.enabled) _atkCol.enabled = true;
        else _atkCol.enabled = false;
    }
}
