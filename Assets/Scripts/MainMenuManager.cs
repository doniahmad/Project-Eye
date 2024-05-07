using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager Instance { get; set; }

    public Button playButton;
    public Button optionButton;
    public Button exitButton;

    private void Awake()
    {
        Instance = this;
        // playbutton
        playButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.LaboratoryScene);
        });

        optionButton.onClick.AddListener(() =>
        {
            OptionUI.Instance.Show();
        });
        exitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });

    }

    public Button SetSelectedButton(Button selectedButton)
    {
        selectedButton.Select();
        return selectedButton;
    }
}
