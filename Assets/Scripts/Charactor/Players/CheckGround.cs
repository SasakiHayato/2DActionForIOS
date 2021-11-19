using System.Collections;
using UnityEngine;

public class CheckGround : MonoBehaviour
{
    public bool IsGround { get => _isGround; }
    bool _isGround = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) _isGround = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) _isGround = false;
    }
}
