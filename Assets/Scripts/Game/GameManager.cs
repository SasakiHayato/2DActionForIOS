using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AudioType;
using UiType;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] GameObject m_player;
    [SerializeField] GameObject[] m_enemys = new GameObject[0];

    List<GameObject> m_Objects = new List<GameObject>();
    UiManager m_ui;
    PlayerController m_playerControl;
    SetCameraPostion m_setCamera = new SetCameraPostion();
    int m_count = 0;

    private void Awake()
    {
        m_ui = FindObjectOfType<UiManager>();
        SetPlayer();
        m_setCamera.GetCramera();

        if (Instance != null)
            Destroy(Instance);

        Instance = this;
        DontDestroyOnLoad(Instance);
    }

    public void SetEnemys(Vector2 setPos)
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
        
        Destroy(set);
    }
    public void EnemysSpeed(bool boolean)
    {
        foreach (GameObject get in m_Objects)
        {
            if(get != null)
            {
                EnemyBase enemyBase = get.GetComponent<EnemyBase>();
                if (boolean)
                    enemyBase.ThisMotionSpeed(2, true);
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
    }
}
