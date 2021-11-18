using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fields;

public class EffectSystems : MonoBehaviour
{
    private static EffectSystems _instance = null;
    public static EffectSystems Instance
    {
        get
        {
            object instance = FindObjectOfType(typeof(EffectSystems));
            if (instance == null)
            {
                GameObject obj = new GameObject("EffectSystem");
                _instance = obj.AddComponent<EffectSystems>();
            }
            else
            {
                _instance = (EffectSystems)instance;
            }

            return _instance;
        }
    }
    private EffectSystems() { }

    Player _player;
    
    private void Awake()
    {
        _instance = this;
        _player = FindObjectOfType<Player>();
    }

    public static void RequestPartical() => Instance.Partical();
    public static void RequestHitStop() => Instance.HitStop();
    public static void RequestKnockBack() => Instance.KnockBack();

    void Partical()
    {
        if (_player.RequestIEnemy() == null) return;

        GameObject get = (GameObject)Resources.Load("Srash");
        Vector2 setPos = _player.RequestIEnemy().GetObj().transform.position;
        GameObject set = Instantiate(get, setPos, Quaternion.identity);
        int rotateZ = Random.Range(0, 360);
        set.transform.rotation = Quaternion.Euler(0, 0, rotateZ);
        UIManager.AddSprite(set.GetComponent<SpriteRenderer>());
        SimpleFade.Fade.OutSingle(set.GetComponent<SpriteRenderer>(), 1f);
    }

    void HitStop()
    {
        _player.Anim.speed = 0.01f;
        StartCoroutine(WaitTime());
    }

    IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(0.3f);
        _player.Anim.speed = 1;
    }

    void KnockBack()
    {
        GameObject enemy = _player.RequestIEnemy().GetObj();
        Rigidbody2D rb = enemy.GetComponent<Rigidbody2D>();
        rb.AddForce(transform.up * 5, ForceMode2D.Impulse);
    }
}
