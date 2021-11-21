using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TutorialUI : MonoBehaviour
{
    [SerializeField] GameObject _arrow;
    GameObject _setobj;
    float _moveArrow = 3;
    
    public void GetData(int id, object type)
    {
        switch (id)
        {
            case 1:
                GameObject obj = (GameObject)type;
                Vector2 setVec = obj.transform.position;
                SetArrowRight(setVec, Quaternion.identity);
                break;
            case 2:
                DeleteArrow();
                break;
            case 3:
                obj = (GameObject)type;
                setVec = obj.transform.position;
                SetArrowUp(setVec, Quaternion.Euler(0, 0, 90));
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

    void DeleteArrow() => Destroy(_setobj);
}
