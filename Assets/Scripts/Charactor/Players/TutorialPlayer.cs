using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPlayer : MonoBehaviour
{
    int _tutorialId = 1;
    float _angle;
    public bool GetBool { get; private set; }

    public void SetData(object type)
    {
        GetBool = false;
        switch (_tutorialId)
        {
            case 1:
                _angle = (float)type;
                if (_angle < 45 && _angle >= -45)
                    IsFlick();
                break;
            case 2:
                _angle = (float)type;
                if (_angle >= 45 && _angle < 150)
                    IsFlick();
                break;
            case 3:
                IsFlick();
                break;
            case 4:
                _angle = (float)type;
                if (_angle > -135 && _angle < -45)
                    IsFlick();
                break;
        }
    }

    void IsFlick()
    {
        _tutorialId++;
        GetBool = true;
    }

    public void ResetBool() => GetBool = false;
}
