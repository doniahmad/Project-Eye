using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TaskUI : MonoBehaviour
{
    public TextMeshProUGUI taskTitle;
    public Image taskIcon;
    public Transform subtaskContainer;
    public TaskManager taskManager;
    public GameObject subTaskPrefabs;

    public void SetTask(TaskSO taskSO)
    {
        taskTitle.text = taskSO.taskName;
    }

    public void SetComplete()
    {
        HideTask();
        // Delete all subtask
        for (int i = 0; i < subtaskContainer.childCount; i++)
        {
            Destroy(subtaskContainer.GetChild(i).gameObject);
        }
    }

    public void ShowTask()
    {
        gameObject.SetActive(true);
        subtaskContainer.gameObject.SetActive(true);
    }

    public void HideTask()
    {
        gameObject.SetActive(false);
        subtaskContainer.gameObject.SetActive(false);
    }

    public void ClearSubtask()
    {
        for (int i = 0; i < subtaskContainer.childCount; i++)
        {
            if (subtaskContainer.GetChild(i).gameObject != null)
            {
                Destroy(subtaskContainer.GetChild(i).gameObject);
            }
        }
    }

    public SubTaskUI AddNewSubtask(SubTaskSO subTaskSO)
    {
        GameObject newSubtask = Instantiate(subTaskPrefabs, subtaskContainer);

        SubTaskUI subtaskUI = newSubtask.GetComponent<SubTaskUI>();

        subtaskUI.subTaskSO = subTaskSO;

        return subtaskUI;
    }
}
