using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerText : MonoBehaviour
{
    Text _txt;
    string _str;

    void Start()
    {
        _txt = GetComponent<Text>();
        _str = _txt.text;
    }

    public void SetTime(float time) => _txt.text = $"{_str} {time.ToString("0:00.00")}";
}
