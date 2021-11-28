using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManagement : MonoBehaviour
{
    [SerializeField] UIManager _uI;
    [SerializeField] FieldManagement _field;
    [SerializeField] AudioManager _audio;

    [SerializeField] GameManager.State _Debugstate;

    public static GameManagement Instance;
    EventSetting _setEvent;
    static SceneManage _scene;

    private void Awake()
    {
        if (Instance != null)
        {
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
        if (_Debugstate != GameManager.State.None)
        {
            GameManager.ChangeState(_Debugstate);
            SetUp();
            return;
        }
        SetUp();
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
                _setEvent.Set(3, _scene);
                break;
            case GameManager.State.Title:
                GameObject.Find("moon").transform
                    .DORotate(new Vector3(0, 0, 360), 20, RotateMode.LocalAxisAdd)
                    .SetLoops(-1).SetEase(Ease.Linear);
                Instantiate(_audio.gameObject);
                break;
            case GameManager.State.Tutorial:
                Instantiate(_audio.gameObject);
                Instantiate(_uI.gameObject);
                _setEvent.Set(1);
                break;
            case GameManager.State.Result:
                Instantiate(_audio.gameObject);
                Instantiate(_uI.gameObject);
                _setEvent.Set(4);
                break;
            case GameManager.State.None:
                break;
        }
    }

    public static void RequestSetUp() => Instance.SetUp();
}
