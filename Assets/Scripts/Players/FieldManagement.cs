using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FieldManagement : MonoBehaviour
{
    [SerializeField] float _createTime;
    [SerializeField] EnemyData _enemyData;
    [SerializeField] Vector2 _setPos;

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
            CreateEnemys();
        }

        _camera.Mode();
    }

    // id => Debug用ID. Debug時にボタン呼び出し.
    public void CreateEnemys(int id = -1)
    {
        _enemyctrl.Create(id);
        _enemyctrl.SetUp(_setPos);
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
    Camera _mainCamera;
    Camera _shakeCm;
    CinemachineBrain _brain;
    public CinemachineTargetGroup TargetGroup { get; private set; }

    GameObject _player;
    GameObject _cameraObj;

    bool _isShake = false;
    string _shakeCmName = "ShekeCamera";
    
    public void SetUp()
    {
        _cameraObj = GameObject.FindGameObjectWithTag("MainCamera");

        TargetGroup = GameObject.Find("TargetGroup").GetComponent<CinemachineTargetGroup>();
        _player = GameObject.FindGameObjectWithTag("Player");
        TargetGroup.AddMember(_player.transform, 1, 0);

        _brain = _cameraObj.GetComponent<CinemachineBrain>();
        _mainCamera = _cameraObj.GetComponent<Camera>();
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

    public Vector3 SetUpShake()
    {
        GameObject shakeCm = GameObject.Find(_shakeCmName);
        if (shakeCm == null) CreateShakeCamera();

        _shakeCm.transform.position = _cameraObj.transform.position;
        _shakeCm.orthographicSize = _mainCamera.orthographicSize;

        _isShake = true;
        _mainCamera.enabled = false;
        _brain.enabled = false;

        _shakeCm.enabled = true;

        return _cameraObj.transform.position;
    }

    public void IsShake(Vector3 set)
    {
        float x = Random.Range(-1, 1);
        float y = Random.Range(-1, 1);

        Vector3 setVec = new Vector3(set.x + x, set.y + y, set.z);
        _shakeCm.transform.position = setVec;
    }

    public void EndShake()
    {
        _isShake = false;
        _mainCamera.enabled = true;
        _brain.enabled = true;
        _shakeCm.enabled = false;
    }

    void CreateShakeCamera()
    {
        GameObject shakeCm = new GameObject(_shakeCmName);
        _shakeCm = shakeCm.AddComponent<Camera>();
        
        _shakeCm.backgroundColor = _mainCamera.backgroundColor;
        _shakeCm.orthographic = true;
    }

    void Noarmal()
    {
        float posX = 0;

        _mainCamera.orthographicSize = 10;
        Vector2 playerPos = _player.transform.position;
        Vector2 playerScale = _player.transform.localScale;

        if (playerScale.x > 0) posX = 2;
        else posX = -2;

        _cameraObj.transform.position = new Vector3(playerPos.x + posX, playerPos.y, -10);
    }

    void Target()
    {
        
    }
}

