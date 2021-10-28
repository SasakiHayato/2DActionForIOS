using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MoveFloating : MonoBehaviour
{
    private static MoveFloating _floating = new MoveFloating();
    public static MoveFloating Instance => _floating;

    GameObject _collect = null;
    List<GameObject> _enemys = new List<GameObject>();
    void Update()
    {
        if (Instance._enemys.Count <= 0) return;

        foreach (GameObject enemy in Instance._enemys)
        {
            Debug.Log("a");
            Vector2 pos = enemy.transform.position;
            enemy.transform.position = Vector2.
                MoveTowards(pos, Instance._collect.transform.position, Time.deltaTime * 10);
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
            Instance._enemys.Remove(Instance._enemys.First());
        }

        Destroy(Instance._collect);
        Instance._collect = null;
    }

    void Create()
    {
        GameObject set = new GameObject();
        set.name = "Collect";
        set.AddComponent<MoveFloating>();

        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        set.transform.position = new Vector2(player.position.x, player.position.y + 5);
        Instance._collect = set;
    }
}
