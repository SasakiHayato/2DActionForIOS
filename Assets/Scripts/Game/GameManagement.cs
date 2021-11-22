using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagement : MonoBehaviour
{
    [SerializeField] UIManager _uI;
    [SerializeField] FieldManagement _field;
    [SerializeField] AudioManager _audio;

    [SerializeField] bool _isDebug;

    public static GameManagement Instance;
    EventSetting _setEvent;
    static SceneManage _scene;

    private void Awake()
    {
        if (_isDebug) GameManager.ChangeState(GameManager.State.IsGame);

        if (Instance != null)
        {
            Debug.Log("å≥ÅXÇ†ÇÈ");
            _setEvent = FindObjectOfType<EventSetting>();
            _scene = FindObjectOfType<SceneManage>();
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            _scene = gameObject.AddComponent<SceneManage>();
            _setEvent = gameObject.AddComponent<EventSetting>();
            DontDestroyOnLoad(gameObject);
        }

        SetUp();
    }

    void Start()
    {
        if (!_isDebug)
        {
            GameManager.ChangeState(GameManager.State.Title);
            SetUp();
        }
    }

    public void GoTutorial(string name)
    {
        if (name == "Main") GameManager.ChangeState(GameManager.State.Tutorial);
        AudioManager.StopSource();
        AudioManager.OnClickSE();
        _scene.LoadAsync(name, _setEvent);
    }

    public void ChangeScene(string name)
    {
        AudioManager.StopSource();
        AudioManager.OnClickSE();
        _scene.Load(name);
    }

    public void SetEvents(int id) => _setEvent.Set(id);

    void SetUp()
    {
        switch (GameManager.CurrentState)
        {
            case GameManager.State.IsGame:
                Instantiate(_uI.gameObject);
                Instantiate(_field.gameObject);
                Instantiate(_audio.gameObject);
                break;
            case GameManager.State.EndGame:
                break;
            case GameManager.State.Title:
                Instantiate(_audio.gameObject);
                break;
            case GameManager.State.Tutorial:
                Instantiate(_audio.gameObject);
                Instantiate(_uI.gameObject);
                _setEvent.Set(1);
                break;
            case GameManager.State.None:
                break;
        }
    }
}
