using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionUI : MonoBehaviour
{
    public static OptionUI Instance { get; private set; }

    public Transform container;
    public Button buttonBack;
    public Button buttonCredit;
    public Button decreaseMusic;
    public Button increaseMusic;
    public Button decreaseSound;
    public Button increaseSound;
    public TextMeshProUGUI musicText;
    public RectTransform sliderMusic;
    public TextMeshProUGUI soundText;
    public RectTransform sliderSound;
    private int maxSliderWidth = 258;

    [Header("Keybinds")]
    public Button forwardButton;
    public Button backwardButton;
    public Button leftButton;
    public Button rightButton;
    public Button interactButton;
    public Button inventory1Button;
    public Button inventory2Button;
    public TextMeshProUGUI forwardText;
    public TextMeshProUGUI backwardText;
    public TextMeshProUGUI leftText;
    public TextMeshProUGUI rightText;
    public TextMeshProUGUI interactText;
    public TextMeshProUGUI inventory1Text;
    public TextMeshProUGUI inventory2Text;

    [SerializeField] private Transform bindingAreaTransform;

    private void Awake()
    {
        Instance = this;

        buttonBack.onClick.AddListener(() =>
        {
            Hide();
            MainMenuManager.Instance.SetSelectedButton(MainMenuManager.Instance.optionButton);
        });
        buttonCredit.onClick.AddListener(() =>
        {
            // open credit ui
        });
        // Music setting
        // Decrease Music Volume
        decreaseMusic.onClick.AddListener(() =>
        {
            MusicManager.Instance.DecreaseVolume();
            UpdateVisual();
        });
        // Increase Music Volume
        increaseMusic.onClick.AddListener(() =>
        {
            MusicManager.Instance.IncreaseVolume();
            UpdateVisual();
        });
        // Sound setting
        // Decrease Music Volume
        decreaseSound.onClick.AddListener(() =>
        {
            SoundManager.Instance.DecreaseVolume();
            UpdateVisual();
        });
        // Increase Music Volume
        decreaseSound.onClick.AddListener(() =>
        {
            SoundManager.Instance.IncreaseVolume();
            UpdateVisual();
        });

        Hide();
        HideBindingArea();

        forwardButton.onClick.AddListener(() => { RebindBinding(InputManager.Binding.Move_Up); });
        backwardButton.onClick.AddListener(() => { RebindBinding(InputManager.Binding.Move_Down); });
        leftButton.onClick.AddListener(() => { RebindBinding(InputManager.Binding.Move_Left); });
        rightButton.onClick.AddListener(() => { RebindBinding(InputManager.Binding.Move_Right); });
        interactButton.onClick.AddListener(() => { RebindBinding(InputManager.Binding.Interact); });
        inventory1Button.onClick.AddListener(() => { RebindBinding(InputManager.Binding.Inventory1); });
        inventory2Button.onClick.AddListener(() => { RebindBinding(InputManager.Binding.Inventory2); });
    }

    private void Start()
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        musicText.text = MathF.Round(MusicManager.Instance.GetVolume() * 100).ToString();
        soundText.text = MathF.Round(SoundManager.Instance.GetVolume() * 100).ToString();
        sliderMusic.sizeDelta = new Vector2(Mathf.Round(MusicManager.Instance.GetVolume() * 100 / 100 * maxSliderWidth), sliderMusic.sizeDelta.y);

        forwardText.text = InputManager.Instance.GetBindingText(InputManager.Binding.Move_Up);
        backwardText.text = InputManager.Instance.GetBindingText(InputManager.Binding.Move_Down);
        leftText.text = InputManager.Instance.GetBindingText(InputManager.Binding.Move_Left);
        rightText.text = InputManager.Instance.GetBindingText(InputManager.Binding.Move_Right);
        interactText.text = InputManager.Instance.GetBindingText(InputManager.Binding.Interact);
        inventory1Text.text = InputManager.Instance.GetBindingText(InputManager.Binding.Inventory1);
        inventory2Text.text = InputManager.Instance.GetBindingText(InputManager.Binding.Inventory2);
    }

    public void Hide()
    {
        container.gameObject.SetActive(false);
    }

    public void Show()
    {
        container.gameObject.SetActive(true);
        decreaseMusic.Select();
    }

    private void RebindBinding(InputManager.Binding binding)
    {
        ShowBindingArea();
        InputManager.Instance.RebindBinding(binding, () =>
        {
            HideBindingArea();
            UpdateVisual();
        });
    }

    private void ShowBindingArea()
    {
        bindingAreaTransform.gameObject.SetActive(true);
    }

    private void HideBindingArea()
    {
        bindingAreaTransform.gameObject.SetActive(false);
    }
}
