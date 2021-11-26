using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject _comboObj;
    [SerializeField] string _tPanelName;
    [SerializeField] string _gPanelName;

    // ‚Ç‚±‚©‚ç‚Å‚àŒÄ‚Ño‚¹‚é‚æ‚¤‚É
    private static UIManager _instance = null;
    public static UIManager Instance => _instance;
    private UIManager() { }

    ScoreManage _score;
    TimerText _timer;
    TutorialUI _tUI;

    private void Awake()
    {
        _instance = this;

        if (GameManager.CurrentState == GameManager.State.Tutorial)
        {
            _tUI = gameObject.GetComponent<TutorialUI>();
            GameObject.Find(_tPanelName).SetActive(true);
            GameObject.Find(_gPanelName).SetActive(false);
        }
        else
        {
            GameObject.Find(_gPanelName).SetActive(true);
            GameObject.Find(_tPanelName).SetActive(false);
        }

        _score = FindObjectOfType<ScoreManage>();
        _timer = FindObjectOfType<TimerText>();
    }

    public static void UpDateScore()
    {
        if (GameManager.CurrentState != GameManager.State.IsGame) return;
        Instance._score.Add();
    }

    public static void UpDateTime(float time)
    {
        if (GameManager.CurrentState != GameManager.State.IsGame) return;
        Instance._timer.SetTime(time);
    }

    public static void UpDateCombo(int count)
    {
        if (GameManager.CurrentState != GameManager.State.IsGame) return;
        if (count == 0) return;
        GameObject obj = Instantiate(Instance._comboObj);
        obj.GetComponentInChildren<ComboUISetting>().GetData(count);
    }

    public static void SetTUIData(int id, object type = null) => Instance._tUI.GetData(id, type); 
}
