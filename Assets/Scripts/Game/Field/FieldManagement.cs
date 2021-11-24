using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Fields;

public class FieldManagement : MonoBehaviour
{
    [SerializeField] float _createTime;
    [SerializeField] float _shakeTime;
    [SerializeField] EnemyData _enemyData;
    [SerializeField] Explosion _explosion;

    // ‚Ç‚±‚©‚ç‚Å‚àŒÄ‚Ño‚¹‚é‚æ‚¤‚É
    private static FieldManagement _instance = null;
    public static FieldManagement Instance => _instance;
    private FieldManagement() { }

    public static List<IEnemys> EnemysList { get; set; } = new List<IEnemys>();
    public static List<GameObject> FieldCharas { get; set; } = new List<GameObject>();

    EnemyController _enemyctrl;
    CameraController _camera;
    CharaBase _player;

    float _timer;

    string _ownerName = null;
    float _ownerTime = 0;

    private void Awake()
    {
        _instance = this;
        EnemysList = new List<IEnemys>();
        _enemyctrl = new EnemyController();
        _camera = new CameraController();

        GameObject player = Instantiate((GameObject)Resources.Load("TestPlayer"));
        _player = player.GetComponent<CharaBase>();
        player.transform.position = GameManager.CurrentPlayerPos;
        FieldCharas.Add(player);
        
        _camera.SetUp();
        _enemyctrl.SetUpEnemy = _enemyData;
        _enemyctrl.SetUp();
    }

    void Update()
    {
        _camera.Mode();

        _timer += Time.deltaTime;
        if (_timer > _createTime)
        {
            _timer = 0;
            _enemyctrl.Setting();
        }

        _ownerTime += Time.unscaledDeltaTime;
        if (_ownerTime > 3)
        {
            _ownerTime = 0;
            DeleteOnwer();
        }
    }

    public static bool AttackOwner(string name)
    {
        if (Instance._ownerName == null)
        {
            Instance._ownerTime = 0;
            Instance._ownerName = name;
            return true;
        }
        else
        {
            if (Instance._ownerName == name) return true;
            else return false;
        }
    }

    public static void DeleteOnwer() => Instance._ownerName = null;

    public static void ShakeCm()
    {
        if (GameManager.CurrentState != GameManager.State.IsGame) return;
        Instance.StartCoroutine(Instance.Goshake());
    }

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

    public static void SetTimeRate(bool set)
    {
        if (GameManager.CurrentState == GameManager.State.Tutorial) return;

        if (set && Instance._player.Current == State.IsGround)
        {
            GameManager.Setting(true);
            Time.timeScale = 0f;
        }
        else
        {
            GameManager.Setting(false);
            Time.timeScale = 1;
        }
    }

    public static void ReExplosion(GameObject target)
    {
        if (GameManager.CurrentState != GameManager.State.IsGame) return;

        Explosion e = Instantiate(Instance._explosion);
        e.transform.position = target.transform.position;
        e.Target = target;
    }
}
