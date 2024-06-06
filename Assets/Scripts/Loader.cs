using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{
    public enum Scene
    {
        MainMenuScene,
        LoadingScene,
        OpeningScene,
        LaboratoryScene,
    }

    public static Scene targetScene;

    public static void Load(Scene targetScene)
    {
        Time.timeScale = 1f;
        Loader.targetScene = targetScene;

        SceneManager.LoadScene(Scene.LoadingScene.ToString());
    }

    public static void LoaderCallback()
    {
        SceneManager.LoadScene(targetScene.ToString());
    }
}
