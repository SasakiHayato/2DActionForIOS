using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // ������΂�����
    [SerializeField] Text _scoreText;
    [SerializeField] Text _comboText;

    // �ǂ�����ł��Ăяo����悤��
    private static UIManager _instance = null;
    public static UIManager Instance => _instance;
    private UIManager() { }

    List<SpriteRenderer> _spritesList = new List<SpriteRenderer>();
    GameObject _deleteTarget = null;

    int _count = 0;
    int _updateCombo = 0;

    private void Awake()
    {
        _instance = this;
    }

    void Update()
    {
        DeleteSprite();
    }

    public static void AddScore()
    {
        Instance._count++;
        Instance._scoreText.text = $"Score : {Instance._count.ToString("d3")}" ;
    }

    public static void SetComboText(int num = 1)
    {
        Instance._updateCombo++;
        if (num == 0) Instance._updateCombo = num;
        
        Instance._comboText.text = $"{Instance._updateCombo} Combo";
    }

    public static void AddSprite(SpriteRenderer set) 
        => Instance._spritesList.Add(set);

    void DeleteSprite()
    {
        bool set = false;
        if (_spritesList.Count > 0)
        {
            _spritesList.ForEach(s =>
            {
                if (s.color.a <= 0 && !set)
                {
                    set = true;
                    _deleteTarget = s.gameObject;
                }
            });

            if (_deleteTarget != null)
            {
                _spritesList.Remove(_deleteTarget.GetComponent<SpriteRenderer>());
                Destroy(_deleteTarget.gameObject);
                _deleteTarget = null;
            }
        }
    }
}
