using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TutorialUI : MonoBehaviour
{
    [SerializeField] GameObject _arrow;
    [SerializeField] GameObject _textBox;

    GameObject _setobj;
    GameObject _setText;
    float _moveArrow = 3;

    Text _txt = null;

    public void GetData(int id, object type)
    {
        switch (id)
        {
            case 1:
                GameObject obj = (GameObject)type;
                Vector2 setVec = obj.transform.position;
                SetArrowRight(setVec, Quaternion.identity);
                SetText(setVec, "Flick");
                break;
            case 2:
                DeleteUI();
                break;
            case 3:
                obj = (GameObject)type;
                setVec = obj.transform.position;
                SetArrowUp(setVec, Quaternion.Euler(0, 0, 90));
                SetText(setVec, "Flick");
                break;
            case 4:
                obj = (GameObject)type;
                setVec = obj.transform.position;
                SetText(setVec, "Tup");
                break;
            case 5:
                obj = (GameObject)type;
                setVec = obj.transform.position;
                SetArrowDown(setVec, Quaternion.Euler(0, 0, -90));
                SetText(setVec, "Flick");
                break;
        }
    }

    void SetArrowRight(Vector2 getVec, Quaternion q)
    {
        Vector2 set = new Vector2(getVec.x, getVec.y + 2);
        Transform arrowT = Instantiate(_arrow).transform;
        _setobj = arrowT.gameObject;
        arrowT.position = set;
        arrowT.localRotation = q;
        arrowT.DOMove(new Vector3(set.x + _moveArrow, set.y, 0), 0.5f)
            .SetLoops(-1, LoopType.Restart).SetUpdate(true);
    }

    void SetArrowUp(Vector2 getVec, Quaternion q)
    {
        Vector2 set = new Vector2(getVec.x, getVec.y + 2);
        Transform arrowT = Instantiate(_arrow).transform;
        _setobj = arrowT.gameObject;
        arrowT.position = set;
        arrowT.localRotation = q;
        arrowT.DOMove(new Vector3(set.x, set.y + _moveArrow, 0), 0.5f)
            .SetLoops(-1, LoopType.Restart).SetUpdate(true);
    }

    void SetArrowDown(Vector2 getVec, Quaternion q)
    {
        Vector2 set = new Vector2(getVec.x, getVec.y + 2);
        Transform arrowT = Instantiate(_arrow).transform;
        _setobj = arrowT.gameObject;
        arrowT.position = set;
        arrowT.localRotation = q;
        arrowT.DOMove(new Vector3(set.x, set.y - _moveArrow, 0), 0.5f)
            .SetLoops(-1, LoopType.Restart).SetUpdate(true);
    }

    void SetText(Vector2 getVec, string name)
    {
        Vector2 set = new Vector2(getVec.x, getVec.y + 5);
        _setText = Instantiate(_textBox);
        if (_txt == null) _txt = _setText.GetComponentInChildren<Text>();
        _txt.text = name;
        _setText.transform.position = set;
        Vector3 scale = _setText.transform.localScale;
        _setText.transform.DOScale(new Vector3(scale.x + 0.1f, scale.y + 0.1f, 0), 0.5f)
            .SetLoops(-1, LoopType.Yoyo).SetUpdate(true);
    }

    void DeleteUI()
    {
        Destroy(_setobj);
        Destroy(_setText);
    }
}
