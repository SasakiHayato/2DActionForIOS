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
        Knockback,
    }

    [SerializeField] List<Data> _setAction = new List<Data>();
    [SerializeField] CharaBase _chara;

    delegate void SetAnimEvent();
    SetAnimEvent _animEvent;

    [System.Serializable]
    class Data
    {
        [SerializeField] public string ActionAnimName;
        [SerializeField] public int Power;
        [SerializeField] public ActionType Action;
        [SerializeField] public EffectType[] Effect;
    }

    class EffectSetting
    {
        public void Set(ref SetAnimEvent setEvent, EffectType[] effects)
        {
            foreach (EffectType effect in effects)
            {
                switch (effect)
                {
                    case EffectType.CameraShake:
                        setEvent += CameraShake;
                        break;
                    case EffectType.HitStop:
                        setEvent += HitStop;
                        break;
                    case EffectType.Particle:
                        setEvent += Partical;
                        break;
                    case EffectType.Knockback:
                        setEvent += Knockback;
                        break;
                }
            }
        }

        void CameraShake() => FieldManagement.ReqestShakeCm();
        void HitStop() => EffectSystems.RequestHitStop();
        void Partical() => EffectSystems.RequestPartical();
        void Knockback() => EffectSystems.RequestKnockBack();
    }

    EffectSetting _effectSetting = new EffectSetting();
    Animator _anim = null;

    int _saveActionId = int.MaxValue;
    int _combo = 0;
    int _GroungDataId;
    int _setUpGroundData;
    int _comboLength;

    private void Awake()
    {
        for (int setCount = 0; setCount < _setAction.Count; setCount++)
        {
            if (_setAction[setCount].Action == ActionType.Ground)
            {
                _GroungDataId = setCount;
                _setUpGroundData = setCount;
                break;
            }
        }
        _setAction.ForEach(a => { if (a.Action == ActionType.Ground) _comboLength++; });
    }

    public void RequestToCombo() => SetDatas(0, _setAction[_GroungDataId]);
    public void RequestToAt(ActionType type) => _setAction.ForEach(d => { if (d.Action == type) SetDatas(1, d);});
    public void RequestAnimEvent()
    {
        _animEvent.Invoke();
        _animEvent = null;
    }

    void SetDatas(int requestId, Data data)
    {
        if (_anim == null) _anim = GetComponent<Animator>();

        if (_saveActionId != requestId)
        {
            _saveActionId = requestId;
            _combo = 0;
        }
        
        switch (data.Action)
        {
            case ActionType.Ground:
                ComboSetting();
                _chara.AttackMove(data.Action, _combo);
                _chara.Power = data.Power;
                _effectSetting.Set(ref _animEvent, data.Effect);
                _anim.Play(data.ActionAnimName);
                break;

            case ActionType.Floating:
                _chara.AttackMove(data.Action);
                _chara.Power = data.Power;
                _effectSetting.Set(ref _animEvent, data.Effect);
                _anim.Play(data.ActionAnimName);
                break;

            default:
                break;
        }
    }

    void ComboSetting()
    {
        for (int setCount = _GroungDataId; setCount < _setAction.Count; setCount++)
        {
            if (_setAction[setCount].Action == ActionType.Ground)
            {
                _GroungDataId = setCount;
                break;
            }
        }
        
        _combo++;
        if (_combo >= _comboLength)
        {
            _GroungDataId = _setUpGroundData;
            _combo = 0;
        }
    }
}