using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSetting : MonoBehaviour
{
    public enum ActionType
    {
        Ground,
        Floating,
    }

    enum EffectType
    {
        CameraShake,
        HitStop,
        Particle,
    }

    [SerializeField] List<Data> _setAction = new List<Data>();
    [SerializeField] CharaBase _chara;

    [System.Serializable]
    class Data
    {
        [SerializeField] public string ActionAnimName;
        [SerializeField] public int Power;
        [SerializeField] public ActionType Action;
        [SerializeField] public EffectType[] Effect;
    }

    Animator _anim = null;
    int _saveId = int.MaxValue;
    int _combo = 0;

    public void RequestToCombo() => SetDatas(0, _setAction[_combo]);
    public void RequestToAt(ActionType type) => _setAction.ForEach(d => { if (d.Action == type) SetDatas(1, d);});

    void SetDatas(int requestId, Data data)
    {
        if (_anim == null) _anim = GetComponent<Animator>();

        if (_saveId != requestId)
        {
            _saveId = requestId;
            _combo = 0;
        }

        switch (data.Action)
        {
            case ActionType.Ground:
                Combo(data);
                break;
            case ActionType.Floating:
                _chara.Power = data.Power;
                _anim.Play(data.ActionAnimName);
                break;
            default:
                break;
        }
    }

    void Combo(Data data)
    {
        _chara.Power = data.Power;
        _anim.Play(data.ActionAnimName);

        switch (_combo)
        {
            default:
                break;
        }

        _combo++;
        if (_combo >= _setAction.Count) _combo = 0;
    }

    void SetAnimEvent()
    {

    }
}
