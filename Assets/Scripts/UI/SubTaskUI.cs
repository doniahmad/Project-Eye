using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SubTaskUI : MonoBehaviour
{
    public TextMeshProUGUI subtaskTitle;
    public SubTaskSO subTaskSO;
    public bool isComplete;

    private void Start()
    {
        subtaskTitle.text = subTaskSO.taskName;
    }

    private void Update()
    {
        if (!isComplete)
        {
            EnableSubtask();
        }
        else
        {
            DisbaleSubtask();
        }
    }

    public void EnableSubtask()
    {
        subtaskTitle.color = Color.black;
        subtaskTitle.fontStyle = FontStyles.Normal;
    }

    public void DisbaleSubtask()
    {
        subtaskTitle.color = Color.gray;
        subtaskTitle.fontStyle = FontStyles.Strikethrough;
    }

    public void DestroySubtask()
    {
        Destroy(gameObject);
    }
}
