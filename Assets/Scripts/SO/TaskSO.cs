using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Task/TaskSO")]
public class TaskSO : ScriptableObject
{
    public bool isComplete = false;
    public string taskName;
    public Dialogue dialogue;
    public List<SubTaskSO> listSubtasks;

}

