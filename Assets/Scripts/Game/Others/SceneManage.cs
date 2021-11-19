using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using SimpleFade;

public class SceneManage : MonoBehaviour
{
    public void Load(string name) => SceneManager.LoadScene(name);
    public void LoadAsync(string name, EventSetting setEvent = null) 
        => StartCoroutine(Loading(SceneManager.LoadSceneAsync(name), setEvent));

    IEnumerator Loading(AsyncOperation operation, EventSetting setEvent)
    {
        operation.allowSceneActivation = false;

        while (!setEvent.IsEnd) yield return null;
        
        Fade.InSingle(Fade.CreateFadeImage(), 1);
        while (!Fade.EndFade) yield return null;

        yield return new WaitForSeconds(0.2f);
        operation.allowSceneActivation = true;
    }
}
