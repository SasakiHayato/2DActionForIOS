using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public void Shake()
    {
        GameObject camera = GameObject.Find("Main Camera");
        Vector3 setVec = camera.transform.position;
        StartCoroutine(Set(camera.transform, setVec));
        camera.transform.position = setVec;
    }

    IEnumerator Set(Transform get, Vector3 reset)
    {
        Vector2 vec = get.position;
        for (int i = 0; i < 50; i++)
        {
            int x = Random.Range(-1, 2);
            int y = Random.Range(-1, 2);

            get.position = new Vector3(vec.x + x, vec.y + y, get.position.z);
            yield return null;
        }

        get.position = reset;
    }
}
