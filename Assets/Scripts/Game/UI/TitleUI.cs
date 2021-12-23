using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;

public class TitleUI : MonoBehaviour
{
    Image aa;

    public void SetUp()
    {
        GameObject.Find("StratButton").GetComponent<Button>()
            .onClick.AddListener(() => GameManagement.Instance.GoTutorial("Main"));
        GameObject.Find("StratButton").GetComponent<Button>()
            .onClick.AddListener(() => GameManagement.Instance.SetEvents(0));

        aa.DOFade(1, 1);
    }
}
