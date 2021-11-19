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
    private void Awake()
    {
        if (_isDebug)
            GameManager.ChangeState(GameManager.State.IsGame);

        if (Instance != null)
        {
            Debug.Log("å≥ÅXÇ†ÇÈ");
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        SetUp();
    }

    public void ChangeScene(string name)
    {
        if (name == "Main") GameManager.ChangeState(GameManager.State.IsGame);
        
        SceneManage scene = new SceneManage();
        scene.Load(name);
    }

    void SetUp()
    {
        if (GameManager.State.IsGame == GameManager.CurrentState)
        {
            Instantiate(Instance._uI.gameObject);
            Instantiate(Instance._field.gameObject);
            Instantiate(Instance._audio.gameObject);
        }
    }
}
