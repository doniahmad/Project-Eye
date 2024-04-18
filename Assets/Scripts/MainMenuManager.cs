using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button OptionButton;
    [SerializeField] private Button ExitButton;

    private void Awake()
    {
        playButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.LaboratoryScene);
        });
        OptionButton.onClick.AddListener(() =>
        {

        });
        ExitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });

    }
}
