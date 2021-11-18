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
    [SerializeField] Player _player;

    delegate void SetAnimEvent();
    SetAnimEvent _animEvent;

    [System.Serializable]
    class Data
    {
        [SerializeField] public string ActionAnimName;
        [SerializeField] public int Power;
        [SerializeField] public ActionType Action;
        [SerializeField] public EffectType[] Effect;
        [SerializeField] public AudioClip SE;
        [SerializeField] public bool CallBackAttack;
        [SerializeField] public float ImputTime;
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
                        setEvent += FieldManagement.ShakeCm;
                        break;
                    case EffectType.HitStop:
                        setEvent += EffectSystems.RequestHitStop;
                        break;
                    case EffectType.Particle:
                        setEvent += EffectSystems.RequestPartical;
                        break;
                    case EffectType.Knockback:
                        setEvent += EffectSystems.RequestKnockBack;
                        break;
                }
            }
        }
    }

    EffectSetting _effectSetting = new EffectSetting();
    Animator _anim = null;
    Data _data = null;

    int _saveActionId = int.MaxValue;
    int _combo = 0;
    int _groungDataId;
    int _setUpGroundData;

    int _floatDataId;
    int _setUpFloatDataId;

    int _comboLengthGround;
    int _comboLengthFloat;

    int _totalCombo = 0;

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

    float _time;
    float _resetCombTime = float.MaxValue;

    void Update()
    {
        _time += Time.deltaTime;
        if (_time >= _resetCombTime)
        {
            _groungDataId = _setUpGroundData;
            _floatDataId = _setUpFloatDataId;
            _combo = 0;
            _time = 0;
            UIManager.UpDateCombo(0);
        }
    }

    public void RequestToGround() => SetDatas(0, _setAction[_groungDataId]);
    public void RequestToFloating() => SetDatas(1, _setAction[_floatDataId]);

    // AnimatorEventÇ≈ÇÃåƒÇ—èoÇµ
    public void RequestAnimEvent()
    {
        _animEvent.Invoke();
        _animEvent = null;
        _data = null;
    }

    void SetDatas(int requestId, Data data)
    {
        if (_anim == null) _anim = GetComponent<Animator>();
        _data = data;
        _time = 0;
        _totalCombo++;
        _resetCombTime = data.ImputTime;

        if (_saveActionId != requestId)
        {
            _saveActionId = requestId;
            _combo = 0;
        }

        _animEvent += CallBackSE;
        _animEvent += CallBackCombo;

        switch (data.Action)
        {
            case ActionType.Ground:
                ComboSettingToGround();
                _player.Power = data.Power;
                _animEvent += _player.GroundAttack;
                _effectSetting.Set(ref _animEvent, data.Effect);
                _anim.Play(data.ActionAnimName);
                break;

            case ActionType.Floating:
                ComboSettingToFloating();
                if (data.CallBackAttack) _animEvent += CallBackFloat;
                else _player.FloatAttack(_combo);
                _player.Power = data.Power;
                _effectSetting.Set(ref _animEvent, data.Effect);
                _anim.Play(data.ActionAnimName);
                break;
        }
    }

    void CallBackFloat() => _player.FloatAttack(_combo);
    void CallBackCombo() => UIManager.UpDateCombo(_totalCombo);
    void CallBackSE() => AudioManager.PlayOneShot(_data.SE);

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