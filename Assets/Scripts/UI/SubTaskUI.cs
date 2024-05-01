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
    }

    public void DisbaleSubtask()
    {
        subtaskTitle.color = Color.red;
    }

    public void DestroySubtask()
    {
        Destroy(gameObject);
    }
}
