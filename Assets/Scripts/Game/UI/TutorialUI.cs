using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class TutorialUI : MonoBehaviour
{
    [SerializeField] GameObject _arrow;
    
    public void GetData(int id, object type)
    {
        Debug.Log("s");
        switch (id)
        {
            case 1:
                GameObject obj = (GameObject)type;
                Vector2 setVec = obj.transform.position;
                SetArrow(setVec);
                break;
        }
    }

    void SetArrow(Vector2 getVec)
    {
        Vector2 set = new Vector2(getVec.x, getVec.y + 2);
        Transform arrowT = Instantiate(_arrow).transform;
        arrowT.position = set;
        arrowT.DOMove(new Vector3(set.x + 3, set.y, 0), 2).SetLoops(-1, LoopType.Restart);
    }
}
