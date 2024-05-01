using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TaskContainerUI : MonoBehaviour
{
    public event EventHandler OnTaskLoaded;

    public Transform taskContainer;
    public TaskUI taskUI;

    private TaskSO taskSO;

    public bool taskActive;

    private void Awake()
    {
        HideTaskContainer();
    }

    private void Start()
    {
        OnTaskLoaded += TaskContainerUI_OnTaskLoaded;
    }

    private void TaskContainerUI_OnTaskLoaded(object sender, EventArgs e)
    {
        if (!taskActive)
        {
            taskActive = true;
        }

        if (taskActive)
        {
            ShowTaskContainer();
            ShowTask();
        }
    }

    public void SetTaskSO(TaskSO taskSO)
    {
        this.taskSO = taskSO;

        if (!taskActive)
        {
            taskActive = true;
        }

        if (taskActive)
        {
            ShowTaskContainer();
            ShowTask();
        }
        OnTaskLoaded?.Invoke(this, EventArgs.Empty);
    }

    public void ClearTask()
    {
        taskUI.ClearSubtask();
    }

    public void ShowTask()
    {
        taskUI.taskTitle.text = taskSO.taskName;
        taskUI.taskIcon.sprite = taskSO.taskUncheckedIcon;
    }

    public void ShowTaskContainer()
    {
        taskContainer.gameObject.SetActive(true);
    }

    public void HideTaskContainer()
    {
        taskContainer.gameObject.SetActive(false);
    }
}
