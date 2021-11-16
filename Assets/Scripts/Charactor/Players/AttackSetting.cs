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
        [SerializeField] public bool CallBackAttack;
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

        void CameraShake() => FieldManagement.ShakeCm();
        void HitStop() => EffectSystems.RequestHitStop();
        void Partical() => EffectSystems.RequestPartical();
        void Knockback() => EffectSystems.RequestKnockBack();
    }

    EffectSetting _effectSetting = new EffectSetting();
    Animator _anim = null;

    int _saveActionId = int.MaxValue;
    int _combo = 0;
    int _groungDataId;
    int _setUpGroundData;

    int _floatDataId;
    int _setUpFloatDataId;

    int _comboLengthGround;
    int _comboLengthFloat;

    private void Awake()
    {
        for (int setCount = 0; setCount < _setAction.Count; setCount++)
        {
            if (_setAction[setCount].Action == ActionType.Ground)
            {
                _groungDataId = setCount;
                _setUpGroundData = setCount;
                break;
            }
        }
        for (int setCount = 0; setCount < _setAction.Count; setCount++)
        {
            if (_setAction[setCount].Action == ActionType.Floating)
            {
                _floatDataId = setCount;
                _setUpFloatDataId = setCount;
                break;
            }
        }
        _setAction.ForEach(a => { if (a.Action == ActionType.Ground) _comboLengthGround++; });
        _setAction.ForEach(a => { if (a.Action == ActionType.Floating) _comboLengthFloat++; });
    }

    public void RequestToGround() => SetDatas(0, _setAction[_groungDataId]);
    public void RequestToFloating() => SetDatas(1, _setAction[_floatDataId]);

    // AnimatorEventÇ≈ÇÃåƒÇ—èoÇµ
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
                ComboSettingToGround();
                _chara.Power = data.Power;
                _effectSetting.Set(ref _animEvent, data.Effect);
                _anim.Play(data.ActionAnimName);
                break;

            case ActionType.Floating:
                ComboSettingToFloating();
                if (data.CallBackAttack) _animEvent += CallBack;
                else _chara.AttackMove(_combo);
                
                _chara.Power = data.Power;
                _effectSetting.Set(ref _animEvent, data.Effect);
                _anim.Play(data.ActionAnimName);
                break;
        }
    }

    void CallBack() => _chara.AttackMove(_combo);

    void ComboSettingToGround()
    {
        for (int setCount = _groungDataId + 1; setCount < _setAction.Count; setCount++)
        {
            if (_setAction[setCount].Action == ActionType.Ground)
            {
                _groungDataId = setCount;
                break;
            }
        }
        
        _combo++;
        if (_combo >= _comboLengthGround)
        {
            _groungDataId = _setUpGroundData;
            _combo = 0;
        }
    }

    void ComboSettingToFloating()
    {
        for (int setCount = _floatDataId + 1; setCount < _setAction.Count; setCount++)
        {
            if (_setAction[setCount].Action == ActionType.Floating)
            {
                _floatDataId = setCount;
                break;
            }
        }
        _combo++;
        
        if (_combo > _comboLengthFloat)
        {
            _floatDataId = _setUpFloatDataId;
            _combo = 0;
        }
    }
}