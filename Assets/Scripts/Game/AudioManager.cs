using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] float _vol;
    [SerializeField] AudioClip[] _bgm = new AudioClip[3];

    private static AudioManager s_instance;
    public static AudioManager Instence => s_instance;
    private AudioManager() { }

    AudioSource _source;

    private void Awake()
    {
        s_instance = this;
        _source = gameObject.AddComponent<AudioSource>();
    }

    void Start()
    {
        _source.volume = _vol;
        PlayBGM();
    }

    void PlayBGM()
    {
        if (_bgm == null) return;
        switch (GameManager.CurrentState)
        {
            case GameManager.State.IsGame:
                _source.clip = _bgm[0];
                break;
            case GameManager.State.EndGame:
                _source.clip = _bgm[1];
                break;
            case GameManager.State.Others:
                _source.clip = _bgm[2];
                break;
            case GameManager.State.None:
                Debug.LogError("State.None");
                break;
        }

        _source.Play();
    }
}
