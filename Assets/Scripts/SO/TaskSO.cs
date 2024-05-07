using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(menuName = "Task/TaskSO")]
public class TaskSO : ScriptableObject
{
    public bool isComplete = false;
    public string taskName;
    public Sprite taskUncheckedIcon;
    public Sprite taskCheckedIcon;
    public List<SubTaskSO> listSubtasks;

}

