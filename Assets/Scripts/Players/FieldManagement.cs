using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FieldManagement : MonoBehaviour
{
    [SerializeField] float _createTime;
    [SerializeField] EnemyData _enemyData;
    [SerializeField] Vector2 _setPos;
    
    float _timer;

    private static FieldManagement _instance = new FieldManagement();
    public static FieldManagement Instance => _instance;

    public static List<IEnemys> EnmysList { get; set; } = new List<IEnemys>();

    EnemyController _enemyctrl = new EnemyController();
    CameraController _camera = new CameraController();

    void Start()
    {
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
            CreateEnemys();
        }

        _camera.Mode();
    }

    void CreateEnemys(int id = -1)
    {
        _enemyctrl.Create(id);
        _enemyctrl.SetUp(_setPos);
    }

    public static void ReqestShakeCamera() => Instance._camera.Shake();
}

class EnemyController
{
    public EnemyData SetUpEnemy { private get; set; } = null;
    public CinemachineTargetGroup Group { private get; set; } = null;
    
    GameObject _setEnemy = default;
    Enemys.Data _data = null;

    public void Create(int id)
    {
        int set = Random.Range(0, SetUpEnemy.DataLength);
        if (id >= 0) set = id;
        _data = SetUpEnemy.GetData(set);
        GameObject obj = _data.Obj;
        
        _setEnemy = obj;
    }

    public void SetUp(Vector2 pos)
    {
        if (_setEnemy == null || _data == null) return;

        GameObject get = MonoBehaviour.Instantiate(_setEnemy, pos, Quaternion.identity);
        Group.AddMember(get.transform, 1, 0);
        IEnemys iEnemy = get.GetComponent<IEnemys>();
        if (iEnemy != null) FieldManagement.EnmysList.Add(iEnemy);
        
        EnemyBase enemy = get.GetComponent<EnemyBase>();
        enemy.Speed = _data.Speed;
        enemy.Hp = _data.Hp;
        
        _setEnemy = null;
        _data = null;
    }
}

class CameraController
{
    Camera _camera;
    Camera _shakeCm;
    CinemachineBrain _brain;
    public CinemachineTargetGroup TargetGroup { get; private set; }

    GameObject _player;
    GameObject _mainCamera;

    bool _isShake = false;
    bool _shakeCmSetUp = false;

    public void SetUp()
    {
        _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");

        TargetGroup = GameObject.Find("TargetGroup").GetComponent<CinemachineTargetGroup>();
        _player = GameObject.FindGameObjectWithTag("Player");
        TargetGroup.AddMember(_player.transform, 1, 0);

        _brain = _mainCamera.GetComponent<CinemachineBrain>();
        _camera = _mainCamera.GetComponent<Camera>();
    }

    public void Mode()
    {
        if (_isShake) return;

        if (TargetGroup.m_Targets.Length <= 1)
        {
            _brain.enabled = false;
            Noarmal();
        }
        else
        {
            _brain.enabled = true;
            Target();
        }
    }

    public void Shake()
    {
        _isShake = true;
        if (!_shakeCmSetUp)
        {
            _shakeCmSetUp = true;
            CreateShakeCamera();
        }
    }

    void CreateShakeCamera()
    {
        GameObject shakeCm = new GameObject("ShekeCamera");
        _shakeCm = shakeCm.AddComponent<Camera>();
        Debug.Log($"{_mainCamera} {_camera}");
        _shakeCm.transform.position = _mainCamera.transform.position;
        _shakeCm.backgroundColor = _camera.backgroundColor;
        _shakeCm.orthographic = true;
        _shakeCm.orthographicSize = _camera.orthographicSize;
    }

    void Noarmal()
    {
        float posX = 0;

        _camera.orthographicSize = 10;
        Vector2 playerPos = _player.transform.position;
        Vector2 playerScale = _player.transform.localScale;

        if (playerScale.x > 0) posX = 2;
        else posX = -2;

        _mainCamera.transform.position = new Vector3(playerPos.x + posX, playerPos.y, -10);
    }

    void Target()
    {
        
    }
}

