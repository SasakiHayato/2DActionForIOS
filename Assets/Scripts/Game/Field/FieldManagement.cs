using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Fields;

public class FieldManagement : MonoBehaviour
{
    [SerializeField] float _createTime;
    [SerializeField] float _shakeTime;
    [SerializeField] float _timeRate;
    [SerializeField] EnemyData _enemyData;
    [SerializeField] GameObject _player;

    // ‚Ç‚±‚©‚ç‚Å‚àŒÄ‚Ño‚¹‚é‚æ‚¤‚É
    private static FieldManagement _instance = null;
    public static FieldManagement Instance => _instance;
    private FieldManagement() { }

    public static List<IEnemys> EnemysList { get; set; } = new List<IEnemys>();
    public static List<GameObject> FieldCharas { get; set; } = new List<GameObject>();

    EnemyController _enemyctrl;
    CameraController _camera;

    float _timer;

    private void Awake()
    {
        _instance = this;
        _enemyctrl = new EnemyController();
        _camera = new CameraController();

        GameObject player = Instantiate(_player);
        FieldCharas.Add(player);
        
        _camera.SetUp();
        _enemyctrl.SetUpEnemy = _enemyData;
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

    public static void ShakeCm() => Instance.StartCoroutine(Instance.Goshake());
    IEnumerator Goshake()
    {
        float time = 0;
        while (time < _shakeTime)
        {
            time += Time.deltaTime;
            _camera.Shake();
            yield return null;
        }
        _camera.EndShake();
    }
}
