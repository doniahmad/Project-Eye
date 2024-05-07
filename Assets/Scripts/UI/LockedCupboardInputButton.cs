using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LockedCupboardInputButton : MonoBehaviour
{
    public static LockedCupboardInputButton Instance { get; private set; }

    public event EventHandler<InputButtonEventArgs> OnLockedInputButtonClick;
    public class InputButtonEventArgs : EventArgs
    {
        public int inputNumber;
    }

    [SerializeField] private int inputNumber;
    private Button button;
    private TextMeshProUGUI number;

    private void Start()
    {
        Instance = this;

        button = GetComponent<Button>();
        number = GetComponentInChildren<TextMeshProUGUI>();

        number.text = inputNumber.ToString();

        button.onClick.AddListener(() =>
        {
            OnLockedInputButtonClick?.Invoke(this, new InputButtonEventArgs { inputNumber = inputNumber });
        });
    }
}
