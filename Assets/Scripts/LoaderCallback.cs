using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoaderCallback : MonoBehaviour
{
    private void Update()
    {
        if (Loader.targetScene == Loader.Scene.LaboratoryScene)
        {
            SceneManager.LoadScene(Loader.Scene.OpeningScene.ToString());
        }
        else
        {
            Loader.LoaderCallback();
        }
    }
}
