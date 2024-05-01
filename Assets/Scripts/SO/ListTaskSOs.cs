using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Task/ListTaskSO")]
public class ListTaskSOs : ScriptableObject
{
    public bool isComplete = false;
    public string PhaseTask;
    public List<TaskSO> listTaskSO;
}
