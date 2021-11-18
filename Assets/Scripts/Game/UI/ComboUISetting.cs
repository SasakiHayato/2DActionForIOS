using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboUISetting : MonoBehaviour
{
    public void GetData(int count)
    {
        Text txt = GetComponentInChildren<Text>();
        txt.text = $"{count} Combo";
    }
}
