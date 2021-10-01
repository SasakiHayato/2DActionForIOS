using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GetEnumToGame;
using AudioType;
using UiType;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] GameObject m_player;
    [SerializeField] GameObject[] m_enemys = new GameObject[0];
    [SerializeField] Transform[] m_spawnPos = new Transform[2];
    [SerializeField] float m_setTime;
    [SerializeField] bool m_isDebug = false;

    List<GameObject> m_Objects = new List<GameObject>();

    UiManager m_ui;
    PlayerController m_playerControl;

    SetCameraPostion m_setCamera = new SetCameraPostion();
    SceneController m_scene = new SceneController();

    float m_time;
    int m_count = 0;

    private void Awake()
    {
        m_ui = FindObjectOfType<UiManager>();

        if (GameMaster.Instance().CurrentType == Game.Main || m_isDebug)
        {
            SetPlayer();
            m_setCamera.GetCramera();
        }

        // DontDestroy
        if (Instance != null)
            Destroy(Instance);

        Instance = this;
        DontDestroyOnLoad(Instance);
    }

    void Update()
    {
        if (GameMaster.Instance().CurrentType != Game.Main && !m_isDebug) return;

        m_setCamera.Set();

        // CreateEnemy
        m_time += Time.deltaTime;
        if (m_time > m_setTime)
        {
            int set = Random.Range(0, m_spawnPos.Length);
            Vector2 setPos = m_spawnPos[set].position;
            SetEnemys(setPos);

            m_time = 0;
        }
    }

    void SetEnemys(Vector2 setPos)
    {
        int range = Random.Range(0, m_enemys.Length);
        GameObject get = Instantiate(m_enemys[range]);

        get.transform.position = setPos;

        get.name = $"Enemy{m_count}";

        EnemyBase enemyBase = get.GetComponent<EnemyBase>();
        enemyBase.ThisID = m_count;

        m_Objects.Add(get);
        m_count++;
    }

    void SetPlayer()
    {
        GameObject player = Instantiate(m_player);
        m_playerControl = player.GetComponent<PlayerController>();

        m_ui.GetHp = m_playerControl.Hp;
    }

    public void DeidPlayer()
    {
        Debug.Log("Ž€‚ñ‚¾");
    }

    public void InformDeid(GameObject set, Quaternion quaternion, int id)
    {
        DeidUi deid = FindObjectOfType<DeidUi>();
        Transform t = set.transform;
        deid.SetUi(t, quaternion);
        
        m_Objects.Remove(m_Objects[id]);
        m_Objects.Add(null);

        m_ui.AddScore(100);

        Destroy(set);
    }
    public void EnemysSpeed(bool boolean, float setSpeed)
    {
        foreach (GameObject get in m_Objects)
        {
            if(get != null)
            {
                EnemyBase enemyBase = get.GetComponent<EnemyBase>();
                if (boolean)
                    enemyBase.ThisMotionSpeed(setSpeed, true);
                else
                    enemyBase.ThisMotionSpeed(0, false);
            }
        }
    }
    public void PlayAudio(ClipType clipType, BGMType bgmType)
    {
        AudioManager audio = FindObjectOfType<AudioManager>();
        if (clipType != ClipType.None)
            audio.PlayClip(clipType);

        if (bgmType != BGMType.None)
            audio.PlayBGM(bgmType);
    }

    public void Shake()
    {
        CameraShake shake = FindObjectOfType<CameraShake>();
        shake.Shake();
    }

    public void SetUiParam(Ui type)
    {
        if (type == Ui.PlayerSlider)
            m_ui.SetSliderParam(m_playerControl.CurrentAttack);
        else if (type == Ui.PlayerHp)
            m_ui.ChengeHpImage();


    }

    public void Scenes(string set) => m_scene.SetScene(set);
}
