using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LockedCupboardUI : MonoBehaviour
{
    public event EventHandler OnCodeSuccess;

    public GameObject container;
    public TextMeshProUGUI displayInputedCode;
    public string lockCode = "624135";
    [SerializeField] private InteractUI interactUI;
    private PlayerController player;
    private List<int> inputedCode;
    public List<LockedCupboardInputButton> inputButtons = new List<LockedCupboardInputButton>();

    private void Start()
    {
        displayInputedCode.text = "";
        foreach (LockedCupboardInputButton button in inputButtons)
        {
            button.OnLockedInputButtonClick += LockedCupboardInputButton_OnLockedInputButtonClick;
        }
        Hide();
    }

    private void LockedCupboardInputButton_OnLockedInputButtonClick(object sender, LockedCupboardInputButton.InputButtonEventArgs e)
    {
        AddInputNumber(e.inputNumber);
        DisplayInputedCode();
        CheckInputCode();
    }

    public void SetPlayer(PlayerController player)
    {
        this.player = player;
    }

    public bool CheckInputCode()
    {
        string strInputedCode = string.Join("", inputedCode);
        if (strInputedCode.Length == lockCode.Length)
        {
            if (strInputedCode == lockCode)
            {
                Hide();
                Debug.Log("Code Success");
                OnCodeSuccess?.Invoke(this, EventArgs.Empty);
                return true;
            }
            else
            {
                ResetDisplayInputedCode();
                return false;
            }
        }
        return false;
    }

    public void DisplayInputedCode()
    {
        displayInputedCode.text = string.Join("", inputedCode);
    }

    public void ResetDisplayInputedCode()
    {
        displayInputedCode.text = "";
        inputedCode = new List<int>();
    }

    public void Show()
    {
        ResetDisplayInputedCode();
        inputedCode = new List<int>();
        container.SetActive(true);
        player.DisableControl();
        interactUI.onNewDisplay = true;
        GameManager.Instance.ShowCursor();
    }

    public void Hide()
    {
        if (player != null)
            player.EnableControl();
        container.SetActive(false);
        interactUI.onNewDisplay = false;
        GameManager.Instance.HideCursor();
    }

    public void AddInputNumber(int number)
    {
        inputedCode.Add(number);

    }
}
