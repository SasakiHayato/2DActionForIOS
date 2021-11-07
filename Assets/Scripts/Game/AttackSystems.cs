using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AttackSystems : MonoBehaviour
{
    private static AttackSystems _instance = new AttackSystems();
    public static AttackSystems Instance => _instance;

    GameObject _collect = null;
    List<GameObject> _enemys = new List<GameObject>();

    void Update()
    {
        if (Instance._enemys.Count <= 0) return;

        foreach (GameObject enemy in Instance._enemys)
        {
            Vector2 pos = enemy.transform.position;
            enemy.transform.position = Vector2.
                MoveTowards(pos, Instance._collect.transform.position, Time.deltaTime * 20);
        }
    }

    public static void SetEnemy(GameObject target)
    {
        if (Instance._collect == null) Instance.Create();
        Instance._enemys.Add(target);
    }

    public static void DeleteList()
    {
        while (Instance._enemys.Count > 0)
        {
            IState state = Instance._enemys.First().GetComponent<IState>();
            state.ChangeState(State.IsGround);
            Instance.Force(Instance._enemys.First());
            Instance._enemys.Remove(Instance._enemys.First());
        }

        Destroy(Instance._collect);
        Instance._collect = null;
    }

    void Force(GameObject enemy)
    {
        Rigidbody2D rb = enemy.GetComponent<Rigidbody2D>();
        
    }

    public static void MoveAttack(GameObject player, float setY)
    {
        Transform collect = GameObject.Find("Collect").transform;
        player.transform.position = new Vector2(collect.transform.position.x, collect.transform.position.y + setY);
    }

    void Create()
    {
        GameObject set = new GameObject();
        set.name = "Collect";
        set.AddComponent<AttackSystems>();

        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        set.transform.position = new Vector2(player.position.x, player.position.y + 5);
        Instance._collect = set;
    }
}
