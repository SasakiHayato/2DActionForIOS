using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using GetEnumToGame;

public class SceneController
{
    public void SetScene(string name)
    {
        switch (name)
        {
            case "Main":
                GameMaster.Instance().IsSceneCheck(Game.Main);
                SceneManager.LoadScene(name);
                break;
            case "Titel":
                GameMaster.Instance().IsSceneCheck(Game.Titel);
                SceneManager.LoadScene(name);
                break;
            case "Result":
                GameMaster.Instance().IsSceneCheck(Game.Result);
                SceneManager.LoadScene(name);
                break;
        }
    }
}
