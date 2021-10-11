using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

using IManage;

public class Player : MonoBehaviour, IDamageble, ICharactors
{
    [SerializeField] float m_speed;
    [SerializeField] float m_teleportTime;
    [SerializeField] float m_attackRange;

    Rigidbody2D m_rb;

    FlickControl m_flick = new FlickControl();
    AttackClass m_attack = new AttackClass();
    DrawLine m_line = new DrawLine();
    FindEnemy m_find = new FindEnemy();

    float m_slideSpeed = 0;

    Vector2 m_dir = Vector2.zero;

    public Vector2[] NearEnemy { get; set; } = new Vector2[2] { Vector2.zero, Vector2.zero };

    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_flick.GetPlayer = this;
        m_teleportTime /= 60;
        m_find.Player = this;

        GameManager.Instance.GoSystem(Systems.CameraManage);
    }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
            m_flick.Pushed();

        else if (Input.GetMouseButton(0))
        {
            m_flick.Pressing();
            m_line.Draw(gameObject.transform, m_dir, m_attackRange);
        }
            
        if (Input.GetMouseButtonUp(0))
        {
            m_flick.Separated();
            m_line.DeleteLine();
        }

        m_find.Find(transform);
        SetDir();

        float speed = m_flick.Dir * m_speed + m_slideSpeed;
        m_rb.velocity = new Vector2(speed, m_rb.velocity.y);

        GameManager.Instance.GoSystem(Systems.GetTarget);
    }

    void SetDir()
    {
        Quaternion q = Quaternion.identity;
        
        if (NearEnemy[0].x > transform.position.x)
        {
            m_dir = Vector2.right;
            q = Quaternion.Euler(0, 0, 0);
        }
        else if (NearEnemy[0].x < transform.position.x)
        {
            m_dir = Vector2.left;
            q = Quaternion.Euler(0, 180, 0);
        }

        transform.localRotation = q;
    }

    public void Attack()
    {
        m_attack.Set(gameObject, m_dir, m_attackRange, GetParent.Parent.Player);
        SetPos();
    }

    void SetPos()
    {
        if (m_attack.HitPos == null) return;

        float distance = m_attack.HitPos.position.x - transform.position.x;
        StartCoroutine(Move(distance / m_teleportTime));
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