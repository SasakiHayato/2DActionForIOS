using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Fields;

public class FieldManagement : MonoBehaviour
{
    [SerializeField] float _createTime;
    [SerializeField] EnemyData _enemyData;
    [SerializeField] GameObject _player;

    // ‚Ç‚±‚©‚ç‚Å‚àŒÄ‚Ño‚¹‚é‚æ‚¤‚É
    private static FieldManagement _instance = null;
    public static FieldManagement Instance => _instance;
    private FieldManagement() { }

    public static List<IEnemys> EnmysList { get; set; } = new List<IEnemys>();

    EnemyController _enemyctrl;
    CameraController _camera;

    float _timer;

    private void Awake()
    {
        _instance = this;
        Instantiate(_player);
        _enemyctrl = new EnemyController();
        _camera = new CameraController();
        
        _camera.SetUp();
        SetUpEnemyContrl();
    }

    void SetUpEnemyContrl()
    {
        _enemyctrl.SetUpEnemy = _enemyData;
        _enemyctrl.Group = _camera.TargetGroup;
    }

    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > _createTime)
        {
            _timer = 0;
            _enemyctrl.SetUp();
        }

        _camera.Mode();
    }

    public static void ReqestShakeCm() => Instance.SetShakeCm();
   
    void SetShakeCm()
    {
        Vector3 setVec = _camera.SetUpShake();
        StartCoroutine(Instance.GoShake(setVec));
    }

    IEnumerator GoShake(Vector3 set)
    {
        float time = 0;
        while (0.15f > time)
        {
            time += Time.deltaTime;
            _camera.IsShake(set);
            yield return null;
        }
        
        _camera.EndShake();
    }
}
