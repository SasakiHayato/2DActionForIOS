using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using SimpleFade;

public class SceneManage : MonoBehaviour
{
    public void Load(string name)
    {
        switch (name)
        {
            case "Main":
                GameManager.ChangeState(GameManager.State.IsGame);
                SceneManager.LoadScene(name);
                break;
            case "Title":
                GameManager.ChangeState(GameManager.State.Title);
                SceneManager.LoadScene(name);
                break;
            case "Result":
                GameManager.ChangeState(GameManager.State.Result);
                SceneManager.LoadScene(name);
                break;
        }
    }
    public void LoadAsync(string name, EventSetting setEvent = null) 
        => StartCoroutine(Loading(SceneManager.LoadSceneAsync(name), setEvent));

    IEnumerator Loading(AsyncOperation operation, EventSetting setEvent)
    {
        operation.allowSceneActivation = false;
        yield return new WaitUntil(() => setEvent.IsEnd);
        
        Fade.InSingle(Fade.CreateFadeImage(), 1);
        yield return new WaitUntil(() => Fade.EndFade);
        yield return new WaitForSeconds(0.2f);
        operation.allowSceneActivation = true;
    }
}
