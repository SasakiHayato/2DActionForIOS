using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageble
{
    AttackClass m_attack = new AttackClass();

    [SerializeField] float m_speed;

    Rigidbody2D m_rb;
    Flick m_flick = new Flick();

    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_flick.GetPlayer = gameObject;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
            m_flick.Pushed();

        else if (Input.GetMouseButton(0)) 
            m_flick.Pressing();

        if (Input.GetMouseButtonUp(0)) 
            m_flick.Separated();

        m_rb.velocity = new Vector2(m_flick.Dir * m_speed, m_rb.velocity.y);
    }

    private void FixedUpdate()
    {
        FindEnemy();
    }

    void FindEnemy()
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
                }
            }
        }
        
        SetTrans(setVec);
    }

    void SetTrans(Vector2 get)
    {
        Quaternion q = Quaternion.identity;

        if (get.x < 0)
            q = Quaternion.Euler(0, 0, 0);
        else if (get.x > 0)
            q = Quaternion.Euler(0, 180, 0);

        transform.localRotation = q;
    }

    public float AddDamage() => 1;

    public void GetDamage(float damage)
    {
        Debug.Log("Player����");
    }

}

public class Flick
{
    AttackClass m_attack = new AttackClass();

    public GameObject GetPlayer { private get; set; }

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
    }

    private void FlickCheck()
    {
        if (m_pushTime >= 0.5f) return;
        
        
        Vector2 dir = new Vector2(Dir, 0);
        m_attack.Set(GetPlayer, dir, 100, GetParent.Parent.Player);
    }
}
