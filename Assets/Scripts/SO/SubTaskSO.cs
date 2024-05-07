using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(menuName = "Task/SubTaskSO")]
public class SubTaskSO : ScriptableObject
{
    public enum TaskCategory
    {
        Gathering,
        Crafting,
        Insert,
        Solving
    }

    public enum ProblemCategory
    {
        ReadBook,
        WashHand,
        GloveHand,
        WriteWhiteboard,
        OpenLockedCupboard,
        FixFumeHoodDoor,
        FindRedKey,
        OpenWerehouse,
    }

    public bool isOneTimeEvent;
    public bool isComplete = false;
    public string taskName;
    public TaskCategory taskCategory;

    // Properti khusus untuk Gathering Task
    [HideInInspector]
    public List<ItemObjectSO> itemsToGather = new List<ItemObjectSO>();
    [HideInInspector]
    public ItemObjectSO itemToCraft;
    [HideInInspector]
    public List<ItemObjectSO> itemToInsert = new List<ItemObjectSO>();
    [HideInInspector]
    public ProblemCategory problem;
}

#if UNITY_EDITOR
[CustomEditor(typeof(SubTaskSO))]
public class SubTaskSOEditor : Editor
{
    public override void OnInspectorGUI()
    {
        SubTaskSO task = (SubTaskSO)target;

        DrawDefaultInspector();
        EditorGUILayout.LabelField(task.taskCategory.ToString());

        switch (task.taskCategory)
        {
            case SubTaskSO.TaskCategory.Gathering:
                // Menampilkan properti itemsToGather dengan PropertyField
                EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(SubTaskSO.itemsToGather)), true);
                break;
            case SubTaskSO.TaskCategory.Crafting:
                EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(SubTaskSO.itemToCraft)), true);
                break;
            case SubTaskSO.TaskCategory.Insert:
                EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(SubTaskSO.itemToInsert)), true);
                break;
            case SubTaskSO.TaskCategory.Solving:
                EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(SubTaskSO.problem)), true);
                break;
        }

        serializedObject.ApplyModifiedProperties();
    }
}
#endif

