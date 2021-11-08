using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FieldManagement : MonoBehaviour
{
    [SerializeField] float _createTime;
    [SerializeField] EnemyData _enemyData;
    [SerializeField] Vector2 _setPos;
    [SerializeField] GameObject _mainCamera;

    CinemachineTargetGroup _targetGroup;
    Camera _camera;
    CinemachineBrain _brain;

    GameObject _player;
    
    float _timer;

    private static FieldManagement _instance = new FieldManagement();
    public static FieldManagement Instance => _instance;

    public static List<IEnemys> EnmysList { get; set; } = new List<IEnemys>(); 
    EnemyController _enemyctrl = new EnemyController();

    void Start()
    {
        _targetGroup = GameObject.Find("TargetGroup").GetComponent<CinemachineTargetGroup>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _targetGroup.AddMember(_player.transform, 1, 0);

        SetUpEnemyContrl();
        SetUpCamera();
    }
    
    void SetUpEnemyContrl()
    {
        _enemyctrl.SetUpEnemy = _enemyData;
        _enemyctrl.Group = _targetGroup;
    }

    void SetUpCamera()
    {
        _camera = _mainCamera.GetComponent<Camera>();
        _brain = _mainCamera.GetComponent<CinemachineBrain>();
        _brain.enabled = false;
    }

    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > _createTime)
        {
            _timer = 0;
            CreateEnemys();
        }

        CameraMode();
    }

    void CreateEnemys(int id = -1)
    {
        _enemyctrl.Create(id);
        _enemyctrl.SetUp(_setPos);
    }

    void CameraMode()
    {
        if (_targetGroup.m_Targets.Length <= 1)
        {
            _brain.enabled = false;
            _camera.orthographicSize = 10;
            Vector2 playerPos = _player.transform.position;
            _mainCamera.transform.position = new Vector3(playerPos.x, playerPos.y, -10);
        }
        else
        {
            _brain.enabled = true;
        }
    }
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
