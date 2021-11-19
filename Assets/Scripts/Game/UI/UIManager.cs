using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject _comboObj;

    // ‚Ç‚±‚©‚ç‚Å‚àŒÄ‚Ño‚¹‚é‚æ‚¤‚É
    private static UIManager _instance = null;
    public static UIManager Instance => _instance;
    private UIManager() { }

    ScoreManage _score;
    
    private void Awake()
    {
        _instance = this;
        
        _score = FindObjectOfType<ScoreManage>();
    }

    public static void UpDateScore() => Instance._score.Add();
    public static void UpDateCombo(int count)
    {
        GameObject obj = Instantiate(Instance._comboObj);
        obj.GetComponentInChildren<ComboUISetting>().GetData(count);
    }
}
