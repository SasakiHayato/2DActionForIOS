using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManage : MonoBehaviour
{
    Text _scoreTxt;
    int _count;

    void Start()
    {
        _scoreTxt = GetComponent<Text>();
        _scoreTxt.text = "Score : 000";
    }

    public void Add()
    {
        _count++;
        _scoreTxt.text = $"Score : {_count.ToString("d3")}";
    }
}
