using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    [SerializeField] private Transform containerUI;
    [SerializeField] private Button resumeBtn;
    [SerializeField] private Button optionBtn;
    [SerializeField] private Button mainmenuBtn;
    [SerializeField] private Button backBtn;

    private void Awake()
    {
        resumeBtn.onClick.AddListener(() =>
        {
            GameManager.Instance.TogglePauseGame();
        });
        optionBtn.onClick.AddListener(() =>
        {
            OptionUI.Instance.Show();
        });
        mainmenuBtn.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.MainMenuManager);
        });
        backBtn.onClick.AddListener(() =>
        {
            Hide();
            GameManager.Instance.TogglePauseGame();
        });
    }

    private void Start()
    {
        GameManager.Instance.OnGamePause += GameManager_OnGamePause;
        GameManager.Instance.OnGameUnpause += GameManager_OnGameUnpause;

        Hide();
    }

    private void GameManager_OnGameUnpause(object sender, EventArgs e)
    {
        Hide();
        OptionUI.Instance.Hide();
    }

    private void GameManager_OnGamePause(object sender, EventArgs e)
    {
        Show();
    }

    private void Show()
    {
        resumeBtn.Select();
        containerUI.gameObject.SetActive(true);
    }

    private void Hide()
    {
        containerUI.gameObject.SetActive(false);
    }
}
