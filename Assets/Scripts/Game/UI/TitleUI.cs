using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleUI : MonoBehaviour
{
    public void SetUp()
    {
        GameObject.Find("StratButton").GetComponent<Button>()
            .onClick.AddListener(() => GameManagement.Instance.GoTutorial("Main"));
        GameObject.Find("StratButton").GetComponent<Button>()
            .onClick.AddListener(() => GameManagement.Instance.SetEvents(0));
    }
}
