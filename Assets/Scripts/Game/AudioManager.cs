using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AudioType;

public class AudioManager : MonoBehaviour
{
    [SerializeField] float m_clipVolu;
    [SerializeField] float m_bgmVolu;
    [SerializeField] AudioClip[] m_clips = new AudioClip[0];
    [SerializeField] AudioClip[] m_bgms = new AudioClip[0];

    AudioSource m_source;

    void Awake() => m_source = gameObject.AddComponent<AudioSource>();
    
    public void PlayClip(ClipType type)
    {
        AudioClip clip = m_clips[(int)type];
        
        if (ClipType.Slashing == type)
            SetSlash(ref clip);

        m_source.PlayOneShot(clip, m_clipVolu);
    }

    AudioClip SetSlash(ref AudioClip clip)
    {
        int random = Random.Range(0, 10);
        if (random >= 4)
            clip = Resources.Load<AudioClip>("AudioSlashing2");

        return clip;
    }

    public void PlayBGM(BGMType type)
    {
        AudioClip clip = m_bgms[(int)type];

        m_source.loop = true;
        m_source.volume = m_bgmVolu;
        m_source.clip = clip;
        m_source.Play();
    }
}
