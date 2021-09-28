using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeidUi : MonoBehaviour
{
    public void SetUi(Transform parent, Quaternion quaternion)
    {
        GameObject sprite = Instantiate(Resources.Load<GameObject>("GameObjectBlood"));
        Vector2 spriteScale = sprite.transform.localScale;
        float setY = 0;
        Vector2 setVec = Vector2.zero;
        if (quaternion.y == 0)
        {
            setY = 180;
            setVec = new Vector2(parent.position.x + spriteScale.x, parent.position.y + spriteScale.y);
        } 
        else if (quaternion.y == 1)
        {
            setY = 0;
            setVec = new Vector2(parent.position.x - spriteScale.x, parent.position.y + spriteScale.y);
        }

        sprite.transform.position = new Vector3(setVec.x, setVec.y, 1);
        sprite.transform.localRotation = Quaternion.Euler(0, setY, 0);

        StartCoroutine(FadeUi(sprite));
    }

    IEnumerator FadeUi(GameObject set)
    {
        SpriteRenderer sprite = set.GetComponent<SpriteRenderer>();
        float r = sprite.color.r;
        float g = sprite.color.g;
        float b = sprite.color.b;
        float a = 1.0f;

        while(a > 0)
        {
            sprite.color = new Color(r, g, b, a);
            a -= 0.001f;
            yield return null;
        }

        Destroy(set);
    }
}

