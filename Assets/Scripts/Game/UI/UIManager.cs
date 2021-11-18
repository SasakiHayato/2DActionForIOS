using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // ‚Á”ò‚Î‚µ‚½‰ñ”
    [SerializeField] Text _scoreText;
    [SerializeField] Text _comboText;

    // ‚Ç‚±‚©‚ç‚Å‚àŒÄ‚Ño‚¹‚é‚æ‚¤‚É
    private static UIManager _instance = null;
    public static UIManager Instance => _instance;
    private UIManager() { }

    List<SpriteRenderer> _spritesList = new List<SpriteRenderer>();
    GameObject _deleteTarget = null;

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
