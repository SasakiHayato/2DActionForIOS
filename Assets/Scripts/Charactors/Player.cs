using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using IManage;

public class Player : MonoBehaviour, IDamageble, ICharactors
{
    [SerializeField] float m_speed;
    [SerializeField] float m_teleportTime;
    [SerializeField] float m_attackRange;

    Rigidbody2D m_rb;

    Flick m_flick = new Flick();
    AttackClass m_attack = new AttackClass();
    DrawLine m_line = new DrawLine();

    float m_slideSpeed = 0;
    float m_attackDir = 0;

    public Vector2 NearEnemy { get; private set; }

    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_flick.GetPlayer = this;
        m_teleportTime /= 60;
    }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
            m_flick.Pushed();

        else if (Input.GetMouseButton(0))
        {
            m_flick.Pressing();
            m_line.Draw(gameObject.transform, new Vector2(m_attackDir, 0), m_attackRange);
        }
            
        if (Input.GetMouseButtonUp(0))
        {
            m_flick.Separated();
            m_line.DeleteLine();
        }
            

        float speed = m_flick.Dir * m_speed + m_slideSpeed;
        m_rb.velocity = new Vector2(speed, m_rb.velocity.y);
    }

    private void FixedUpdate() => FindEnemyToLook();
    void FindEnemyToLook()
    {
        float posX = float.MaxValue;
        Vector3 setVec = Vector3.zero;
        EnemyBase[] enemys = FindObjectsOfType<EnemyBase>();

        foreach (EnemyBase get in enemys)
        {
            IEnemys check = get.GetComponent<IEnemys>();
            if (check != null)
            {
                float absX = Mathf.Abs(transform.position.x - check.GetPos().x);
                if (posX > absX)
                {
                    posX = absX;
                    setVec = transform.position - check.GetPos();
                    NearEnemy = check.GetPos();
                }
            }
        }
        GameManager.Instance.GoSystem(Systems.GetTarget);
        SetDir(setVec);
    }

    void SetDir(Vector2 get)
    {
        Quaternion q = Quaternion.identity;

        if (get.x < 0)
        {
            m_attackDir = 1;
            q = Quaternion.Euler(0, 0, 0);
        }
        else if (get.x > 0)
        {
            m_attackDir = -1;
            q = Quaternion.Euler(0, 180, 0);
        }

        transform.localRotation = q;
    }

    public void Attack()
    {
        Vector2 dir = new Vector2(m_attackDir, 0);
        m_attack.Set(gameObject, dir, m_attackRange, GetParent.Parent.Player);

        SetPos();
    }

    void SetPos()
    {
        if (m_attack.HitPos == null) return;

        float distance = m_attack.HitPos.position.x - transform.position.x;
        StartCoroutine(Move(distance / (m_teleportTime)));
        m_attack.HitPos = null;
    }

    IEnumerator Move(float speed)
    {
        m_slideSpeed = speed;
        yield return new WaitForSeconds(m_teleportTime);
        m_slideSpeed = 0;
    }

    public float AddDamage() => 1;
    public void GetDamage(float damage)
    {
        Debug.Log("PlayerŽ€‚ñ‚¾");
    }

    public GameObject GetObject() => gameObject;
}

public class Flick
{
    public Player GetPlayer {private get; set; }

    Vector2 m_startPos = Vector2.zero;
    Vector2 m_endPos = Vector2.zero;

    float m_pushTime;
    public float Dir { get; private set; }

    public void Pushed()
    {
        m_startPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public void Pressing()
    {
        m_endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        m_pushTime += Time.deltaTime;

        SetDir();
    }

    public void Separated()
    {
        FlickCheck();
        Dir = 0;
        m_pushTime = 0;
    }

    private void SetDir()
    {
        float check = m_startPos.x - m_endPos.x;

        if (check > 1.5f) Dir = -1;
        else if (check < -1.5f) Dir = 1;
        else Dir = 0;
    }

    private void FlickCheck()
    {
        if (m_pushTime >= 0.2f || Dir == 0) return;
        GetPlayer.Attack();
        GameManager.Instance.GoSystem(Systems.TimeRate);
    }
}
