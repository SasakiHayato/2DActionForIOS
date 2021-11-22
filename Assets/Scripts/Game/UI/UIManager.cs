using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject _comboObj;
    [SerializeField] string _tPanelName;

    // ‚Ç‚±‚©‚ç‚Å‚àŒÄ‚Ño‚¹‚é‚æ‚¤‚É
    private static UIManager _instance = null;
    public static UIManager Instance => _instance;
    private UIManager() { }

    ScoreManage _score;
    TutorialUI _tUI;
    GameObject _tPanel;

    private void Awake()
    {
        _instance = this;
        _tPanel = GameObject.Find(_tPanelName);
        _tPanel.SetActive(false);

        _score = FindObjectOfType<ScoreManage>();
        if (GameManager.CurrentState == GameManager.State.Tutorial)
        {
            _tUI = gameObject.GetComponent<TutorialUI>();
            _tPanel.SetActive(true);
        }
    }

    public static void UpDateScore()
    {
        if (GameManager.CurrentState != GameManager.State.IsGame) return;
        Instance._score.Add();
    }

    public static void UpDateCombo(int count)
    {
        if (GameManager.CurrentState != GameManager.State.IsGame) return;
        GameObject obj = Instantiate(Instance._comboObj);
        obj.GetComponentInChildren<ComboUISetting>().GetData(count);
    }

    public static void SetTUIData(int id, object type = null) => Instance._tUI.GetData(id, type); 
}
