using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultUI : MonoBehaviour
{
    [SerializeField] Text _scoreTxt;
    [SerializeField] Text _phaseTet;

    public void SetUp()
    {
        string scoreStr = _scoreTxt.text;
        _scoreTxt.text = $"{scoreStr} {GameManager.GameScore.ToString("d3")}";

        string phaseStr = _phaseTet.text;
        _phaseTet.text = $"{phaseStr} {GameManager.GamePhase.ToString("d2")}";
    }
}
